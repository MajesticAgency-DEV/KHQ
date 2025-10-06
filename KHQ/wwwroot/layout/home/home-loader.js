function loadHomeSections() {
    var nodes = document.querySelectorAll('[data-include]');
    nodes.forEach(function (node) {
        var url = node.getAttribute('data-include');
        if (!url) return;
        fetch(url)
            .then(function (res) { return res.text(); })
            .then(function (html) {
                node.innerHTML = html;
                try {
                    var evtName = 'home:section-loaded';
                    document.dispatchEvent(new CustomEvent(evtName, { detail: { url: url, node: node } }));
                    if (url.indexOf('slider.html') !== -1) {
                        document.dispatchEvent(new CustomEvent('home:slider-loaded', { detail: { node: node } }));
                    } else if (url.indexOf('testimonials.html') !== -1) {
                        document.dispatchEvent(new CustomEvent('home:testimonials-loaded'));
                    } else if (url.indexOf('projects.html') !== -1) {
                        document.dispatchEvent(new CustomEvent('home:projects-loaded'));
                    } else if (url.indexOf('brands.html') !== -1) {
                        document.dispatchEvent(new CustomEvent('home:brands-loaded', { detail: { node: node } }));
                    } else if (url.indexOf('categories.html') !== -1) {
                        document.dispatchEvent(new CustomEvent('home:categories-loaded', { detail: { node: node } }));
                    } else if (url.indexOf('stats.html') !== -1) {
                        document.dispatchEvent(new CustomEvent('home:stats-loaded', { detail: { node: node } }));
                    } else if (url.indexOf('blog.html') !== -1) {
                        document.dispatchEvent(new CustomEvent('home:blog-loaded', { detail: { node: node } }));
                    } else if (url.indexOf('about.html') !== -1) {
                        document.dispatchEvent(new CustomEvent('home:about-loaded', { detail: { node: node } }));
                    } else if (url.indexOf('why-choose.html') !== -1) {
                        document.dispatchEvent(new CustomEvent('home:why-choose-loaded', { detail: { node: node } }));
                    }
                } catch (_e) { }
            })
            .catch(function () { /* ignore */ });
    });
}

document.addEventListener('DOMContentLoaded', loadHomeSections);

(function () {	

    function applySlider(container, payload) {
        try {
            var items = Array.isArray(payload) ? payload : [];
            if (!items || !items.length) return;

            var slides = container.querySelectorAll('.rev_slider ul li[data-index]');

            // Get references to both template slides
            var template1 = slides[0]; // First slide (rs-901)
            var template2 = slides[1]; // Second slide (rs-902)

            if (!template1 || !template2) {
                console.warn('Both slider templates are required');
                return;
            }

            // Clear existing slides beyond the templates
            var slideContainer = container.querySelector('.rev_slider ul');
            if (slideContainer) {
                // Remove any extra slides beyond the two templates
                var allSlides = slideContainer.querySelectorAll('li[data-index]');
                for (var i = 2; i < allSlides.length; i++) {
                    allSlides[i].remove();
                }
            }

            items.forEach(function (sli, index) {
                var slider = sli || {};
                var images = Array.isArray(slider.Images || slider.images) ? (slider.Images || slider.images) : [];
                var title = slider.Title || slider.title || '';
                var description = slider.Description || slider.description || '';
                var buttonText = slider.ButtonText || slider.buttonText || '';
                var link = slider.Link || slider.link || '';

                // Determine which template to use (alternating)
                var useTemplate1 = index >= 2 ? true : (index % 2 === 0);
                var templateSlide = useTemplate1 ? template1 : template2;

                var currentSlide;

                if (index < 2) {
                    // Use existing template slides for first two items
                    currentSlide = slides[index];
                } else {
                    // Deep clone
                    var newSlide = templateSlide.cloneNode(true);
                    newSlide.removeAttribute('id'); // avoid duplicate IDs if present
                    newSlide.style.display = '';    // make sure it's visible

                    // Unique attributes for Revolution Slider
                    newSlide.setAttribute('data-index', 'rs-90' + (index + 1));
                    newSlide.setAttribute('data-param1', (index + 1).toString());

                    slideContainer.appendChild(newSlide);
                }

                // Apply the slider data to the current slide
                applySliderData(currentSlide, {
                    images: images,
                    title: title,
                    description: description,
                    buttonText: buttonText,
                    link: link,
                    useTemplate1: useTemplate1
                });
            });

            // Hide unused template slides if we have fewer items than templates
            if (items.length === 1) {
                template2.style.display = 'none';
            }

        } catch (_err) {
            if (window && window.console) {
                console.warn('Slider apply failed', _err);
            }
        }
    }

    function applySliderData(slide, data) {
        var images = data.images;
        var title = data.title;
        var description = data.description;
        var buttonText = data.buttonText;
        var link = data.link;
        var useTemplate1 = data.useTemplate1;

        try {
            // Handle main background image
            var mainImg = slide.querySelector('.defaultimg');
            if (mainImg && images[0]) {
                mainImg.setAttribute('src', images[0]);
                mainImg.style.setProperty('background-image', 'url("' + images[0] + '")', 'important');
                mainImg.setAttribute('data-lazyload', images[0]);
            }

            // Handle template-specific image layers
            if (useTemplate1) {
                // Template 1 specific images
                var layer1Img = slide.querySelector('.layer_1 img');
                if (layer1Img && images[1]) {
                    layer1Img.setAttribute('src', images[1]);
                }

                var layer2Img = slide.querySelector('.layer_2 img');
                if (layer2Img && images[2]) {
                    layer2Img.setAttribute('src', images[2]);
                }
            } else {
                // Template 2 specific images
                var layer1Img = slide.querySelector('.sec_layer_1 img');
                if (layer1Img && images[1]) {
                    layer1Img.setAttribute('src', images[1]);
                }
            }

            // Handle title (layer_4 for both templates)
            var titleEl = slide.querySelector('.layer_4, .sec_layer_4');
            if (titleEl && title) {
                titleEl.textContent = title;
            }

            // Handle description (layer_5 for both templates)
            var descEl = slide.querySelector('.layer_5, .sec_layer_5');
            if (descEl && description) {
                if (descEl.children && descEl.children.length === 0) {
                    descEl.textContent = description;
                } else {
                    var p = descEl.querySelector('p');
                    if (p) {
                        p.textContent = description;
                    } else {
                        descEl.textContent = description;
                    }
                }
            }

            // Handle button (layer_6 for both templates)
            var btnWrapper = slide.querySelector('.layer_6, .sec_layer_6');
            var btn = btnWrapper ? btnWrapper.querySelector('a') : null;
            if (btn) {
                if (buttonText) btn.textContent = buttonText;
                if (link) btn.setAttribute('href', link);
            }

        } catch (err) {
            if (window && window.console) {
                console.warn('Error applying slider data:', err);
            }
        }
    }

    document.addEventListener('home:slider-loaded', function (e) {
        var container = e && e.detail && e.detail.node ? e.detail.node : null;
        if (!container) return;

        fetch('/api/Sliders/GetAll', { method: 'GET' })
            .then(function (res) { return res.ok ? res.json() : Promise.reject(res); })
            .then(function (payload) {
                applySlider(container, payload);
            })
            .catch(function (err) {
                if (window && window.console) {
                    console.warn('Sliders GetAll request failed', err && err.status ? err.status : err);
                }
            });
    });

    //function buildSliderSlideHtml(slider, index) {
    //    var images = Array.isArray(slider.Images || slider.images) ? (slider.Images || slider.images) : [];
    //    var title = slider.Title || slider.title || '';
    //    var description = slider.Description || slider.description || '';
    //    var buttonText = slider.ButtonText || slider.buttonText || '';
    //    var link = slider.Link || slider.link || '';

    //    // Determine template: odd index = template 1, even index = template 2
    //    var isTemplate1 = (index % 2 === 0);
    //    var slideIndex = 'template-slide-' + (index + 1);
    //    var param1 = index + 1;

    //    if (isTemplate1) {
    //        // Template 1: Has LAYER NR. 2 (Circle)
    //        return (
    //            '<li data-index="' + slideIndex + '" data-fstransition="fade" data-fsmasterspeed="300" data-fsslotamount="7" data-saveperformance="off" data-param1="' + param1 + '">' +
    //            '<!-- MAIN IMAGE -->' +
    //            '<img src="' + (images[0] || '') + '" alt="" data-lazyload="' + (images[0] || '') + '" data-bgposition="center center" data-kenburns="on" data-duration="10000" data-ease="Power1.easeOut" data-scalestart="110" data-scaleend="100" data-rotatestart="0" data-rotateend="0" data-offsetstart="0 0" data-offsetend="0 0" class="rev-slidebg xyz" data-no-retina>' +
    //            '<!-- LAYER NR. 1 Img -->' +
    //            '<div class="tp-caption tp-resizeme" id="slide-' + (901 + index) + '-layer-1" data-x="[\'left\',\'left\',\'center\',\'center\']" data-hoffset="[\'-300\',\'-100\',\'0\',\'0\']" data-y="[\'bottom\',\'bottom\',\'bottom\',\'bottom\']" data-voffset="[\'0\',\'-50\',\'-0\',\'-0\']" data-width="none" data-height="none" data-whitespace="nowrap" data-type="image" data-responsive_offset="off" data-frames=\'[{"from":"x:left;","speed":1500,"to":"o:1;","delay":2500,"ease":"Power3.easeOut"},{"delay":"wait","speed":1500,"to":"opacity:0;","ease":"Power4.easeIn"}]\' data-textAlign="[\'left\',\'left\',\'center\',\'center\']" data-paddingtop="[0,0,0,0]" data-paddingright="[0,0,0,0]" data-paddingbottom="[0,0,0,0]" data-paddingleft="[0,0,0,0]" style="z-index: 13;border-width:0px;">' +
    //            '<div><img src="' + (images[1] || '') + '" alt="" data-ww="[\'826px\',\'450px\',\'400px\',\'300px\']" data-hh="[\'558px\',\'304px\',\'270px\',\'203px\']" width="826" height="558" data-no-retina></div>' +
    //            '</div>' +
    //            '<!-- LAYER NR. 2 [ Circle ] -->' +
    //            '<div class="tp-caption" id="slide-' + (901 + index) + '-layer-2" data-x="[\'left\',\'left\',\'center\',\'center\']" data-hoffset="[\'200\',\'200\',\'0\',\'0\']" data-y="[\'bottom\',\'bottom\',\'bottom\',\'bottom\']" data-voffset="[\'200\',\'80\',\'80\',\'80\']" data-lineheight="[\'0\',\'0\',\'0\',\'0\']" data-width="[\'100\',\'100\',\'100\',\'100\']" data-height="[\'none\',\'none\',\'none\',\'none\']" data-whitespace="[\'normal\',\'normal\',\'normal\',\'normal\']" data-type="text" data-responsive_offset="on" data-frames=\'[{"from":"y:100px(R);opacity:0;","speed":2000,"to":"o:1;","delay":2000,"ease":"Power4.easeOut"},{"delay":"wait","speed":1000,"to":"y:-50px;opacity:0;","ease":"Power2.easeInOut"}]\' data-textAlign="[\'right\',\'right\',\'center\',\'center\']" data-paddingtop="[0,0,0,0]" data-paddingright="[0,0,0,0]" data-paddingbottom="[0,0,0,0]" data-paddingleft="[0,0,0,0]" style="z-index:9;">' +
    //            '<div><img src="' + (images[2] || '') + '" alt="" data-ww="[\'446px\',\'300px\',\'250px\',\'250px\']" data-hh="[\'445px\',\'299px\',\'250px\',\'250px\']" width="446" height="445" data-no-retina></div>' +
    //            '</div>' +
    //            '<!-- LAYER NR. 3 [ for title ] -->' +
    //            '<div class="tp-caption tp-resizeme" id="slide-' + (901 + index) + '-layer-4" data-x="[\'right\',\'right\',\'center\',\'center\']" data-hoffset="[0\',\'0\',\'0\',\'0\']" data-y="[\'middle\',\'middle\',\'middle\',\'middle\']" data-voffset="[\'0\',\'-50\',\'-140\',\'-140\']" data-fontsize="[\'80\',\'52\',\'42\',\'38\']" data-lineheight="[\'80\',\'52\',\'42\',\'38\']" data-width="[\'500\',\'500\',\'500\',\'500\']" data-height="[\'none\',\'none\',\'none\',\'none\']" data-whitespace="[\'normal\',\'normal\',\'normal\',\'normal\']" data-type="text" data-responsive_offset="on" data-frames=\'[{"from":"y:100px(R);opacity:0;","speed":2000,"to":"o:1;","delay":2000,"ease":"Power4.easeOut"},{"delay":"wait","speed":1000,"to":"y:-50px;opacity:0;","ease":"Power2.easeInOut"}]\' data-textAlign="[\'left\',\'left\',\'center\',\'center\']" data-paddingtop="[5,5,5,5]" data-paddingright="[0,0,0,20]" data-paddingbottom="[0,0,0,0]" data-paddingleft="[0,0,0,20]" style="z-index: 11; white-space: normal; font-weight: 900; color:#fff; border-width:0px; font-family: \'Overlock\', cursive;">' + title + '</div>' +
    //            '<!-- LAYER NR. 4 [ for paragraph] -->' +
    //            '<div class="tp-caption tp-resizeme" id="slide-' + (901 + index) + '-layer-5" data-x="[\'right\',\'right\',\'center\',\'center\']" data-hoffset="[\'0\',\'0\',\'0\',\'0\']" data-y="[\'middle\',\'middle\',\'middle\',\'middle\']" data-voffset="[\'150\',\'20\',\'-60\',\'-60\']" data-fontsize="[\'19\',\'19\',\'18\',\'16\']" data-lineheight="[\'28\',\'28\',\'28\',\'22\']" data-width="[\'500\',\'500\',\'500\',\'500\']" data-height="[\'none\',\'none\',\'none\',\'none\']" data-whitespace="[\'normal\',\'normal\',\'normal\',\'normal\']" data-type="text" data-responsive_offset="on" data-frames=\'[{"from":"y:100px(R);opacity:0;","speed":2000,"to":"o:1;","delay":2500,"ease":"Power4.easeOut"},{"delay":"wait","speed":1000,"to":"y:-50px;opacity:0;","ease":"Power2.easeInOut"}]\' data-textAlign="[\'left\',\'left\',\'center\',\'center\']" data-paddingtop="[0,0,0,0]" data-paddingright="[0,0,0,30]" data-paddingbottom="[0,0,0,0]" data-paddingleft="[0,0,0,30]" style="z-index: 11; font-weight: 300; color:#fff; border-width:0px;font-family: \'Heebo\', sans-serif;">' + description + '</div>' +
    //            '<!-- LAYER NR. 5 [ for botton ] -->' +
    //            '<div class="tp-caption tp-resizeme rev-btn" id="slide-' + (901 + index) + '-layer-6" data-x="[\'right\',\'right\',\'center\',\'center\']" data-hoffset="[\'0\',\'0\',\'0\',\'0\']" data-y="[\'middle\',\'middle\',\'middle\',\'middle\']" data-voffset="[\'260\',\'120\',\'40\',\'40\']" data-lineheight="[\'none\',\'none\',\'none\',\'none\']" data-width="[\'500\',\'500\',\'300\',\'300\']" data-height="[\'none\',\'none\',\'none\',\'none\']" data-whitespace="[\'normal\',\'normal\',\'normal\',\'normal\']" data-type="text" data-responsive_offset="on" data-frames=\'[{"from":"y:100px(R);opacity:0;","speed":2000,"to":"o:1;","delay":3000,"ease":"Power4.easeOut"},{"delay":"wait","speed":1000,"to":"y:-50px;opacity:0;","ease":"Power2.easeInOut"}]\' data-textAlign="[\'left\',\'left\',\'center\',\'center\']" data-paddingtop="[0,0,0,0]" data-paddingright="[0,0,0,0]" data-paddingbottom="[0,0,0,0]" data-paddingleft="[0,0,0,0]" style="z-index:14;">' +
    //            '<a href="' + link + '" class="site-button-secondry">' + buttonText + '</a>' +
    //            '</div>' +
    //            '</li>'
    //        );
    //    } else {
    //        // Template 2: No LAYER NR. 2 (Circle)
    //        return (
    //            '<li data-index="' + slideIndex + '" data-fstransition="fade" data-fsmasterspeed="300" data-fsslotamount="7" data-saveperformance="off" data-param1="' + param1 + '">' +
    //            '<!-- MAIN IMAGE -->' +
    //            '<img src="' + (images[0] || '') + '" alt="" data-lazyload="' + (images[0] || '') + '" data-bgposition="center center" data-kenburns="on" data-duration="10000" data-ease="Power1.easeOut" data-scalestart="110" data-scaleend="100" data-rotatestart="0" data-rotateend="0" data-offsetstart="0 0" data-offsetend="0 0" class="rev-slidebg xyz" data-no-retina>' +
    //            '<!-- LAYER NR. 1 Img -->' +
    //            '<div class="tp-caption tp-resizeme" id="slide-' + (901 + index) + '-layer-1" data-x="[\'right\',\'right\',\'center\',\'center\']" data-hoffset="[\'-150\',\'0\',\'0\',\'0\']" data-y="[\'bottom\',\'bottom\',\'bottom\',\'bottom\']" data-voffset="[\'0\',\'0\',\'0\',\'0\']" data-width="none" data-height="none" data-whitespace="nowrap" data-type="image" data-responsive_offset="off" data-frames=\'[{"from":"x:right;","speed":1500,"to":"o:1;","delay":2500,"ease":"Power3.easeOut"},{"delay":"wait","speed":1500,"to":"opacity:0;","ease":"Power4.easeIn"}]\' data-textAlign="[\'right\',\'right\',\'center\',\'center\']" data-paddingtop="[0,0,0,0]" data-paddingright="[0,0,0,0]" data-paddingbottom="[0,0,0,0]" data-paddingleft="[0,0,0,0]" style="z-index: 13;border-width:0px;">' +
    //            '<div><img src="' + (images[1] || '') + '" alt="" data-ww="[\'537px\',\'400px\',\'200px\',\'200px\']" data-hh="[\'691px\',\'515px\',\'257px\',\'257px\']" width="537" height="691" data-no-retina></div>' +
    //            '</div>' +
    //            '<!-- LAYER NR. 3 [ for title ] -->' +
    //            '<div class="tp-caption tp-resizeme" id="slide-' + (901 + index) + '-layer-4" data-x="[\'left\',\'left\',\'center\',\'center\']" data-hoffset="[50\',\'50\',\'0\',\'0\']" data-y="[\'middle\',\'middle\',\'middle\',\'middle\']" data-voffset="[\'0\',\'-50\',\'-130\',\'-130\']" data-fontsize="[\'80\',\'52\',\'42\',\'38\']" data-lineheight="[\'100\',\'52\',\'42\',\'38\']" data-width="[\'720\',\'500\',\'500\',\'500\']" data-height="[\'none\',\'none\',\'none\',\'none\']" data-whitespace="[\'normal\',\'normal\',\'normal\',\'normal\']" data-type="text" data-responsive_offset="on" data-frames=\'[{"from":"y:100px(R);opacity:0;","speed":2000,"to":"o:1;","delay":2000,"ease":"Power4.easeOut"},{"delay":"wait","speed":1000,"to":"y:-50px;opacity:0;","ease":"Power2.easeInOut"}]\' data-textAlign="[\'left\',\'left\',\'center\',\'center\']" data-paddingtop="[5,5,5,5]" data-paddingright="[0,0,0,30]" data-paddingbottom="[0,0,0,0]" data-paddingleft="[0,0,0,30]" style="z-index: 11; white-space: normal; font-weight: 900; color:#fff; border-width:0px; font-family: \'Overlock\', cursive;">' + title + '</div>' +
    //            '<!-- LAYER NR. 4 [ for paragraph] -->' +
    //            '<div class="tp-caption tp-resizeme" id="slide-' + (901 + index) + '-layer-5" data-x="[\'left\',\'left\',\'center\',\'center\']" data-hoffset="[\'50\',\'50\',\'0\',\'0\']" data-y="[\'middle\',\'middle\',\'middle\',\'middle\']" data-voffset="[\'170\',\'70\',\'0\',\'0\']" data-fontsize="[\'19\',\'19\',\'18\',\'16\']" data-lineheight="[\'28\',\'28\',\'28\',\'22\']" data-width="[\'750\',\'500\',\'500\',\'500\']" data-height="[\'none\',\'none\',\'none\',\'none\']" data-whitespace="[\'normal\',\'normal\',\'normal\',\'normal\']" data-type="text" data-responsive_offset="on" data-frames=\'[{"from":"y:100px(R);opacity:0;","speed":2000,"to":"o:1;","delay":2500,"ease":"Power4.easeOut"},{"delay":"wait","speed":1000,"to":"y:-50px;opacity:0;","ease":"Power2.easeInOut"}]\' data-textAlign="[\'left\',\'left\',\'center\',\'center\']" data-paddingtop="[0,0,0,0]" data-paddingright="[0,0,0,30]" data-paddingbottom="[0,0,0,0]" data-paddingleft="[0,0,0,30]" style="z-index: 11; font-weight: 300; color:#fff; border-width:0px;font-family: \'Heebo\', sans-serif;">' + description + '</div>' +
    //            '<!-- LAYER NR. 5 [ for botton ] -->' +
    //            '<div class="tp-caption tp-resizeme rev-btn" id="slide-' + (901 + index) + '-layer-6" data-x="[\'left\',\'left\',\'center\',\'center\']" data-hoffset="[\'50\',\'50\',\'0\',\'0\']" data-y="[\'middle\',\'middle\',\'middle\',\'middle\']" data-voffset="[\'290\',\'250\',\'100\',\'100\']" data-lineheight="[\'none\',\'none\',\'none\',\'none\']" data-width="[\'500\',\'500\',\'300\',\'300\']" data-height="[\'none\',\'none\',\'none\',\'none\']" data-whitespace="[\'normal\',\'normal\',\'normal\',\'normal\']" data-type="text" data-responsive_offset="on" data-frames=\'[{"from":"y:100px(R);opacity:0;","speed":2000,"to":"o:1;","delay":3000,"ease":"Power4.easeOut"},{"delay":"wait","speed":1000,"to":"y:-50px;opacity:0;","ease":"Power2.easeInOut"}]\' data-textAlign="[\'left\',\'left\',\'center\',\'center\']" data-paddingtop="[0,0,0,0]" data-paddingright="[0,0,0,0]" data-paddingbottom="[0,0,0,0]" data-paddingleft="[0,0,0,0]" style="z-index:14;">' +
    //            '<a href="' + link + '" class="site-button-secondry">' + buttonText + '</a>' +
    //            '</div>' +
    //            '</li>'
    //        );
    //    }
    //}

    //document.addEventListener('home:slider-loaded', function (e) {
    //    var container = e && e.detail && e.detail.node ? e.detail.node : null;
    //    if (!container) return;

    //    fetch('/api/Sliders/GetAll', { method: 'GET' })
    //        .then(function (res) { return res.ok ? res.json() : Promise.reject(res); })
    //        .then(function (payload) {
    //            if (!payload) return;
    //            var items = Array.isArray(payload) ? payload : [];
    //            if (!items || !items.length) return;

    //            var ulContainer = container.querySelector('.rev_slider ul');
    //            if (!ulContainer) return;

    //            // Clear existing slides
    //            ulContainer.innerHTML = '';

    //            // Add new slides with alternating templates
    //            var html = items.map(function (item, index) {
    //                return buildSliderSlideHtml(item, index);
    //            }).join('');
    //            ulContainer.innerHTML = html;
    //        })
    //        .catch(function (err) {
    //            if (window && window.console) {
    //                console.warn('Sliders GetAll request failed', err && err.status ? err.status : err);
    //            }
    //        });
    //});

    function buildBrandCardHtml(brand) {
        var id = brand && (brand.id || brand.Id) ? (brand.id || brand.Id) : '';
        var imageSrc = brand && brand.imageLink ? brand.imageLink : '';
        var name = brand && brand.name ? brand.name : '';
        var description = brand && brand.description ? brand.description : '';
        var face = brand && brand.faceLink ? brand.faceLink : '';
        var insta = brand && brand.instaLink ? brand.instaLink : '';
        var socials = '';
        if (face) socials += '<li><a href="' + face + '" target="_blank" rel="noopener"><i class="fa fa-facebook"></i></a></li>';
        if (insta) socials += '<li><a href="' + insta + '" target="_blank" rel="noopener"><i class="fa fa-instagram"></i></a></li>';
        return (
            '<div class="col-xl-4 col-lg-4 col-md-6 m-b30">' +
            '<div class="wt-team-2 bg-white radius-md brand-card" data-brand-id="' + id + '">' +
            '<div class="wt-team-2-content">' +
            '<div class="wt-media">' +
            '<img src="' + imageSrc + '" alt="" />' +
            '</div>' +
            '<div class="wt-info">' +
            '<div class="team-detail">' +
            '<span class="title-style-2 team-position site-text-primary">' + description + '</span>' +
            '<h3 class="m-t0 team-name">' + name + '</h3>' +
            '</div>' +
            '<div class="team-social-center">' +
            '<ul class="team-social-bar">' + socials + '</ul>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>'
        );
    }

    document.addEventListener('home:brands-loaded', function (e) {
        var container = e && e.detail && e.detail.node ? e.detail.node : null;
        if (!container) return;

        // Fetch API data
        fetch('/api/Brands/GetAll', { method: 'GET' })
            .then(function (res) { return res.ok ? res.json() : Promise.reject(res); })
            .then(function (payload) {
                if (!payload) return;
                // Title and description
                try {
                    var titleEl = container.querySelector('.section-head h2');
                    var apiTitle = payload.title || payload.Title;
                    if (titleEl && apiTitle) titleEl.textContent = apiTitle;
                    var descEl = container.querySelector('.wt-separator-two-part-right p');
                    var apiDesc = payload.main_Description || payload.Main_Description;
                    if (descEl && apiDesc) descEl.textContent = apiDesc;
                    var brandCover = $(".bg-section");
                    if (brandCover)
                        brandCover.css("background-image", "url(" + payload.coverPhoto + ")");

                } catch (_err) { }

                // List of brands
                var listContainer = container.querySelector('.row.justify-content-center');
                if (!listContainer) return;
                var items = Array.isArray(payload.brandsDtos) ? payload.brandsDtos : (payload.BrandsDtos || []);
                if (!items || !items.length) { listContainer.innerHTML = ''; return; }
                var html = items.map(function (it) {
                    return buildBrandCardHtml({
                        id: it.Id || it.id,
                        name: it.Name || it.name,
                        description: it.Description || it.description,
                        imageLink: it.ImageLink || it.imageLink,
                        faceLink: it.FaceLink || it.faceLink,
                        instaLink: it.InstaLink || it.instaLink
                    });
                }).join('');
                listContainer.innerHTML = html;
                try {
                    listContainer.addEventListener('click', function (ev) {
                        var target = ev.target;
                        var card = target.closest && target.closest('.brand-card');
                        if (card && card.getAttribute) {
                            var bid = card.getAttribute('data-brand-id');
                            if (bid) {
                                window.location.href = 'products.html?brandId=' + encodeURIComponent(bid);
                            }
                        }
                    });
                } catch (_navErr) { }
            })
            .catch(function (err) {
                if (window && window.console) {
                    console.warn('Brands GetAll request failed', err && err.status ? err.status : err);
                }
            });
    });
    function buildCategoryCardHtml(cat) {
        var id = cat && (cat.id || cat.Id) ? (cat.id || cat.Id) : '';
        var imageSrc = cat && cat.imageLink ? cat.imageLink : '';
        var name = cat && cat.name ? cat.name : '';
        var description = cat && cat.description ? cat.description : '';
        return (
            '<div class="col-lg-4 col-md-6 m-b30">' +
            '<div class="wt-box d-icon-box-one bg-white shadow card1 radius-md">' +
            '<div class="d-icon-box-one-media m-b20">' +
            '<img src="' + imageSrc + '" alt="" />' +
            '</div>' +
            '<div class="d-icon-box-title title-style-2 site-text-secondry">' +
            '<h3 class="s-title-one">' + name + '</h3>' +
            '</div>' +
            '<div class="d-icon-box-content">' +
            '<p>' + description + '</p>' +
            '<a href="products.html?categoryId=' + encodeURIComponent(id) + '" class="site-button-link site-text-primary">Read More</a>' +
            '</div>' +
            '</div>' +
            '</div>'
        );
    }

    document.addEventListener('home:categories-loaded', function (e) {
        var container = e && e.detail && e.detail.node ? e.detail.node : null;
        if (!container) return;

        fetch('/api/Category/GetAll', { method: 'GET' })
            .then(function (res) { return res.ok ? res.json() : Promise.reject(res); })
            .then(function (payload) {
                if (!payload) return;
                // Title and description if present (centered heading format)
                try {
                    var titleEl = container.querySelector('.section-head h2');
                    var apiTitle = payload.title || payload.Title;
                    if (titleEl && apiTitle) titleEl.textContent = apiTitle;
                    var descEl = container.querySelector('.section-head p');
                    var apiDesc = payload.main_Description || payload.Main_Description;
                    if (descEl && apiDesc) descEl.textContent = apiDesc;
                    var brandCover = $(".bg-section");
                    if (brandCover)
                        brandCover.css("background-image", "url(" + payload.coverPhoto + ")");

                } catch (_err) { }

                var listContainer = container.querySelector('.row.justify-content-center');
                if (!listContainer) listContainer = container.querySelector('.row.justify-content-center.d-flex');
                var items = Array.isArray(payload) ? payload : (payload.categoriesDtos || payload.CategoriesDtos || []);
                if (!items || !items.length) { if (listContainer) listContainer.innerHTML = ''; return; }
                var html = items.map(function (it) {
                    return buildCategoryCardHtml({
                        id: it.Id || it.id,
                        name: it.Name || it.name,
                        description: it.Description || it.description,
                        imageLink: it.ImageLink || it.imageLink
                    });
                }).join('');
                if (listContainer) listContainer.innerHTML = html;
            })
            .catch(function (err) {
                if (window && window.console) {
                    console.warn('Category GetAll request failed', err && err.status ? err.status : err);
                }
            });
    });
    function buildStatisticsHtml(stat) {
        var number = stat && stat.number ? stat.number : 0;
        var name = stat && stat.name ? stat.name : '';
        return (
            '<div class="col-lg-3 col-md-6 col-sm-6 m-b30">' +
            '<div class="counter-box site-text-white">' +
            '<h2 class="counter site-text-secondry" data-target="' + number + '">1</h2>' +
            '<span>' + name + '</span>' +
            '</div>' +
            '</div>'
        );
    }

    document.addEventListener('home:stats-loaded', function (e) {
        var container = e && e.detail && e.detail.node ? e.detail.node : null;
        if (!container) return;

        fetch('/api/E_Con_Inner/GetAll', { method: 'GET' })
            .then(function (res) { return res.ok ? res.json() : Promise.reject(res); })
            .then(function (payload) {
                if (!payload) return;
                // Title and description
                try {
                    var titleEl = container.querySelector('.wt-small-separator div');
                    var apiTitle = payload.Main_Title || payload.main_Title;
                    if (titleEl && apiTitle) titleEl.textContent = apiTitle;
                    var descEl = container.querySelector('.section-head h2');
                    var apiDesc = payload.Main_Description || payload.main_Description;
                    if (descEl && apiDesc) descEl.textContent = apiDesc;
                } catch (_err) { }

                // List of statistics
                var listContainer = container.querySelector('.row.justify-content-center');
                if (!listContainer) return;
                var items = Array.isArray(payload.statistics) ? payload.statistics : [];
                if (!items || !items.length) { listContainer.innerHTML = ''; return; }
                var html = items.map(function (it) {
                    return buildStatisticsHtml({
                        name: it.Name || it.name,
                        number: it.Number || it.number
                    });
                }).join('');
                listContainer.innerHTML = html;

                // Animate counters from 1 to target when section is visible
                try {
                    var durationMs = 2200;
                    var ranOnce = false;
                    function runCounters() {
                        if (ranOnce) return; ranOnce = true;
                        var counters = listContainer.querySelectorAll('.counter');
                        counters.forEach(function (el) {
                            if (el.getAttribute('data-animated') === '1') return;
                            el.setAttribute('data-animated', '1');
                            var target = parseInt(el.getAttribute('data-target') || el.textContent || '0', 10) || 0;
                            var start = 1;
                            if (target < start) { el.textContent = String(target); return; }
                            var startTime = null;
                            function step(ts) {
                                if (!startTime) startTime = ts;
                                var progress = Math.min((ts - startTime) / durationMs, 1);
                                var current = Math.floor(start + (target - start) * progress);
                                if (current < start) current = start;
                                el.textContent = String(current);
                                if (progress < 1) {
                                    requestAnimationFrame(step);
                                } else {
                                    el.textContent = String(target);
                                }
                            }
                            requestAnimationFrame(step);
                        });
                    }

                    if ('IntersectionObserver' in window) {
                        var io = new IntersectionObserver(function (entries) {
                            entries.forEach(function (entry) {
                                if (entry.isIntersecting) {
                                    runCounters();
                                    if (io && listContainer) io.unobserve(listContainer);
                                }
                            });
                        }, { threshold: 0.2 });
                        io.observe(listContainer);
                    } else {
                        function isInView(el) {
                            var rect = el.getBoundingClientRect();
                            var vh = window.innerHeight || document.documentElement.clientHeight;
                            return rect.top < vh * 0.8 && rect.bottom > vh * 0.2;
                        }
                        function onScroll() {
                            if (!ranOnce && isInView(listContainer)) {
                                runCounters();
                                window.removeEventListener('scroll', onScroll);
                            }
                        }
                        window.addEventListener('scroll', onScroll);
                        onScroll();
                    }
                } catch (_animErr) { }
            })
            .catch(function (err) {
                if (window && window.console) {
                    console.warn('E_Con_Inner GetAll request failed', err && err.status ? err.status : err);
                }
            });
    });
    function buildBlogCardHtml(stain) {
        var imageSrc = stain && stain.imageLink ? stain.imageLink : '';
        var id = stain && (stain.id || stain.Id) ? (stain.id || stain.Id) : '';
        var name = stain && stain.name ? stain.name : '';
        var description = stain && stain.description ? stain.description : '';
        return (
            '<div class="col-lg-4 col-md-6 col-sm-12">' +
            '<div class="blog-post date-style-1 latest-blog radius-md bg-white">' +
            '<div class="wt-post-media">' +
            '<a href="javascript:;"><img src="' + imageSrc + '" alt=""></a>' +
            '</div>' +
            '<div class="wt-post-info">' +
            '<div class="wt-post-meta">' +
            '<ul>' +
            '<li class="post-category site-text-primary">' + name + '</li>' +
            '</ul>' +
            '</div>' +
            '<div class="wt-post-title">' +
            '<h3 class="post-title">' + description + '</h3>' +
            '</div>' +
            '<div class="wt-post-readmore">' +
            '<a href="stainDetails.html?id=' + encodeURIComponent(id) + '" class="site-button-link site-text-primary">Read More</a>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>'
        );
    }

    document.addEventListener('home:blog-loaded', function (e) {
        var container = e && e.detail && e.detail.node ? e.detail.node : null;
        if (!container) return;

        fetch('/api/Stains/GetAll', { method: 'GET' })
            .then(function (res) { return res.ok ? res.json() : Promise.reject(res); })
            .then(function (payload) {
                if (!payload) return;
                // Title and description
                try {
                    var titleEl = container.querySelector('.wt-small-separator div');
                    var apiTitle = payload.Main_Title || payload.main_Title;
                    if (titleEl && apiTitle) titleEl.textContent = apiTitle;
                    var descEl = container.querySelector('.section-head h2');
                    var apiDesc = payload.Main_Description || payload.main_Description;
                    if (descEl && apiDesc) descEl.textContent = apiDesc;
                } catch (_err) { }

                // List of blog posts
                var listContainer = container.querySelector('.row.d-flex.justify-content-center.blog-post-1-outer');
                if (!listContainer) return;
                var items = Array.isArray(payload.stains) ? payload.stains : [];
                if (!items || !items.length) { listContainer.innerHTML = ''; return; }
                var html = items.map(function (it) {
                    return buildBlogCardHtml({
                        id: it.Id || it.id,
                        name: it.Name || it.name,
                        description: it.Description || it.description,
                        imageLink: it.ImageLink || it.imageLink
                    });
                }).join('');
                listContainer.innerHTML = html;
            })
            .catch(function (err) {
                if (window && window.console) {
                    console.warn('Stains GetAll request failed', err && err.status ? err.status : err);
                }
            });
    });
    function buildWhyChooseCard(item) {
        var icon = item && (item.Icon || item.icon) ? (item.Icon || item.icon) : 'flaticon-clean';
        var title = item && (item.Title || item.title) ? (item.Title || item.title) : '';
        var description = item && (item.Description || item.description) ? (item.Description || item.description) : '';
        return (
            '<div class="col-lg-3 col-md-6 m-b30">' +
            '<div class="half-icon-box site-bg-secondry radius-md">' +
            '<div class="icon-box">' +
            '<span class="icon-small">' + icon + '</span>' +
            '<span class="icon-large"><i class="flaticon-clean"></i></span>' +
            '</div>' +
            '<div class="icon-content">' +
            '<h3 class="wt-title site-text-primary">' + title + '</h3>' +
            '<p>' + description + '</p>' +
            '</div>' +
            '</div>' +
            '</div>'
        );
    }

    document.addEventListener('home:why-choose-loaded', function (e) {
        var container = e && e.detail && e.detail.node ? e.detail.node : null;
        if (!container) return;

        fetch('/api/WhyChooseUs/GetAll', { method: 'GET' })
            .then(function (res) { return res.ok ? res.json() : Promise.reject(res); })
            .then(function (payload) {
                if (!payload) return;
                try {
                    var titleEl = container.querySelector('.section-head h2');
                    var descEl = container.querySelector('.wt-separator-two-part-right p');
                    var mainTitle = payload.Main_Title || payload.main_Title;
                    var mainDesc = payload.Main_Description || payload.main_Description;
                    if (titleEl && mainTitle) titleEl.textContent = mainTitle;
                    if (descEl && mainDesc) descEl.textContent = mainDesc;
                } catch (_err) { }

                var listContainer = container.querySelector('.section-content .row.d-flex.justify-content-center');
                if (!listContainer) return;
                var items = payload.WhyChooseUs || payload.whyChooseUs || [];
                var html = Array.isArray(items) ? items.map(buildWhyChooseCard).join('') : '';
                listContainer.innerHTML = html;
            })
            .catch(function (err) {
                if (window && window.console) {
                    console.warn('WhyChooseUs GetAll request failed', err && err.status ? err.status : err);
                }
            });
    });
    document.addEventListener('home:about-loaded', function (e) {
        var container = e && e.detail && e.detail.node ? e.detail.node : null;
        if (!container) return;

        // Fetch BaseHome for About (SectionType.AboutUs = 1) and H_AboutUs points in parallel
        var baseHomeReq = fetch('/api/BaseHome/GetByType/1', { method: 'GET' }).then(function (r) { return r.ok ? r.json() : Promise.reject(r); });
        var pointsReq = fetch('/api/H_AboutUs/GetAll', { method: 'GET' }).then(function (r) { return r.ok ? r.json() : Promise.reject(r); });

        Promise.all([baseHomeReq, pointsReq]).then(function (results) {
            var baseHome = results[0] || {};
            var pointsPayload = results[1] || {};

            // Title and description mapping per requirement (use H_AboutUs aggregate like Stains)
            try {
                var descTitleEl = container.querySelector('.left h2');
                var descParaEl = container.querySelector('.left p');
                var mainTitle = pointsPayload.Main_Title || pointsPayload.main_Title;
                var mainDesc = pointsPayload.Main_Description || pointsPayload.main_Description;
                if (descTitleEl && mainTitle) descTitleEl.textContent = mainTitle;
                if (descParaEl && mainDesc) descParaEl.textContent = mainDesc;
            } catch (_err) { }

            // Replace intro image from first About item ImageLink
            try {
                var aboutImg = container.querySelector('.about-clean-one-media img');
                var aboutItemsForImage = pointsPayload.H_Aboutus || pointsPayload.h_Aboutus || [];
                if (aboutImg && Array.isArray(aboutItemsForImage) && aboutItemsForImage.length) {
                    var imgSrc = aboutItemsForImage[0].ImageLink || aboutItemsForImage[0].imageLink || '';
                    if (imgSrc) aboutImg.setAttribute('src', imgSrc);
                }
            } catch (_imgErr) { }

            // Points list: About.Point and About.Icon
            try {
                var listEl = container.querySelector('.site-list-style-one.icon-style');
                if (listEl) {
                    var list = pointsPayload.H_Aboutus || pointsPayload.h_Aboutus || [];
                    var html = list.map(function (it) {
                        var point = it.Point || it.point || it.PointEn || '';
                        var icon = it.Icon || it.icon || 'flaticon-checked';
                        return (
                            '<li><div class="check-list-outer"><span>' + icon + '</span>' + point + '</div></li>'
                        );
                    }).join('');
                    if (html) listEl.innerHTML = html;
                }
            } catch (_err2) { }
        }).catch(function (err) {
            if (window && window.console) {
                console.warn('About section load failed', err && err.status ? err.status : err);
            }
        });
    });
})();

