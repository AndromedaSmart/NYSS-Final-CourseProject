using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using Course.Tools;
using Xceed.Words.NET;
using Xceed.Document.NET;

namespace Course.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly char[] rusAlph = new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
        private readonly char[] engAlph = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private char[] alph;

        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public string Input { get; set; }
        public string Keyword { get; set; }
        public string Output { get; set; }
        [BindProperty]
        public IFormFile InputFile { get; set; }
        [BindProperty]
        public string SelectedAlph { get; set; }
        [BindProperty]
        public string SelectedFunc { get; set; }


        public IndexModel(ILogger<IndexModel> logger,
            IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            this.webHostEnvironment = webHostEnvironment;
        }

        public void OnPostAsync(string input, string keyword)
        {
            alph = (SelectedAlph == "rus") ? rusAlph : engAlph;

            if (InputFile != null)
            {
                FileParser();
            }
            else
            {
                Input = input;
            }

            var encryptor = new Cipher(alph);

            Keyword = keyword;

            if (string.IsNullOrEmpty(Input) || string.IsNullOrEmpty(Keyword))
            {
                return;
            }

            try
            {
                Output = (SelectedFunc == "enc") ? encryptor.Encrypt(Input, Keyword) : encryptor.Decrypt(Input, Keyword);
            }
            catch
            {
                _logger.LogError("Ошибка алфавита");
                Output = Input;
            }

            GenerateFiles();
        }

        private void GenerateFiles()
        {
            var uploadFolder = Path.Combine(webHostEnvironment.WebRootPath,
                    "UploadedFiles");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var filePathTxt = Path.Combine(uploadFolder, "output.txt");
            var filePathDocx = Path.Combine(uploadFolder, "output.docx");

            using (var fs = new FileStream(filePathTxt, FileMode.Create))
            {
                var array = Encoding.Default.GetBytes(Output);
                fs.Write(array, 0, array.Length);
            }

            using (DocX doc = DocX.Create(filePathDocx))
            {
                Paragraph title = doc.InsertParagraph();
                title.Append(Output);
                doc.Save();
            }

        }

        private void FileParser()
        {
            var extension = InputFile.FileName.Split('.')[1];
            var uploadFolder = Path.Combine(webHostEnvironment.WebRootPath,
                    "UploadedFiles");
            var filePath = Path.Combine(uploadFolder, "input." + extension);

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                InputFile.CopyTo(fs);
            }
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (extension == "txt")
            {
                Input = System.IO.File.ReadAllText(filePath);
                if (Input.Contains('�'))
                {
                    Input = System.IO.File.ReadAllText(filePath, Encoding.GetEncoding(1251));
                }
            }
            else
            {
                try
                {
                    using (var document = DocX.Load(filePath))
                    {
                        Input = document.Text;
                    }
                }
                catch
                {
                    _logger.LogError("Ошибка ввода docx");
                }
            }
        }
    }
}