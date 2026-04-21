# Hacker News Best Stories API

## Overview

This project is a RESTful API built using ASP.NET Core that retrieves the top **n best stories** from the Hacker News API, sorted by score in descending order.

The API is designed with performance and scalability in mind, minimizing external API calls while maintaining fast response times.

---

## How It Works

1. Fetches the list of best story IDs from the Hacker News API
2. Retrieves story details in parallel
3. Maps the data into a clean response format
4. Sorts stories by score (descending)
5. Caches results to reduce repeated external calls

---

## Endpoint

GET /api/stories/best?n=10

### Query Parameters

* `n` → Number of top stories to return

---

## Sample Response

```json
[
  {
    "title": "Example Story",
    "uri": "https://example.com",
    "postedBy": "author",
    "time": "2023-01-01T12:00:00Z",
    "score": 100,
    "commentCount": 50
  }
]
```

---

## How to Run

1. Clone the repository
2. Navigate to the project folder
3. Run the following command:

```

4. Open browser or Postman:

```
https://localhost:{port}/api/stories/best?n=10
```

---

## Design Decisions

### 1. Layered Architecture

The application is structured into:

* Controllers → Handle HTTP requests
* Services → Contain business logic
* Clients → Handle external API communication

This separation improves maintainability and testability.

---

### 2. Parallel Data Fetching

Story details are fetched using asynchronous parallel calls to reduce latency.

---

### 3. Caching Strategy

An in-memory cache is used to store results for a short duration (5 minutes), reducing:

* External API load
* Response time for repeated requests

---

### 4. Input Validation

Basic validation is applied to ensure valid input (`n > 0`).

---

## Assumptions

* Only a subset of stories is fetched (e.g., top 50–100 IDs) to prevent excessive external API calls
* Cached data is acceptable for a short duration

---

## Possible Improvements

The following enhancements could be implemented:

* Distributed caching (e.g., Redis)
* Retry and resilience policies (e.g., Polly)
* Logging and monitoring
* Rate limiting
* Background job to refresh cache
* Pagination support

---

## Technologies Used

* ASP.NET Core Web API
* HttpClientFactory
* In-memory caching

---
