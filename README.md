**TripSample – Otobüs Bileti Arama ve Yolculuk Planlama Uygulaması**

TripSample, Obilet API’lerini kullanarak otobüs konumlarını, sefer bilgilerini ve kullanıcı oturum verilerini yöneten bir örnek .NET Core MVC uygulamasıdır.
Uygulama hem modern frontend davranışları (Select2, tarih bileşenleri, cookie yönetimi) hem de sağlam bir backend katman mimarisi içermektedir.

🚀 **Özellikler**

+ Otomatik oturum oluşturma (SessionService)
+ Konum arama (BusLocationService)
+ Yolculuk arama (JourneyService)
+ MemoryCache kullanımı ile performans artırımı
+ Select2 ile autocomplete özelliği
+ Cookie yönetimi ile son seçimlerin saklanması
+ Tarih seçimi, “Bugün / Yarın” butonları
+ Değer değişimi butonu (Nereden ↕ Nereye)
+ View/Controller formatında temiz MVC yapısı

🏗 **Mimari Yapı**
Uygulama klasik katmanlı mimariyi kullanır:

TripSample
 - ├── TripSample.Domain
 - ├── TripSample.Application
 - ├── TripSample.Infrastructure
 - └── TripSample.WebUI

**Domain Katmanı**
- Model’ler (BusLocationModel, BusJourneysResponseModel, SessionModel)
- API DTO’ları
- Sabitler (Const)

**Application Katmanı**
- İş servisleri burada yer alır:
- BusLocationService
- JourneyService
- SessionService
Tüm servisler DI ile enjekte edilmiştir.

**Infrastructure Katmanı**
- Obilet API Client
- Endpoints tanımları

**WebUI Katmanı**
- MVC Controller’lar
- View’lar (Index.cshtml, JourneyIndex.cshtml)
- Cookie yönetimi
- Select2 setup'ları
- Attribute filtreleri

🔌** Servislerin Akışı**
- 1) SessionService → Kullanıcı oturumunun oluşturulması
ve Kullanıcı siteye geldiğinde tarayıcı & bağlantı bilgileri ile bir session yaratılır.

- 2) BusLocationService → Otobüs konumları (autocomplete)
Kullanıcı "Nereden / Nereye" alanına yazdıkça API’den konum listesi alınır.
- ✔ MemoryCache ile sonuçlar 60 dakika saklanır
- ✔ API cevap vermiyorsa exception fırlatılır


- 3) JourneyService → Sefer sonuçları
-  Kullanıcı “Bileti Bul” butonuna bastığında:
- Session tekrar kullanılır
- Nereden / Nereye / Tarih cookie’den doldurulur
- API’den sefer listesi alınır
- Cache’e kaydedilir
- JourneyService

**🗂 Cache Mekanizması**
- Kullanılan Cache Key formatları:
- BusLocations_{query}
- BusJourneys_{targetId}_{originId}_{departureDate}

- MemoryCache kullanılarak performans arttırıldı ve API nin sürekli tekrar çağrılması engellendi
- İstenirse Redis’e geçmeye uygun olarak tasarlanmıştır (interface yapısı buna izin verir)

**💾 Cookie Kullanımı**
- Aşağıdaki veriler cookie’de tutulur:
- Seçilen OriginId / OriginName
- Seçilen TargetId / TargetName
- Seçilen Tarih
- Neden?
Kullanıcı geri geldiğinde formdaki bilgilerin korunması için.

**🖥 Frontend Davranışları**
- Select2 ile konum arama:
- Delay: 250ms
- API üzerinden autocomplete
- Önceki seçim cookie’den yükleniyor
- Tarih seçimi: jQuery UI datepicker
- Bugün / Yarın butonları
- Geçmiş tarihler engelleniyor
- Nereden ↕ Nereye swap butonu: Seçilen iki input’un birbirinin yerine geçmesi
- Eğer kalan koltuk sayısı <= 10 ise sefer alanında kırmızı kutuda gösterilir.

<img width="481" height="647" alt="image" src="https://github.com/user-attachments/assets/1989159d-e7d1-484a-b74d-433e09a427e9" />
<img width="432" height="1005" alt="image" src="https://github.com/user-attachments/assets/749997bc-57a8-4197-86be-7dc5e3377f1c" />


