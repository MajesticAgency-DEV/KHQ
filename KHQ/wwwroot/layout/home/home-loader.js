function loadHomeSections() {
    var nodes = document.querySelectorAll('[data-include]');
    nodes.forEach(function(node){
        var url = node.getAttribute('data-include');
        if (!url) return;
        fetch(url)
            .then(function(res){ return res.text(); })
            .then(function(html){
                node.innerHTML = html;
                // Dispatch per-section events to allow lazy JS inits
                try {
                    var evtName = 'home:section-loaded';
                    document.dispatchEvent(new CustomEvent(evtName, { detail: { url: url, node: node } }));
                    if (url.indexOf('slider.html') !== -1) {
                        document.dispatchEvent(new CustomEvent('home:slider-loaded'));
                    } else if (url.indexOf('testimonials.html') !== -1) {
                        document.dispatchEvent(new CustomEvent('home:testimonials-loaded'));
                    } else if (url.indexOf('projects.html') !== -1) {
                        document.dispatchEvent(new CustomEvent('home:projects-loaded'));
                    }
                } catch(_e) {}
            })
            .catch(function(){ /* ignore */ });
    });
}

document.addEventListener('DOMContentLoaded', loadHomeSections);

