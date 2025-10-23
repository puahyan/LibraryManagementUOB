# LibraryManagementUOB

<img width="1833" height="1010" alt="image" src="https://github.com/user-attachments/assets/22b9c36e-ac87-4494-8e61-15a916028e5e" />


1. Command (Databse = master)

Under LibraryManagementUOB\LibraryManagementSystem\LibraryManagementSystem

Open with Visual Studio , Right click on "LibraryManagementSystem", clean and rebuild

Make sure Sql Management Installed, use master schema

dotnet ef migrations add SetupDatabase

dotnet ef database update
 
dotnet ef migrations add SeedData

dotnet ef database update


2. Scaffolding command if any changes in databases

Under LibraryManagementSystem\LibraryManagementSystem 

dotnet ef dbcontext scaffold "Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Entity --context LibraryContext --use-database-names --force


3. Execute as IIS Express under Visual Studio


4. choose AuthController to sign in user before perform any action on web API
   
<img width="1832" height="730" alt="image" src="https://github.com/user-attachments/assets/a598b7da-6f7e-4f6f-9a71-57e0a90b871e" />

For example : user1 , password : password123


5.Once token generated, do Key in Bearer Token on the top right side "Authorize" button

<img width="655" height="325" alt="image" src="https://github.com/user-attachments/assets/23af7ed0-2602-4b59-a9bf-d879a7beb31c" />


6. Then you may browse certain API depending on user roles.

7. Dynatrace

<img width="1838" height="789" alt="image" src="https://github.com/user-attachments/assets/cda9dcdb-9744-4de4-b7c1-e6728d1bc978" />


At the end of the appsetting.json content, please included :

 "SecretKey": "5HjZzOG2LGDe1FNK6wkptaRbNKknhrJ8",
 
 "DynatraceId": "mro66917",
 
 "DynatraceToken": "provided if requested"
