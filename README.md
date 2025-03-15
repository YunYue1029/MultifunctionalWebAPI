# MultifunctionalWeb

是一個基於 ASP.NET Core Web API 的應用程式，提供 **記帳管理** 和 **任務管理** 相關的 API 服務。此 API 可用於處理簡單的收支記錄與待辦事項管理，適合作為個人或小型應用的後端服務。

## 功能概述

MYAPI 提供兩個主要的 API 端點：

1. **記帳管理 (`/api/expenses`)**
   - 取得所有記帳記錄 (`GET /api/expenses`)
   - 新增記帳 (`POST /api/expenses`)
   - 刪除記帳 (`DELETE /api/expenses/{id}`)

2. **任務管理 (`/api/tasks`)**
   - 取得所有任務 (`GET /api/tasks`)
   - 新增任務 (`POST /api/tasks`)
   - 更新任務狀態 (`PUT /api/tasks/{id}`)

## 環境需求

- .NET 6.0 或更新版本
- ASP.NET Core Web API

## 執行
```
dotnet run
```
