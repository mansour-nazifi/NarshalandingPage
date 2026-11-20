/**
 * Custom Tailoring - Price Calculator & Bale Integration
 * Vanilla JS - No Dependencies
 */

(function () {
    'use strict';

    /* =================== BALE CONFIGURATION =================== */
    window.NarshaConfig = {
        bale: {
            username: 'narsha_ir',
            baseUrl: 'https://ble.ir/',
            getChatUrl: function() {
                return this.baseUrl + this.username;
            }
        },
        priceDisclaimer: 'این مبلغ هزینه شروع دوخت است و قیمت نهایی پس از بررسی جزئیات سفارش اعلام خواهد شد.',
        currency: {
            locale: 'fa-IR',
            currency: 'IRR',
            minimumFractionDigits: 0
        }
    };

    /* =================== PRICE CALCULATOR CLASS =================== */
    class PriceCalculator {
        constructor(containerSelector, config) {
            this.container = document.querySelector(containerSelector);
            this.config = config;
            this.basePrice = 0;
            this.options = [];

            if (this.container) {
                this.init();
            }
        }

        init() {
            this.basePrice = parseInt(this.container.dataset.basePrice, 10) || 0;
            this.setupOptions();
            this.bindEvents();
            this.updateTotal();
        }

        setupOptions() {
            const optionElements = this.container.querySelectorAll('.option-checkbox');
            this.options = Array.from(optionElements).map(el => ({
                element: el,
                id: el.id || '',
                name: el.dataset.name || el.getAttribute('data-name') || '',
                price: parseInt(el.dataset.price || el.getAttribute('data-price'), 10) || 0,
                mandatory: (el.dataset.mandatory || el.getAttribute('data-mandatory') || '').toString().toLowerCase() === 'true',
                disabled: !!el.disabled || !!el.hasAttribute('disabled')
            }));
        }

        bindEvents() {
            if (this._bound) return;
            this._bound = true;
            this.container.addEventListener('change', (e) => {
                const t = e.target;
                if (t && (t.classList && t.classList.contains('option-checkbox') || (t.matches && t.matches('.option-checkbox')))) {
                    this.updateTotal();
                }
            });
            this.container.addEventListener('click', (e) => {
                const t = e.target;
                if (t && (t.classList && t.classList.contains('option-checkbox') || (t.matches && t.matches('.option-checkbox')))) {
                    // wait for state change
                    setTimeout(() => this.updateTotal(), 0);
                }
            });
            this.container.querySelectorAll('.option-checkbox').forEach(el => {
                if (el._narshaBound) return;
                el._narshaBound = true;
                el.addEventListener('change', () => this.updateTotal());
            });
        }

        resolveElement(opt) {
            if (opt.element && document.contains(opt.element)) return opt.element;
            let el = null;
            if (opt.id) el = document.getElementById(opt.id);
            if (!el && opt.name) {
                const candidates = this.container.querySelectorAll('.option-checkbox');
                for (let i = 0; i < candidates.length; i++) {
                    if ((candidates[i].dataset.name || candidates[i].getAttribute('data-name')) === opt.name) { el = candidates[i]; break; }
                }
            }
            opt.element = el;
            return el;
        }

        updateTotal() {
            let total = this.basePrice;

            this.options.forEach(opt => {
                const el = this.resolveElement(opt);
                const mandatory = !!opt.mandatory;
                if (el && el.checked && !mandatory) {
                    total += parseInt(opt.price, 10) || 0;
                }
            });

            const totalEl = this.container.querySelector('.price-total-value');
            if (totalEl) {
                totalEl.textContent = this.formatPrice(total);
            }

            this.updateBaleButton(total);
        }

        updateBaleButton(total) {
            const baleBtn = this.container.querySelector('.btn-bale') || document.querySelector('.btn-bale');
            if (baleBtn) {
                baleBtn.dataset.calculatedTotal = total;
                baleBtn.dataset.selectedOptions = JSON.stringify(
                    this.options.filter(o => o.element && o.element.checked && !o.mandatory).map(o => ({
                        name: o.name,
                        price: o.price
                    }))
                );
            }
        }

        formatPrice(amount) {
            return amount.toLocaleString(this.config.currency.locale) + ' تومان';
        }

        getCalculatedTotal() {
            let total = this.basePrice;
            this.options.forEach(opt => {
                const el = this.resolveElement(opt);
                if (el && el.checked && !opt.mandatory) {
                    total += parseInt(opt.price, 10) || 0;
                }
            });
            return total;
        }

        getSelectedOptions() {
            return this.options
                .filter(o => {
                    const el = this.resolveElement(o);
                    return el && el.checked && !o.mandatory;
                })
                .map(o => ({ name: o.name, price: parseInt(o.price, 10) || 0 }));
        }

        generateBaleMessage(productData) {
            const total = this.getCalculatedTotal();
            const selectedOptions = this.getSelectedOptions();

            let message = `سلام، برای دوخت سفارشی محصول زیر درخواست مشاوره دارم.\n\n`;
            message += `نام محصول: ${productData.name}\n\n`;
            message += `هزینه پایه دوخت: ${this.formatPrice(this.basePrice)}\n\n`;

            if (selectedOptions.length > 0) {
                message += `گزینه‌های انتخابی:\n`;
                selectedOptions.forEach(opt => {
                    message += `• ${opt.name}: ${this.formatPrice(opt.price)}\n`;
                });
                message += `\n`;
            } else {
                message += `گزینه‌های انتخابی: هیچکدام\n\n`;
            }

            message += `هزینه شروع دوخت (محاسبه‌شده): ${this.formatPrice(total)}\n\n`;
            message += `${this.config.priceDisclaimer}\n\n`;
            message += `لینک محصول: ${window.location.href}\n\n`;
            message += `لطفاً برای بررسی سفارش و اعلام هزینه نهایی دوخت راهنمایی‌ام کنید.`;

            return message;
        }
    }

    /* =================== BALE INTEGRATION =================== */
    class BaleIntegration {
        static init(calculator, productData) {
            const baleBtn = document.querySelector('.btn-bale');
            if (!baleBtn) return;

            baleBtn.addEventListener('click', (e) => {
                e.preventDefault();

                const message = calculator.generateBaleMessage(productData);
                const encodedMessage = encodeURIComponent(message);
                const baleUrl = `${window.NarshaConfig.bale.getChatUrl()}?text=${encodedMessage}`;

                // Try to open Bale
                const newWindow = window.open(baleUrl, '_blank', 'noopener,noreferrer');

                // Fallback: copy to clipboard if blocked
                if (!newWindow || newWindow.closed || typeof newWindow.closed === 'undefined') {
                    BaleIntegration.copyToClipboard(message);
                    BaleIntegration.showToast('پیام برای بله کپی شد. لطفاً در اپلیکیشن بله پیست و ارسال کنید.');
                }
            });
        }

        static copyToClipboard(text) {
            if (navigator.clipboard && window.isSecureContext) {
                navigator.clipboard.writeText(text).catch(() => {
                    BaleIntegration.fallbackCopy(text);
                });
            } else {
                BaleIntegration.fallbackCopy(text);
            }
        }

        static fallbackCopy(text) {
            const textarea = document.createElement('textarea');
            textarea.value = text;
            textarea.style.position = 'fixed';
            textarea.style.opacity = '0';
            document.body.appendChild(textarea);
            textarea.select();
            try {
                document.execCommand('copy');
            } catch (err) {
                console.error('Copy failed:', err);
            }
            document.body.removeChild(textarea);
        }

        static showToast(message) {
            // Remove existing toast
            const existing = document.querySelector('.narsha-toast');
            if (existing) existing.remove();

            const toast = document.createElement('div');
            toast.className = 'narsha-toast';
            toast.style.cssText = `
                position: fixed;
                bottom: 100px;
                right: 24px;
                left: 24px;
                max-width: 400px;
                margin: 0 auto;
                padding: 16px 20px;
                background: var(--color-dark-card);
                border: 1px solid var(--color-gold);
                border-radius: var(--radius-md);
                color: var(--color-white);
                font-size: 0.9rem;
                line-height: 1.6;
                z-index: 10000;
                box-shadow: var(--shadow-lg);
                animation: slideUp 0.3s ease;
                direction: rtl;
                text-align: center;
            `;
            toast.textContent = message;

            document.body.appendChild(toast);

            setTimeout(() => {
                toast.style.animation = 'slideDown 0.3s ease forwards';
                setTimeout(() => toast.remove(), 300);
            }, 5000);
        }
    }

    // Add toast animations
    const style = document.createElement('style');
    style.textContent = `
        @keyframes slideUp {
            from { opacity: 0; transform: translateY(20px); }
            to { opacity: 1; transform: translateY(0); }
        }
        @keyframes slideDown {
            from { opacity: 1; transform: translateY(0); }
            to { opacity: 0; transform: translateY(20px); }
        }
    `;
    document.head.appendChild(style);

    /* =================== PRODUCT DATA =================== */
    // Product data will be defined in each product page HTML

    /* =================== INITIALIZATION =================== */
    function initCustomTailoring() {
        // Initialize price calculator on product pages
        const calculatorContainer = document.querySelector('[data-price-calculator]');
        if (calculatorContainer) {
            const basePrice = parseInt(calculatorContainer.dataset.basePrice, 10);
            const productName = calculatorContainer.dataset.productName;

            // Define options from data attributes
            const optionsConfig = JSON.parse(calculatorContainer.dataset.options || '[]');

            // Create calculator instance
            window.priceCalculator = new PriceCalculator('[data-price-calculator]', window.NarshaConfig);

            // Override options with predefined config
            if (optionsConfig.length > 0) {
                window.priceCalculator.options = optionsConfig.map((opt, i) => {
                    const el = (opt.id && document.getElementById(opt.id)) || calculatorContainer.querySelector('[data-name="' + opt.name + '"]') || calculatorContainer.querySelectorAll('.option-checkbox')[i] || null;
                    return {
                        id: opt.id || (el ? el.id : ''),
                        name: opt.name || (el ? (el.dataset.name || el.getAttribute('data-name')) : ''),
                        price: parseInt(opt.price, 10) || 0,
                        mandatory: (String(opt.mandatory).toLowerCase() === 'true' || opt.mandatory === true),
                        disabled: !!opt.disabled,
                        element: el
                    };
                });
            }
            window.priceCalculator.setupOptions(); // refresh from DOM as source of truth for elements
            // merge config prices/mandatory back onto DOM-matched options
            if (optionsConfig.length > 0) {
                window.priceCalculator.options = window.priceCalculator.options.map((o, i) => {
                    const cfg = optionsConfig.find(c => c.id === o.id || c.name === o.name) || optionsConfig[i] || null;
                    if (!cfg) return o;
                    return Object.assign({}, o, {
                        name: cfg.name || o.name,
                        price: parseInt(cfg.price, 10) || 0,
                        mandatory: (String(cfg.mandatory).toLowerCase() === 'true' || cfg.mandatory === true)
                    });
                });
            }
            window.priceCalculator._bound = false;
            window.priceCalculator.bindEvents();
            window.priceCalculator.updateTotal();

            // Initialize Bale button
            const productData = {
                name: productName,
                basePrice: basePrice,
                url: window.location.href
            };
            BaleIntegration.init(window.priceCalculator, productData);
        }

        // Initialize category page Bale buttons (generic consultation)
        const categoryBaleBtns = document.querySelectorAll('.category-bale-btn');
        categoryBaleBtns.forEach(btn => {
            btn.addEventListener('click', (e) => {
                e.preventDefault();
                const category = btn.dataset.category || 'دوخت سفارشی';
                const message = `سلام، برای ${category} درخواست مشاوره و استعلام قیمت دارم.\n\nلطفاً مدل‌ها، هزینه‌ها و شرایط را راهنمایی کنید.\n\nلینک: ${window.location.href}`;
                const encodedMessage = encodeURIComponent(message);
                const baleUrl = `${window.NarshaConfig.bale.getChatUrl()}?text=${encodedMessage}`;

                const newWindow = window.open(baleUrl, '_blank', 'noopener,noreferrer');
                if (!newWindow || newWindow.closed || typeof newWindow.closed === 'undefined') {
                    BaleIntegration.copyToClipboard(message);
                    BaleIntegration.showToast('پیام برای بله کپی شد. لطفاً در اپلیکیشن بله پیست و ارسال کنید.');
                }
            });
        });
    }

    // Run on DOM ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initCustomTailoring);
    } else {
        initCustomTailoring();
    }

    // Export for use in other scripts
    window.NarshaCustomTailoring = {
        PriceCalculator,
        BaleIntegration,
        config: window.NarshaConfig
    };
})();