# GamePal

GamePal is a community-driven web application designed to help gamers find like-minded teammates based on shared interests, playstyles, and schedules. Whether you prefer competitive matches, casual co-op sessions, or just want to chat about your favorite titles, GamePal makes it easy to discover players who match your gaming profile.

---

## Table of Contents

1. [Features](#features-planned)
2. [Technology Stack](#technology-stack)  
3. [Getting Started](#getting-started)  
   - [Prerequisites](#prerequisites)  
   - [Installation](#installation)  
   - [Environment Configuration](#environment-configuration)  
   - [Database Setup](#database-setup)  
4. [Running the Application](#running-the-application)  
   - [Backend (ASP.NET Core)](#backend-aspnet-core)  
   - [Frontend (Next.js)](#frontend-nextjs)  
5. [Authentication & Authorization](#authentication--authorization)  
6. [Usage](#usage)  
7. [Project Structure](#project-structure)  
8. [Contributing](#contributing)  
9. [License](#license)  

---
<a name="features-planned"></a>
## Features (planned)

- **User Profiles**  
  - Create and manage personal gamer profiles  
  - Add profile details such as preferred genres, playstyle, and typical playtime  

- **Advanced Filtering & Search**  
  - Search for players by game title, genre, playstyle, or playtime window  
  - Filter results based on ranking, region, or language  

- **Game Library**  
  - Access a comprehensive list of supported games (titles and genres)  
  - Suggest new games to be added to the library (admin-driven)  

- **Player Reviews & Ratings**  
  - Leave and read reviews for fellow gamers  
  - Rate players based on punctuality, communication, and teamwork (or any criteria basically)

- **Social Interactions**  
  - Send and accept friend requests  
  - One-to-one direct messaging system  
  - Real-time live chat for group conversations or 1:1 chats (powered by WebSockets)  

- **Notifications**  
  - In-app notifications for friend requests, messages, and review replies  
  - As well as email notifications

---

## Technology Stack

### Backend
- **Framework**: ASP.NET Core (.NET 8.0+)  
- **ORM**: Entity Framework Core  
- **Authentication**: ASP.NET Identity with JWT (JSON Web Tokens)  
- **Database**: PostgreSQL

### Frontend
- **Framework**: Next.js
- **Authentication Library**: Auth.js
  - Supports Credentials, GitHub, and Google providers (maybe later extended)
- **Styling**: SCSS (Sass) with custom theming  

### DevOps & Tools
- **Version Control**: Git
- **Containerization **: Docker & Docker Compose  

---

## Getting Started

Follow these instructions to get a local copy up and running on your machine.

### Prerequisites

1. **.NET SDK 8.0 (or later)**  
   Install from [.NET Downloads](https://dotnet.microsoft.com/download).

2. **Node.js & npm/yarn**  
   Install from [Node.js Official Website](https://nodejs.org/).  
   - Recommended: Node.js v18.x or later  

3. **PostgreSQL**  
   Install and ensure the service is running.  
   - Version 12.x or later is recommended.

4. **Git**  
   For cloning the repository and version control.

---

### Installation

1. **Clone the repository**  
   ```bash
   git clone https://github.com/vulpes556/GamePal.git

(Will be continued...)
