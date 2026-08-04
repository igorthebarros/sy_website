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
dotnet user-secrets set "Telegram:PhotoStoragePath" "C:\\photo-storage"
```

### 3) Verify no secrets are tracked

The repository `appsettings.json` now keeps secret keys with empty values.
Do not commit live credentials to source control.
