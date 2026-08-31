(function () {
    "use strict";

    var cards = document.querySelectorAll('.artist-card');
    cards.forEach(function (card, index) {
        card.style.animationDelay = (index * 40) + 'ms';
    });

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
})();