document.addEventListener("DOMContentLoaded", function () {
    var searchInput = document.getElementById('albumSearchInput');
    if (searchInput) {
        searchInput.addEventListener('input', function () {
            var term = searchInput.value.trim().toLowerCase();
            var rows = document.querySelectorAll('#albumListBody tr');
            rows.forEach(function (row) {
                var text = row.textContent.toLowerCase();
                row.style.display = text.indexOf(term) !== -1 ? '' : 'none';
            });
        });
    }



    const input = document.getElementById('albumCoverUrl');
    const img = document.getElementById('coverPreview');
    const placeholder = document.getElementById('coverPlaceholder');

    if (input && img && placeholder) {
        input.addEventListener('input', function () {
            const url = input.value.trim();
            if (url.length > 5) {
                img.src = url;
                img.style.display = 'block';
                placeholder.style.display = 'none';
            } else {
                img.src = '';
                img.style.display = 'none';
                placeholder.style.display = 'block';
            }
        });

        img.addEventListener('error', function () {
            img.style.display = 'none';
            placeholder.style.display = 'block';
        });
    }
});