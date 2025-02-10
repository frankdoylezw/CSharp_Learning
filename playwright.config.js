const { defineConfig } = require('@playwright/test');

module.exports = defineConfig({
    use: {
        headless: true, // Run in headless mode (set to false for UI mode)
        viewport: { width: 1280, height: 720 }, // Set the default browser window size
        ignoreHTTPSErrors: true, // Ignore HTTPS errors
        screenshot: 'on', // Take a screenshot on every test step
        video: 'on', // Record a video for every test run
        trace: 'on', // Always capture Playwright trace
    },
    testDir: './tests', // Set test directory
    timeout: 30000, // 30s timeout per test
    retries: 2, // Retry failed tests twice
    reporter: [['list'], ['json', { outputFile: 'test-results.json' }]], // Save test results
});