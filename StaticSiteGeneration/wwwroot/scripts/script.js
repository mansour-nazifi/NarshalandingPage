/**
 * مزون نارشا - Narsha Boutique
 * Main JavaScript - Vanilla JS
 * Production Ready
 */

(function () {
    'use strict';

    /* =================== DOM ELEMENTS =================== */
    const header = document.getElementById('header');
    const navList = document.getElementById('navList');
    const scrollTopBtn = document.getElementById('scrollTop');

    /* =================== HEADER SCROLL =================== */
    function handleHeaderScroll() {
        if (!header) return;
        if (window.scrollY > 50) {
            header.classList.add('scrolled');
        } else {
            header.classList.remove('scrolled');
        }
    }

    /* =================== SCROLL TO TOP BUTTON =================== */
    function handleScrollTopVisibility() {
        if (!scrollTopBtn) return;
        if (window.scrollY > 500) {
            scrollTopBtn.classList.add('visible');
            scrollTopBtn.hidden = false;
        } else {
            scrollTopBtn.classList.remove('visible');
        }
    }

    if (scrollTopBtn) {
        scrollTopBtn.addEventListener('click', function () {
            window.scrollTo({ top: 0, behavior: 'smooth' });
        });
    }

    /* =================== MOBILE MENU =================== */
    if (navList) {
        navList.querySelectorAll('.nav-link').forEach(function (link) {
            link.addEventListener('click', function () {
                var instance = bootstrap.Offcanvas.getInstance(navList);
                if (instance) instance.hide();
            });
        });
    }

    /* =================== INTERSECTION OBSERVER - REVEAL =================== */
    function setupRevealObserver() {
        var revealElements = document.querySelectorAll('.reveal-element');

        if (!('IntersectionObserver' in window)) {
            revealElements.forEach(function (el) {
                el.classList.add('revealed');
            });
            return;
        }

        var observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    entry.target.classList.add('revealed');
                    observer.unobserve(entry.target);
                }
            });
        }, {
            threshold: 0.05,
            rootMargin: '0px 0px 100px 0px'
        });

        revealElements.forEach(function (el) {
            observer.observe(el);
        });
    }

    /* =================== PORTFOLIO LIGHTBOX (Bootstrap Modal + Carousel) =================== */
    function setupPortfolioLightbox() {
        var lightbox = document.getElementById('lightbox');
        var carouselEl = document.getElementById('lightboxCarousel');
        if (!lightbox || !carouselEl || typeof bootstrap === 'undefined') return;

        var carousel = bootstrap.Carousel.getOrCreateInstance(carouselEl, { interval: false, ride: false, touch: true, wrap: true });

        lightbox.addEventListener('shown.bs.modal', function (event) {
            var trigger = event.relatedTarget;
            if (!trigger) return;
            var index = parseInt(trigger.getAttribute('data-bs-slide-to'), 10);
            if (!isNaN(index)) carousel.to(index);
        });
    }

    /* =================== SMOOTH SCROLL FOR ANCHOR LINKS =================== */
    document.querySelectorAll('a[href^="#"]').forEach(function (anchor) {
        anchor.addEventListener('click', function (e) {
            var targetId = this.getAttribute('href');
            if (targetId === '#') return;

            var target = document.querySelector(targetId);
            if (target) {
                e.preventDefault();
                var headerHeight = header ? header.offsetHeight : 0;
                var targetPosition = target.getBoundingClientRect().top + window.scrollY - headerHeight;

                window.scrollTo({
                    top: targetPosition,
                    behavior: 'smooth'
                });
            }
        });
    });

    /* =================== THROTTLE UTILITY =================== */
    function throttle(fn, delay) {
        var lastCall = 0;
        return function () {
            var now = Date.now();
            if (now - lastCall >= delay) {
                lastCall = now;
                fn.apply(this, arguments);
            }
        };
    }

    /* =================== SCROLL EVENT =================== */
    var handleScroll = throttle(function () {
        handleHeaderScroll();
        handleScrollTopVisibility();
    }, 100);

    window.addEventListener('scroll', handleScroll, { passive: true });

    /* =================== LAZY LOAD IMAGES =================== */
    function setupLazyLoading() {
        var lazyImages = document.querySelectorAll('img[loading="lazy"]');

        if ('loading' in HTMLImageElement.prototype) {
            return;
        }

        if (!('IntersectionObserver' in window)) {
            lazyImages.forEach(function (img) {
                if (img.dataset.src) {
                    img.src = img.dataset.src;
                }
            });
            return;
        }

        var imageObserver = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    var img = entry.target;
                    if (img.dataset.src) {
                        img.src = img.dataset.src;
                    }
                    imageObserver.unobserve(img);
                }
            });
        }, {
            rootMargin: '200px'
        });

        lazyImages.forEach(function (img) {
            imageObserver.observe(img);
        });
    }

    /* =================== HERO CATEGORY SCROLL =================== */
    function setupHeroCatScroll() {
        var track = document.getElementById('heroCatTrack');
        if (!track) return;
        var slider = track.parentElement;
        var prev = slider.querySelector('.hero-cat-prev');
        var next = slider.querySelector('.hero-cat-next');
        function itemWidth() {
            var item = track.querySelector('.hero-cat-item');
            return item ? item.offsetWidth + 12 : 0;
        }
        function updateButtons() {
            var canScroll = track.scrollWidth > track.clientWidth + 1;
            prev.style.display = canScroll ? '' : 'none';
            next.style.display = canScroll ? '' : 'none';
        }
        if (prev) prev.addEventListener('click', function () { track.scrollBy({ left: -itemWidth(), behavior: 'smooth' }); });
        if (next) next.addEventListener('click', function () { track.scrollBy({ left: itemWidth(), behavior: 'smooth' }); });
        window.addEventListener('resize', updateButtons);
        updateButtons();
    }
    function init() {
        setupHeroCatScroll();
        setupPortfolioLightbox();
        setupRevealObserver();
        handleHeaderScroll();
        handleScrollTopVisibility();
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }
})();
