document.addEventListener("DOMContentLoaded", function () {
    var searchInput = document.getElementById('artistSearchInput');
    if (searchInput) {
        searchInput.addEventListener('input', function () {
            var term = searchInput.value.trim().toLowerCase();
            var rows = document.querySelectorAll('#artistListBody tr');
            rows.forEach(function (row) {
                var text = row.textContent.toLowerCase();
                row.style.display = text.indexOf(term) !== -1 ? '' : 'none';
            });
        });
    }

    function bindPreview(inputId, imgId, placeholderId) {
        var input = document.getElementById(inputId);
        var img = document.getElementById(imgId);
        var placeholder = document.getElementById(placeholderId);

        if (input && img && placeholder) {
            input.addEventListener('input', function () {
                var url = input.value.trim();
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
    }

    bindPreview('artistImageUrl', 'avatarPreview', 'avatarPlaceholder');
    bindPreview('artistBannerUrl', 'bannerPreview', 'bannerPlaceholder');
});