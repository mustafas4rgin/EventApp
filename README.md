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
cd backend
# Gerekli NuGet paketlerini yükle
# ve veritabanı yapılandırmalarını tamamla
```

### 💻 Frontend (React)
```bash
cd frontend
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

## 📸 Ekran Görüntüleri (opsiyonel)

İstersen proje ekran görüntülerini buraya ekleyebiliriz:
```
[home]<img width="1417" alt="Screenshot 2025-04-16 at 11 51 47" src="https://github.com/user-attachments/assets/80ef7149-e8a1-4a1f-aee8-da848349df2d" />
[dashboard]<img width="1508" alt="Screenshot 2025-04-16 at 11 52 48" src="https://github.com/user-attachments/assets/65acb790-e45a-4c95-b411-925e54f682ba" />
[AdminPanel]<img width="1511" alt="Screenshot 2025-04-16 at 11 53 57" src="https://github.com/user-attachments/assets/a9c5afda-4d4d-4fe4-9d5c-9de4591f3868" />

```

---

