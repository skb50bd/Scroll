# Scroll

A product magazine.

## Dev Notes

### Add Migration
`dotnet ef migrations add "Initial" -o ".\Migrations" --project .\src\Scroll.Data\Scroll.Data.csproj --startup-project .\src\Scroll.Web\Scroll.Web.csproj`

### Update DB
`dotnet ef database update --project .\src\Scroll.Data\Scroll.Data.csproj --startup-project .\src\Scroll.Web\Scroll.Web.csproj`