# 🧠 GPT Destekli CV Oluşturma Sistemi

Bu proje, yapay zeka destekli olarak kullanıcıların adım adım profesyonel özgeçmiş (CV) oluşturmasını sağlayan web tabanlı bir sistemdir. .NET Core altyapısıyla geliştirilmiş, OpenAI'nin GPT tabanlı doğal dil işleme modeli kullanılmış ve veriler güvenli şekilde Microsoft SQL Server üzerinde saklanmaktadır.

## 🚀 Özellikler

- ✅ Üyelik ve kullanıcı giriş sistemi  
- 📝 Çok adımlı CV oluşturma formu  
- 🤖 GPT destekli dil analizi ve yazım düzeltmesi  
- 📄 Profesyonel CV formatında çıktı üretimi  
- 💾 Veritabanında kullanıcıya özel CV saklama  
- 🎨 Modern ve kullanıcı dostu web arayüzü  
- 📥 CV çıktısını PDF olarak indirme desteği  
- 📂 Önceki CV’lere tekrar erişme imkanı  

## 🛠️ Kullanılan Teknolojiler

- ASP.NET Core (.NET 6+)
- Entity Framework Core
- Microsoft SQL Server
- OpenAI GPT API (NLP işlemleri için)
- HTML/CSS & Bootstrap
- JavaScript (isteğe bağlı dinamik form işlemleri için)

## 🖼️ Ekran Görüntüleri

> 📌 Aşağıdaki adımlar ekran görüntüleriyle desteklenmiştir:

- Üye Ol ve Giriş Yap ekranı  
- CV Oluşturma adımları (kişisel bilgiler, eğitim, deneyim vb.)  
- Yapay zeka tarafından düzenlenen CV önizleme sayfası  
- CV'yi indir ve geçmişe eriş arayüzü

## 🔧 Kurulum

```bash
# Projeyi klonlayın
git clone https://github.com/kullaniciadiniz/.netcore-cv-chatgpt-cv-generator.git

# Proje klasörüne geçin
cd .netcore-cv-chatgpt-cv-generator

# Gerekli NuGet paketlerini yükleyin
dotnet restore

# Uygulamayı başlatın
dotnet run
