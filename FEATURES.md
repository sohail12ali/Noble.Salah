# Noble Salah — Feature Overview

**Noble Salah** is a comprehensive Islamic mobile/web application designed to support Muslims in their daily religious practices. The application aims to offer accurate prayer time tracking, Islamic educational content, and community-based features with an elegant and intuitive user experience.

---

## 📦 Core Features

### 1. **Prayer Times**
- Accurate daily prayer times calculation using Batoulapps.Adhan library
  - Five daily prayers: Fajr, Dhuhr, Asr, Maghrib, Isha
  - Additional timings: Sunrise
- Multiple calculation methods support:
  - Muslim World League (default)
  - Egyptian
  - Karachi
  - Umm Al-Qura
  - Dubai
  - Moon Sighting Committee
  - North America
  - Kuwait
  - Qatar
  - Singapore
- Location-based adjustments:
  - Automatic location detection using device GPS
  - Timezone detection and handling
  - Support for both Hanafi and Shafi schools of thought

### 2. **Prayer Alerts & Countdown**
- Smart prayer time tracking
  - Automatic next prayer detection
  - Real-time countdown timer to next prayer
  - Local timezone awareness
- Notifications system (planned)
  - Audio alerts for prayer times
  - Customizable notification preferences
  - Platform-specific push notifications

### 3. **Ramadan Mode**
- Ramadan-specific UI and color themes
- Sehri and Iftar alerts
- Countdown to Iftar/Sehri
- Ramadan timetable (city-based)

### 4. **Monthly & Yearly Prayer Calendar**
- Exportable and printable timetable
- Filters by city or region
- Sync with device calendar

---

## 🧭 Direction & Discovery

### 5. **Qibla Compass**
- Real-time direction to Kaaba
- Magnetic field & compass error detection

### 6. **Masjid Finder**
- Nearby mosques using GPS
- Directions and contact info
- Map integration

### 7. **Halal Finder**
- Locate halal food places or markets
- Filter by cuisine or product type

---

## 📖 Islamic Essentials

### 8. **Quran**
- Full Quran in Arabic with translation
- Bookmarks and reading progress
- Search functionality

### 9. **Daily Duas**
- Categorized supplications (morning, night, travel, etc.)
- Audio and text display

### 10. **Tasbeeh Counter**
- Digital tasbeeh with vibration support
- Reset/save tasbeeh counts
- Multiple presets (SubhanAllah, Alhamdulillah, etc.)

### 11. **99 Names of Allah**
- Arabic, translation, and meaning
- Audio pronunciation

### 12. **Five Pillars of Islam**
- Educational section covering core beliefs

### 13. **Six Kalimas**
- Arabic with translation and audio

---

## 🕌 Community & Info

### 14. **Ask Imam**
- Submit religious questions
- View public Q&A from scholars

### 15. **Islamic News & World Updates**
- Feed of latest Islamic news
- Ramadan moon sighting updates

### 16. **Business Directory**
- Islamic service providers (schools, shops, NGOs)

### 17. **Cards & Islamic Greetings**
- Shareable e-cards for Eid, Ramadan, and daily reminders

---

## ⚙️ App Utilities

### 18. **User Profile & Greeting**
- Personalized user greeting
- Saved settings and preferences

### 19. **Settings**
- Language support (English, Arabic, Hindi, Urdu)
- Notification management
- Theme customization (Day/Night/Ramadan)

### 20. **Explore / Home Dashboard**
- Intro screens for Ramadan
- Featured articles, quotes, or tips

### 21. **Calendar**
- Islamic and Gregorian dates
- Highlight events and important days

### 22. **Shop**
- Islamic books, products, and accessories
- Online purchase integration (optional)

### 23. **Coming Soon**
- Placeholder for future features
- Encourage user feedback and suggestions

---

## ✅ Summary

| Category             | Implementation Status |
|----------------------|---------------------|
| Core Religious Tools | Prayer Times ✅, Ramadan Alerts 🚧, Calendar 🚧 |
| Navigation & Utility | Qibla Compass 🚧, Masjid Finder 🚧, Halal Finder 🚧 |
| Education & Worship  | Quran 🚧, Duas 🚧, Tasbeeh 🚧, Kalimas 🚧 |
| Community Features   | Ask Imam 🚧, Business Directory 🚧, News 🚧 |
| UX & System Features | Profiles 🚧, Themes 🚧, Settings 🚧, Explore 🚧 |

Legend: ✅ Implemented | 🚧 Planned/In Progress

---

## 🛠 Current Tech Stack

- **Frontend:** 
  - Blazor WASM for Web (.NET 9.0)
  - .NET MAUI for Mobile/Desktop (.NET 9.0)
  - Shared UI components between platforms
- **Core Libraries:**
  - Batoulapps.Adhan for prayer calculations
  - Native Geolocation services integration
- **Architecture:**
  - Clean architecture with clear separation of concerns
  - Common interfaces for cross-platform compatibility
  - Service-based design for modularity
- **Key Projects:**
  - `Noble.Salah.Common`: Shared models, interfaces, and constants
  - `Noble.Salah.Integration`: Core prayer calculation services
  - `Noble.Salah.UI.Web`: Blazor WASM implementation
  - `Noble.Salah.UI`: MAUI mobile/desktop implementation
- **Platform Support:**
  - Web: Modern browsers (Progressive Web App)
  - Mobile: Android & iOS (via MAUI)
  - Desktop: Windows (via MAUI)
- **UI Design:** Responsive, mobile-first, RTL support for Arabic/Urdu

---

> Designed to bring the beauty of Salah and the convenience of modern technology together. _“Indeed, the prayer is a prescribed duty that has to be performed at the appointed times.”_ — (Quran 4:103)
