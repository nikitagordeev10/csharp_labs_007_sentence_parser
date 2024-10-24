using System.Collections.Generic;
using System.Text;

namespace TextAnalysis {
    static class FrequencyAnalysisTask {
        //
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text) {
            //
            var unSort = new Dictionary<string, Dictionary<string, int>>();
            var result = new Dictionary<string, string>();
            var firstWords = new StringBuilder();

            // Встречаемость слов по отдельности 
            unSort = CreateDictionary(text, unSort, 1);
            
            // Встречаемость биграм
            var forSort = CreateDictionary(text, unSort, 2);
            
            // 
            return SortDictionary(forSort, result, firstWords);
        }

        //
        public static Dictionary<string, Dictionary<string, int>> CreateDictionary(List<List<string>> text, Dictionary<string, Dictionary<string, int>> unSort, int a) {
            //
            foreach (var sentence in text) {
                for (var i = 0; i < sentence.Count - a; i++) {
                    var bond = "";
                    var val = "";
                    //
                    if (a == 2) {
                        bond = sentence[i] + " " + sentence[i + 1];
                        val = sentence[i + 2];
                    } else {
                        bond = sentence[i];
                        val = sentence[i + 1];
                    }
                    //
                    if (unSort.ContainsKey(bond))
                        if (unSort[bond].ContainsKey(val))
                            unSort[bond][val]++;
                        else
                            unSort[bond][val] = 1;
                    else
                        unSort[bond] = new Dictionary<string, int> { { val, 1 } };
                }
            }
            return unSort;
        }

        // Объединяю биграмы и триграмы в один список nграм
        public static Dictionary<string, string> SortDictionary(Dictionary<string, Dictionary<string, int>> forSort, Dictionary<string, string> result, StringBuilder firstWords) {
            // 
            var maxValue = 0;
            var endWord = "";
            //
            foreach (var pair in forSort) {
                //
                firstWords.Append(pair.Key);
                //
                foreach (var value in forSort[pair.Key]) {
                    if (value.Value > maxValue) {
                        maxValue = value.Value;
                        endWord = value.Key;
                    } else if (value.Value == maxValue)
                        
                        // лексикографическое сравнения
                        if (string.CompareOrdinal(value.Key, endWord.ToString()) < 0)
                            endWord = value.Key;
                }
                //
                result.Add(firstWords.ToString(), endWord.ToString());
                maxValue = 0;
                endWord = "";
                firstWords.Clear();
            }
            //
            return result;
        }
    }
}



/*
 * Подсказки
 * В этой задаче важно придумать декомпозицию всей задачи на 3-4 подзадачи. 
 * Первая подзадача — построить частотный словарь: начало N-граммы → последнее слово N-граммы → количество повторов этой N-граммы
 * Вторая подзадача — по началу N-граммы выбирать самое частное продолжение.
 * Третья подзадача — собрать результаты остальных подзадач в решение всей задачи.
 */

/* 
 * Материалы:
 * String.CompareOrdinal Метод // Microsoft URL: https://learn.microsoft.com/ru-Ru/dotnet/api/system.string.compareordinal?view=net-5.0 (дата обращения: 18.12.2022).
 * Улучшить код поиска частотности словосочетаний // cyberforum URL: https://www.cyberforum.ru/csharp-beginners/thread2348594.html (дата обращения: 18.12.2022).
 * Частотность N-грамм // pastebin URL: https://pastebin.com/dmw8KCzY (дата обращения: 18.12.2022).
 * Проблема с памятью // stackoverflow URL: https://ru.stackoverflow.com/questions/1037316/Проблема-с-памятью (дата обращения: 18.12.2022).
 * Dictionary<TKey,TValue>.ContainsKey(TKey) Метод // Microsoft URL: https://learn.microsoft.com/ru-ru/dotnet/api/system.collections.generic.dictionary-2.containskey?view=net-6.0 (дата обращения: 18.12.2022).
 * Проблема с памятью // StackOverflow URL: https://ru.stackoverflow.com/questions/1037316/Проблема-с-памятью (дата обращения: 18.12.2022).
 * Двойной словарь // StackOverflow URL: https://ru.stackoverflow.com/questions/827941/Двойной-словарь (дата обращения: 18.12.2022).
 */