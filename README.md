# FamApp
FamApp is an application for a group of users with an advanced TODO list and a live chat. All users can create Tickets for themselves or other users and solve them. They also can communicate with others through a live SignalR chat in direct messages or group messages.

## Technologies
* **ASP.NET Core MVC** is used for backend logic of application to manage users, tickets and chats.
* **SignalR** used for live chat
* with **Entity Framework** serves the database
*  **Razor Pages** are used for simple GUI

## Instalation
* .NET 6.0 or later versions
* SQL (or another database configured in appsettings.json)

## Use
Registration or login is required when turn on the app.
After that, user can update thier personal data or profile settings, manages tickets or chat.

## Project Structure
* **Models:** Contain classes, that represents data in database.
* **Controllers:** Contain logic for communitacion with views and database.
* **Services:** Contain bussiness logic for processing data before sending it into dabase or controller.
* **Repositories:** Handle conmmunication with the database.
* **Views:** User interface for managing the app.
* **ViewModels:** Edited model for the data transer into view.
* **Hub:** SingnalR hub for real-time communication

## Functions
* **Registration / Login** - User can create their own account and update it (e. g., change their password)
* **Update profile** - User can choose nickname and set their account color.

### Tickets
* **Create** - set a variety of  required or optional parameters (e. g., deadLines, solvers, names, descriptions, private, ...) and create new TODO ticket for themself or other user.
* **Delete** - the creator of the ticket can delete it.
* **Solve** - The solver of a ticket can mark the ticket as solved.
* **Filtering** - each user can filter tickets

### Chat
* **Create** - create direct or group chats with other users, set the name and color.
* **Chatting** - use chat similary to meesenger or whatsapp.
* **Delete** - delete chat and linked data. 
