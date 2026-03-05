# BattleGame Frontend

A Next.js (App Router, TypeScript) frontend that displays the **Player Asset Report** by calling the Azure Functions backend.

## Prerequisites

- Node.js 18+
- The `BattleGame.Functions` Azure Functions project running locally on port **7071**

## Getting started

```bash
npm install
npm run dev
```

Open [http://localhost:3000](http://localhost:3000).

## Environment

| Variable               | Default                     | Description                         |
| ---------------------- | --------------------------- | ----------------------------------- |
| `NEXT_PUBLIC_API_BASE` | `http://localhost:7071/api` | Base URL of the Azure Functions API |

Create a `.env.local` file to override:

```env
NEXT_PUBLIC_API_BASE=http://localhost:7071/api
```
