using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
	static class SentencesParserTask
	{
		public static List<List<string>> ParseSentences(string text)
		{
			var rawSentences = text.ToLower().Split(".!?;:()".ToCharArray()); // разделение текста на предложения
			var sentences = rawSentences.Select(ParseSentence); // разделение предложений на слова

			return sentences.Where(s => s.Count > 0).ToList();
		}

		private static List<string> ParseSentence(string text)
		{
			var sentence = new List<string>();
			var word = new StringBuilder(); // класс, представляющий собой изменяемую строку
			foreach (var l in text)
			{
				if (char.IsLetter(l) || l == '\'')
				{
					word.Append(l);
				}
				else if (word.Length > 0)
				{
					sentence.Add(word.ToString());
					word.Length = 0;
				}
			}

			if (word.Length > 0)
			{
				sentence.Add(word.ToString()); // обработать последнее слово в предложении
			}

			return sentence;
		}
	}
}

//Подсказки:
//1) Разделить текст на предложения можно методом string.Split, указав ему все возможные разделители
//2) Чтобы разделить предложение на слова, придется написать проход по строке циклом. Лучше сразу выделить эту логику во вспомогательный метод
//3) Распространенная ошибка — забыть обработать последнее слово в предложении.
//4) Не забудьте отфильтровать предложения, в которых не оказалось слов.

/* Использованные материалы
 * Парсер предложений( // cyberforum.ru URL: https://www.cyberforum.ru/csharp-beginners/thread2348304.html (дата обращения: 04.11.2022).
 * Практика «Парсер предложений» после обновления // cyberforum URL: https://www.cyberforum.ru/csharp-beginners/thread2333400.html (дата обращения: 04.11.2022).
 * String.Trim Метод // Microsoft URL: https://learn.microsoft.com/ru-ru/dotnet/api/system.string.trim?view=xamarinandroid-7.1 (дата обращения: 04.11.2022).
 * Char.IsLetter Метод // Microsoft URL: https://learn.microsoft.com/ru-ru/dotnet/api/System.Char.IsLetter?view=net-6.0 (дата обращения: 04.11.2022).
 * оператор => (справочник по C#) // Microsoft URL: https://learn.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/lambda-operator (дата обращения: 04.11.2022).
 * Объяснение работы оператора => // cyberforum URL: https://www.cyberforum.ru/csharp-beginners/thread2343875.html (дата обращения: 04.11.2022).
 */