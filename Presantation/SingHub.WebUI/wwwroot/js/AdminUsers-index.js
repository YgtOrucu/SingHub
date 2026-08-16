document.addEventListener("DOMContentLoaded", function () {
    // Tablo Arama İşlevi
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

// Modalı Aç ve Kullanıcı Bilgilerini Aktar
function openRoleModal(userId, fullName, currentRole) {
    document.getElementById('modalUserId').value = userId;
    document.getElementById('modalUserName').value = fullName;

    var roleSelect = document.getElementById('modalRoleSelect');
    if (roleSelect) {
        roleSelect.value = currentRole;
    }

    document.getElementById('roleModal').style.display = 'flex';
}

// Modalı Kapat
function closeRoleModal() {
    document.getElementById('roleModal').style.display = 'none';
}

function closeAlert(btn) {
    const alertCard = btn.closest('.alert-card');
    if (alertCard) {
        alertCard.style.opacity = '0';
        alertCard.style.transform = 'translateY(-8px)';
        setTimeout(() => {
            alertCard.remove();
        }, 300);
    }
}