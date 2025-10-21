# LibraryManagementUOB
1. Command
Under LibraryManagementUOB\LibraryManagementSystem\LibraryManagementSystem

dotnet ef migrations add SetupDatabase
dotnet ef database update
 
dotnet ef migrations add SeedData
dotnet ef database update

2. Scaffolding command if any changes in databases
3. 
LibraryManagementSystem\LibraryManagementSystem 


dotnet ef dbcontext scaffold "Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Entity --context LibraryContext --use-database-names --force

3. Execute as IIS Express under Visual Studio

4. choose AuthController to sign in user before perform any action on web API
   
<img width="1832" height="730" alt="image" src="https://github.com/user-attachments/assets/a598b7da-6f7e-4f6f-9a71-57e0a90b871e" />

For example : user1 , password : password123

4.Once token generated, do Key in Bearer Token on the top right side "Authorize" button

<img width="655" height="325" alt="image" src="https://github.com/user-attachments/assets/23af7ed0-2602-4b59-a9bf-d879a7beb31c" />


5. Then you may browse certain API depending on user roles.
