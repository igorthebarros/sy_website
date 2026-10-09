# sy_website

Professional photographer website integrated with Meta's Instagram Graph API
and a Telegram bot for photoshoot uploads.

## Wave 0 Security Baseline

Before running the API, rotate previously exposed tokens and configure secrets locally.

### 1) Rotate compromised credentials

- Revoke and recreate Telegram bot token using BotFather.
- Regenerate Instagram access token in Meta developer tools.

### 2) Configure API secrets with user-secrets (development)

Run these commands from `sy_api/API`:

```powershell
dotnet user-secrets set "Instagram:AccountId" "<your-instagram-account-id>"
dotnet user-secrets set "Instagram:Token" "<your-instagram-access-token>"
dotnet user-secrets set "Telegram:BotToken" "<your-telegram-bot-token>"
dotnet user-secrets set "Telegram:AllowedUserId" "<your-telegram-user-id>"
```

### 3) Verify no secrets are tracked

The repository `appsettings.json` now keeps secret keys with empty values.
Do not commit live credentials to source control.

### Local Telegram photo folder (Windows)

Run this block from the **repository root** to keep uploaded photos outside the API build output:

```powershell
$photoStoragePath = Join-Path $env:LOCALAPPDATA "SyPortfolio\photos"
New-Item -ItemType Directory -Path $photoStoragePath -Force | Out-Null
dotnet user-secrets set "Telegram:PhotoStoragePath" "$photoStoragePath" --project ".\sy_api\API\API.csproj"
Write-Host "Photo folder: $photoStoragePath"
```

The base folder is `%LOCALAPPDATA%\SyPortfolio\photos`; an album selected with `/shoot test` is saved under its `test` subfolder. This User Secrets setting applies locally in Development and is not distributed through Git.

After changing the setting, stop the API with `Ctrl+C` and restart it from the repository root:

```powershell
dotnet run --no-build --project ".\sy_api\API\API.csproj" --launch-profile http
```

Use `--no-build` only when the API has already been built and no source code has changed. Keep ngrok running. The active album is held in memory, so send `/shoot test` again after restarting, wait for `Album set to: test`, and send one regular photo. After `Photo saved.`, check the file:

```powershell
explorer (Join-Path $env:LOCALAPPDATA "SyPortfolio\photos\test")
```

If `Telegram:PhotoStoragePath` is empty or unset, storage falls back to `AppContext.BaseDirectory/photo-storage` (typically `sy_api\API\bin\Debug\net10.0\photo-storage` during local Debug runs). Changing the setting does not move existing uploads.

See [Telegram Bot Integration](PROJECT_DOCUMENTATION.md#telegram-bot-integration) for configuration details and [the testing checklist](markdowns/testing-checklist.md#manual-local-integration-validation--2026-10-08) for the verified local test.
