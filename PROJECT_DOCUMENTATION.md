# Syella Pinheiro Photography Portfolio - Project Documentation

## Table of Contents
1. [Executive Summary](#executive-summary)
2. [Tech Stack](#tech-stack)
3. [Architecture & Design Patterns](#architecture--design-patterns)
4. [Project Structure](#project-structure)
5. [Frontend Components](#frontend-components)
6. [Backend Services](#backend-services)
7. [Data Models](#data-models)
8. [Styling & Design System](#styling--design-system)
9. [Development Setup & Workflow](#development-setup--workflow)
10. [API Reference](#api-reference)
11. [Deployment & Hosting](#deployment--hosting)
12. [Telegram Bot Integration](#telegram-bot-integration)
13. [Contributing Guidelines](#contributing-guidelines)
14. [Future Roadmap](#future-roadmap)

---

## Executive Summary

**Project Name:** Syella Pinheiro Photography Portfolio Website

**Description:** A modern, full-stack photographer portfolio website designed to showcase professional photography services integrated with Meta's Instagram Graph API and Telegram Bot automation for photoshoot management.

**Primary User:** Syella Pinheiro - Professional Photographer

**Key Features:**
- Dynamic Instagram photo gallery powered by real Instagram posts
- Modern, animated user interface with smooth transitions
- Responsive design optimized for all devices
- RESTful API backend with Swagger documentation
- Telegram Bot integration for automated photoshoot uploads
- Professional portfolio sections (About, Services, Gallery, Contact)

**Core Purpose:** Establish a professional online presence with automated content management, enabling photographers to showcase their work dynamically while streamlining photoshoot documentation and sharing through Telegram Bot integration.

---

## Tech Stack

### Frontend
| Technology | Version | Purpose |
|-----------|---------|---------|
| **React** | 19.2.0 | UI framework and component architecture |
| **TypeScript** | 5.9.3 | Type-safe JavaScript development |
| **Vite** | 7.3.1 | Build tool and development server |
| **Framer Motion** | 12.34.3 | Advanced animations and motion effects |
| **React Photo View** | 1.2.7 | Image gallery and lightbox functionality |
| **React Icons** | 5.5.0 | Icon library (FontAwesome 6) |
| **React Router DOM** | 7.13.0 | Client-side routing (prepared for future use) |

### Backend
| Technology | Version | Purpose |
|-----------|---------|---------|
| **.NET** | 10.0 | Web framework and runtime |
| **ASP.NET Core** | 10.0 | Web API framework |
| **C#** | Latest | Backend language |
| **Swashbuckle** | 10.1.4 | OpenAPI/Swagger documentation |

### Infrastructure & Deployment
| Technology | Purpose |
|-----------|---------|
| **Docker** | Containerization for development and deployment |
| **Node.js** | 24 Alpine - Runtime for frontend containerization |
| **.NET Runtime** | Backend execution environment |

### Development Tools
| Tool | Purpose |
|------|---------|
| **ESLint** | Code quality and consistency |
| **npm** | Frontend package management |
| **.NET CLI** | Backend package and project management |
| **Git** | Version control |

---

## Architecture & Design Patterns

### Overall Architecture
This project follows a **modern monorepo architecture** with clear separation between frontend and backend concerns.

```
┌─────────────────────────────────────────────────────────┐
│                    CLIENT BROWSER                       │
└────────────┬────────────────────────────────────────────┘
             │
┌────────────▼────────────────────────────────────────────┐
│         REACT FRONTEND (Port 3420)                      │
│  ├─ Components (Navbar, Hero, About, Gallery, Footer)  │
│  ├─ Services (InstagramService API calls)              │
│  ├─ Models (TypeScript type definitions)               │
│  └─ Styles (CSS Modules, Global Styles)                │
└────────────┬────────────────────────────────────────────┘
             │
             │ HTTP REST API
             │ (CORS Enabled)
             │
┌────────────▼────────────────────────────────────────────┐
│        .NET 10.0 BACKEND API                            │
│  ├─ InstagramController (API endpoints)                │
│  ├─ InstagramService (Business logic)                  │
│  ├─ InstagramClient (Meta API integration)             │
│  └─ Swagger/OpenAPI Documentation                      │
└─────────────────────────────────────────────────────────┘
             │
             │ HTTP Requests
             │
┌────────────▼────────────────────────────────────────────┐
│    META INSTAGRAM GRAPH API                             │
│    (Real Instagram posts & media)                       │
└─────────────────────────────────────────────────────────┘
```

### Design Patterns Used

#### 1. Component-Based Architecture (Frontend)
- **Pattern:** React functional components with hooks
- **Benefits:** Reusable, modular, easy to test and maintain
- **Examples:** Navbar, Hero, About, Gallery, Footer components

#### 2. Service Layer Pattern
- **Pattern:** API calls abstracted into service classes
- **Benefits:** Separation of concerns, easier testing, centralized API logic
- **Implementation:** `InstagramService.tsx` handles all Instagram API communication
- **Location:** `src/services/InstagramService.tsx`

#### 3. Clean Architecture (Backend)
- **Pattern:** Three-layer architecture (API → Service → Infrastructure)
- **Benefits:** Maintainability, testability, clear dependency flow
- **Layers:**
  - **API Layer:** Controllers and HTTP endpoints (`InstagramAPI`)
  - **Service Layer:** Business logic (`InstagramService`)
  - **Infrastructure Layer:** External API integration (`InstagramInfrastructure`)

#### 4. REST API with OpenAPI Documentation
- **Pattern:** Standard REST conventions with Swagger/OpenAPI
- **Benefits:** Consistent API design, self-documenting, easier integration
- **Tools:** Swashbuckle generates OpenAPI specs automatically
- **Access:** Swagger UI available at `/swagger` in development

#### 5. Dependency Injection
- **Frontend:** Service functions imported where needed
- **Backend:** .NET DI container manages service lifetimes
- **Benefits:** Loose coupling, testability, configuration management

#### 6. CSS Modules with Global Styles
- **Pattern:** Co-located component styles with global theme variables
- **Benefits:** Style isolation, maintainability, consistent theming
- **Structure:** Each component has associated `.css` file + global `global.css`

### Architectural Decisions & Rationale

| Decision | Rationale |
|----------|-----------|
| **Monorepo Structure** | Easier to manage related frontend/backend, shared documentation, unified version control |
| **React + TypeScript** | Type safety, large ecosystem, component reusability, excellent developer experience |
| **Vite over Create React App** | Faster development, better build performance, modern tooling |
| **.NET Backend** | Strong typing, performance, enterprise-grade frameworks, excellent API tooling |
| **Instagram Graph API** | Real-time portfolio updates, no manual content management, SEO benefits |
| **Docker Containerization** | Consistent development/production environments, easy deployment, scalability |
| **Framer Motion** | Professional animations, performance optimized, easy API for complex effects |

---

## Project Structure

### Root Directory
```
sy_website/
├── sy_website/                 # Frontend (React + TypeScript + Vite)
├── sy_api/                     # Backend (.NET 10.0)
├── README.md                   # Project overview and Telegram Bot info
└── PROJECT_DOCUMENTATION.md    # This file
```

### Frontend Structure: `/sy_website`
```
sy_website/
├── src/
│   ├── main.tsx                # React entry point
│   ├── App.tsx                 # Root component with page layout
│   ├── index.html              # HTML shell
│   │
│   ├── components/             # Feature-based components
│   │   ├── navbar/
│   │   │   ├── Navbar.tsx      # Navigation bar component
│   │   │   └── Navbar.css      # Navbar styles
│   │   ├── hero/
│   │   │   ├── Hero.tsx        # Hero/banner section
│   │   │   └── Hero.css        # Hero styles
│   │   ├── about/
│   │   │   ├── About.tsx       # About section with bio
│   │   │   └── About.css       # About styles
│   │   ├── gallery/
│   │   │   ├── Gallery.tsx     # Instagram photo gallery
│   │   │   └── Gallery.css     # Gallery styles
│   │   └── footer/
│   │       ├── Footer.tsx      # Footer with social links
│   │       └── Footer.css      # Footer styles
│   │
│   ├── services/
│   │   └── InstagramService.tsx  # API service for Instagram data
│   │
│   ├── models/
│   │   └── InstagramPost.tsx   # TypeScript types and interfaces
│   │
│   ├── styles/
│   │   └── global.css          # Global styles and CSS variables
│   │
│   └── assets/
│       ├── sponge.png          # Hero background image
│       └── [other images]      # Project images
│
├── public/                     # Static assets
├── package.json                # Dependencies and scripts
├── vite.config.ts              # Vite configuration
├── tsconfig.json               # TypeScript root config
├── tsconfig.app.json           # TypeScript app config
├── tsconfig.node.json          # TypeScript build config
├── eslint.config.js            # ESLint rules
├── Dockerfile                  # Development Docker image
├── index.html                  # HTML template
└── README.md                   # Frontend-specific documentation
```

### Backend Structure: `/sy_api`
```
sy_api/
├── sy_api/                     # Solution folder
│   ├── InstagramAPI/           # Web API project (entry point)
│   │   ├── Controllers/
│   │   │   └── InstagramController.cs    # REST endpoints
│   │   ├── Program.cs          # DI setup, middleware configuration
│   │   ├── appsettings.json    # Configuration
│   │   ├── appsettings.Development.json
│   │   └── InstagramAPI.csproj # Project file
│   │
│   ├── InstagramService/       # Business logic layer
│   │   ├── Services/
│   │   │   └── IInstagramService.cs      # Service interface
│   │   ├── Domain/Entities/
│   │   │   ├── InstagramPostMedia.cs
│   │   │   └── InstagramPostMediaComment.cs
│   │   └── InstagramService.csproj
│   │
│   ├── InstagramInfrastructure/ # External API integration
│   │   ├── InstagramClient.cs  # Meta API HTTP client
│   │   ├── InstagramOptions.cs # Configuration class
│   │   ├── InstagramRoutesConstant.cs
│   │   └── InstagramInfrastructure.csproj
│   │
│   └── sy_api.slnx             # Solution file
│
├── Dockerfile                  # Production Docker image
├── .gitignore
└── README.md
```

---

## Frontend Components

### 1. **Navbar** (`src/components/navbar/Navbar.tsx`)
**Purpose:** Navigation bar for the website
- **Features:**
  - Links to main sections (Sobre mim, Projetos, Contato)
  - Animated entrance effect with Framer Motion
  - Portuguese language support
  - Responsive design
- **Dependencies:** Framer Motion, React Icons
- **Styling:** CSS modules with dark theme

### 2. **Hero Section** (`src/components/hero/Hero.tsx`)
**Purpose:** Landing section with photographer branding
- **Features:**
  - Full-width background image (sponge.png)
  - Main title: "Syella Pinheiro"
  - Service subtitle: "Casamentos • Aniversários • Infantil" (Weddings, Birthdays, Children)
  - Animated fade-in effects
  - Call-to-action potential
- **Styling:** CSS modules with overlay effects

### 3. **About Section** (`src/components/about/About.tsx`)
**Purpose:** Photographer biography and professional background
- **Features:**
  - Professional bio text
  - Image stack layout showcasing portfolio
  - Static placeholder images (uses Unsplash URLs)
  - Professional presentation
- **Structure:** Text column + image column layout

### 4. **Gallery Section** (`src/components/gallery/Gallery.tsx`)
**Purpose:** Dynamic Instagram photo gallery
- **Features:**
  - Fetches real Instagram posts via API
  - Displays up to 12 high-quality photos
  - Filters out videos (only shows image media)
  - React Photo View lightbox for full-image viewing
  - Loading state with spinner animation
  - Error state with user feedback
  - Lazy loading for performance
  - Responsive grid layout
- **API Integration:** Calls `InstagramService.getInstagramPosts()`
- **Data Flow:**
  ```
  Gallery.tsx → InstagramService.tsx → Backend API → Instagram Graph API
  ```

### 5. **Footer** (`src/components/footer/Footer.tsx`)
**Purpose:** Footer with contact and social information
- **Features:**
  - Social media links (Instagram, WhatsApp, Email)
  - Copyright information
  - Contact information
  - Professional styling

---

## Backend Services

### 1. **InstagramAPI Project** (Entry Point)
**Location:** `sy_api/sy_api/InstagramAPI/`

**Purpose:** HTTP API server exposing Instagram functionality

**Key Files:**
- `Program.cs` - Application startup, dependency injection, middleware configuration
- `Controllers/InstagramController.cs` - REST endpoints
- `InstagramAPI.csproj` - Project configuration

**Configuration:**
- CORS enabled for frontend requests
- Swagger/OpenAPI auto-documentation
- Dependency injection container setup
- Hosted on configurable port (typically 5000)

### 2. **InstagramController**
**Location:** `sy_api/sy_api/InstagramAPI/Controllers/InstagramController.cs`

**Main Endpoints:**

| Method | Endpoint | Description | Returns |
|--------|----------|-------------|---------|
| `GET` | `/instagram/posts` | Fetch Instagram media posts | Array of InstagramPostMedia |

**Response Example:**
```json
{
  "data": [
    {
      "id": "123456789",
      "caption": "Beautiful wedding moment",
      "media_url": "https://...",
      "permalink": "https://instagram.com/...",
      "media_type": "IMAGE"
    }
  ]
}
```

### 3. **InstagramService** (Business Logic)
**Location:** `sy_api/sy_api/InstagramService/`

**Purpose:** Core business logic for Instagram operations

**Responsibilities:**
- Data transformation and filtering
- Business rule enforcement
- Service interface definition (`IInstagramService`)

**Key Types:**
- `InstagramPostMedia` - Media post entity
- `InstagramPostMediaComment` - Comment entity

### 4. **InstagramClient** (Infrastructure)
**Location:** `sy_api/sy_api/InstagramInfrastructure/InstagramClient.cs`

**Purpose:** HTTP client for Meta's Instagram Graph API

**Responsibilities:**
- Authenticate with Meta API using access tokens
- Make requests to Instagram Graph API
- Handle API responses and errors
- Rate limiting and retry logic

**Configuration Class:** `InstagramOptions`
- API base URL
- Access tokens
- API version
- Rate limits

**Configuration Routes:** `InstagramRoutesConstant.cs`

---

## Data Models

### Frontend TypeScript Models
**Location:** `src/models/InstagramPost.tsx`

```typescript
type InstagramPost = {
  id: string;                    // Unique post identifier
  caption?: string;              // Post description (optional)
  media_url: string;             // Direct image URL
  permalink: string;             // Instagram post URL
  media_type?: string;           // "IMAGE" | "VIDEO" | "CAROUSEL_ALBUM"
};
```

### Backend DTO Models

#### InstagramPostMedia
Represents a single Instagram media item
```
Properties:
- Id (string)
- Caption (string, nullable)
- MediaUrl (string)
- Permalink (string)
- MediaType (string)
```

#### InstagramPostMediaComment
Represents user comments on Instagram posts
```
Properties:
- Id (string)
- Text (string)
- Username (string)
- Timestamp (DateTime)
```

### Type Safety & Validation
- **Frontend:** TypeScript ensures compile-time type checking
- **Backend:** C# strong typing with validation attributes
- **API Contract:** OpenAPI/Swagger documents expected types

---

## Styling & Design System

### Color Scheme
**CSS Variables** (defined in `styles/global.css`):

| Variable | Value | Usage |
|----------|-------|-------|
| `--bg` | `#0b0b0b` | Background (dark navy) |
| `--text` | `#f5f5f5` | Primary text (off-white) |
| `--accent` | `#c9a96e` | Accent color (gold) |
| `--muted` | `#777` | Secondary text (gray) |

### Typography
**Fonts:**
- **Headers:** Playfair Display (serif, Google Fonts)
  - Letter spacing: 2px
  - Elegant, professional appearance
- **Body Text:** Inter (sans-serif, Google Fonts)
  - Clean, modern, excellent readability

### Visual Hierarchy
1. **Primary Text** (`--text`): Main content
2. **Accent Elements** (`--accent`): Important buttons, highlights
3. **Secondary Text** (`--muted`): Metadata, timestamps, hints
4. **Backgrounds** (`--bg`): Dark theme foundation

### Animation System
**Framework:** Framer Motion
- **Component Animations:** Fade-in, slide-in effects on mount
- **Interactive Animations:** Hover states, button feedback
- **Gallery Animations:** Image transitions, lightbox effects

### Responsive Design
- **Container:** Max-width 1400px centered
- **Breakpoints:** Fluid scaling based on viewport
- **Mobile First:** Base styles mobile-optimized, enhanced on larger screens
- **Images:** CSS object-fit for consistent display

---

## Development Setup & Workflow

### Prerequisites
- **Node.js** 20+ (for frontend)
- **.NET 10.0 SDK** (for backend)
- **Git** for version control
- **Docker** (optional, for containerized development)

### Frontend Development

#### Setup
```bash
cd sy_website
npm install
```

#### Development Server
```bash
npm run dev
```
- Starts Vite dev server on `http://localhost:5173` (or configured port)
- Hot Module Reload (HMR) enabled for instant updates
- Docker: `docker build -t sy-website . && docker run -p 3420:3420 sy-website`

#### Build
```bash
npm run build
```
- Produces optimized production bundle in `dist/`
- Tree-shaking and code splitting enabled
- Source maps generated (optional)

#### Preview Production Build
```bash
npm run preview
```
- Local preview of production bundle

#### Linting
```bash
npm run lint
```
- ESLint checks code quality
- Enforces React best practices

### Backend Development

#### Setup
```bash
cd sy_api/sy_api
dotnet restore
```

#### Development Server
```bash
dotnet run --project InstagramAPI/InstagramAPI.csproj
```
- Starts API on `http://localhost:5000`
- Swagger UI available at `/swagger`
- Hot reload enabled

#### Build
```bash
dotnet build
```

#### Testing
```bash
dotnet test
```

### Environment Variables

**Frontend (.env):**
```
VITE_API_URL=http://localhost:5000
```

**Backend (appsettings.Development.json):**
```json
{
  "Instagram": {
    "AccessToken": "your_meta_access_token",
    "UserId": "instagram_user_id",
    "BaseUrl": "https://graph.instagram.com/v19.0"
  }
}
```

### Running Both Services
**Option 1: Sequential**
```bash
# Terminal 1
cd sy_website && npm run dev

# Terminal 2
cd sy_api/sy_api && dotnet run --project InstagramAPI/InstagramAPI.csproj
```

**Option 2: Docker Compose** (if configured)
```bash
docker-compose up
```

### Development Workflow Best Practices

1. **Feature Branches**
   ```bash
   git checkout -b feature/component-name
   ```

2. **Component Creation** (Frontend)
   - Create folder in `src/components/`
   - Create `.tsx` file with component
   - Create `.css` file with styles
   - Export from component index

3. **API Endpoint Addition** (Backend)
   - Add endpoint in Controller
   - Add service logic
   - Update Swagger docs
   - Test with Swagger UI

4. **Testing During Development**
   - Frontend: Check component visually in dev server
   - Backend: Use Swagger UI to test endpoints
   - Integration: Verify API calls work end-to-end

---

## API Reference

### Base URL
- **Development:** `http://localhost:5000`
- **Production:** Configured per deployment

### Authentication
Current implementation: **No authentication required** (public portfolio)
- Instagram Graph API authentication handled server-side
- Frontend requests require CORS headers only

### Endpoints

#### Get Instagram Posts
```
GET /instagram/posts
```

**Description:** Retrieve Instagram media posts for the photographer's account

**Response Parameters:**
```json
{
  "data": [
    {
      "id": "string",
      "caption": "string (optional)",
      "media_url": "string (URL to image)",
      "permalink": "string (URL to Instagram post)",
      "media_type": "string (IMAGE|VIDEO|CAROUSEL_ALBUM)"
    }
  ]
}
```

**Status Codes:**
- `200 OK` - Successfully retrieved posts
- `400 Bad Request` - Invalid parameters
- `401 Unauthorized` - Invalid Instagram credentials
- `500 Internal Server Error` - Server error

**Example Request:**
```bash
curl http://localhost:5000/instagram/posts
```

**Example Response:**
```json
{
  "data": [
    {
      "id": "123456789",
      "caption": "Beautiful wedding ceremony",
      "media_url": "https://example.com/photo.jpg",
      "permalink": "https://instagram.com/p/ABCD1234/",
      "media_type": "IMAGE"
    },
    {
      "id": "987654321",
      "caption": "Birthday celebration",
      "media_url": "https://example.com/photo2.jpg",
      "permalink": "https://instagram.com/p/WXYZ9876/",
      "media_type": "IMAGE"
    }
  ]
}
```

**Response Headers:**
```
Content-Type: application/json
Access-Control-Allow-Origin: * (CORS)
```

### OpenAPI/Swagger Documentation
- **URL:** `http://localhost:5000/swagger`
- **Format:** OpenAPI 3.0
- **Auto-generated** by Swashbuckle
- **Use Swagger UI** to test endpoints directly

### Rate Limiting
- Instagram Graph API rate limits apply
- Currently handled by Meta API (adjust as needed)
- Monitor X-Rate-Limit headers in responses

### Error Handling
```json
{
  "error": {
    "message": "Error description",
    "code": "ERROR_CODE"
  }
}
```

---

## Deployment & Hosting

### Docker Containerization

#### Frontend Docker Image
**Location:** `sy_website/Dockerfile`

**Base Image:** Node 24 Alpine (lightweight, ~50MB)

**Build:**
```bash
docker build -t sy-website-frontend:latest .
```

**Run:**
```bash
docker run -p 3420:3420 sy-website-frontend:latest
```

**Volume Mounting (Development):**
```bash
docker run -p 3420:3420 -v $(pwd)/src:/app/src sy-website-frontend:latest
```

#### Backend Docker Image
**Location:** `sy_api/Dockerfile`

**Base Image:** .NET 10.0 runtime

**Multi-stage Build:**
- Stage 1: Build application
- Stage 2: Runtime image (smaller, optimized)

**Build:**
```bash
docker build -t sy-website-api:latest .
```

**Run:**
```bash
docker run -p 5000:5000 \
  -e Instagram__AccessToken=your_token \
  -e Instagram__UserId=your_id \
  sy-website-api:latest
```

### Environment Configuration

**Production Environment Variables:**
```
# Frontend
VITE_API_URL=https://api.example.com

# Backend
Instagram__AccessToken=<meta_access_token>
Instagram__UserId=<instagram_user_id>
Instagram__BaseUrl=https://graph.instagram.com/v19.0
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://+:5000
```

### Deployment Scenarios

#### Scenario 1: Cloud Hosting (AWS, Azure, GCP)
1. Push Docker images to container registry
2. Deploy frontend to static host or container service
3. Deploy backend to serverless or container service
4. Configure CDN for static assets
5. Set up SSL/TLS certificates
6. Configure environment variables via platform

#### Scenario 2: Traditional VPS
1. SSH into server
2. Install Docker and Docker Compose
3. Clone repository
4. Configure `.env` files
5. Run `docker-compose up -d`
6. Configure Nginx reverse proxy
7. Set up SSL with Let's Encrypt

#### Scenario 3: Managed PaaS
- **Frontend:** Deploy to Vercel, Netlify, or similar
- **Backend:** Deploy to Railway, Heroku, or cloud app services
- **Database:** Use managed services if needed (future enhancement)

### Performance Optimization
- Frontend: Vite build minification, code splitting
- Backend: ASP.NET Core optimizations, response caching
- Images: Lazy loading, responsive images via CDN
- Caching: Browser caching, API response caching

### Monitoring & Logging
- Application Insights (Azure) or equivalent
- Error tracking service (Sentry, Rollbar)
- Performance monitoring (APM)
- Log aggregation (ELK, Splunk)

### Backup Strategy
- Database backups (if added)
- Configuration backups
- GitHub as source backup
- Media storage backups (Instagram acts as primary source)

---

## Telegram Bot Integration

### Overview
The project includes a **Telegram Bot** integration for automated photoshoot management and uploads. This feature streamlines the workflow for photographers to share photoshoots directly from their mobile devices to the website.

### Purpose & Benefits
- **Streamlined Sharing:** Upload photos directly via Telegram
- **Real-time Updates:** Website gallery updates automatically
- **Mobile Convenience:** Photographer can manage content from mobile
- **Organization:** Automatic tagging and categorization
- **Backup:** Telegram serves as secondary backup mechanism

### Current Status
- **In Development** (mentioned in recent commits: "Update README to include Telegram Bot integration")
- Basic structure being established
- Integration points being defined

### Planned Architecture
```
Photographer → Telegram Bot → Backend Service → Instagram API
                                     ↓
                          Database/Media Storage
                                     ↓
                          Website Gallery
```

### Implementation Details (Future)

#### Key Components
1. **Telegram Bot Handler**
   - Listen for messages from authorized photographer
   - Process media uploads (photos, videos)
   - Validate file types and sizes

2. **Backend Integration**
   - New endpoint: `/telegram/webhook` for Telegram callbacks
   - New service: `TelegramBotService.cs`
   - Authentication via Telegram token

3. **Media Processing**
   - Save photos to storage
   - Upload to Instagram Graph API
   - Update website gallery
   - Handle batch uploads

#### Expected Workflow
1. Photographer sends photos to Telegram Bot
2. Bot receives and validates media
3. Backend processes and stores photos
4. Photos pushed to Instagram (if desired)
5. Website gallery auto-updates
6. Confirmation sent back to Telegram

### Configuration (Future)
```json
{
  "Telegram": {
    "BotToken": "your_bot_token",
    "AuthorizedUserId": "photographer_user_id",
    "FileStoragePath": "/storage/media",
    "WebhookSecret": "secure_webhook_secret"
  }
}
```

### Security Considerations
- Validate Telegram token on webhook
- Restrict bot access to authorized users only
- Sanitize file uploads
- Validate file types and sizes
- Rate limiting on uploads

### Future Enhancements
- Batch upload with progress tracking
- Caption and metadata from Telegram messages
- Photo organizing by event/date
- Archive management
- Hashtag extraction for Instagram

---

## Contributing Guidelines

### Development Philosophy
This project values:
- **Code Quality:** Clean, readable, well-structured code
- **Type Safety:** Leveraging TypeScript and C# strong typing
- **Component Reusability:** Creating composable, single-responsibility components
- **Performance:** Optimizing bundle size and runtime performance
- **Documentation:** Clear code and inline comments where logic is non-obvious

### Code Standards

#### Frontend (React + TypeScript)
**File Organization:**
```
ComponentName/
├── ComponentName.tsx       # Component logic
├── ComponentName.css       # Component styles
└── [Optional] hooks/       # Custom hooks if needed
```

**Component Template:**
```typescript
import { FC } from 'react';
import styles from './ComponentName.css';

interface ComponentNameProps {
  title: string;
  onClick?: () => void;
}

const ComponentName: FC<ComponentNameProps> = ({ title, onClick }) => {
  return (
    <div className={styles.container}>
      {title}
      {onClick && <button onClick={onClick}>Action</button>}
    </div>
  );
};

export default ComponentName;
```

**Naming Conventions:**
- Components: PascalCase (Header, Gallery, UserProfile)
- Functions: camelCase (getInstagramPosts, handleClick)
- Constants: UPPER_SNAKE_CASE (API_BASE_URL, MAX_ITEMS)
- CSS classes: kebab-case (hero-section, gallery-container)

**Style Guidelines:**
- Use CSS modules for component styles
- Define colors using global CSS variables
- Keep styles scoped to component
- Avoid inline styles

#### Backend (C#)
**Naming Conventions:**
- Classes: PascalCase (InstagramService, UserController)
- Methods: PascalCase (GetInstagramPosts, ProcessMedia)
- Properties: PascalCase (Id, MediaUrl)
- Private fields: camelCase (_logger, _repository)
- Constants: PascalCase (MaxRetries, DefaultTimeout)

**Code Structure:**
```csharp
namespace InstagramAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstagramController : ControllerBase
{
    private readonly IInstagramService _service;

    public InstagramController(IInstagramService service)
    {
        _service = service;
    }

    [HttpGet("posts")]
    public async Task<ActionResult<IEnumerable<PostMediaDto>>> GetPosts()
    {
        var posts = await _service.GetInstagramPostsAsync();
        return Ok(posts);
    }
}
```

### Git Workflow

#### Branch Naming
- Feature: `feature/feature-name`
- Bug fix: `fix/bug-description`
- Documentation: `docs/doc-name`
- Example: `feature/telegram-bot-integration`

#### Commit Messages
```
[Type] Brief description (50 chars max)

Detailed explanation if needed (wrap at 72 chars)
- Bullet points for multiple changes
- Reference issues: Closes #123
```

**Types:**
- `feat:` New feature
- `fix:` Bug fix
- `docs:` Documentation
- `style:` Code style changes
- `refactor:` Code restructuring
- `perf:` Performance improvements
- `test:` Test addition/modification

#### Pull Request Process
1. Create feature branch from `main`
2. Make changes and commit with descriptive messages
3. Push branch and create Pull Request
4. Add PR description explaining changes
5. Request code review
6. Address feedback
7. Merge after approval
8. Delete feature branch

### Testing Expectations

#### Frontend
- Component rendering tests (Jest, React Testing Library)
- Service function tests (mock API calls)
- Integration tests for critical workflows

#### Backend
- Unit tests for business logic
- Integration tests for API endpoints
- Mock external API calls

**Test Command:**
```bash
# Frontend
npm run test

# Backend
dotnet test
```

### Code Review Checklist
- [ ] Code follows style guidelines
- [ ] No console errors or warnings
- [ ] TypeScript/C# type correctness
- [ ] Logic is readable and maintainable
- [ ] Performance implications considered
- [ ] Tests added/updated
- [ ] Documentation updated
- [ ] No breaking changes to existing functionality

### Adding New Features

#### Frontend Feature Example
1. Create component folder: `src/components/FeatureName/`
2. Create `FeatureName.tsx` and `FeatureName.css`
3. Add TypeScript interfaces if needed
4. Create service function(s) if API calls needed
5. Integrate into parent component (usually `App.tsx`)
6. Update styles with global variables
7. Test in dev server
8. Commit and create PR

#### Backend Endpoint Example
1. Add method to controller
2. Create service method with interface
3. Add DTOs if needed
4. Implement service logic
5. Test with Swagger UI
6. Update API documentation
7. Test frontend integration
8. Commit and create PR

### Performance Considerations
- Lazy load components when possible
- Use React.memo for expensive components
- Optimize images and media
- Monitor bundle size
- Backend: Use async/await, connection pooling
- Caching: Implement where appropriate

### Documentation Updates
- Update README if public API changes
- Add JSDoc/XML comments for public APIs
- Document new environment variables
- Update this file if architecture changes
- Keep comments explaining "why", not "what"

---

## Future Roadmap

### Phase 1: Core Enhancement (Next)
- [ ] **Complete Telegram Bot Integration**
  - Full upload workflow
  - Batch processing
  - Progress notifications

- [ ] **Database Addition**
  - PostgreSQL for persistent storage
  - User management (future expansion)
  - Analytics tracking

- [ ] **Advanced Gallery Features**
  - Event-based filtering
  - Photo collections/albums
  - Video support
  - Carousel handling

### Phase 2: User Experience
- [ ] **Authentication & Admin Panel**
  - Photographer login
  - Content management interface
  - Analytics dashboard

- [ ] **Search & Filter**
  - Gallery search by caption/tags
  - Date range filtering
  - Category filtering

- [ ] **Responsive Improvements**
  - Mobile-optimized interface
  - Touch gestures for gallery
  - Progressive Web App (PWA)

### Phase 3: Marketing & SEO
- [ ] **SEO Optimization**
  - Metadata generation
  - Open Graph tags
  - Sitemap generation

- [ ] **Performance**
  - Image optimization
  - caching strategies
  - CDN integration

- [ ] **Analytics**
  - Visitor tracking
  - Popular galleries
  - Client engagement metrics

### Phase 4: Expansion
- [ ] **Multi-Photographer Support**
  - Portfolio management for multiple photographers
  - Shared gallery features

- [ ] **E-Commerce Integration**
  - Photo ordering/purchasing
  - Print fulfillment

- [ ] **Booking System**
  - Event scheduling
  - Appointment management
  - Payment integration

### Known Limitations & Future Improvements
1. **Current:** Manual environment variable management
   - **Future:** Configuration UI in admin panel

2. **Current:** Instagram Graph API dependency
   - **Future:** Multi-platform support (Facebook, Pinterest)

3. **Current:** No commenting system
   - **Future:** Client reviews/testimonials

4. **Current:** Static about/service sections
   - **Future:** CMS for content management

5. **Current:** Basic error handling
   - **Future:** Comprehensive error tracking and reporting

### Technical Debt & Refactoring
- [ ] Separate API client into reusable library
- [ ] Implement proper error boundaries (React)
- [ ] Add comprehensive logging (frontend & backend)
- [ ] Create shared type library for frontend/backend DTOs
- [ ] Extract configuration management into separate service

---

## Quick Reference

### Essential Commands

**Frontend:**
```bash
npm install        # Install dependencies
npm run dev        # Start dev server
npm run build      # Production build
npm run preview    # Preview build
npm run lint       # Check code quality
```

**Backend:**
```bash
dotnet restore     # Install packages
dotnet run         # Start dev server
dotnet build       # Build project
dotnet test        # Run tests
```

### Important URLs (Development)
- Frontend: `http://localhost:5173` (or configured port)
- Backend API: `http://localhost:5000`
- Swagger UI: `http://localhost:5000/swagger`

### File Paths Quick Access
- Frontend components: `sy_website/src/components/`
- API services: `sy_website/src/services/`
- Backend controllers: `sy_api/sy_api/InstagramAPI/Controllers/`
- Backend services: `sy_api/sy_api/InstagramService/Services/`
- Global styles: `sy_website/src/styles/global.css`
- Configuration: `sy_api/sy_api/InstagramAPI/appsettings.json`

### Support & Questions
- Check existing code for examples
- Review Git history for implementation patterns
- Refer to component comments for non-obvious logic
- Check API documentation in Swagger UI
- Review this file for architecture understanding

---

**Last Updated:** March 2025
**Created By:** Software Architecture Team
**Version:** 1.0
