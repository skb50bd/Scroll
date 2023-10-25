# [Scroll](https://github.com/skb50bd/Scroll)

Scroll is a product magazine built on the .NET ecosystem. The project serves as a RESTful API that allows users to browse through various product categories and details. It's not your run-of-the-mill CRUD application; it incorporates advanced features like image processing and complex database relationships. So, if you're looking for a project that's more than just a "Hello, World!" in .NET, you've found it.

## Features

- **Product Management**: Add, update, and delete products with ease.
- **Category Management**: Organize products into various categories.
- **Image Upload**: Upload and manage product images.
- **Search Functionality**: Search products by name, category, or tags.
- **Pagination**: Browse through products and categories with pagination support.
- **Image Processing**: Automatically resize and optimize uploaded images for web use.
- **User Authentication**: Secure user authentication and role-based access control.

## Technologies Used

- **C#**: The backbone of the project. All the logic, services, and API controllers are written in C#.
- **Entity Framework Core**: For all your database needs.
- **ASP.NET Core**: Used for building the RESTful API.
- **AutoMapper**: Used for object-object mapping between DTOs and entities.
- **SQL Server**: The database of choice.
- **Azure Storage**: Used for storing images in blob storage.

### Dev Notes

- Install Docker
- Install Azure Storage Emulator
- Add .NET EF Core Tools

    ```shell
    dotnet tool install --global dotnet-ef
    ```

- Add Migration

    ```shell
    dotnet ef migrations add "Initial" -o "./Migrations" --project ./src/Scroll.Data/Scroll.Data.csproj --startup-project ./src/Scroll.Web/Scroll.Web.csproj --json --prefix-output
    ```

- Update DB

    ```shell
    dotnet ef database update --project ./src/Scroll.Data/Scroll.Data.csproj --startup-project ./src/Scroll.Web/Scroll.Web.csproj
    ```

- Drop DB

    ```shell
    dotnet ef database drop --project ./src/Scroll.Data/Scroll.Data.csproj --startup-project ./src/Scroll.Web/Scroll.Web.csproj
    ```
