const { defineConfig } = require('@playwright/test');

module.exports = defineConfig({
    reporter: [['html', { outputFolder: 'playwright-report' }]],
    use: {
        trace: 'on-first-retry',
        screenshot: 'only-on-failure',
        video: 'retain-on-failure',
    },
});