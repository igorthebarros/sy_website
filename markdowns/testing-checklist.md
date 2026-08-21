# Testing Checklist

> Last updated: 2026-08-21
> Scope: Current project testability baseline and repeatable PR validation checklist.

## Purpose

This file is the single source of truth for testing status and routines.
Update it every time a PR is merged so the team always knows what is tested, what is partially testable, and what is still blocked.

## Current Baseline (2026-08-21)

### Verified by execution

- Backend unit tests: PASS (`dotnet test sy_api/Tests/Tests.csproj`) with 11 passed tests.
- Frontend production build: PASS (`npm run build` in `sy_website`).
- API runtime smoke (Development): PASS (`GET /swagger/v1/swagger.json` returned 200).
- Telegram webhook smoke: PASS (`POST /api/telegram/webhook` with a minimal `update_id` payload returned 200).

### Testable now

- Backend unit tests in `sy_api/Tests`.
- API startup and route smoke tests.
- Frontend build validation.
- Frontend-to-backend contract smoke for `GET /instagram/posts`.

### Partially testable (depends on external setup)

- Instagram live integration (valid account/token required).
- Telegram webhook integration (ngrok + real Telegram updates required).

### Not testable yet (missing implementation)

- Album persistence and album read APIs (`/api/albums`, `/api/albums/{id}/photos`).
- Telegram albums UI on frontend.
- Full CI/CD test gates.

## Standard PR Test Checklist

Run this checklist before and after merging PRs.

### Backend

- [ ] `dotnet build sy_api/sy_api.slnx`
- [ ] `dotnet test sy_api/Tests/Tests.csproj`
- [ ] Start API locally and verify Swagger JSON returns 200.

### Frontend

- [ ] `npm run build` in `sy_website`
- [ ] `npm run dev` smoke check for runtime UI loading
- [ ] Verify key pages/components render without console errors

### Contract / Integration

- [ ] Verify frontend API base URL points to the intended backend
- [ ] Verify `GET /instagram/posts` works end-to-end
- [ ] Verify Telegram webhook endpoint accepts payload without 500

### External Integrations (when relevant)

- [ ] Instagram token/account live read test
- [ ] Telegram webhook end-to-end via ngrok

## Merge Update Log

Add one line per merged PR:

- 2026-08-06 - PR #2 merged to `develop`; Wave 1 review fixes integrated; baseline tests re-run (backend tests, frontend build, API smoke).
