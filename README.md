# 🎉 EventApp

**EventApp**, kullanıcıların etkinlik oluşturabildiği, rezervasyon yapabildiği, admin paneliyle kullanıcı ve etkinlikleri yönetebildiği tam özellikli bir web uygulamasıdır.

## 🚀 Özellikler

- 🔐 Kullanıcı girişi ve kayıt (JWT ile kimlik doğrulama)
- 📅 Etkinlik listeleme, detay sayfası ve rezervasyon
- 🌙 Karanlık mod (Dark mode toggle)
- 🛡️ Admin paneli:
  - Kullanıcı yönetimi (rol atama, silme, sayfalama, arama)
  - Etkinlik yönetimi (ekleme, silme, arama, sayfalama)
- 🔁 Şifre sıfırlama (email ile token gönderimi)
- ✅ Frontend: **React + Tailwind + Framer Motion + Axios**
- ✅ Backend: **ASP.NET Core Web API** (EF Core, JWT, FluentValidation vs.)

---

## 🧩 Proje Yapısı

```
EventApp/
├── EventApp.API/              # ASP.NET Core API 
├── eventapp.app/              # React uygulaması 
│   ├── src/
│   ├── public/
│   └── ...
├── EventApp.Core/
│   ├── EventApp.Application
│   ├── EventApp.Domain
└── ...
├── EventApp.Data/
│   ├── Migrations
│   ├── Context
├── EventApp.Infrastructure/
└── README.md
```

---

## ⚙️ Kurulum

### 🔧 Backend (.NET)
```bash
# Gerekli NuGet paketlerini yükle
# ve veritabanı yapılandırmalarını tamamla
```

### 💻 Frontend (React)
```bash
cd eventapp.app
npm install
npm run dev # veya npm start
```

> `.env` dosyasına API adresini yazmayı unutma:
```
VITE_API_BASE_URL=http://localhost:5148
```

---

## 👑 Geliştiriciler
**[@mustafas4rgin](https://github.com/mustafas4rgin)**


---

## 📦 Ekstra

- Dark mode toggle: localStorage ile kalıcı tema
- Tüm formlarda client-side validasyon
- Şifre sıfırlama: token ile güvenli işlem
- Admin route korumaları ve erişim kontrolü

---


---

