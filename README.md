
# 📘 Proje Dokümantasyonu

## 🎉 EventApp Nedir?

EventApp, etkinlikleri yönetmek için geliştirilen, modüler ve katmanlı mimariye sahip bir ASP.NET Core Web API uygulamasıdır.

## 🔧 Kullanılan Teknolojiler

- ASP.NET Core 7.0
- Entity Framework Core
- AutoMapper
- SQL Server
- Repository Pattern
- Katmanlı Mimari (API, Core, Data, Infrastructure)

## 🏗️ Proje Yapısı

```
EventApp
│
├── EventApp.API              → API uç noktalarının bulunduğu katman
│   ├── Controllers
│   ├── Profiles
│   └── Program.cs
│
├── EventApp.Core             → Domain modelleri ve servis arayüzleri
│   ├── Domain
│   ├── Application
│   └── Registrations
│
├── EventApp.Data             → EF Core DbContext, Migrations ve konfigürasyonlar
│   ├── Context
│   ├── Configurations
│   └── Migrations
│
├── EventApp.Infrastracture  → Repository implementasyonları ve servis kayıtları
│   ├── Repository
│   └── Registrations
│
└── EventApp.sln              → Visual Studio çözüm dosyası
```

## 🚀 Kurulum

1. Repoyu klonla:
   ```bash
   git clone https://github.com/mustafas4rgin/EventApp.git
   cd EventApp
   ```

2. NuGet paketlerini yükle:
   ```bash
   dotnet restore
   ```

3. `appsettings.json` dosyasında veritabanı bağlantı ayarlarını yap.

4. Migration’ları uygula:
   ```bash
   dotnet ef database update --project EventApp.Data
   ```

5. Uygulamayı başlat:
   ```bash
   dotnet run --project EventApp.API
   ```

## 📂 Örnek API Uç Noktaları

- `GET /api/events`
- `POST /api/events`
- `PUT /api/events/{id}`
- `DELETE /api/events/{id}`

