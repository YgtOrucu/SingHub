(function () {
    "use strict";

    /* ===== Sidebar Toggle (Mobile) ===== */
    var sidebar = document.getElementById('sidebar');
    var overlay = document.getElementById('overlay');
    var hamburgerBtn = document.getElementById('hamburgerBtn');

    if (sidebar && overlay && hamburgerBtn) {
        function openSidebar() {
            sidebar.classList.add('open');
            overlay.classList.add('show');
        }
        function closeSidebar() {
            sidebar.classList.remove('open');
            overlay.classList.remove('show');
        }
        hamburgerBtn.addEventListener('click', function () {
            sidebar.classList.contains('open') ? closeSidebar() : openSidebar();
        });
        overlay.addEventListener('click', closeSidebar);
    }

    var navLinks = document.querySelectorAll('.nav-link[data-page]');
    navLinks.forEach(function (link) {
        link.addEventListener('click', function () {
            navLinks.forEach(function (l) { l.classList.remove('active'); });
            link.classList.add('active');
            if (window.innerWidth <= 860 && sidebar) {
                sidebar.classList.remove('open');
                if (overlay) overlay.classList.remove('show');
            }
        });
    });

  
    var logoutBtn = document.getElementById('logoutBtn');
    if (logoutBtn) {
        logoutBtn.addEventListener('click', function () {
            window.showToast('Çıkış yapıldı', 'Oturumunuz güvenli şekilde kapatıldı.');
        });
    }

    var goWebSite = document.getElementById('GoWebSite');
    if (goWebSite) {
        goWebSite.addEventListener('click', function () {
            window.showToast('', 'Siteye yönlendiriliyorsunuz...');
        });
    }


    var toast = document.getElementById('toast');
    var toastMsg = document.getElementById('toastMsg');
    var toastSub = document.getElementById('toastSub');
    var toastTimer = null;


    window.showToast = function (msg, sub) {
        if (!toast || !toastMsg) return;
        toastMsg.textContent = msg;
        toastSub.textContent = sub || '';
        toast.classList.add('show');
        clearTimeout(toastTimer);
        toastTimer = setTimeout(function () {
            toast.classList.remove('show');
        }, 3200);
    };
})();