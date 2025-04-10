# 📘 Proje Dokümantasyonu

## 🎉 EventApp Nedir?

EventApp, etkinlikleri yönetmek için geliştirilen modüler ve katmanlı yapıya sahip bir ASP.NET Core Web API uygulamasıdır.

## 🔧 Kullanılan Teknolojiler

- ASP.NET Core 7.0
- Entity Framework Core
- AutoMapper
- SQL Server
- Repository Pattern
- Katmanlı Mimari (API, Core, Data, Infrastructure)

## 🏗️ Proje Yapısı

EventApp │ ├── EventApp.API → API uç noktalarının bulunduğu katman │ ├── Controllers │ ├── Profiles │ └── Program.cs │ ├── EventApp.Core → Domain modelleri ve arayüzler │ ├── Domain │ ├── Application │ └── Registrations │ ├── EventApp.Data → EF Core DbContext, Migrations ve konfigurasyonlar │ ├── Context │ ├── Configurations │ └── Migrations │ ├── EventApp.Infrastracture → Repository implementasyonları ve DI ayarları │ ├── Repository │ └── Registrations │ └── EventApp.sln → Visual Studio çözüm dosyası


## 🚀 Kurulum

1. Bu repoyu klonla:
   ```bash
   git clone https://github.com/mustafas4rgin/EventApp.git
   cd EventApp
Gerekli NuGet paketlerini yükle:
dotnet restore
Veritabanı bağlantı ayarlarını appsettings.json içinde yap.
Migration’ları uygula:
dotnet ef database update --project EventApp.Data
Uygulamayı başlat:
dotnet run --project EventApp.API


📂 Örnek API Uç Noktaları

GET /api/events
POST /api/events
PUT /api/events/{id}
DELETE /api/events/{id}