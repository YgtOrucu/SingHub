(function () {
    "use strict";

    /* ===== Report & View All Buttons ===== */
    var reportBtn = document.getElementById('reportBtn');
    if (reportBtn) {
        reportBtn.addEventListener('click', function () {
            window.showToast('Rapor oluşturuluyor', 'Rapor birkaç saniye içinde hazır olacak.');
        });
    }

    var viewAllBtn = document.getElementById('viewAllBtn');
    if (viewAllBtn) {
        viewAllBtn.addEventListener('click', function () {
            window.showToast('Kullanıcı listesi', 'Tüm kullanıcılar sayfasına yönlendiriliyorsunuz.');
        });
    }

    /* ===== Table Edit & Delete Actions ===== */
    document.querySelectorAll('.edit-btn').forEach(function (btn) {
        btn.addEventListener('click', function () {
            var name = btn.closest('tr').querySelector('.user-cell .name').textContent;
            window.showToast('Düzenleme modu', name + ' için düzenleme paneli açılıyor.');
        });
    });

    document.querySelectorAll('.delete-btn').forEach(function (btn) {
        btn.addEventListener('click', function () {
            var row = btn.closest('tr');
            var name = row.querySelector('.user-cell .name').textContent;
            row.style.opacity = '0';
            row.style.transition = 'opacity .25s ease';
            setTimeout(function () { row.remove(); }, 250);
            window.showToast('Kullanıcı silindi', name + ' listeden kaldırıldı.');
        });
    });

    /* ===== Animated Stat Counters ===== */
    var counters = document.querySelectorAll('[data-count]');
    counters.forEach(function (el) {
        var target = parseInt(el.getAttribute('data-count'), 10);
        var duration = 1200;
        var start = null;

        function step(ts) {
            if (!start) start = ts;
            var progress = Math.min((ts - start) / duration, 1);
            var eased = 1 - Math.pow(1 - progress, 3);
            var current = Math.floor(eased * target);
            el.textContent = current.toLocaleString('tr-TR');
            if (progress < 1) {
                requestAnimationFrame(step);
            } else {
                el.textContent = target.toLocaleString('tr-TR');
            }
        }
        requestAnimationFrame(step);
    });

    /* ===== Canvas Bar + Line Chart ===== */
    var canvas = document.getElementById('streamChart');
    if (canvas) {
        var chartDatasets = {
            "7": {
                labels: ["Pzt", "Sal", "Çar", "Per", "Cum", "Cmt", "Paz"],
                streams: [42, 58, 49, 71, 63, 88, 76],
                signups: [12, 18, 15, 22, 19, 27, 24]
            },
            "30": {
                labels: ["H1", "H2", "H3", "H4"],
                streams: [280, 340, 310, 390],
                signups: [90, 110, 95, 130]
            },
            "90": {
                labels: ["Ay1", "Ay2", "Ay3"],
                streams: [980, 1120, 1340],
                signups: [320, 380, 410]
            }
        };

        var ctx = canvas.getContext('2d');

        function resizeCanvas() {
            var rect = canvas.getBoundingClientRect();
            var dpr = window.devicePixelRatio || 1;
            canvas.width = rect.width * dpr;
            canvas.height = 220 * dpr;
            ctx.setTransform(dpr, 0, 0, dpr, 0, 0);
        }

        function roundRect(context, x, y, width, height, radius) {
            if (height <= 0) return;
            context.beginPath();
            context.moveTo(x, y + radius);
            context.arcTo(x, y + height, x + radius, y + height, radius);
            context.arcTo(x + width, y + height, x + width, y + height - radius, radius);
            context.arcTo(x + width, y, x + width - radius, y, radius);
            context.arcTo(x, y, x, y + radius, radius);
            context.closePath();
        }

        function drawChart(rangeKey) {
            resizeCanvas();
            var rect = canvas.getBoundingClientRect();
            var w = rect.width;
            var h = 220;
            ctx.clearRect(0, 0, w, h);

            var data = chartDatasets[rangeKey];
            var padding = { top: 16, right: 10, bottom: 28, left: 10 };
            var chartW = w - padding.left - padding.right;
            var chartH = h - padding.top - padding.bottom;
            var maxVal = Math.max.apply(null, data.streams) * 1.15;

            /* grid lines */
            ctx.strokeStyle = 'rgba(255,255,255,0.05)';
            ctx.lineWidth = 1;
            var gridLines = 4;
            for (var g = 0; g <= gridLines; g++) {
                var gy = padding.top + (chartH / gridLines) * g;
                ctx.beginPath();
                ctx.moveTo(padding.left, gy);
                ctx.lineTo(w - padding.right, gy);
                ctx.stroke();
            }

            var count = data.labels.length;
            var slot = chartW / count;
            var barWidth = Math.min(28, slot * 0.38);

            /* bars = signups */
            for (var i = 0; i < count; i++) {
                var bx = padding.left + slot * i + slot / 2 - barWidth / 2;
                var bh = (data.signups[i] / maxVal) * chartH;
                var by = padding.top + chartH - bh;

                var grad = ctx.createLinearGradient(0, by, 0, padding.top + chartH);
                grad.addColorStop(0, '#2563eb');
                grad.addColorStop(1, 'rgba(37,99,235,0.15)');
                ctx.fillStyle = grad;
                roundRect(ctx, bx, by, barWidth, bh, 5);
                ctx.fill();
            }

            /* line = streams */
            var points = [];
            for (var j = 0; j < count; j++) {
                var px = padding.left + slot * j + slot / 2;
                var py = padding.top + chartH - (data.streams[j] / maxVal) * chartH;
                points.push([px, py]);
            }

            /* area fill under line */
            var areaGrad = ctx.createLinearGradient(0, padding.top, 0, padding.top + chartH);
            areaGrad.addColorStop(0, 'rgba(124,58,237,0.35)');
            areaGrad.addColorStop(1, 'rgba(124,58,237,0)');
            ctx.beginPath();
            ctx.moveTo(points[0][0], padding.top + chartH);
            points.forEach(function (p) { ctx.lineTo(p[0], p[1]); });
            ctx.lineTo(points[points.length - 1][0], padding.top + chartH);
            ctx.closePath();
            ctx.fillStyle = areaGrad;
            ctx.fill();

            /* line stroke */
            ctx.beginPath();
            points.forEach(function (p, idx) {
                if (idx === 0) ctx.moveTo(p[0], p[1]);
                else ctx.lineTo(p[0], p[1]);
            });
            ctx.strokeStyle = '#a78bfa';
            ctx.lineWidth = 2.5;
            ctx.lineJoin = 'round';
            ctx.stroke();

            /* points */
            points.forEach(function (p) {
                ctx.beginPath();
                ctx.arc(p[0], p[1], 3.5, 0, Math.PI * 2);
                ctx.fillStyle = '#0b0914';
                ctx.fill();
                ctx.lineWidth = 2;
                ctx.strokeStyle = '#a78bfa';
                ctx.stroke();
            });

            /* x labels */
            ctx.fillStyle = '#64748b';
            ctx.font = '11px Inter, sans-serif';
            ctx.textAlign = 'center';
            data.labels.forEach(function (label, k) {
                var lx = padding.left + slot * k + slot / 2;
                ctx.fillText(label, lx, h - 8);
            });
        }

        var currentRange = "7";
        drawChart(currentRange);

        document.querySelectorAll('.chip-tab').forEach(function (tab) {
            tab.addEventListener('click', function () {
                document.querySelectorAll('.chip-tab').forEach(function (t) { t.classList.remove('active'); });
                tab.classList.add('active');
                currentRange = tab.getAttribute('data-range');
                drawChart(currentRange);
            });
        });

        var resizeTimer;
        window.addEventListener('resize', function () {
            clearTimeout(resizeTimer);
            resizeTimer = setTimeout(function () { drawChart(currentRange); }, 150);
        });
    }

    /* ===== Search Filter (Table) ===== */
    var searchInput = document.getElementById('searchInput');
    if (searchInput) {
        searchInput.addEventListener('input', function () {
            var term = searchInput.value.trim().toLowerCase();
            var rows = document.querySelectorAll('#userTableBody tr');
            rows.forEach(function (row) {
                var text = row.textContent.toLowerCase();
                row.style.display = text.indexOf(term) !== -1 ? '' : 'none';
            });
        });
    }
})();