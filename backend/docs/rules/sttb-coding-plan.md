# STTB Website — Coding Phase Planning

## Setup Environment

- Mac: Next.js 15 frontend → **VS Code (biru)**
- Parallels (Windows): ASP.NET Core backend → **Visual Studio (ungu)**
- AI Agent: Claude Code

---

## Fase 0 — Project Initialization

### 0.1 Setup Repository

```bash
mkdir sttb-website && cd sttb-website
git init

mkdir docs/rules
# Taruh standard-rules.md, frontend.md, backend.md di sini

# Buat CLAUDE.md di root
echo "@docs/rules/standard-rules.md
@docs/rules/frontend.md
@docs/rules/backend.md" > CLAUDE.md
```

### 0.2 Init Frontend (Mac)

```bash
cd sttb-website
npx create-next-app@latest frontend \
  --typescript \
  --tailwind \
  --eslint \
  --app \
  --src-dir \
  --import-alias "@/*"

cd frontend
npx shadcn@latest init
```

### 0.3 Init Backend (Parallels - Windows)

```bash
cd sttb-website
dotnet new sln -n Sttb
dotnet new webapi -n Sttb.WebAPI
dotnet new classlib -n Sttb.Commons
dotnet new classlib -n Sttb.Contracts
dotnet new classlib -n Sttb.Entities

dotnet sln add Sttb.WebAPI Sttb.Commons Sttb.Contracts Sttb.Entities
```

### 0.4 Setup CLAUDE.md per subfolder

```
sttb-website/
├── CLAUDE.md              ← @docs/rules semua
├── frontend/
│   └── CLAUDE.md          ← @docs/rules/standard-rules.md + frontend.md
└── backend/
    └── CLAUDE.md          ← @docs/rules/standard-rules.md + backend.md
```

**CHECKPOINT 0:**
- [ ] Repo ter-init dengan struktur yang benar
- [ ] `frontend/` bisa dijalankan dengan `npm run dev` (landing di localhost:3000)
- [ ] `backend/` bisa dijalankan di Parallels (landing di localhost:5052)
- [ ] CORS backend menerima request dari localhost:3000
- [ ] `NEXT_PUBLIC_API_URL` sudah dikonfigurasi di `frontend/.env.local`

---

## Fase 1 — Foundation (Shared Components)

Komponen yang dipakai di SEMUA halaman. Harus beres dan solid sebelum lanjut.

### 1.1 Tailwind Config

Tambahkan custom STTB tokens ke `tailwind.config.ts`:

```
- sttb-navy, sttb-navy-dark, sttb-navy-accent
- sttb-red, sttb-red-hover
- sttb-offwhite
- font-display (Playfair Display)
- font-heading (DM Serif Display)
- font-body (Plus Jakarta Sans)
```

### 1.2 Global Fonts

Setup `next/font` di `app/layout.tsx` untuk ketiga font STTB.

### 1.3 Shared Components

```
components/
├── layout/
│   ├── Navbar.tsx              ← dengan dropdown (Tentang Kami, Akademik, dst)
│   ├── NavbarDropdown.tsx      ← submenu component
│   └── Footer.tsx              ← 4-column footer + newsletter + rekening
├── shared/
│   ├── ImagePlaceholder.tsx    ← navy + STTB logo 30% opacity
│   ├── SectionLabel.tsx        ← red uppercase label (reused everywhere)
│   └── CTASection.tsx          ← dark navy CTA block (reused di banyak halaman)
```

### 1.4 Root Layout

`app/layout.tsx` dengan Navbar + Footer wrapping semua halaman.

**CHECKPOINT 1:**
- [ ] Navbar muncul dengan benar di semua breakpoint (mobile + desktop)
- [ ] Dropdown Navbar berfungsi (hover/click)
- [ ] Footer muncul dengan benar
- [ ] Font STTB (Playfair, DM Serif, Plus Jakarta Sans) ter-load
- [ ] Semua Tailwind custom tokens berfungsi (tidak ada warna yang salah)
- [ ] `ImagePlaceholder` menampilkan navy block + logo

---

## Fase 2 — Static Pages

Konten hardcoded — tidak butuh API sama sekali.
Urutan pengerjaan dari yang paling simpel ke kompleks.

### 2.1 Admisi (prioritas karena deadline)

```
app/admisi/
├── faq/page.tsx
├── info-persyaratan/page.tsx
├── jadwal-pendaftaran/page.tsx
└── prosedur-admisi/page.tsx
```

### 2.2 Tentang Kami

```
app/tentang-kami/
├── sejarah/page.tsx
├── visi-misi/page.tsx
├── mars-sttb/page.tsx
├── pengakuan-iman/page.tsx
├── dewan-dosen/page.tsx
└── yayasan/page.tsx
```

### 2.3 Keuangan

```
app/keuangan/
├── beasiswa/page.tsx
├── biaya-studi/page.tsx
└── dukung-sttb/page.tsx
```

**CHECKPOINT 2:**
- [ ] Semua static pages dapat diakses via URL yang benar
- [ ] Tidak ada broken layout atau overflow horizontal
- [ ] Semua internal links (`next/link`) berfungsi
- [ ] Halaman responsive di mobile (375px) dan desktop (1440px)
- [ ] Tidak ada console error

---

## Fase 3 — Dynamic Pages (Mock Data)

Backend belum siap — gunakan mock data dari `constants/mockData.ts`.
Struktur komponen dibuat seolah-olah data datang dari API,
sehingga swap ke API nanti tidak perlu ubah komponen.

### 3.1 Setup Mock Data

```
constants/
└── mockData.ts    ← berisi MOCK_BERITA, MOCK_KEGIATAN, MOCK_MEDIA, MOCK_PERPUSTAKAAN
```

### 3.2 Berita

```
app/jelajahi/berita/
├── page.tsx            ← list + filter bar
└── [slug]/page.tsx     ← detail + artikel terkait
```

### 3.3 Kegiatan

```
app/jelajahi/kegiatan/
├── page.tsx
└── [slug]/page.tsx
```

### 3.4 Media

```
app/jelajahi/media/
├── page.tsx
├── artikel/[slug]/page.tsx
└── video/[slug]/page.tsx
```

### 3.5 Perpustakaan

```
app/jelajahi/perpustakaan/
├── page.tsx
└── [slug]/page.tsx     ← dengan PDF viewer
```

**CHECKPOINT 3:**
- [ ] Semua dynamic pages menampilkan mock data dengan benar
- [ ] Filter bar berfungsi (client-side filtering dari mock data)
- [ ] `[slug]` routing berfungsi — klik card masuk ke halaman detail yang benar
- [ ] PDF viewer di perpustakaan detail bisa render
- [ ] generateStaticParams dibuat untuk semua `[slug]` pages

---

## Fase 4 — Homepage

Homepage dikerjakan belakangan karena menampilkan
konten dari berbagai section (berita terbaru, kegiatan terbaru).
Lebih mudah dikerjakan setelah semua konten lain sudah ada.

```
app/
└── page.tsx    ← Homepage
```

Sections:
- Hero utama
- Program studi (highlight)
- Berita terbaru (ambil dari mock data / API nanti)
- Kegiatan terbaru
- Stats / highlights STTB
- CTA Daftar Sekarang

**CHECKPOINT 4:**
- [ ] Homepage menampilkan semua sections dengan benar
- [ ] Berita terbaru dan kegiatan terbaru mengambil dari sumber yang sama dengan halaman list-nya
- [ ] Hero image placeholder tampil benar
- [ ] Semua link di homepage menuju halaman yang benar

---

## Fase 5 — Backend API (Parallels)

Dikerjakan paralel setelah Fase 3 beres di frontend.
Urutan: entity → migration → handler → controller → test.

### 5.1 Database Setup

```bash
# Install EF Core tools
dotnet tool install --global dotnet-ef

# Setup PostgreSQL connection di user-secrets
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;..."
```

### 5.2 Entities & Migrations

```
Sttb.Entities/
├── Berita.cs
├── Kegiatan.cs
├── MediaArtikel.cs
├── MediaVideo.cs
└── KoleksiPerpustakaan.cs
```

### 5.3 CQRS Handlers (per feature)

Urutan per feature:
1. Request/Response models di `Sttb.Contracts`
2. Handler di `Sttb.Commons/RequestHandlers`
3. Validator di `Sttb.Commons/Validators`
4. Controller di `Sttb.WebAPI/Controllers`

### 5.4 Endpoints yang dibutuhkan frontend

```
GET /api/Berita/list?page=1&pageSize=9&category=
GET /api/Berita/{slug}
GET /api/Kegiatan/list?page=1&pageSize=9&status=
GET /api/Kegiatan/{slug}
GET /api/Media/list?page=1&pageSize=6&type=
GET /api/Media/artikel/{slug}
GET /api/Media/video/{slug}
GET /api/Perpustakaan/list?page=1&pageSize=8&category=
GET /api/Perpustakaan/{slug}
```

**CHECKPOINT 5:**
- [ ] Semua endpoint bisa dipanggil via Postman/Thunder Client
- [ ] Response format sesuai dengan yang diharapkan frontend
- [ ] CORS menerima request dari localhost:3000
- [ ] Pagination berfungsi

---

## Fase 6 — API Integration

Swap mock data di frontend ke real API calls.

### Per halaman:
1. Buat `types/` interface sesuai response dari backend
2. Buat fetch function di `lib/api.ts`
3. Ganti mock import di page.tsx dengan fetch ke API
4. Test end-to-end

**CHECKPOINT 6:**
- [ ] Semua dynamic pages mengambil data dari API (bukan mock)
- [ ] Loading states tampil saat data sedang di-fetch
- [ ] Error states handled dengan baik (API down, 404, dll)
- [ ] Tidak ada mock data yang tersisa di production code

---

## Ringkasan Timeline

```
Fase 0 — Init          : ~2 jam
Fase 1 — Foundation    : ~1 hari
Fase 2 — Static Pages  : ~3-4 hari
Fase 3 — Dynamic Mock  : ~2-3 hari
Fase 4 — Homepage      : ~1 hari
Fase 5 — Backend       : ~3-4 hari
Fase 6 — Integration   : ~1-2 hari
```

Total estimasi: **~2 minggu** pengerjaan penuh.

---

## Catatan Penting

- Selalu mulai Claude Code session dengan: `claude` di folder yang tepat
  (`frontend/` untuk FE work, `backend/` untuk BE work)
- Claude Code akan otomatis baca `CLAUDE.md` yang relevan
- Commit setelah setiap checkpoint tercapai
- Jangan lanjut ke fase berikutnya sebelum checkpoint sebelumnya hijau semua
