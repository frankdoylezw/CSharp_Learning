<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Comprehensive Learning Plan</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            line-height: 1.6;
            margin: 20px;
        }
        h1, h2, h3 {
            color: #333;
        }
        h2 {
            margin-top: 20px;
        }
        ul {
            list-style: none;
            padding-left: 0;
        }
        li {
            margin: 5px 0;
        }
        input[type="checkbox"] {
            margin-right: 10px;
        }
        .phase-description {
            font-style: italic;
            margin-bottom: 10px;
        }
    </style>
    <script>
        // Save checkbox state to localStorage
        document.addEventListener("DOMContentLoaded", function () {
            const checkboxes = document.querySelectorAll('input[type="checkbox"]');
            
            // Load state from localStorage
            checkboxes.forEach(checkbox => {
                const isChecked = localStorage.getItem(checkbox.id) === "true";
                checkbox.checked = isChecked;
            });

            // Save state on click
            checkboxes.forEach(checkbox => {
                checkbox.addEventListener("change", () => {
                    localStorage.setItem(checkbox.id, checkbox.checked);
                });
            });
        });
    </script>
</head>
<body>
    <h1>Comprehensive Learning Plan</h1>
    <p>This structured learning plan is designed to help you develop essential skills in C# and web application development. Each phase builds upon the previous one, with clear objectives, actionable tasks, and resources to support your progress. Check off tasks as you complete them!</p>

    <h2>Phase 1: C# Basics and Fundamentals</h2>
    <p class="phase-description"><strong>Objective:</strong> Refresh foundational C# skills, including variables, loops, methods, classes, and interfaces, to build a strong base for more advanced topics.</p>
    <ul>
        <li><input type="checkbox" id="variables"> Variables ([Learn More](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/))</li>
        <li><input type="checkbox" id="loops"> Loops ([Learn More](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/control-flow/for-and-foreach-loops))</li>
        <li><input type="checkbox" id="methods"> Methods ([Learn More](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/methods))</li>
        <li><input type="checkbox" id="classes"> Classes ([Learn More](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/))</li>
        <li><input type="checkbox" id="inheritance"> Inheritance ([Learn More](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/inheritance))</li>
        <li><input type="checkbox" id="interfaces"> Interfaces ([Learn More](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/))</li>
    </ul>
    <h3>Exercises</h3>
    <ul>
        <li>Write a simple calculator program that uses methods for addition, subtraction, multiplication, and division.</li>
        <li>Create a class to represent a book and include methods for borrowing and returning books.</li>
        <li>Refactor an existing program to use interfaces for better flexibility.</li>
    </ul>

    <h2>Phase 2: Intermediate C# and Web Development</h2>
    <p class="phase-description"><strong>Objective:</strong> Build on foundational knowledge by learning to use Entity Framework, Dependency Injection, and MVC for real-world application development.</p>
    <ul>
        <li><input type="checkbox" id="ef"> Entity Framework Basics ([Learn More](https://learn.microsoft.com/en-us/ef/))</li>
        <li><input type="checkbox" id="di"> Dependency Injection ([Learn More](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection))</li>
        <li><input type="checkbox" id="mvc"> ASP.NET MVC Overview ([Learn More](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/introduction/getting-started-with-mvc))</li>
    </ul>
    <h3>Exercises</h3>
    <ul>
        <li>Set up a simple database with Entity Framework to manage employee records.</li>
        <li>Implement Dependency Injection in an ASP.NET Core project to manage service lifetimes.</li>
        <li>Create a small MVC application to display and edit product details.</li>
    </ul>

    <h2>Phase 3: Frontend and Integration Development</h2>
    <p class="phase-description"><strong>Objective:</strong> Develop skills in frontend technologies and API integration to build robust and interactive web applications.</p>
    <ul>
        <li><input type="checkbox" id="html-css"> HTML & CSS Basics ([Learn More](https://developer.mozilla.org/en-US/docs/Web/HTML))</li>
        <li><input type="checkbox" id="js"> JavaScript Basics ([Learn More](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Guide))</li>
        <li><input type="checkbox" id="jquery"> jQuery Overview ([Learn More](https://learn.jquery.com/))</li>
        <li><input type="checkbox" id="rest"> REST APIs ([Learn More](https://learn.microsoft.com/en-us/azure/architecture/best-practices/api-design))</li>
        <li><input type="checkbox" id="grpc"> gRPC Basics ([Learn More](https://learn.microsoft.com/en-us/aspnet/core/grpc/))</li>
    </ul>
    <h3>Exercises</h3>
    <ul>
        <li>Create a basic HTML page styled with CSS to display a user profile.</li>
        <li>Write JavaScript to validate a user input form.</li>
        <li>Build a simple application that consumes a REST API to fetch and display data.</li>
    </ul>

    <h2>Phase 4: Advanced Development Practices</h2>
    <p class="phase-description"><strong>Objective:</strong> Master advanced tools and practices such as version control, testing, containerization, and cloud deployment.</p>
    <ul>
        <li><input type="checkbox" id="git"> Version Control with Git ([Learn More](https://git-scm.com/doc))</li>
        <li><input type="checkbox" id="tdd"> Test-Driven Development (TDD) ([Learn More](https://learn.microsoft.com/en-us/dotnet/core/testing/))</li>
        <li><input type="checkbox" id="docker"> Docker Fundamentals ([Learn More](https://docs.docker.com/get-started/))</li>
        <li><input type="checkbox" id="azure"> Azure Basics ([Learn More](https://learn.microsoft.com/en-us/azure/))</li>
    </ul>
    <h3>Exercises</h3>
    <ul>
        <li>Use Git to clone a repository, create branches, and merge changes.</li>
        <li>Write unit tests for a small class using NUnit.</li>
        <li>Containerize a basic application with Docker and deploy it locally.</li>
        <li>Set up a basic Azure App Service to host a web application.</li>
    </ul>

    <h2>Tracking Progress</h2>
    <p>This learning plan includes interactive checkboxes that allow you to track your progress. Your progress is saved in your browser's local storage, so you can pick up where you left off, even after refreshing the page. Simply tick off tasks as you complete them!</p>
</body>
</html>
