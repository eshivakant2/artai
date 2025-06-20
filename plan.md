# Equity Research Platform - High Level Plan

## 1. Project Setup
- Initialize Git repository and solution structure.
- Create `readme.md` and `worklog.md` for documentation and change tracking.
- Set up backend (C# .NET Web API, latest LTS) and frontend (Angular, latest LTS) projects.
- All components will be dockerized for local development and deployment.

## 2. Authentication & Authorization
- Integrate Microsoft Entra ID (Azure AD) for authentication.
- Implement role-based access control (RBAC) for analysts, admins, and other roles.
- Secure API endpoints and frontend routes.

## 3. Research Template Management
- Backend: CRUD APIs for research templates (text with placeholders).
- Frontend: UI for admins to create, edit, and manage templates.
- Store templates in PostgreSQL database (dockerized).

## 4. Security Lookup Integration
- Integrate OpenFigi API for security lookup.
- Allow associating multiple securities with a research.
- Frontend: UI for searching and selecting securities.

## 5. Research Creation Workflow
- Analyst initiates 'create new research'.
- Select template and associate securities.
- Start research session.

## 6. AI-Assisted Research Authoring
- Integrate Semantic Kernel and Kernel Memory for AI chat and context management.
- Use PostgreSQL with pgvector extension for Kernel Memory vector storage (dockerized).
- AI agent guides analyst through template, filling placeholders interactively.
- Support uploading images, PDFs, PPTs; AI extracts and incorporates relevant content.
- Real-time update of research document as AI/analyst interact.

## 7. Research Management
- Backend: CRUD APIs for research documents.
- Frontend: Dashboard for listing, viewing, editing, and managing research.
- Versioning and audit trail for research documents.
- All research data stored in PostgreSQL.

## 8. Observability & Logging
- Implement structured logging (Serilog or similar) in backend.
- Integrate Application Insights or similar for monitoring.
- Frontend error tracking (e.g., Sentry).

## 9. Testing
- Unit and integration tests for backend (xUnit, Moq, etc.).
- Unit and e2e tests for frontend (Jest, Cypress).

## 10. Deployment & CI/CD
- Set up CI/CD pipelines (GitHub Actions/Azure DevOps).
- Ensure all services (backend, frontend, PostgreSQL with pgvector) are dockerized and orchestrated (e.g., with docker-compose).
- Prepare for cloud deployment (Azure preferred).

## 11. Documentation
- Maintain `readme.md` for setup and onboarding.
- Update `worklog.md` with every change.
- API documentation (Swagger/OpenAPI).

---

This plan will be refined and expanded as requirements evolve. 