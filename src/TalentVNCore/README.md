How to run:
1. Update Database from cmd (webapp CLI)
- go to SchoolCMS folder(cmd)
- dotnet ef database update -c appdbcontext -p ../Infrastructure/Infrastructure.csproj -s SchoolCMS.csproj
- dotnet ef database update -c appidentitydbcontext -p ../Infrastructure/Infrastructure.csproj -s SchoolCMS.csproj
- Run project in VS studio with F5

How to create/update migration
1. create migration (from WebApp folder CLI)
- go to WeApp folder(cmd)
- dotnet ef migrations add InitialModel --context appdbcontext -p ../Infrastructure/Infrastructure.csproj -s WebApp.csproj -o Data/Migrations
- dotnet ef migrations add InitialIdentityModel --context appidentitydbcontext -p ../Infrastructure/Infrastructure.csproj -s WebApp.csproj -o Identity/Migrations

Documents relations:
1. Architecture design follow .Net standard design with .Net Core and support docker with Cross FlatForm
  https://github.com/dotnet-architecture/eShopOnWeb
  
2. Entities Framework Core tutorial:
  https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx
  
3. React Native Framework for mobileUI
  https://facebook.github.io/react-native/
  
4. React native UI with https://nativebase.io/

5. React native UI with https://react-native-training.github.io/react-native-elements/ 
