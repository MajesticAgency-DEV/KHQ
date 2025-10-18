// Slider Manager
class SliderManager {
    constructor() {
        this.apiUrl = 'https://your-api-domain.com/api/sliders'; // Replace with your API endpoint
        this.sliderContainer = $('#slider-container');
        this.loadingElement = $('#slider-loading');
        this.sliderElement = $('#webmax-one');
        this.revApi = null;
        this.sliderData = [];
        this.sliderInitialized = false;
    }

    // Fetch slider data from API
    async fetchSliderData() {
        try {
            console.log('Fetching slider data from API...');
            const response = await fetch(this.apiUrl);
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            this.sliderData = await response.json();
            console.log('Slider data fetched successfully:', this.sliderData);
            return this.sliderData;
        } catch (error) {
            console.error('Error fetching slider data:', error);
            // Fallback to demo data if API fails
            this.sliderData = this.getDemoData();
            return this.sliderData;
        }
    }

    // Demo data for testing
    getDemoData() {
        return  [
            {
                Id: '1',
                Link: 'contact-1.html',
                Title: 'Professional Cleaner',
                Description: 'Our professional and experienced cleaning staff does the job right the first time.',
                ButtonText: 'See Our Best Offer',
                Images: [
                    'C:/Users/mtbakhi/OneDrive - Access to Arabia/Desktop/Mutaz/KHQ/SharedImages/A_2a7bdac3-482d-4a95-84bd-acff4fd8bb1a.png',
                    'C:/Users/mtbakhi/OneDrive - Access to Arabia/Desktop/Mutaz/KHQ/SharedImages/A_2e6faa36-6032-4871-bf2e-28da74b398f6.png',
                    'C:/Users/mtbakhi/OneDrive - Access to Arabia/Desktop/Mutaz/KHQ/SharedImages/A_4e84b143-194c-40db-be4d-284ee8cc9ed6.png'
                ]
            },
            {
                Id: '2',
                Link: 'contact-1.html',
                Title: 'Best Quality Solution in Cleaning',
                Description: 'It\'s suitable for cleaning website such as Labor services, House Cleaning, Apartment Cleaners, Office Cleaning, Washing services.',
                ButtonText: 'See Our Best Offer',
                Images: [
                    'C:/Users/mtbakhi/OneDrive - Access to Arabia/Desktop/Mutaz/KHQ/SharedImages/A_a638f3b4-60bc-446e-8834-9fc30bc75de2.png',
                    'C:/Users/mtbakhi/OneDrive - Access to Arabia/Desktop/Mutaz/KHQ/SharedImages/A_7fb03d74-2a4a-400a-92b3-44f2bffb4fd8.png',
                    'C:/Users/mtbakhi/OneDrive - Access to Arabia/Desktop/Mutaz/KHQ/SharedImages/A_2a7bdac3-482d-4a95-84bd-acff4fd8bb1a.png'
                ]
            },
            {
                Id: '3',
                Link: 'services.html',
                Title: 'Eco-Friendly Cleaning Solutions',
                Description: 'We use environmentally friendly products that are safe for your family and pets while delivering exceptional cleaning results.',
                ButtonText: 'View Our Services',
                Images: [
                    'C:/Users/mtbakhi/OneDrive - Access to Arabia/Desktop/Mutaz/KHQ/SharedImages/A_a638f3b4-60bc-446e-8834-9fc30bc75de2.png',
                    'C:/Users/mtbakhi/OneDrive - Access to Arabia/Desktop/Mutaz/KHQ/SharedImages/A_2a7bdac3-482d-4a95-84bd-acff4fd8bb1a.png',
                    'C:/Users/mtbakhi/OneDrive - Access to Arabia/Desktop/Mutaz/KHQ/SharedImages/A_abe58eed-cc57-4c78-b229-cc716e1b42fd.png'
                ]
            }
        ];
    }

    // Generate slide HTML
    generateSlideHtml(slideData, index) {
        const slideId = index + 901; // Starting from 901 as in the template

        return `
            <!-- SLIDE ${index + 1} -->
            <li data-index="rs-${slideId}"
                data-transition="fade"
                data-slotamount="default"
                data-hideafterloop="0"
                data-hideslideonmobile="off"
                data-easein="default"
                data-easeout="default"
                data-masterspeed="default"
                data-thumb=""
                data-rotate="0"
                data-fstransition="fade"
                data-fsmasterspeed="300"
                data-fsslotamount="7"
                data-saveperformance="off"
                data-title="${this.escapeHtml(slideData.title)}"
                data-param1="${index + 1}"
                data-param2=""
                data-param3=""
                data-param4=""
                data-param5=""
                data-param6=""
                data-param7=""
                data-param8=""
                data-param9=""
                data-param10=""
                data-description="${this.escapeHtml(slideData.description)}">
                
                <!-- MAIN IMAGE -->
                <img src="${slideData.images[0]}" alt="${this.escapeHtml(slideData.title)}" 
                     data-lazyload="" 
                     data-bgposition="center center" 
                     data-kenburns="on" 
                     data-duration="10000" 
                     data-ease="Power1.easeOut" 
                     data-scalestart="110" 
                     data-scaleend="100" 
                     data-rotatestart="0" 
                     data-rotateend="0" 
                     data-offsetstart="0 0" 
                     data-offsetend="0 0" 
                     class="rev-slidebg xyz" 
                     data-no-retina>

                <!-- LAYER NR. 1 [ for overlay ] -->
                <div class="tp-caption tp-shape tp-shapewrapper"
                     id="slide-${slideId}-layer-0"
                     data-x="['center','center','center','center']" data-hoffset="['0','0','0','0']"
                     data-y="['middle','middle','middle','middle']" data-voffset="['0','0','0','0']"
                     data-width="full"
                     data-height="full"
                     data-whitespace="nowrap"
                     data-type="shape"
                     data-basealign="slide"
                     data-responsive_offset="off"
                     data-responsive="off"
                     data-frames='[
                            {"from":"opacity:0;","speed":1000,"to":"o:1;","delay":0,"ease":"Power4.easeOut"},
                            {"delay":"wait","speed":1000,"to":"opacity:0;","ease":"Power4.easeOut"}
                            ]'
                     data-textAlign="['left','left','left','left']"
                     data-paddingtop="[0,0,0,0]"
                     data-paddingright="[0,0,0,0]"
                     data-paddingbottom="[0,0,0,0]"
                     data-paddingleft="[0,0,0,0]"
                     style="z-index: 1;background-color:rgba(0, 0, 0, 0);border-color:rgba(0, 0, 0, 0);border-width:0px;">
                </div>

                ${slideData.images.length > 1 ? `
                <!-- LAYER NR. 1  Img -->
                <div class="tp-caption   tp-resizeme layer_1"
                     id="slide-${slideId}-layer-1"
                     data-x="['left','left','center','center']" data-hoffset="['-300','-100','0','0']"
                     data-y="['bottom','bottom','bottom','bottom']" data-voffset="['0','-50','-0','-0']"
                     data-width="none"
                     data-height="none"
                     data-whitespace="nowrap"
                     data-type="image"
                     data-responsive_offset="off"
                     data-frames='[{"from":"x:left;","speed":1500,"to":"o:1;","delay":2500,"ease":"Power3.easeOut"},{"delay":"wait","speed":1500,"to":"opacity:0;","ease":"Power4.easeIn"}]'
                     data-textAlign="['left','left','center','center']"
                     data-paddingtop="[0,0,0,0]"
                     data-paddingright="[0,0,0,0]"
                     data-paddingbottom="[0,0,0,0]"
                     data-paddingleft="[0,0,0,0]"
                     style="z-index: 13;border-width:0px;">
                    <div>
                        <img src="${slideData.images[1]}"
                             alt="${this.escapeHtml(slideData.title)}"
                             data-ww="['826px','450px','400px','300px']"
                             data-hh="['558px','304px','270px','203px']"
                             width="826" height="558"
                             data-no-retina>
                    </div>
                </div>
                ` : ''}

                ${slideData.images.length > 2 ? `
                <!-- LAYER NR. 2 [ Circle ] -->
                <div class="tp-caption layer_2"
                     id="slide-${slideId}-layer-2"
                     data-x="['left','left','center','center']" data-hoffset="['200','200','0','0']"
                     data-y="['bottom','bottom','bottom','bottom']" data-voffset="['200','80','80','80']"
                     data-lineheight="['0','0','0','0']"
                     data-width="['100','100','100','100']"
                     data-height="['none','none','none','none']"
                     data-whitespace="['normal','normal','normal','normal']"
                     data-type="text"
                     data-responsive_offset="on"
                     data-frames='[
                            {"from":"y:100px(R);opacity:0;","speed":2000,"to":"o:1;","delay":2000,"ease":"Power4.easeOut"},
                            {"delay":"wait","speed":1000,"to":"y:-50px;opacity:0;","ease":"Power2.easeInOut"}
                            ]'
                     data-textAlign="['right','right','center','center']"
                     data-paddingtop="[0,0,0,0]"
                     data-paddingright="[0,0,0,0]"
                     data-paddingbottom="[0,0,0,0]"
                     data-paddingleft="[0,0,0,0]"
                     style="z-index:9;">
                    <div>
                        <img src="${slideData.images[2]}"
                             alt="${this.escapeHtml(slideData.title)}"
                             data-ww="['446px','300px','250px','250px']"
                             data-hh="['445px','299px','250px','250px']"
                             width="446" height="445"
                             data-no-retina>
                    </div>
                </div>
                ` : ''}

                <!-- LAYER NR. 3 [ for title ] -->
                <div class="tp-caption   tp-resizeme layer_4"
                     id="slide-${slideId}-layer-4"
                     data-x="['right','right','center','center']" data-hoffset="[0','0','0','0']"
                     data-y="['middle','middle','middle','middle']" data-voffset="['0','-50','-140','-140']"
                     data-fontsize="['80','52','42','38']"
                     data-lineheight="['80','52','42','38']"
                     data-width="['500','500','500','500']"
                     data-height="['none','none','none','none']"
                     data-whitespace="['normal','normal','normal','normal']"
                     data-type="text"
                     data-responsive_offset="on"
                     data-frames='[
                            {"from":"y:100px(R);opacity:0;","speed":2000,"to":"o:1;","delay":2000,"ease":"Power4.easeOut"},
                            {"delay":"wait","speed":1000,"to":"y:-50px;opacity:0;","ease":"Power2.easeInOut"}
                            ]'
                     data-textAlign="['left','left','center','center']"
                     data-paddingtop="[5,5,5,5]"
                     data-paddingright="[0,0,0,20]"
                     data-paddingbottom="[0,0,0,0]"
                     data-paddingleft="[0,0,0,20]"
                     style="z-index: 11;
                            white-space: normal;
                            font-weight: 900;
                            color:#fff;
                            border-width:0px; font-family: 'dinnextltarabic-medium', 'Heebo';">${this.escapeHtml(slideData.title)}</div>

                <!-- LAYER NR. 4 [ for paragraph] -->
                <div class="tp-caption  tp-resizeme layer_5"
                     id="slide-${slideId}-layer-5"
                     data-x="['right','right','center','center']" data-hoffset="['0','0','0','0']"
                     data-y="['middle','middle','middle','middle']" data-voffset="['150','20','-60','-60']"
                     data-fontsize="['19','19','18','16']"
                     data-lineheight="['28','28','28','22']"
                     data-width="['500','500','500','500']"
                     data-height="['none','none','none','none']"
                     data-whitespace="['normal','normal','normal','normal']"
                     data-type="text"
                     data-responsive_offset="on"
                     data-frames='[
                            {"from":"y:100px(R);opacity:0;","speed":2000,"to":"o:1;","delay":2500,"ease":"Power4.easeOut"},
                            {"delay":"wait","speed":1000,"to":"y:-50px;opacity:0;","ease":"Power2.easeInOut"}
                            ]'
                     data-textAlign="['left','left','center','center']"
                     data-paddingtop="[0,0,0,0]"
                     data-paddingright="[0,0,0,30]"
                     data-paddingbottom="[0,0,0,0]"
                     data-paddingleft="[0,0,0,30]"
                     style="z-index: 11;
                            font-weight: 300;
                            color:#fff;
                            border-width:0px;font-family: 'dinnextltarabic-medium', 'Heebo';">
                    ${this.escapeHtml(slideData.description)}
                </div>

                <!-- LAYER NR. 5 [ for botton ] -->
                <div class="tp-caption tp-resizeme rev-btn layer_6"
                     id="slide-${slideId}-layer-6"
                     data-x="['right','right','center','center']" data-hoffset="['0','0','0','0']"
                     data-y="['middle','middle','middle','middle']" data-voffset="['260','120','40','40']"
                     data-lineheight="['none','none','none','none']"
                     data-width="['500','500','300','300']"
                     data-height="['none','none','none','none']"
                     data-whitespace="['normal','normal','normal','normal']"
                     data-type="text"
                     data-responsive_offset="on"
                     data-frames='[
                            {"from":"y:100px(R);opacity:0;","speed":2000,"to":"o:1;","delay":3000,"ease":"Power4.easeOut"},
                            {"delay":"wait","speed":1000,"to":"y:-50px;opacity:0;","ease":"Power2.easeInOut"}
                            ]'
                     data-textAlign="['left','left','center','center']"
                     data-paddingtop="[0,0,0,0]"
                     data-paddingright="[0,0,0,0]"
                     data-paddingbottom="[0,0,0,0]"
                     data-paddingleft="[0,0,0,0]"
                     style="z-index:14;">
                    <a href="${slideData.link}" class="site-button-secondry">${this.escapeHtml(slideData.buttonText)}</a>
                </div>
            </li>
        `;
    }

    // Escape HTML to prevent XSS
    escapeHtml(text) {
        const map = {
            '&': '&amp;',
            '<': '&lt;',
            '>': '&gt;',
            '"': '&quot;',
            "'": '&#039;'
        };
        return text.replace(/[&<>"']/g, function (m) { return map[m]; });
    }

    // Render all slides
    renderSlides() {
        console.log('Rendering slides...');
        let slidesHtml = '';

        this.sliderData.forEach((slide, index) => {
            slidesHtml += this.generateSlideHtml(slide, index);
        });

        console.log('Generated HTML:', slidesHtml);

        // Use different approach to ensure content is added
        this.sliderContainer.empty(); // Clear any existing content
        this.sliderContainer.append(slidesHtml);

        // Verify content was added
        console.log('Slides in container:', this.sliderContainer.children().length);
    }

    // Initialize Revolution Slider
    initRevolutionSlider() {
        if (this.sliderInitialized) {
            console.log('Slider already initialized');
            return;
        }

        if (!this.sliderElement.length) {
            console.error('Slider element not found');
            return;
        }

        if (this.sliderElement.revolution === undefined) {
            console.error("Revolution Slider not found");
            return;
        }

        console.log('Initializing Revolution Slider...');

        try {
            this.revApi = this.sliderElement.show().revolution({
                sliderType: "standard",
                jsFileLocation: "//cdn.jsdelivr.net/npm/revolution-slider@5.4.3.1/js/",
                sliderLayout: "fullwidth",
                dottedOverlay: "none",
                delay: 5000,
                navigation: {
                    keyboardNavigation: "off",
                    keyboard_direction: "horizontal",
                    mouseScrollNavigation: "off",
                    mouseScrollReverse: "default",
                    onHoverStop: "off",
                    touch: {
                        touchenabled: "on",
                        touchOnDesktop: "off",
                        swipe_threshold: 75,
                        swipe_min_touches: 1,
                        swipe_direction: "horizontal",
                        drag_block_vertical: false
                    },
                    tabs: {
                        style: "custom",
                        enable: true,
                        width: 250,
                        height: 40,
                        min_width: 249,
                        wrapper_padding: 0,
                        wrapper_color: "",
                        wrapper_opacity: "0",
                        tmp: '<div class="tp-tab-wrapper slider-number-wraper"><div class="tp-tab-number">{{param1}}</div></div>',
                        visibleAmount: 5,
                        hide_onmobile: true,
                        hide_under: 800,
                        hide_onleave: false,
                        hide_delay: 200,
                        direction: "vertical",
                        span: true,
                        position: "inner",
                        space: 0,
                        h_align: "left",
                        v_align: "center",
                        h_offset: 0,
                        v_offset: 0
                    },
                    bullets: {
                        enable: true,
                        hide_onmobile: false,
                        hide_over: 778,
                        style: "bullet-bar",
                        hide_onleave: false,
                        direction: "horizontal",
                        h_align: "center",
                        v_align: "bottom",
                        h_offset: 0,
                        v_offset: 30,
                        space: 5,
                        tmp: ''
                    }
                },
                viewPort: {
                    enable: true,
                    outof: "wait",
                    visible_area: "100%",
                    presize: true
                },
                responsiveLevels: [1240, 1024, 778, 480],
                visibilityLevels: [1240, 1024, 778, 480],
                gridwidth: [1240, 1024, 778, 480],
                gridheight: [1000, 768, 1080, 950],
                lazyType: "single",
                parallax: {
                    type: "scroll",
                    origo: "slidercenter",
                    speed: 400,
                    levels: [5, 10, 15, 20, 25, 30, 35, 40, 45, 46, 47, 48, 49, 50, 51, 55],
                },
                shadow: 0,
                spinner: "spinner3",
                stopLoop: "off",
                stopAfterLoops: -1,
                stopAtSlide: 1,
                shuffle: "off",
                autoHeight: "off",
                fullScreenAutoWidth: "off",
                fullScreenAlignForce: "off",
                fullScreenOffsetContainer: ".site-header",
                fullScreenOffset: "-50px",
                disableProgressBar: "on",
                hideThumbsOnMobile: "off",
                hideSliderAtLimit: 0,
                hideCaptionAtLimit: 0,
                hideAllCaptionAtLilmit: 0,
                debugMode: false,
                fallbacks: {
                    simplifyAll: "off",
                    nextSlideOnWindowFocus: "off",
                    disableFocusListener: false,
                }
            });

            this.sliderInitialized = true;
            console.log('Revolution Slider initialized successfully');
        } catch (error) {
            console.error('Error initializing Revolution Slider:', error);
        }
    }

    // Initialize the slider
    async init() {
        try {
            console.log('Starting slider initialization...');

            // Show loading spinner
            this.loadingElement.show();
            this.sliderElement.hide();

            // Fetch slider data
            await this.fetchSliderData();

            // Render slides
            this.renderSlides();

            // Small delay to ensure DOM is updated
            await new Promise(resolve => setTimeout(resolve, 100));

            // Hide loading spinner and show slider
            this.loadingElement.hide();
            this.sliderElement.show();

            // Initialize Revolution Slider with a small delay
            setTimeout(() => {
                this.initRevolutionSlider();
            }, 500);

            console.log('Dynamic slider initialization completed');
        } catch (error) {
            console.error('Error initializing slider:', error);
            this.loadingElement.html('<div class="alert alert-danger text-center p-4">Failed to load slider content. Please refresh the page.</div>');
        }
    }
}

// Alternative initialization with better error handling
function initializeSlider() {
    const sliderManager = new SliderManager();

    // Wait for jQuery and Revolution Slider to be ready
    if (window.jQuery && window.jQuery.fn.revolution) {
        sliderManager.init();
    } else {
        // If not ready, wait for document ready
        $(document).ready(function () {
            // Additional check for Revolution Slider
            if (window.jQuery.fn.revolution) {
                sliderManager.init();
            } else {
                // If Revolution Slider still not available, try after a delay
                setTimeout(() => {
                    if (window.jQuery.fn.revolution) {
                        sliderManager.init();
                    } else {
                        console.error('Revolution Slider not available');
                        $('#slider-loading').html('<div class="alert alert-warning text-center p-4">Slider plugin not loaded. Please check your dependencies.</div>');
                    }
                }, 1000);
            }
        });
    }
}

// Initialize when everything is ready
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initializeSlider);
} else {
    initializeSlider();
}

// Also initialize when slider is loaded asynchronously
document.addEventListener('home:slider-loaded', initializeSlider);