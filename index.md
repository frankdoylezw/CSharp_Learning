---
layout: default
title: Comprehensive Learning Plan
---
<style>
ul {
    list-style-type: none;
}

ul li {
    margin-left: -20px; /* Adjust this value as needed */
}
</style>
# Comprehensive Learning Plan

This structured learning plan is designed to help you develop essential skills in C# and web application development. Each phase builds upon the previous one, with clear objectives, actionable tasks, and resources to support your progress. Check off tasks as you complete them!

## Phase 1: C# Basics and Fundamentals

**Objective:** Refresh foundational C# skills, including variables, loops, methods, classes, and interfaces, to build a strong base for more advanced topics.

- [ ] Variables ([Learn More](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/)) {#variables}
- [ ] Loops ([Learn More](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/control-flow/for-and-foreach-loops)) {#loops}
- [ ] Methods ([Learn More](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/methods)) {#methods}
- [ ] Classes ([Learn More](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/)) {#classes}
- [ ] Inheritance ([Learn More](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/inheritance)) {#inheritance}
- [ ] Interfaces ([Learn More](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/)) {#interfaces}

### Exercises

- Write a simple calculator program that uses methods for addition, subtraction, multiplication, and division.
- Create a class to represent a book and include methods for borrowing and returning books.
- Refactor an existing program to use interfaces for better flexibility.

## Phase 2: Intermediate C# and Web Development

**Objective:** Build on foundational knowledge by learning to use Entity Framework, Dependency Injection, and MVC for real-world application development.

- [ ] Entity Framework Basics ([Learn More](https://learn.microsoft.com/en-us/ef/)) {#ef}
- [ ] Dependency Injection ([Learn More](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)) {#di}
- [ ] ASP.NET MVC Overview ([Learn More](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/introduction/getting-started-with-mvc)) {#mvc}

### Exercises

- Set up a simple database with Entity Framework to manage employee records.
- Implement Dependency Injection in an ASP.NET Core project to manage service lifetimes.
- Create a small MVC application to display and edit product details.

## Phase 3: Frontend and Integration Development

**Objective:** Develop skills in frontend technologies and API integration to build robust and interactive web applications.

- [ ] HTML & CSS Basics ([Learn More](https://developer.mozilla.org/en-US/docs/Web/HTML)) {#html}
- [ ] JavaScript Basics ([Learn More](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Guide)) {#js}
- [ ] jQuery Overview ([Learn More](https://learn.jquery.com/)) {#jquery}
- [ ] REST APIs ([Learn More](https://learn.microsoft.com/en-us/azure/architecture/best-practices/api-design)) {#rest}
- [ ] gRPC Basics ([Learn More](https://learn.microsoft.com/en-us/aspnet/core/grpc/)) {#grpc}

### Exercises

- Create a basic HTML page styled with CSS to display a user profile.
- Write JavaScript to validate a user input form.
- Build a simple application that consumes a REST API to fetch and display data.

## Phase 4: Advanced Development Practices

**Objective:** Master advanced tools and practices such as version control, testing, containerization, and cloud deployment.

- [ ] Version Control with Git ([Learn More](https://git-scm.com/doc)) {#git}
- [ ] Test-Driven Development (TDD) ([Learn More](https://learn.microsoft.com/en-us/dotnet/core/testing/)) {#tdd}
- [ ] Docker Fundamentals ([Learn More](https://docs.docker.com/get-started/)) {#docker}
- [ ] Azure Basics ([Learn More](https://learn.microsoft.com/en-us/azure/)) {#azure}

### Exercises

- Use Git to clone a repository, create branches, and merge changes.
- Write unit tests for a small class using NUnit.
- Containerize a basic application with Docker and deploy it locally.
- Set up a basic Azure App Service to host a web application.

## Tracking Progress

This learning plan includes interactive checkboxes that allow you to track your progress. Your progress is saved in your browser's local storage, so you can pick up where you left off, even after refreshing the page. Simply tick off tasks as you complete them!

<script>
document.addEventListener('DOMContentLoaded', (event) => {
    const supabaseUrl = 'libsql://csharplearning-frankdoylezw.aws-eu-west-1.turso.io';
    const supabaseKey = 'eyJhbGciOiJFZERTQSIsInR5cCI6IkpXVCJ9.eyJhIjoicnciLCJpYXQiOjE3MzczNzc0NzQsImlkIjoiYzIwNmY5MDItMTU3Ny00ZDIwLTkzMWQtZDMyZjhjMWJhNDA1IiwicmlkIjoiZTcyMzIyZTEtODkxMy00NDNlLThhMjktY2VmMjExMWM4NWFiIn0.UfVrnC5ytj1q7v9YfLkOD8J5XYWxZBuWVdUuFQl-Pse_bYCgZnp9WAkqLoXkni82tJl7ocB4Rlz8_rQh32ZYBg';
    const supabase = supabase.createClient(supabaseUrl, supabaseKey);

    const checkboxes = document.querySelectorAll('input[type="checkbox"]');

    // Load checkbox states from the server
    supabase
        .from('checkboxes')
        .select('*')
        .then(response => {
            response.data.forEach(item => {
                const checkbox = document.getElementById(item.id);
                if (checkbox) {
                    checkbox.checked = item.checked;
                }
            });
        });

    // Save checkbox state to the server
    checkboxes.forEach(checkbox => {
        checkbox.addEventListener('change', (event) => {
            const id = event.target.id;
            const checked = event.target.checked;

            supabase
                .from('checkboxes')
                .upsert({ id, checked })
                .then(response => {
                    console.log('Checkbox state saved');
                });
        });
    });
});
</script>
