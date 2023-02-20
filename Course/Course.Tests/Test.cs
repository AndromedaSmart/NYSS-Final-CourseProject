using NUnit.Framework;
using Course.Tools;

namespace Course.Course.Tests
{
    [TestFixture]
    public class Test
    {
        private readonly char[] rusAlph = new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };

        private Cipher _encryptor;

        [SetUp]
        public void Setup()
        {
            _encryptor = new Cipher(rusAlph);
        }

        [TestCase("поздравляю", "овен", "юрмсявжщна")]
        [TestCase("ИСХОДНЫЙ ТЕКСТ", "этовсе", "ёддрхтшь бжьцп")]
        [TestCase("от лица компании firstlinesoftware и университета итмо", "абоба", "оу ъйца лэнпаочй firstlinesoftware и уочгертчуетб чумо")]
        public void Encryption_Test(string input, string keyword, string output)
        {
            Assert.AreEqual(_encryptor.Encrypt(input, keyword), output);
        }


        [TestCase("юрмСЯВжщна", "овен", "поздравляю")]
        [TestCase("ЁДДРХТШЬ бжьцп", "этовсе", "исходный текст")]
        [TestCase("оу ъйца лэнпаочй firstlinesoftware и уочгертчуетб чумо", "абоба", "от лица компании firstlinesoftware и университета итмо")]
        public void Decryption_Test(string input, string keyword, string output)
        {
            Assert.AreEqual(_encryptor.Decrypt(input, keyword), output);
        }

    }
}
