function loadLayout() {
    // Load header
    fetch("layout/header.html")
        .then(res => res.text())
        .then(html => {
            var headerEl = document.getElementById("header");
            if (headerEl) {
                headerEl.innerHTML = html;
                // Signal that header markup is now in the DOM so scripts can re-init behaviors
                document.dispatchEvent(new CustomEvent("header:loaded"));
            }
        })
        .catch(function(){ /* ignore header load errors */ });

    // Load footer
    var footerEl = document.getElementById("footer");
    if (footerEl) {
        fetch("layout/footer.html")
            .then(res => res.text())
            .then(html => { footerEl.innerHTML = html; })
            .catch(function(){ /* ignore footer load errors */ });
    }
}

document.addEventListener("DOMContentLoaded", loadLayout);
