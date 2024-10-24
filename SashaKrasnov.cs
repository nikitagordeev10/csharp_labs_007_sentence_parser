using System.Collections.Generic;
using System;
using System.Linq;
namespace TextAnalysis {
    static class FrequencyAnalysisTask {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text) {
            Dictionary<string, int> cheeze3 = RepairGram(AllGrams(text));
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (text.Count == 0) return result;
            foreach (var item in cheeze3) {
                string[] keysSplited = item.Key.ToString().Split(':');
                result.Add(keysSplited[0], keysSplited[1]);
            }
            return result;
        }
        static Dictionary<string, int> AllGrams(List<List<string>> text) {
            Dictionary<string, int> book = new Dictionary<string, int>();
            List<string> twoGrams = new List<string>();
            if (text.Count == 0) return book;
            for (int i = 0; i < text.Count; i++) {
                for (int j = 0; j < text[i].Count - 1; j++) {

                    twoGrams.Add(text[i][j] + ':' + text[i][j + 1]);

                }
            }
            for (int i = 0; i < twoGrams.Count; i++) {
                if (book.ContainsKey(twoGrams[i])) book[twoGrams[i]] += 1;
                else book.Add(twoGrams[i], 1);
            }
            List<string> threeGrams = new List<string>();
            for (int i = 0; i < text.Count; i++) {
                if (text[i].Count > 2) {
                    for (int j = 0; j < text[i].Count - 2; j++) {

                        threeGrams.Add(text[i][j] + ' ' + text[i][j + 1] + ':' + text[i][j + 2]);

                    }
                }
            }
            for (int i = 0; i < threeGrams.Count; i++) {
                if (book.ContainsKey(threeGrams[i])) book[threeGrams[i]] += 1;
                else book.Add(threeGrams[i], 1);
            }
            return book;
        }
        static Dictionary<string, int> RepairGram(Dictionary<string, int> text) {
            if (text.Count < 2) return text;
            foreach (var value1 in text.ToList()) {
                string[] values1 = value1.Key.Split(':');
                foreach (var value2 in text.ToList()) {
                    string[] values2 = value2.Key.Split(':');
                    if (values1[0] == values2[0]) {
                        if (value2.Value > value1.Value) text.Remove(value1.Key);
                        else if (value2.Value < value1.Value) text.Remove(value2.Key);
                        else if (String.Compare(values1[0] + values1[1], values2[0] + values2[1]) < 0) text.Remove(value2.Key);
                        else if (String.Compare(values1[0] + values1[1], values2[0] + values2[1]) > 0) text.Remove(value1.Key);
                    }
                }
            }
            return text;
        }
    }
}