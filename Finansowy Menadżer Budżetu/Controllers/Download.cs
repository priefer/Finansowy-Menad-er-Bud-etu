using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Finansowy_Menadżer_Budżetu.Controllers
{
    public class Download : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public Download(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;

        }

        public IActionResult Index(string nazwaPliku)
        {
            // Ścieżka do katalogu wwwroot/uploads
            var sciezkaDoKatalogu = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

            // Ścieżka do pliku
            var sciezkaDoPliku = Path.Combine(sciezkaDoKatalogu, nazwaPliku);

            // Sprawdź, czy plik istnieje
            if (!System.IO.File.Exists(sciezkaDoPliku))
            {
                return NotFound(); // 404 Not Found
            }

            // Pobierz plik
            var typPliku = "application/octet-stream"; // Domyślny typ MIME dla nieznanych plików
            return PhysicalFile(sciezkaDoPliku, typPliku, enableRangeProcessing: true, fileDownloadName: nazwaPliku);

        }
    }
}
