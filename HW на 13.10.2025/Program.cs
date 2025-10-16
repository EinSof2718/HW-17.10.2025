
namespace HW_на_13._10._2025
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите путь к папке: ");
            string folderPath = Console.ReadLine();

            var dictionaryCreator = new WordFreqencyDictionary();

            var frequencyDictionary = dictionaryCreator.Create(folderPath);

            Console.WriteLine($"\nЧастотный словарь успешно создан");
            Console.WriteLine($"Обработано файлов: {Directory.GetFiles(folderPath, "*.txt").Length}");
            Console.WriteLine($"Уникальных слов: {frequencyDictionary.Count}");

            dictionaryCreator.Print(frequencyDictionary);

            string resFileName = "result.txt";
            dictionaryCreator.Save(frequencyDictionary, resFileName);
            Console.WriteLine($"\n\nРезультат сохранен в: {Path.GetFullPath(resFileName)}");


            Console.WriteLine("Add to Second commit");
        }//Main
    }
}
