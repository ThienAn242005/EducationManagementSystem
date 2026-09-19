FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
RUN ln -s ~/.dotnet/tools/libman /usr/local/bin/libman

WORKDIR /src
COPY ["src/MyCompany.MyProject.Web.Mvc/MyCompany.MyProject.Web.Mvc.csproj", "src/MyCompany.MyProject.Web.Mvc/"]
COPY ["src/MyCompany.MyProject.Web.Core/MyCompany.MyProject.Web.Core.csproj", "src/MyCompany.MyProject.Web.Core/"]
COPY ["src/MyCompany.MyProject.Application/MyCompany.MyProject.Application.csproj", "src/MyCompany.MyProject.Application/"]
COPY ["src/MyCompany.MyProject.Core/MyCompany.MyProject.Core.csproj", "src/MyCompany.MyProject.Core/"]
COPY ["src/MyCompany.MyProject.EntityFrameworkCore/MyCompany.MyProject.EntityFrameworkCore.csproj", "src/MyCompany.MyProject.EntityFrameworkCore/"]
WORKDIR "/src/src/MyCompany.MyProject.Web.Mvc"
RUN dotnet restore 

WORKDIR /src
COPY ["src/MyCompany.MyProject.Web.Mvc", "src/MyCompany.MyProject.Web.Mvc"]
COPY ["src/MyCompany.MyProject.Web.Core", "src/MyCompany.MyProject.Web.Core"]
COPY ["src/MyCompany.MyProject.Application", "src/MyCompany.MyProject.Application"]
COPY ["src/MyCompany.MyProject.Core", "src/MyCompany.MyProject.Core"]
COPY ["src/MyCompany.MyProject.EntityFrameworkCore", "src/MyCompany.MyProject.EntityFrameworkCore"]
WORKDIR "/src/src/MyCompany.MyProject.Web.Mvc"
RUN libman restore
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "MyCompany.MyProject.Web.Mvc.dll"]
