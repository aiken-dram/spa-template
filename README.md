# SPA Template

## Sample application

Sample application is hosted on https://aikendrum.bsite.net/

Use `admin` username and `admin` password for logging in

## Project structure:

- frontend
  - frontend vue + vuetify application
- database
  - development project for various databases
- backend
  - dotnet core web api based on clean architecture
- docs
  - vuepress static site for documentation from .md files

see `readme.md` in each subdirectory for additional information

## Required software

- Node.js
- Dotnet core SDK
- Visual Studio Code
- IBM Data Studio 4.1.3 for IBM database project

## Visual Studio Code

#### Extensions

- C#

- Vetur

- ESLint

- Prettier

## Initializing

In `frontend` run `npm i` to restore npm packages for frontend application

In `backend` run `dotnet restore` to restore nuget packages for backend application

In `docs` run `npm i` to restore npm packages for vuepress documentation

## Running in development

In `frontend` run `npm run serve`

In `backend/webapi` run `dotnet watch run`

Frontend application will be available from `http://localhost:5000/app/`

Swagger will be available from `http://localhost:5000/swagger/`

## Publishing

In `frontend` run `npm run build`

In `backend/webapi` run `dotnet publish`

In `docs` run `npm run build`

Production build will be available in `{project root}/publish` and contain subdirectories `backend`, `frontend` and `doc`

Deployment in production expects frontend application to be available from `/app/` route and documentation from `/doc/` route
