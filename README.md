
# AI-powered Text Assistant API

## Overview
AI-powered Text Assistant API is a backend-only .NET 8 project that exposes a REST API for interacting with a local Large Language Model (LLM). The application integrates with Ollama, enabling fully offline AI capabilities without any paid cloud services.

The API currently supports AI question answering, and the architecture allows easy expansion into summarization, sentiment analysis, translation, and more.

## Project Goals
- Integrate local AI models with .NET  
- Avoid paid APIs — 100% free and offline  
- Provide clean, extensible architecture  
- Serve as a professional portfolio project  
- Demonstrate production-ready API patterns  

## Current AI Capability
Ask Questions (Q&A)

Send any question or prompt, and the local LLM returns a helpful answer.

Use cases:
- Explain concepts  
- Answer general knowledge questions  
- Provide definitions  
- Assist with problem-solving  

## Architecture
/src  
 ├── Api → Minimal API endpoints  
 ├── Application → DTOs, interfaces, business logic  
 ├── UnitTests → Tests  

Key principles:
- Separation of concerns  
- AI logic hidden behind interfaces  
- Replaceable AI provider  
- Testable application core  
- Minimal API for simplicity and performance  

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
- Fully offline after initial model download  

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
- Awareness of cost, privacy, and deployment constraints  
- Real-world API design  
- A strong portfolio example for backend + AI skills  
