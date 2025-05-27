using CvMakerAi.Models;
using DinkToPdf.Contracts;
using DinkToPdf;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// HttpClient servisleri
builder.Services.AddHttpClient<AIServices>();



// DinkToPdf converter
builder.Services.AddSingleton<IConverter, SynchronizedConverter>(provider =>
    new SynchronizedConverter(new PdfTools()));

// HttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Session



// DinkToPdf converter'ý ekle
builder.Services.AddSingleton<IConverter>(new SynchronizedConverter(new PdfTools()));


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Middleware sýralamasý doðru olmalý
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
