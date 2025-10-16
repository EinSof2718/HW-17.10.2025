using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HW_на_13._10._2025
{
    public class WordFreqencyDictionary
    {
        public Dictionary<string, int> Create(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
                throw new ArgumentNullException("Путь к папке не может быть пустым");

            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"Папка {folderPath} не найдена");
            
            var frequencyDictionary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var txtFiles = Directory.GetFiles(folderPath, "*.txt");

            if(txtFiles.Length==0)
                throw new FileNotFoundException($"В папке {folderPath} не найдено текстовых файлов");

            foreach( var txtFile in txtFiles)
                ProcessFile(txtFile, frequencyDictionary);

            return frequencyDictionary;
        }//Create

        public void ProcessFile(string txtFile, Dictionary<string, int> frequencyDictionary)
        {
            try
            {
                var content = File.ReadAllText(txtFile, Encoding.UTF8);
                var words = ExtractWords(content);

                foreach (var word in words)
                {
                    if(string.IsNullOrEmpty(word)) continue;

                    var lowRegistrWord = word.ToLowerInvariant();

                    if (frequencyDictionary.ContainsKey(lowRegistrWord))
                        frequencyDictionary[lowRegistrWord]++;

                    else frequencyDictionary[lowRegistrWord]=1;
                }//foreach
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"Ошибка при обработке файла {txtFile}: {ex.Message}");
            }//try-catch
        }//ProcessFile

        public IEnumerable<string> ExtractWords(string content)
        {
            string pattern = @"\b[a-zA-Zа-яА-ЯёЁ]+(?:[`-][a-zA-Zа-яА-ЯёЁ]+)*\b";
            var regex = new Regex(pattern);
            var matches = regex.Matches(content);
            var words = new List<string>();

            foreach (Match match in matches) 
                if(match.Success)
                    words.Add(match.Value.ToLowerInvariant());
            return words;
        }//ExtractWords

        public void Save(Dictionary<string, int> frequencyDictionary, string fileName)
        {
            if(frequencyDictionary == null)
                throw new ArgumentNullException(nameof(frequencyDictionary));

            if (string.IsNullOrEmpty(fileName)) 
                throw new ArgumentNullException("Не найден файл для сохранения");

            try
            {
                var sortedDict = frequencyDictionary.OrderByDescending(w=>w.Value).ThenBy(w=>w.Key);

                using var writer = new StreamWriter(fileName, false);
                writer.WriteLine("Частотный словарь\n\n");

                foreach(var pair in sortedDict)
                    writer.WriteLine($"{pair.Key}\t{pair.Value}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка при сохранении файла: {ex.Message}");
            }
        }//Save

        public void Print(Dictionary<string, int> frequencyDictionary)
        {
            Console.WriteLine("\n-----ЧАСТОТНЫЙ СЛОВАРЬ-----\n");

            var sortedDict = frequencyDictionary.OrderByDescending(w => w.Value).ThenBy(w => w.Key);

            int pos = 1;
            foreach(var pair in sortedDict)
            {
                Console.WriteLine($"{pos}) {pair.Key} - {pair.Value}-раз(а)");
                pos++;
            }//foreach
        }//Print
    }//WordFreqencyDictionary

}//HW_на_13._10._2025
