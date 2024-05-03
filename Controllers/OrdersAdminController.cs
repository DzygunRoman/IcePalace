using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcePalace.Areas.Identity.Data;
using AcePalace.Models;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.IO;
using System.Text;

using iText.IO.Source;
using iText.Kernel.Geom;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.IO.Font;
using iText.Layout.Font;
using Org.BouncyCastle.Crypto.Engines;
using NuGet.Configuration;
using iText.Layout;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Forms.Form.Element;

namespace AcePalace.Controllers
{
    public class OrdersAdminController : Controller
    {
        private readonly AcePalaceContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public OrdersAdminController(AcePalaceContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;            
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DateTime startTime,DateTime finishTime, OrderAdminOtchet otchet)
        {
            var data = new OrderAdminOtchet
            {
                OrderDetails = _context.OrderDetails
                            .Include(x => x.Product)
                            .ThenInclude(x => x.Category)
                            .Where(x => x.ProductId == x.Product.ProductID)
                            .GroupBy(x => new { x.DateTime, x.UnitPrice, x.ProductId })
                            .Select(g => new OrderDetail
                            {
                                DateTime = g.Key.DateTime,
                                ProductId = g.Key.ProductId,
                                UnitPrice = g.Key.UnitPrice,
                                Quantity = g.Sum(x => x.Quantity)
                            }).OrderBy(x => x.DateTime)                           
                            .ToList(),
                Shedules =  _context.Shedules.OrderBy(s => s.DateTime).ToList()               
            };
            otchet.startTime = startTime;
            ViewBag.StartTime = startTime; ViewBag.FinishTime = finishTime;
            otchet.finishTime = finishTime;
            
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileResult Export(string GridHtml)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(GridHtml)))
            {
               
                WriterProperties writerProperties = new WriterProperties();
                writerProperties.AddXmpMetadata();
                ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
                PdfWriter writer = new PdfWriter(byteArrayOutputStream);
                PdfDocument pdfDocument = new PdfDocument(writer);
                pdfDocument.GetCatalog().SetLang(new PdfString("ru-RU"));
                pdfDocument.SetTagged();
                pdfDocument.GetCatalog().SetViewerPreferences(new PdfViewerPreferences().SetDisplayDocTitle(true));
                pdfDocument.SetDefaultPageSize(PageSize.A3);
                ConverterProperties props = new ConverterProperties();
                FontProvider fontProvider = new FontProvider();
                fontProvider.AddFont("C:\\Windows\\Fonts\\Arial.ttf", PdfEncodings.IDENTITY_H);
                props.SetFontProvider(fontProvider);                
                HtmlConverter.ConvertToPdf(stream, pdfDocument,props);
                pdfDocument.Close();
                return File(byteArrayOutputStream.ToArray(), "application/pdf", "Grid.pdf");
            }           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileResult ExportXl(string GridHtml)
        {
            return File(Encoding.UTF8.GetBytes(GridHtml), "application/vnd.ms-excel", "Grid.xls");
        }
    }
}
