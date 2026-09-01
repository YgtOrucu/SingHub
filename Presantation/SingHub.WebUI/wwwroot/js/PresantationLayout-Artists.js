(function () {
    "use strict";

    var cards = document.querySelectorAll('.artist-card');

    // Kart animasyon gecikmeleri
    cards.forEach(function (card, index) {
        card.style.animationDelay = (index * 40) + 'ms';
    });

    // Kart tıklama davranışı
    cards.forEach(function (card) {
        card.addEventListener('click', function (e) {
            var isInteractive = e.target.closest('a');
            if (isInteractive) return;

            var link = card.querySelector('.detail-btn');
            if (link) {
                window.location.href = link.getAttribute('href');
            }
        });
        card.style.cursor = 'pointer';
    });

    var searchInput = document.getElementById('searchInput');
    if (searchInput) {
        searchInput.addEventListener('input', function () {
            var term = searchInput.value.trim().toLowerCase();
            cards.forEach(function (card) {
                var nameEl = card.querySelector('.artist-name');
                var artistName = nameEl ? nameEl.textContent.toLowerCase() : '';
                card.style.display = artistName.indexOf(term) !== -1 ? '' : 'none';
            });
        });
    }
})();