#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5090

FROM mcr.microsoft.com/dotnet/sdk:6.0-windowsservercore-ltsc2019 AS build 
WORKDIR /src

COPY ["NuGet.config", "."]
COPY ["Directory.Packages.props", "."]

COPY ["src/AAPM.PopForums/", "."]

#Restore nuget packages
RUN dotnet restore "PopForums.sln"

#Build project
RUN dotnet build "src/PopForums.Web/PopForums.Web.csproj" -c Release -o /app/build

#Publish project for deployment
FROM build AS publish
RUN dotnet publish "src/PopForums.Web/PopForums.Web.csproj" -c Release -o /app/publish

#Copy published files from sdk image to base image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#Start application
ENTRYPOINT ["dotnet", "PopForums.Web.dll"]
