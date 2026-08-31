
(function () {
    "use strict";

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

    var navLinks = document.querySelectorAll('.sidebar .nav-link[href]');
    var currentPath = window.location.pathname.replace(/\/$/, '').toLowerCase();
    navLinks.forEach(function (link) {
        var linkPath = link.getAttribute('href');
        if (!linkPath) return;
        linkPath = linkPath.replace(/\/$/, '').toLowerCase();
        if (linkPath !== '' && currentPath.indexOf(linkPath) === 0) {
            link.classList.add('active');
        }
    });

    navLinks.forEach(function (link) {
        link.addEventListener('click', function () {
            if (window.innerWidth <= 860 && sidebar) {
                sidebar.classList.remove('open');
                if (overlay) overlay.classList.remove('show');
            }
        });
    });
})();