# playwright-two-traces-per-test

Preconditions:
- active Microsoft Playwright Testing service on Azure

Steps:
- build solution
- execute in Powershell `$env:PLAYWRIGHT_SERVICE_URL="wss://your-workspace-address/browsers"`
- run tests by command: `dotnet test --logger "microsoft-playwright-testing" -- NUnit.NumberOfTestWorkers=10`

- verify both traces are accessible in report
