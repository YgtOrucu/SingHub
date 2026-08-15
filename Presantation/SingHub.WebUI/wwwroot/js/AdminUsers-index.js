document.addEventListener("DOMContentLoaded", function () {
    var searchInput = document.getElementById('tableSearchInput');

    if (searchInput) {
        searchInput.addEventListener('input', function () {
            var term = searchInput.value.trim().toLowerCase();
            var rows = document.querySelectorAll('#userListBody tr');

            rows.forEach(function (row) {
                var text = row.textContent.toLowerCase();
                row.style.display = text.indexOf(term) !== -1 ? '' : 'none';
            });
        });
    }
});