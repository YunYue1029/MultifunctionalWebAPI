# 使用官方 .NET SDK 映像來建置應用程式
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# 複製 csproj 並還原相依套件
COPY *.csproj ./
RUN dotnet restore

# 複製整個專案並建置
COPY . ./
RUN dotnet publish -c Release -o out

# 使用精簡版執行時映像
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# 暴露預設的 ASP.NET 埠
EXPOSE 80

# 啟動應用程式
ENTRYPOINT ["dotnet", "MyApi.dll"]