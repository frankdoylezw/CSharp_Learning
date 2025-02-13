# CSharp_Learning
Learning plan and progress checker for C# development

## Changes Made to Generate Trace Files

The following changes were made to ensure trace files are generated when tests run:

1. **Playwright Tests Configuration**:
   - The `Context.Tracing.StartAsync` method is called in the `Setup` method with appropriate options.
   - The `Context.Tracing.StopAsync` method is called in the `TearDown` method with appropriate options.
   - Trace files are saved in the `playwright-traces` directory.

2. **GitHub Actions Workflow**:
   - A step was added to upload trace files to the `playwright-traces` directory.
   - The `Run Playwright Tests` step includes the `--trace on` option.

3. **Verification**:
   - The `tests/bin/Debug/net8.0/playwright-traces` directory contains trace files after tests run.
   - The GitHub Actions workflow in `.github/workflows/playwright.yml` uploads trace files.

