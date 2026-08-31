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

    var accessToast = document.getElementById('accessToast');
    var accessToastMsg = document.getElementById('accessToastMsg');
    var accessToastSub = document.getElementById('accessToastSub');

    function hideModal() {
        if (accessToast) {
            accessToast.classList.remove('show');
        }
    }

    function showAccessToast(message, sub) {
        if (!accessToast) return;
        accessToastMsg.textContent = message;
        accessToastSub.textContent = sub || '';
        accessToast.classList.add('show');
    }

    if (accessToast) {
        var closeBtn = accessToast.querySelector('.access-modal-btn');
        if (closeBtn) {
            closeBtn.addEventListener('click', hideModal);
        }

        accessToast.addEventListener('click', function (e) {
            if (e.target === accessToast) {
                hideModal();
            }
        });

        document.addEventListener('keydown', function (e) {
            if (e.key === 'Escape' && accessToast.classList.contains('show')) {
                hideModal();
            }
        });
    }

    window.closeAccessToast = hideModal;

    var cards = Array.prototype.slice.call(document.querySelectorAll('.song-card'));
    var currentlyPlaying = null;

    function setCardPlayingState(card, isPlaying) {
        var playIcon = card.querySelector('.icon-play');
        var pauseIcon = card.querySelector('.icon-pause');
        if (isPlaying) {
            card.classList.add('is-playing');
            if (playIcon) playIcon.style.display = 'none';
            if (pauseIcon) pauseIcon.style.display = '';
        } else {
            card.classList.remove('is-playing');
            if (playIcon) playIcon.style.display = '';
            if (pauseIcon) pauseIcon.style.display = 'none';
        }
    }

    // İkon durumlarını sıfırlayan güncellenmiş kontrol fonksiyonu
    function setButtonChecking(card, playBtn, isChecking) {
        var playIcon = playBtn.querySelector('.icon-play');
        var pauseIcon = playBtn.querySelector('.icon-pause');
        var spinner = playBtn.querySelector('.icon-spinner');
        var isPlaying = currentlyPlaying && currentlyPlaying.card === card;

        if (isChecking) {
            playBtn.classList.add('is-checking');
            playBtn.disabled = true;
            if (playIcon) playIcon.style.display = 'none';
            if (pauseIcon) pauseIcon.style.display = 'none';
            if (spinner) spinner.style.display = 'inline-block';
        } else {
            playBtn.classList.remove('is-checking');
            playBtn.disabled = false;
            if (spinner) spinner.style.display = 'none';

            // Kontrol bittiğinde çalıp çalmama durumuna göre doğru ikonu geri getir
            setCardPlayingState(card, isPlaying);
        }
    }

    function stopCurrent() {
        if (!currentlyPlaying) return;
        currentlyPlaying.audio.pause();
        setCardPlayingState(currentlyPlaying.card, false);
        currentlyPlaying = null;
    }

    function startPlayback(card, audio) {
        if (currentlyPlaying && currentlyPlaying.audio !== audio) {
            stopCurrent();
        }

        var playPromise = audio.play();
        if (playPromise && typeof playPromise.catch === 'function') {
            playPromise.catch(function () {
                setCardPlayingState(card, false);
            });
        }
        setCardPlayingState(card, true);
        currentlyPlaying = { card: card, audio: audio };
    }

    cards.forEach(function (card) {
        var audio = card.querySelector('.song-audio');
        var playBtn = card.querySelector('.play-btn');
        var songId = card.getAttribute('data-song-id');
        var songTitle = card.getAttribute('data-title') || 'Şarkı';
        if (!audio || !playBtn) return;

        playBtn.addEventListener('click', async function () {
            var isThisPlaying = currentlyPlaying && currentlyPlaying.audio === audio && !audio.paused;

            if (isThisPlaying) {
                audio.pause();
                setCardPlayingState(card, false);
                currentlyPlaying = null;
                return;
            }

            setButtonChecking(card, playBtn, true);
            try {
                var result = await checkPlayAccess(songId);

                if (!result || !result.allowed) {
                    showAccessToast(
                        (result && result.message) || 'Bu şarkıyı açmaya yetkiniz yok.',
                        songTitle
                    );
                    return;
                }

                startPlayback(card, audio);
            } catch (err) {
                showAccessToast('Yetki kontrolü sırasında bir hata oluştu.', 'Lütfen tekrar deneyin.');
            } finally {
                setButtonChecking(card, playBtn, false);
            }
        });

        audio.addEventListener('ended', function () {
            setCardPlayingState(card, false);
            if (currentlyPlaying && currentlyPlaying.audio === audio) {
                currentlyPlaying = null;
            }
        });

        audio.addEventListener('pause', function () {
            if (!currentlyPlaying || currentlyPlaying.audio !== audio) return;
            if (audio.currentTime === 0 || audio.ended) {
                setCardPlayingState(card, false);
            }
        });
    });
})();