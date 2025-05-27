using System.Net.Http;
using System.Threading.Tasks;
using CvMakerAi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;

namespace CvMakerAi.Models
{
    public class AIServices
    {
        private readonly HttpClient _httpClient;

        public AIServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "GPT TOKEN");
        }

        public async Task<CVModel> ProcessCvFormWithAI(CVModel model)
        {
            var prompt = $@"
Aşağıdaki verileri bir özgeçmiş formatında düzelt:
- Yazım ve dilbilgisi hatalarını düzelt.
- Açıklamalar Kısa Olduğu için onları uzun uzun  detaylandır.
- Pozisyonları anlamlı hale getir.

Veri:
{JsonConvert.SerializeObject(model)}

Yanıtı JSON formatında aynı yapıda döndür.";

            var requestContent = new
            {
                model = "gpt-4",
                messages = new[]
                {
                    new { role = "system", content = "Sen bir CV editörüsün." },
                    new { role = "user", content = prompt }
                },
                temperature = 0.7
            };

            var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"OpenAI isteği başarısız: {errorContent}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var parsed = JObject.Parse(json);
            var content = parsed["choices"]?[0]?["message"]?["content"]?.ToString();

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new Exception("OpenAI'den gelen içerik boş.");
            }

            try
            {
                return JsonConvert.DeserializeObject<CVModel>(content);
            }
            catch (JsonException ex)
            {
                throw new Exception("OpenAI'den gelen cevap JSON formatında değil: " + ex.Message);
            }
        }
















    }
}
