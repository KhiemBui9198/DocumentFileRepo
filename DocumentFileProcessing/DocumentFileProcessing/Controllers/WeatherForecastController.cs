using iText.IO.Image;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf;
using iText.Layout;
using Microsoft.AspNetCore.Mvc;
using iText.Layout.Element;
using iText.Kernel.Geom;
using DocumentFileProcessing.Data.Entities.DocumentData;
using System.IO;

namespace DocumentFileProcessing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddImgToPdf(IFormFile imgFile, IFormFile files)
        {
            //   var FileOutPut =  await _fileUploadRespository.CreateNewBlankfile("sad");
            var fileImgModel = new FileOnDataBaseModel();
            using (var dataStream = new MemoryStream())
            {
                await imgFile.CopyToAsync(dataStream);
                fileImgModel.Data = dataStream.ToArray();
            };
            var fileDocumentModel = new FileOnFileSystemModel();
            var basePath = System.IO.Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
            bool basePathExists = System.IO.Directory.Exists(basePath);
            if (!basePathExists) Directory.CreateDirectory(basePath);
            var fileName = System.IO.Path.GetFileNameWithoutExtension(files.FileName);
            var filePath = System.IO.Path.Combine(basePath, files.FileName);
            var extension = System.IO.Path.GetExtension(files.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await files.CopyToAsync(stream);
            }
            var fileModel = new FileOnFileSystemModel
            {
                CreatedAt = DateTime.UtcNow,
                FileType = files.ContentType,
                Extension = extension,
                Name = fileName,
                Description = "s",
                FilePath = filePath
            };
             var myStream = new FileStream(fileModel.FilePath, FileMode.Open);
            PdfDocument pdfDocument = new PdfDocument(new PdfReader(myStream), new PdfWriter("C:\\KMData\\Respositories\\PDF\\UploadFileToDB\\DocumentFileProcessing\\DocumentFileProcessing\\FileResult\\Result_" + fileName + ".pdf"));
            Image imgs = new Image(ImageDataFactory.Create(fileImgModel.Data));
            int n = pdfDocument.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                PdfPage page = pdfDocument.GetPage(i);
                Rectangle area = new Rectangle(40, 40, 60, 60);
                PdfCanvas aboveCanvas = new PdfCanvas(page.NewContentStreamAfter(),
                        page.GetResources(), pdfDocument);
                new Canvas(aboveCanvas, area)
                       .Add(imgs);
            }
            Document document = new Document(pdfDocument);
            document.Close();
            string newPath = System.IO.Path.GetFullPath("FileResult\\Result_" + fileName + ".pdf");
            var memory = new MemoryStream();
            using (var stream = new FileStream(newPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/pdf", "Result_" + fileName + extension);
        }
    }
}