using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDFConverter.Models;
using System.Web;

using System.IO;
using SautinSoft.Document;

namespace PDFConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConverterController : ControllerBase
    {
        // String basePath = "/home/gayan/Desktop/CiperLabs/unicode-pleco/Documents/";

        // GET: api/Converter
        [HttpGet]
        public IEnumerable<string> Get()
        {
            Console.WriteLine("Inside Get request");
            return new string[] { "value1", "valueer23"  };
        }

        // GET: api/Converter/5
       // [HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
          //  return "value";
        //}

        // POST: api/Converter
        [HttpPost]
        public String Post([FromForm] Converter converter)
        {   
            //string value = data.name;
            //return data.value;
            //return "fd";
            //    return data;
            Console.WriteLine(converter.DocxFilePath);
            Console.WriteLine("PDF File Path Is : "+ converter.PDFFilePath);
            Console.WriteLine("DOCX File Path is : " +  converter.DocxFilePath);
           // return converter.FilePath+" ded ";
            //return new HttpStatusCodeResult(200);
            int status = ConvertPdfToDocx( converter.PDFFilePath, converter.DocxFilePath);

            if(status == 1){
              return "failed";
            }
            return "success";

        }

        // PUT: api/Converter/5
      //  [HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        public static int ConvertPdfToDocx(string pdfFlePath, String docxFilePath)
        {
            string pdfFile = pdfFlePath; //Path.GetFullPath(pdfFlePath);
            string docxFile = docxFilePath;  //Path.GetFullPath(docxFilePath);
            //DocumentCore.Serial = "put your serial here";
            int status = 0;
            try
            {
                PdfLoadOptions pdfOptions = new PdfLoadOptions();
                pdfOptions.ConversionMode = PdfConversionMode.Flowing;
                pdfOptions.DetectTables = true;
                pdfOptions.RasterizeVectorGraphics = true;
                // pdfOptions.FromPage = 1;
                pdfOptions.KeepCharScaleAndSpacing = true;
                pdfOptions.PreserveImages  =true;
                //pdfOptions.PreserveEmbeddedFonts = true;
                // pdfOptions.OCRMode = OCRMode.Auto;
                System.Console.WriteLine(pdfFile.GetType());
                DocumentCore pdf = DocumentCore.Load(pdfFile, pdfOptions);
                pdf.Save(docxFile);
            }

            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                status = 1;
            }
            return status;
        }
    }
}

