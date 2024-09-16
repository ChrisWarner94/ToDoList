This ToDo List application is a console-based app written in C# 12.0 that allows users to create, manage, and complete tasks. It utilizes a SQLite database to store tasks and Dapper for database interactions.



**Features**

Add Task: Add a new task to your ToDo list.

View Tasks: View all pending tasks.

Complete Task: Mark a task as completed.

Delete Task: Remove a task from the list.

Text Search Function: Search for tasks by entering a partial word or sequence of letters. The application will return all tasks that contain the entered characters, even if they are part of a word.

**Dependencies**

Dapper (v2.1.35)
A simple object mapper for .NET, used for mapping SQL query results to C# objects.

SQLite (v3.13.0)
SQLite is a lightweight, self-contained SQL database engine used for local storage.

System.Configuration.ConfigurationManager (v8.0.0)
This package provides access to the configuration file settings (such as App.config or Web.config).

System.Data.SQLite (v1.0.118)
This package is the official ADO.NET provider for SQLite. It allows the app to interact with SQLite databases.
