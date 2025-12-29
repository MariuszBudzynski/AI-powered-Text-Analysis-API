
# AI-powered Text Assistant API

## Overview
AI-powered Text Assistant API is a backend-only .NET 8 project that exposes a REST API for interacting with a local Large Language Model (LLM). The application integrates with Ollama, enabling AI capabilities without any paid cloud services.

The API currently supports AI question answering, and the architecture allows easy expansion into summarization, sentiment analysis, translation, and more.

## Project Goals
- Integrate local AI models with .NET  
- Avoid paid APIs — 100% free
- Provide clean, extensible architecture  
- Serve as a professional portfolio project  
- Demonstrate production-ready API patterns  

## Current AI Capability
Ask Questions (Q&A)

Send any question and the local LLM returns a helpful answer.

## Architecture
/src  
 ├── Api → Minimal API endpoints  
 ├── Application → DTOs, interfaces, business logic  
 ├── UnitTests → Tests  

## Technology Stack
- .NET 8  
- ASP.NET Core Minimal API  
- Ollama (local LLM runtime)  
- HttpClient  
- Swagger / OpenAPI  
- Dependency Injection  
- JSON REST API
- XUnit  

## AI Model
- Default model: llama3 (or any other installed in Ollama)  
- Runs locally  
- No API keys required  

## Example Endpoint
POST /api/text/ask

Request:
{
  "text": "What is dependency injection in .NET?"
}

Response:
{
  "answer": "Dependency injection is a design pattern..."
}

## Getting Started
Prerequisites:
- .NET 8 SDK  
- Ollama installed  

Setup:
1. Download a model:
   ollama pull llama3

2. Run the API:
   dotnet run

Swagger UI:
https://localhost:{port}/swagger

## Why This Project?
This project demonstrates:
- Practical AI integration in backend systems  
- Clean, maintainable .NET architecture  
- Real-world API design  
- A strong portfolio example for backend + AI skills  
