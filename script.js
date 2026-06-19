/**
 * مزون نارشا - Narsha Boutique
 * Main JavaScript - Vanilla JS
 * Production Ready - No Dependencies
 */

(function () {
    'use strict';

    /* =================== DOM ELEMENTS =================== */
    const header = document.getElementById('header');
    const menuToggle = document.getElementById('menuToggle');
    const navList = document.getElementById('navList');
    const navLinks = document.querySelectorAll('.nav-link');
    const scrollTopBtn = document.getElementById('scrollTop');
    const heroParticles = document.getElementById('heroParticles');
    const statNumbers = document.querySelectorAll('.stat-number');
    const portfolioItems = document.querySelectorAll('.portfolio-item');
    const lightbox = document.getElementById('lightbox');
    const lightboxImg = document.getElementById('lightboxImg');
    const lightboxCaption = document.getElementById('lightboxCaption');
    const lightboxClose = document.getElementById('lightboxClose');
    const lightboxPrev = document.getElementById('lightboxPrev');
    const lightboxNext = document.getElementById('lightboxNext');

    let currentLightboxIndex = 0;
    let statsAnimated = false;

    /* =================== HEADER SCROLL =================== */
    function handleHeaderScroll() {
        if (window.scrollY > 50) {
            header.classList.add('scrolled');
        } else {
            header.classList.remove('scrolled');
        }
    }

    /* =================== ACTIVE NAV LINK =================== */
    function updateActiveNavLink() {
        const sections = document.querySelectorAll('section[id]');
        const scrollPos = window.scrollY + 150;

        sections.forEach(function (section) {
            const sectionTop = section.offsetTop;
            const sectionHeight = section.offsetHeight;
            const sectionId = section.getAttribute('id');

            if (scrollPos >= sectionTop && scrollPos < sectionTop + sectionHeight) {
                navLinks.forEach(function (link) {
                    link.classList.remove('active');
                    if (link.getAttribute('href') === '#' + sectionId) {
                        link.classList.add('active');
                    }
                });
            }
        });
    }

    /* =================== SCROLL TO TOP BUTTON =================== */
    function handleScrollTopVisibility() {
        if (window.scrollY > 500) {
            scrollTopBtn.classList.add('visible');
            scrollTopBtn.hidden = false;
        } else {
            scrollTopBtn.classList.remove('visible');
        }
    }

    scrollTopBtn.addEventListener('click', function () {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    });

    /* =================== MOBILE MENU =================== */
    menuToggle.addEventListener('click', function () {
        const isOpen = navList.classList.toggle('open');
        menuToggle.classList.toggle('active');
        menuToggle.setAttribute('aria-expanded', isOpen);
        document.body.style.overflow = isOpen ? 'hidden' : '';
    });

    navLinks.forEach(function (link) {
        link.addEventListener('click', function () {
            navList.classList.remove('open');
            menuToggle.classList.remove('active');
            menuToggle.setAttribute('aria-expanded', 'false');
            document.body.style.overflow = '';
        });
    });

    /* =================== HERO PARTICLES =================== */
    function createParticles() {
        if (!heroParticles) return;
        var count = window.innerWidth < 768 ? 12 : 25;

        for (var i = 0; i < count; i++) {
            var particle = document.createElement('div');
            particle.className = 'hero-particle';
            particle.style.left = Math.random() * 100 + '%';
            particle.style.animationDelay = Math.random() * 8 + 's';
            particle.style.animationDuration = (6 + Math.random() * 6) + 's';
            particle.style.width = (2 + Math.random() * 3) + 'px';
            particle.style.height = particle.style.width;
            heroParticles.appendChild(particle);
        }
    }

    /* =================== COUNTER ANIMATION =================== */
    function animateCounters() {
        if (statsAnimated) return;

        statNumbers.forEach(function (el) {
            var target = parseInt(el.getAttribute('data-target'), 10);
            var duration = 2000;
            var startTime = null;

            function easeOutQuart(t) {
                return 1 - Math.pow(1 - t, 4);
            }

            function update(timestamp) {
                if (!startTime) startTime = timestamp;
                var progress = Math.min((timestamp - startTime) / duration, 1);
                var easedProgress = easeOutQuart(progress);
                var currentValue = Math.floor(easedProgress * target);
                el.textContent = currentValue.toLocaleString('fa-IR') + '+';

                if (progress < 1) {
                    requestAnimationFrame(update);
                } else {
                    el.textContent = target.toLocaleString('fa-IR') + '+';
                }
            }

            requestAnimationFrame(update);
        });

        statsAnimated = true;
    }

    /* =================== INTERSECTION OBSERVER - REVEAL =================== */
    function setupRevealObserver() {
        var revealElements = document.querySelectorAll('.reveal-element');

        if (!('IntersectionObserver' in window)) {
            // Fallback for older browsers
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
            threshold: 0.15,
            rootMargin: '0px 0px -50px 0px'
        });

        revealElements.forEach(function (el) {
            observer.observe(el);
        });
    }

    /* =================== STATS OBSERVER =================== */
    function setupStatsObserver() {
        var statsSection = document.querySelector('.hero-stats');
        if (!statsSection) return;

        if (!('IntersectionObserver' in window)) {
            animateCounters();
            return;
        }

        var observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    animateCounters();
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.5 });

        observer.observe(statsSection);
    }

    /* =================== LIGHTBOX =================== */
    var portfolioImages = [];

    /* =================== FAQ ACCORDION =================== */
    function setupFAQAccordion() {
        var questions = document.querySelectorAll('.faq-question');
        questions.forEach(function (q) {
            q.addEventListener('click', function () {
                var expanded = this.getAttribute('aria-expanded') === 'true';
                // close all
                questions.forEach(function (other) {
                    other.setAttribute('aria-expanded', 'false');
                    var ans = other.nextElementSibling;
                    if (ans) ans.hidden = true;
                });

                if (!expanded) {
                    this.setAttribute('aria-expanded', 'true');
                    var answer = this.nextElementSibling;
                    if (answer) answer.hidden = false;
                }
            });
        });
    }

    function initPortfolioData() {
        portfolioItems.forEach(function (item, index) {
            var img = item.querySelector('.portfolio-img');
            if (img) {
                portfolioImages.push({
                    src: img.src.replace('h=600&w=400', 'h=1200&w=800'),
                    alt: img.alt,
                    caption: item.querySelector('.portfolio-name') ? item.querySelector('.portfolio-name').textContent : ''
                });
            }
        });
    }

    function openLightbox(index) {
        if (index < 0 || index >= portfolioImages.length) return;

        currentLightboxIndex = index;
        var data = portfolioImages[index];

        lightboxImg.src = data.src;
        lightboxImg.alt = data.alt;
        lightboxCaption.textContent = data.caption;

        lightbox.hidden = false;
        // Force reflow before adding active class
        void lightbox.offsetHeight;
        lightbox.classList.add('active');
        document.body.style.overflow = 'hidden';
    }

    function closeLightbox() {
        lightbox.classList.remove('active');
        document.body.style.overflow = '';
        setTimeout(function () {
            lightbox.hidden = true;
            lightboxImg.src = '';
        }, 350);
    }

    function navigateLightbox(direction) {
        var newIndex = currentLightboxIndex + direction;
        if (newIndex < 0) newIndex = portfolioImages.length - 1;
        if (newIndex >= portfolioImages.length) newIndex = 0;
        openLightbox(newIndex);
    }

    // Portfolio click handlers
    portfolioItems.forEach(function (item) {
        var zoomBtn = item.querySelector('.portfolio-zoom');
        var index = parseInt(item.getAttribute('data-index'), 10);

        item.addEventListener('click', function (e) {
            if (e.target.closest('.portfolio-zoom')) {
                openLightbox(index);
            }
        });

        // Also open on image click
        item.addEventListener('click', function () {
            openLightbox(index);
        });
    });

    lightboxClose.addEventListener('click', closeLightbox);
    lightboxPrev.addEventListener('click', function () { navigateLightbox(-1); });
    lightboxNext.addEventListener('click', function () { navigateLightbox(1); });

    // Close on background click
    lightbox.addEventListener('click', function (e) {
        if (e.target === lightbox || e.target.classList.contains('lightbox-content')) {
            closeLightbox();
        }
    });

    // Keyboard navigation
    document.addEventListener('keydown', function (e) {
        if (!lightbox.classList.contains('active')) return;

        switch (e.key) {
            case 'Escape':
                closeLightbox();
                break;
            case 'ArrowRight':
                navigateLightbox(-1); // RTL: right = prev
                break;
            case 'ArrowLeft':
                navigateLightbox(1); // RTL: left = next
                break;
        }
    });

    /* =================== SMOOTH SCROLL FOR ANCHOR LINKS =================== */
    document.querySelectorAll('a[href^="#"]').forEach(function (anchor) {
        anchor.addEventListener('click', function (e) {
            var targetId = this.getAttribute('href');
            if (targetId === '#') return;

            var target = document.querySelector(targetId);
            if (target) {
                e.preventDefault();
                var headerHeight = header.offsetHeight;
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
        updateActiveNavLink();
        handleScrollTopVisibility();
    }, 100);

    window.addEventListener('scroll', handleScroll, { passive: true });

    /* =================== SERVICE CARD HOVER EFFECT =================== */
    var serviceCards = document.querySelectorAll('.service-card');
    serviceCards.forEach(function (card) {
        card.addEventListener('mousemove', function (e) {
            var rect = card.getBoundingClientRect();
            var x = e.clientX - rect.left;
            var y = e.clientY - rect.top;

            card.style.setProperty('--mouse-x', x + 'px');
            card.style.setProperty('--mouse-y', y + 'px');
        });
    });

    /* =================== LAZY LOAD IMAGES =================== */
    function setupLazyLoading() {
        var lazyImages = document.querySelectorAll('img[loading="lazy"]');

        if ('loading' in HTMLImageElement.prototype) {
            // Browser supports native lazy loading - nothing extra needed
            return;
        }

        // Fallback: use Intersection Observer
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

    /* =================== INITIALIZE =================== */
    function init() {
        createParticles();
        initPortfolioData();
        setupRevealObserver();
        setupStatsObserver();
        setupLazyLoading();
        setupFAQAccordion();
        handleHeaderScroll();
        updateActiveNavLink();
    }

    // Run initialization when DOM is ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }
})();
