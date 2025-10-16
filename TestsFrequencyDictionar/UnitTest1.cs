
using HW_на_13._10._2025;

namespace TestsFrequencyDictionar
{
    public class Tests
    {
        WordFreqencyDictionary dictionary = new WordFreqencyDictionary();
        private void CreateTestFile(string fileName, string content)
        {
            File.WriteAllText(Path.Combine(fileName), content);
        }//CreateTestFile



        [SetUp]
        public void Setup()
        {
        }


        //Корректное создание словаря
        [Test]
        public void Test1()
        {
            CreateTestFile("test1_1.txt", "hello world hello");
            CreateTestFile("test1_2.txt", "world test");

            var res = dictionary.Create("C:\\Академия\\UnitTesting\\HW на 13.10.2025\\TestsFrequencyDictionar\\bin\\Debug\\net9.0");
            
            Assert.That(res.Count, Is.EqualTo(3));
            Assert.That(res["hello"], Is.EqualTo(2));
            Assert.That(res["world"], Is.EqualTo(2));
            Assert.That(res["test"], Is.EqualTo(1));
        }//Test1


        //Обработка пустой папки
        [Test] 
        public void Test2()
        {
            Assert.Throws<FileNotFoundException>(() => dictionary.Create("C:\\Академия\\UnitTesting\\HW на 13.10.2025\\Новая папка"));
        }//Test2

        //Обработка несуществующей папки
        [Test]
        public void Test3()
        {
            Assert.Throws<DirectoryNotFoundException>(() => dictionary.Create("C:\\Академия\\UnitTesting\\HW на 13.10.2025\\Новая папка_2"));
        }//Test3


        //Обработка регистра слов
        [Test]
        public void Test4()
        {
            CreateTestFile("C:\\Test\\folder\\test4.txt", "HELLO hello WORLD world");

            var res = dictionary.Create("C:\\Test\\folder");

            Assert.That(res.Count, Is.EqualTo(2));
            Assert.That(res["hello"], Is.EqualTo(2));
            Assert.That(res["world"], Is.EqualTo(2));
        }//Test4


        //Обработка апострофов
        [Test]
        public void Test5()
        {
            var text = "don`t can`t o`clock";
            var words = dictionary.ExtractWords(text);

            Assert.That(words.Count(), Is.EqualTo(3));
            Assert.That(words, Contains.Item("don`t"));
            Assert.That(words, Contains.Item("can`t"));
            Assert.That(words, Contains.Item("o`clock"));
        }//Test5

        //Обработка пустого текста
        [Test]
        public void Test6()
        {
            var words = dictionary.ExtractWords("");
            Assert.That(words.Count(), Is.EqualTo(0));
        }//Test6


        //Корректное извлечение слов
        [Test]
        public void Test7()
        {
            var text = "Hello, world! This is a test";
            var words = dictionary.ExtractWords(text);

            Assert.That(words.Count(), Is.EqualTo(6));
        }//Test7

        /*//Игнорирование чисел
        [Test]
        public void Test8()
        {
            var text = "word1 123 word2 456";
            var words = dictionary.ExtractWords(text);

            Assert.That(words.Count(), Is.EqualTo(2));
            Assert.That(words, Contains.Item("word1"));
            Assert.That(words, Contains.Item("word2"));
        }//Test8*/
    }
}
