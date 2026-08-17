document.addEventListener("DOMContentLoaded", function () {
    var searchInput = document.getElementById('genreSearchInput');
    if (searchInput) {
        searchInput.addEventListener('input', function () {
            var term = searchInput.value.trim().toLowerCase();
            var rows = document.querySelectorAll('#genreListBody tr');
            rows.forEach(function (row) {
                var text = row.textContent.toLowerCase();
                row.style.display = text.indexOf(term) !== -1 ? '' : 'none';
            });
        });
    }

    var imageUrlInput = document.getElementById('genreImageUrl');
    var imagePreview = document.getElementById('imagePreview');
    var previewPlaceholder = document.getElementById('previewPlaceholder');

    if (imageUrlInput && imagePreview && previewPlaceholder) {
        imageUrlInput.addEventListener('input', function () {
            var url = imageUrlInput.value.trim();
            if (url.length > 5) {
                imagePreview.src = url;
                imagePreview.style.display = 'block';
                previewPlaceholder.style.display = 'none';
            } else {
                imagePreview.src = '';
                imagePreview.style.display = 'none';
                previewPlaceholder.style.display = 'block';
            }
        });

        imagePreview.addEventListener('error', function () {
            imagePreview.style.display = 'none';
            previewPlaceholder.style.display = 'block';
            previewPlaceholder.innerText = 'Görsel yüklenemedi. Lütfen geçerli bir direkt resim bağlantısı girin.';
        });
    }
});