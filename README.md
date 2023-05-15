# AssignmentWeb

## General Information
Solutin Assignment consists of four projects:
- AssignmentWeb
- Assignment.DataAccess
- Assignment.Models
- Assignment.Utility

## Technologies Used
- ASP.NET Core MVC - version .NET 7
- Console App
- AutoMapper - version 12.0.1
- Newtonsoft.Json - version 13.0.3
- Facebook Graph API Explorer - version 16.0
- Bootstrap
- Toastr

## Features
- LIKED PAGES On page load, displays last 10 liked pages from Facebook for the authenticated user (the Facebook endpoint is described at https://developers.facebook.com/docs/graph-api/reference/user/likes/) There is an option to Edit  a specific record, and by clicking UPDATE the json file will be saved in C:\UpdatedAssignmentData folder (folder with that name will be created automatically during the first update) Note: If you want to change the path on which documents will be saved, you can do that by modifying the FolderPath property inside the class SD of the Assignment.Utility project.
- FACEBOOK AUTHENTIFICATION If the user is not logged in, redirecting to the Facebook Log In page is required to retrieve the CODE, which is required to retrieve the access token.

## Setup
- VISUAL STUDIO 2022
- .NET 7
- NuGet packages:  Newtonsoft.Json, AutoMapper
- Launch as https in order to open localhost on port 7061, this is important because Facebook App is set up to redirect to that port 
