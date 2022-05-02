# Scroll

A product magazine.

## Dev Notes

- Install SQL Server 
- Install Azure Storage Emulator
- Add a Blob Container named: `scrollimages`

### Add .NET EF Core Tools
`dotnet tool install --global dotnet-ef`

### Add Migration
`dotnet ef migrations add "Initial" -o "./Migrations" --project ./src/Scroll.Data/Scroll.Data.csproj --startup-project ./src/Scroll.Web/Scroll.Web.csproj --json --prefix-output`

### Update DB
`dotnet ef database update --project ./src/Scroll.Data/Scroll.Data.csproj --startup-project ./src/Scroll.Web/Scroll.Web.csproj`

### Drop DB
`dotnet ef database drop --project ./src/Scroll.Data/Scroll.Data.csproj --startup-project ./src/Scroll.Web/Scroll.Web.csproj`