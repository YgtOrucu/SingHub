
(function () {
    "use strict";

    var CHECK_ACCESS_ENDPOINT = '/Users/Songs/CheckPlayAccess';

    function getAccessToken() {
        return localStorage.getItem('accessToken') || sessionStorage.getItem('accessToken') || null;
    }

    async function checkPlayAccess(songId) {
        var headers = {};
        var token = getAccessToken();
        if (token) {
            headers['Authorization'] = 'Bearer ' + token;
        }

        var url = CHECK_ACCESS_ENDPOINT + '?songId=' + encodeURIComponent(songId);

        var response = await fetch(url, {
            method: 'POST',
            headers: headers,
            credentials: 'include'
        });

        var data = null;
        try {
            data = await response.json();
        } catch (e) {
            data = null;
        }

        if (!response.ok) {
            return {
                allowed: false,
                message: (data && data.message) ? data.message : 'Bu şarkıyı açmaya yetkiniz yok.'
            };
        }

        var isAllowed = data ? (data.status === true || data.Status === true) : false;

        return {
            allowed: isAllowed,
            message: data ? data.message : ''
        };
    }
    function showAccessError(message) {
        var toast = document.getElementById('accessToast');
        var msgEl = document.getElementById('accessToastMsg');
        var subEl = document.getElementById('accessToastSub');

        if (toast) {
            if (msgEl) {
                msgEl.textContent = 'Erişim Engellendi';
            }
            if (subEl) {
                subEl.textContent = message || 'Bu şarkıyı dinlemek için gerekli rollere sahip değilsiniz.';
            }
            toast.classList.add('show');
        } else {
            alert(message || 'Bu şarkıyı açmaya yetkiniz yok.');
        }
    }

    function setPlayingIcon(root, isPlaying) {
        var playIcon = root.querySelector('.icon-play');
        var pauseIcon = root.querySelector('.icon-pause');
        if (playIcon) playIcon.style.display = isPlaying ? 'none' : '';
        if (pauseIcon) pauseIcon.style.display = isPlaying ? '' : 'none';
    }

    function setChecking(btn, isChecking) {
        var playIcon = btn.querySelector('.icon-play');
        var pauseIcon = btn.querySelector('.icon-pause');
        var spinner = btn.querySelector('.icon-spinner');
        btn.disabled = isChecking;
        if (isChecking) {
            if (playIcon) playIcon.style.display = 'none';
            if (pauseIcon) pauseIcon.style.display = 'none';
            if (spinner) spinner.style.display = '';
        } else if (spinner) {
            spinner.style.display = 'none';
        }
    }

    var rows = Array.prototype.slice.call(document.querySelectorAll('.song-row'));
    var playAllBtn = document.getElementById('playAllBtn');
    var currentlyPlaying = null; 
    var queueMode = false;
    var queueIndex = -1;

    function stopCurrent() {
        if (!currentlyPlaying) return;
        currentlyPlaying.audio.pause();
        currentlyPlaying.row.classList.remove('is-playing');
        setPlayingIcon(currentlyPlaying.row.querySelector('.row-play-btn'), false);
        currentlyPlaying = null;
    }

    function playRow(row) {
        var audio = row.querySelector('.song-audio');
        var btn = row.querySelector('.row-play-btn');
        if (!audio) return;

        if (currentlyPlaying && currentlyPlaying.audio !== audio) {
            stopCurrent();
        }

        var p = audio.play();
        if (p && typeof p.catch === 'function') {
            p.catch(function () {
                row.classList.remove('is-playing');
                setPlayingIcon(btn, false);
            });
        }
        row.classList.add('is-playing');
        setPlayingIcon(btn, true);
        if (playAllBtn) setPlayingIcon(playAllBtn, true);
        currentlyPlaying = { row: row, audio: audio };
    }

    function playNextInQueue() {
        queueIndex++;
        if (queueIndex >= rows.length) {
            queueMode = false;
            queueIndex = -1;
            if (playAllBtn) setPlayingIcon(playAllBtn, false);
            return;
        }
        playRow(rows[queueIndex]);
    }

    rows.forEach(function (row) {
        var audio = row.querySelector('.song-audio');
        var btn = row.querySelector('.row-play-btn');
        var songId = row.getAttribute('data-song-id');
        if (!audio || !btn) return;

        btn.addEventListener('click', async function () {
            var isThisPlaying = currentlyPlaying && currentlyPlaying.audio === audio && !audio.paused;

            if (isThisPlaying) {
                audio.pause();
                row.classList.remove('is-playing');
                setPlayingIcon(btn, false);
                if (playAllBtn) setPlayingIcon(playAllBtn, false);
                currentlyPlaying = null;
                queueMode = false;
                return;
            }

            setChecking(btn, true);
            try {
                var result = await checkPlayAccess(songId);
                if (!result || !result.allowed) {
                    showAccessError(result && result.message);
                    return;
                }
                queueMode = false;
                queueIndex = parseInt(row.getAttribute('data-index'), 10);
                playRow(row);
            } catch (err) {
                showAccessError('Yetki kontrolü sırasında bir hata oluştu.');
            } finally {
                setChecking(btn, false);
            }
        });

        audio.addEventListener('ended', function () {
            row.classList.remove('is-playing');
            setPlayingIcon(btn, false);
            if (currentlyPlaying && currentlyPlaying.audio === audio) {
                currentlyPlaying = null;
            }
            if (queueMode) {
                playNextInQueue();
            } else if (playAllBtn) {
                setPlayingIcon(playAllBtn, false);
            }
        });
    });
    if (playAllBtn) {
        playAllBtn.addEventListener('click', async function () {
            if (rows.length === 0) return;

            var isAnyPlaying = currentlyPlaying !== null;
            if (isAnyPlaying) {
                stopCurrent()
                queueMode = false;
                queueIndex = -1;
                setPlayingIcon(playAllBtn, false);
                return;
            }

            var firstSongId = rows[0].getAttribute('data-song-id');
            setChecking(playAllBtn, true);
            try {
                var result = await checkPlayAccess(firstSongId);
                if (!result || !result.allowed) {
                    showAccessError(result && result.message);
                    return;
                }
                queueMode = true;
                queueIndex = -1;
                playNextInQueue();
            } catch (err) {
                showAccessError('Yetki kontrolü sırasında bir hata oluştu.');
            } finally {
                setChecking(playAllBtn, false);
            }
        });
    }

    var bio = document.getElementById('artistBio');
    var bioToggle = document.getElementById('bioToggle');
    if (bio && bioToggle) {
        requestAnimationFrame(function () {
            if (bio.scrollHeight > bio.clientHeight + 2) {
                bioToggle.style.display = 'inline-block';
            }
        });
        bioToggle.addEventListener('click', function () {
            var expanded = bio.classList.toggle('expanded');
            bioToggle.textContent = expanded ? 'Daha az göster' : 'Devamını oku';
        });
    }
})();

function closeAccessToast() {
    var toast = document.getElementById('accessToast');
    if (toast) {
        toast.classList.remove('show');
    }
}