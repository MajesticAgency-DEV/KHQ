function loadLayout() {
    // Load header
    fetch("layout/header.html")
        .then(res => res.text())
        .then(html => document.getElementById("header").innerHTML = html);

    // Load footer
    fetch("layout/footer.html")
        .then(res => res.text())
        .then(html => document.getElementById("footer").innerHTML = html);
}

document.addEventListener("DOMContentLoaded", loadLayout);
