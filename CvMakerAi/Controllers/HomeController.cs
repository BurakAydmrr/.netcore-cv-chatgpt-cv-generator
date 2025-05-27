using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using CvMakerAi.Models;
using DinkToPdf;
using DinkToPdf.Contracts;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace CvMakerAi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AIServices _aiService;
       
        private readonly IConverter _converter;  // DinkToPdf converter

        public HomeController(
            ILogger<HomeController> logger,
            AIServices aiService,
           
            IConverter converter)
        {
            _logger = logger;
            _aiService = aiService;
           
            _converter = converter;
        }




        public IActionResult Index()
        {
            // Check if the session contains UserId (indicating the user is logged in)
            var userId = HttpContext.Session.GetString("UserId");
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userEmail))
            {
                // Optionally, retrieve more user details from the database if needed
                using (var dbHelper = new DatabaseHelper())
                {
                    var userList = dbHelper.ExecuteReader(
                        "SELECT Id, Email, Ad, Soyad FROM Users WHERE Id = @p0",
                        userId
                    );

                    if (userList.Count > 0)
                    {
                        var user = userList[0];
                        ViewBag.UserInfo = new
                        {
                            Id = user["Id"],
                            Email = user["Email"],
                            Name = user["Ad"],
                            Surname = user["Soyad"]
                        };
                    }
                }
            }

            return View();
        }


        [HttpGet]
        [Route("/GirisYap")]
        public IActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        [Route("/GirisYap")]
        public IActionResult GirisYap(string Email, string Sifre)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Sifre))
            {
                ModelState.AddModelError("", "E-posta ve �ifre alanlar� zorunludur.");
                ViewBag.Hata = "E-posta ve �ifre alanlar� zorunludur.";
                return View();
            }

            using (var dbHelper = new DatabaseHelper())
            {
                var userList = dbHelper.ExecuteReader(
                    "SELECT Id, Email, Sifre FROM Users WHERE Email = @p0",
                    Email
                );

                if (userList.Count > 0)
                {
                    var user = userList[0];

                    var settings = new Settings();
                    bool isPasswordValid = settings.VerifyPassword(Sifre, user["Sifre"].ToString());

                    if (isPasswordValid)
                    {
                        HttpContext.Session.SetString("UserId", user["Id"].ToString());
                        HttpContext.Session.SetString("UserEmail", user["Email"].ToString());
                        TempData["SuccessMessage"] = "Ba�ar�yla giri� yapt�n�z!";
                        return RedirectToAction("Index");
                    }
                }
            }

            ModelState.AddModelError("", "E-posta adresi veya �ifre hatal�.");
            ViewBag.Hata = "E-posta adresi veya �ifre hatal�.";
            return View();
        }


        [HttpGet]
        [Route("UyeOl")]
        public IActionResult UyeOl()
        {
            return View();
        }


        [HttpGet]
        [Route("CvBasarili")]
        public IActionResult CvBasarili()
        {
            return View();
        }


        [HttpPost]
        [Route("Home/UyeOl")]
        public IActionResult UyeOl(string Ad, string Soyad, string Email, string Sifre)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Sifre))
            {
                ModelState.AddModelError("", "E-posta ve �ifre alanlar� zorunludur.");
                TempData["Error"] = "E-posta ve �ifre alanlar� zorunludur.";
                return View();
            }

            try
            {
                var settings = new Settings();
                string hashedPassword = settings.HashPassword(Sifre);

                string query = "INSERT INTO Users (Ad, Soyad, Email, Sifre,Durum) VALUES (@p0, @p1, @p2, @p3,0)";

                // DatabaseHelper nesnesi olu�tur
                var dbHelper = new DatabaseHelper();

                // SQL komutunu �al��t�r
                int result = dbHelper.ExecuteNonQuery(query, Ad, Soyad, Email, hashedPassword);

                if (result > 0)
                {
                    TempData["Success"] = "�yelik ba�ar�l�.";

                }
                else
                {
                    TempData["Error"] = "�yelik ba�ar�s�z oldu.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Bir hata olu�tu: " + ex.Message;
            }

            return View();
        }


        [HttpGet]
        [Route("CvOlustur")]
        public IActionResult CvOlustur()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("GirisYap");
            }
            return View();
        }





        
        [HttpPost]
        [Route("/cv-olustur")]
        public async Task<IActionResult> CvOlustur(CVModel model)
        {
           
            var _db = new DatabaseHelper();
            var userId = HttpContext.Session.GetString("UserId");

            var aiService = new AIServices(new HttpClient());
            var duzenlenmisModel = await aiService.ProcessCvFormWithAI(model);

            _db.ExecuteQuery(@"
        INSERT INTO KisiselBilgiler (UserId, Isim, Soyisim, Email, Telefon, Sehir, Adres, Cinsiyet, DogumTarihi, Aciklama) 
        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)",
                userId, duzenlenmisModel.Isim, duzenlenmisModel.Soyisim, duzenlenmisModel.Email, duzenlenmisModel.Telefon,
                duzenlenmisModel.Sehir, duzenlenmisModel.Adres, duzenlenmisModel.Cinsiyet, duzenlenmisModel.DogumTarihi,
                duzenlenmisModel.Aciklama);

            _db.ExecuteQuery(@"
        INSERT INTO Egitim (UserId, OkulAdi, Derece, Sehir, Baslangic, Bitis, Aciklama) 
        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6)",
                userId, duzenlenmisModel.OkulAdi, duzenlenmisModel.Derece, duzenlenmisModel.SehirEgitim,
                duzenlenmisModel.Baslangic, duzenlenmisModel.Bitis, duzenlenmisModel.AciklamaEgitim);

            _db.ExecuteQuery(@"
        INSERT INTO Deneyimler (UserId, Sirket, Pozisyon, Sure, Aciklama) 
        VALUES (@p0, @p1, @p2, @p3, @p4)",
                userId, duzenlenmisModel.Sirket, duzenlenmisModel.Pozisyon, duzenlenmisModel.Sure,
                duzenlenmisModel.IsAciklama);

            _db.ExecuteQuery(@"
        INSERT INTO Sertifikalar (UserId, SertifikaAdi, SertifikaKurum, Aciklama) 
        VALUES (@p0, @p1, @p2, @p3)",
                userId, duzenlenmisModel.SertifikaAdi, duzenlenmisModel.SertifikaKurum,
                duzenlenmisModel.AciklamaSertifika);

            return RedirectToAction("CvBasarili");
        }



        [HttpGet]
        [Route("/HesapBilgilerim")]
        public IActionResult HesapBilgilerim()
        {


            var userId = HttpContext.Session.GetString("UserId");
           
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Giris");

            using (var db = new DatabaseHelper())
            {

                var kisisel = db.ExecuteReader("SELECT * FROM KisiselBilgiler WHERE UserId = @p0", userId).FirstOrDefault();

              
                if (kisisel != null)
                {
                    ViewBag.Isim = kisisel["Isim"]?.ToString();
                    ViewBag.Soyisim = kisisel["Soyisim"]?.ToString();
                    ViewBag.Email = kisisel["Email"]?.ToString();
                    ViewBag.Telefon = kisisel["Telefon"]?.ToString();
                    ViewBag.Sehir = kisisel["Sehir"]?.ToString();
                    ViewBag.Adres = kisisel["Adres"]?.ToString();
                    ViewBag.Cinsiyet = kisisel["Cinsiyet"]?.ToString();
                    ViewBag.DogumTarihi = kisisel["DogumTarihi"]?.ToString();
                    ViewBag.Aciklama = kisisel["Aciklama"]?.ToString();
                }

                var egitim = db.ExecuteReader("SELECT * FROM Egitim WHERE UserId = @p0", userId).FirstOrDefault();
                if (egitim != null)
                {
                    ViewBag.OkulAdi = egitim["OkulAdi"]?.ToString();
                    ViewBag.Derece = egitim["Derece"]?.ToString();
                    ViewBag.SehirEgitim = egitim["Sehir"]?.ToString();
                    ViewBag.Baslangic = egitim["Baslangic"]?.ToString();
                    ViewBag.Bitis = egitim["Bitis"]?.ToString();
                    ViewBag.AciklamaEgitim = egitim["Aciklama"]?.ToString();
                }

                var deneyim = db.ExecuteReader("SELECT * FROM Deneyimler WHERE UserId = @p0", userId).FirstOrDefault();
                if (deneyim != null)
                {
                    ViewBag.Sirket = deneyim["Sirket"]?.ToString();
                    ViewBag.Pozisyon = deneyim["Pozisyon"]?.ToString();
                    ViewBag.Sure = deneyim["Sure"]?.ToString();
                    ViewBag.IsAciklama = deneyim["Aciklama"]?.ToString();
                }

                var sertifika = db.ExecuteReader("SELECT * FROM Sertifikalar WHERE UserId = @p0", userId).FirstOrDefault();
                if (sertifika != null)
                {
                    ViewBag.SertifikaAdi = sertifika["SertifikaAdi"]?.ToString();
                    ViewBag.SertifikaKurum = sertifika["SertifikaKurum"]?.ToString();
                    ViewBag.AciklamaSertifika = sertifika["Aciklama"]?.ToString();
                }
            }


        
          

            return View();
        }






        [HttpPost]
        [Route("/cv-pdf-olustur")]
        public IActionResult CvPdfOlustur()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Giris");

            // Veritabanından veriler çekiliyor (aynı kalabilir)
            string isim = "", soyisim = "", email = "", telefon = "", sehir = "", adres = "", cinsiyet = "", dogumTarihi = "", aciklama = "";
            string okulAdi = "", derece = "", sehirEgitim = "", baslangic = "", bitis = "", aciklamaEgitim = "";
            string sirket = "", pozisyon = "", sure = "", isAciklama = "";
            string sertifikaAdi = "", sertifikaKurum = "", aciklamaSertifika = "";

            using (var db = new DatabaseHelper())
            {
                var kisisel = db.ExecuteReader("SELECT * FROM KisiselBilgiler WHERE UserId = @p0", userId).FirstOrDefault();
                if (kisisel != null)
                {
                    isim = kisisel["Isim"]?.ToString();
                    soyisim = kisisel["Soyisim"]?.ToString();
                    email = kisisel["Email"]?.ToString();
                    telefon = kisisel["Telefon"]?.ToString();
                    sehir = kisisel["Sehir"]?.ToString();
                    adres = kisisel["Adres"]?.ToString();
                    cinsiyet = kisisel["Cinsiyet"]?.ToString();
                    dogumTarihi = kisisel["DogumTarihi"]?.ToString();
                    aciklama = kisisel["Aciklama"]?.ToString();
                }

                var egitim = db.ExecuteReader("SELECT * FROM Egitim WHERE UserId = @p0", userId).FirstOrDefault();
                if (egitim != null)
                {
                    okulAdi = egitim["OkulAdi"]?.ToString();
                    derece = egitim["Derece"]?.ToString();
                    sehirEgitim = egitim["Sehir"]?.ToString();
                    baslangic = egitim["Baslangic"]?.ToString();
                    bitis = egitim["Bitis"]?.ToString();
                    aciklamaEgitim = egitim["Aciklama"]?.ToString();
                }

                var deneyim = db.ExecuteReader("SELECT * FROM Deneyimler WHERE UserId = @p0", userId).FirstOrDefault();
                if (deneyim != null)
                {
                    sirket = deneyim["Sirket"]?.ToString();
                    pozisyon = deneyim["Pozisyon"]?.ToString();
                    sure = deneyim["Sure"]?.ToString();
                    isAciklama = deneyim["Aciklama"]?.ToString();
                }

                var sertifika = db.ExecuteReader("SELECT * FROM Sertifikalar WHERE UserId = @p0", userId).FirstOrDefault();
                if (sertifika != null)
                {
                    sertifikaAdi = sertifika["SertifikaAdi"]?.ToString();
                    sertifikaKurum = sertifika["SertifikaKurum"]?.ToString();
                    aciklamaSertifika = sertifika["Aciklama"]?.ToString();
                }
            }

            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 50, 50, 50, 50);
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                doc.Open();

                // Fontlar
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK);
                var sectionFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13, BaseColor.DARK_GRAY);
                var textFont = FontFactory.GetFont(FontFactory.HELVETICA, 11, BaseColor.BLACK);

                // Başlık
                doc.Add(new Paragraph($"{isim} {soyisim}", titleFont));
                doc.Add(new Paragraph($"{email} | {telefon} | {sehir}", textFont));
                doc.Add(new Paragraph($"{adres}", textFont));
                doc.Add(new Paragraph(" "));
                AddLine(doc);

                // KİŞİSEL BİLGİLER
                doc.Add(new Paragraph("Kisisel Bilgiler", sectionFont));
                doc.Add(new Paragraph($"Cinsiyet: {cinsiyet}", textFont));
                doc.Add(new Paragraph($"Doğum Tarihi: {dogumTarihi}", textFont));
                doc.Add(new Paragraph($"Hakkımda: {aciklama}", textFont));
                doc.Add(new Paragraph(" "));
                AddLine(doc);

                // EĞİTİM
                doc.Add(new Paragraph("Egitim Bilgileri", sectionFont));
                doc.Add(new Paragraph($"Okul: {okulAdi}", textFont));
                doc.Add(new Paragraph($"Derece: {derece}", textFont));
                doc.Add(new Paragraph($"Şehir: {sehirEgitim}", textFont));
                doc.Add(new Paragraph($"Dönem: {baslangic} - {bitis}", textFont));
                doc.Add(new Paragraph($"Açıklama: {aciklamaEgitim}", textFont));
                doc.Add(new Paragraph(" "));
                AddLine(doc);

                // DENEYİM
                doc.Add(new Paragraph("Is Deneyimi", sectionFont));
                doc.Add(new Paragraph($"Şirket: {sirket}", textFont));
                doc.Add(new Paragraph($"Pozisyon: {pozisyon}", textFont));
                doc.Add(new Paragraph($"Süre: {sure}", textFont));
                doc.Add(new Paragraph($"İş Tanımı: {isAciklama}", textFont));
                doc.Add(new Paragraph(" "));
                AddLine(doc);

                // SERTİFİKALAR
                doc.Add(new Paragraph("Sertifikalar", sectionFont));
                doc.Add(new Paragraph($"Sertifika: {sertifikaAdi}", textFont));
                doc.Add(new Paragraph($"Kurum: {sertifikaKurum}", textFont));
                doc.Add(new Paragraph($"Açıklama: {aciklamaSertifika}", textFont));

                doc.Close();
                return File(ms.ToArray(), "application/pdf", "cv.pdf");
            }
        }

        private void AddLine(Document doc)
        {
            var line = new iTextSharp.text.pdf.draw.LineSeparator(1f, 100f, BaseColor.LIGHT_GRAY, Element.ALIGN_CENTER, -1);
            doc.Add(new Chunk(line));
            doc.Add(new Paragraph(" "));
        }











    }
}



