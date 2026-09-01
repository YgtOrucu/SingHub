# 🎵 SingHub - Dynamic Music & Artist Management System

SingHub, müzik, albüm ve sanatçı yönetimini modern yazılım mimarilerine uygun olarak sunan, **Clean Architecture** prensipleri ve **CQRS** deseni ile geliştirilmiş tam kapsamlı bir Web API ve Web UI platformudur.

---

## 📸 Ekran Görüntüleri (Screenshots)

<details>
<summary><b>👤 Kullanıcı Arayüzü (User Interface) - Görselleri Görmek İçin Tıklayın</b></summary>
<br>

---

<img width="1894" height="828" alt="Ekran görüntüsü 2026-09-01 180006" src="https://github.com/user-attachments/assets/85228d16-7ab4-44c5-b3ce-3e7f4d55a01d" />

---

<img width="1622" height="759" alt="Ekran görüntüsü 2026-09-01 180025" src="https://github.com/user-attachments/assets/9d4fae53-cad9-4308-90a4-d1e7a9c055d1" />





</details>

<details>
<summary><b>👨‍💼 Admin Paneli (Admin Interface) - Görselleri Görmek İçin Tıklayın</b></summary>
<br>

---

<img width="1889" height="819" alt="Ekran görüntüsü 2026-09-01 175933" src="https://github.com/user-attachments/assets/62970795-e446-4292-b80b-f63391515293" />

---

<img width="1895" height="917" alt="Ekran görüntüsü 2026-09-01 175911" src="https://github.com/user-attachments/assets/fa0ab826-3f65-4dc2-90ab-d15081492dc4" />

---

<img width="1636" height="861" alt="Ekran görüntüsü 2026-09-01 180038" src="https://github.com/user-attachments/assets/cc31b4ed-29f6-4ab8-85db-f4209cdfc66f" />

---

<img width="1085" height="588" alt="Ekran görüntüsü 2026-09-01 175953" src="https://github.com/user-attachments/assets/12c165f9-5cf8-424d-be10-9a08f2883af4" />

</details>

---

## 🏗️ Proje Mimarisi (Architecture)

Proje, bağımlılıkların yönetilebilirliği ve sürdürülebilirlik açısından **Clean Architecture** prensiplerine sadık kalınarak katmanlandırılmıştır:

```text
SingHub/
├── 📁 Core/
│   ├── 📄 SingHub.Application/       # CQRS Commands, Queries, Handlers, DTO Mapping, Validators
│   └── 📄 SingHub.Domain/            # Domain Entities, Enums, Base Contracts
├── 📁 Infrastructure/
│   ├── 📄 SingHub.Infrastructure/    # JwtService, MailService, External Integrations
│   └── 📄 SingHub.Persistence/       # EF Core DbContext, Repositories, DbSeeder
└── 📁 Presentation/
    ├── 📄 SingHub.Dto/               # Data Transfer Objects
    ├── 📄 SingHub.WebAPI/            # Minimal API Endpoints, Custom Middlewares, Scalar UI
    └── 📄 SingHub.WebUI/             # ASP.NET Core MVC Admin & User Interfaces

```

---

## 🚀 Öne Çıkan Özellikler & Teknik Detaylar

- **CQRS & Validation Architecture** — İş mantıkları Command ve Query olarak ayrılmış, gelen istekler ValidationBehavior üzerinden **FluentValidation** ile doğrulanmıştır.
- **Minimal API & Endpoint Routing** — Web API tarafında controller yükünü hafifleten, performanslı ve derli toplu EndpointGroup yönlendirmeleri kullanılmıştır.
- **Kategori & Yazar Yönetimi** — Tam CRUD işlemleri
- **API Dokümantasyonu (Scalar API)** — OpenAPI standartlarında modern ve etkileşimli API dokümantasyonu için Scalar entegre edilmiştir.
- **Güvenlik & Yetkilendirme** — JWT (JSON Web Token) tabanlı kimlik doğrulama ve rol bazlı erişim kontrolü (Role-Based Access Control).
- **Merkezi Hata Yönetimi** — Tüm API hataları CustomExceptionHandlingMiddleware ile yakalanıp standart BaseResult formatında istemciye dönüştürülür.
- **Database Seeding** — Uygulama ayağa kalkarken otomatik veri beslemesi için UseDbSeederAsync yapısı kurulmuştur.

---

## 🛠 Kullanılan Teknolojiler

| Katman | Teknoloji |
|--------|-----------|
| **Framework** | .NET 9 / ASP.NET Core |
| **ORM** | Entity Framework Core |
| **API & UI** | ASP.NET Core Web API, ASP.NET Core MVC |
| **API UI Documentation:** | Scalar API Reference (Scalar.AspNetCore) |
| **Doğrulama & Mapping** | FluentValidation, AutoMapper |
| **Güvenlik** | JWT Bearer Authentication, Password Hashing |
| **Bildirim** | MailKit / SMTP Mail Service |
