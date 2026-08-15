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

    /* ===== Nav Link Active State ===== */
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

    /* ===== Logout ===== */
    var logoutBtn = document.getElementById('logoutBtn');
    if (logoutBtn) {
        logoutBtn.addEventListener('click', function () {
            window.showToast('Çıkış yapıldı', 'Oturumunuz güvenli şekilde kapatıldı.');
        });
    }

    /* ===== Toast Helper (Global Access) ===== */
    var toast = document.getElementById('toast');
    var toastMsg = document.getElementById('toastMsg');
    var toastSub = document.getElementById('toastSub');
    var toastTimer = null;

    // Sayfa içindeki diğer JS dosyalarının da erişebilmesi için window'a bağlandı
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