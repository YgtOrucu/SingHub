
(function () {
    "use strict";

    var toast = document.getElementById("toast");
    var toastMsg = document.getElementById("toastMsg");
    var toastSub = document.getElementById("toastSub");
    var toastTimer = null;

    function showToast(msg, sub) {
        if (!toast || !toastMsg) return;

        toastMsg.textContent = msg;

        if (toastSub) {
            toastSub.textContent = sub || "";
        }

        toast.classList.add("show");

        clearTimeout(toastTimer);

        toastTimer = setTimeout(function () {
            toast.classList.remove("show");
        }, 3200);
    }

    document
        .querySelectorAll('[data-action="view-all"]')
        .forEach(function (btn) {

            btn.addEventListener("click", function () {

                var card = btn.closest(".card");
                var titleElement = card
                    ? card.querySelector("h2")
                    : null;

                var title = titleElement
                    ? titleElement.textContent
                    : "Liste";

                showToast(
                    title,
                    "İlgili liste sayfasına yönlendiriliyorsunuz."
                );
            });
        });

    document
        .querySelectorAll(".edit-btn")
        .forEach(function (btn) {

            btn.addEventListener("click", function () {

                var row = btn.closest("tr");

                if (!row) return;

                var nameElement = row.querySelector(".name");

                var name = nameElement
                    ? nameElement.textContent.trim()
                    : "Kayıt";

                showToast(
                    "Düzenleme modu",
                    name + " için düzenleme paneli açılıyor."
                );
            });
        });

    document
        .querySelectorAll(".delete-btn")
        .forEach(function (btn) {

            btn.addEventListener("click", function () {

                var row = btn.closest("tr");

                if (!row) return;

                var nameElement = row.querySelector(".name");

                var name = nameElement
                    ? nameElement.textContent.trim()
                    : "Kayıt";

                row.style.transition = "opacity .25s ease";
                row.style.opacity = "0";

                setTimeout(function () {
                    row.remove();
                }, 250);

                showToast(
                    "Kayıt silindi",
                    name + " listeden kaldırıldı."
                );
            });
        });

    document
        .querySelectorAll("[data-count]")
        .forEach(function (element) {

            var target = parseInt(
                element.getAttribute("data-count"),
                10
            );

            if (isNaN(target)) return;

            var duration = 1200;
            var startTime = null;

            function animate(timestamp) {

                if (!startTime) {
                    startTime = timestamp;
                }

                var progress = Math.min(
                    (timestamp - startTime) / duration,
                    1
                );

                var eased =
                    1 - Math.pow(1 - progress, 3);

                var current =
                    Math.floor(eased * target);

                element.textContent =
                    current.toLocaleString("tr-TR");

                if (progress < 1) {
                    requestAnimationFrame(animate);
                } else {
                    element.textContent =
                        target.toLocaleString("tr-TR");
                }
            }

            requestAnimationFrame(animate);
        });


    var searchInput =
        document.getElementById("searchInput");

    var userTableBody =
        document.getElementById("userTableBody");

    if (searchInput && userTableBody) {

        searchInput.addEventListener("input", function () {

            var term =
                searchInput.value
                    .trim()
                    .toLowerCase();

            var rows =
                userTableBody.querySelectorAll("tr");

            rows.forEach(function (row) {

                var text =
                    row.textContent.toLowerCase();

                row.style.display =
                    text.includes(term)
                        ? ""
                        : "none";
            });
        });
    }

})();

