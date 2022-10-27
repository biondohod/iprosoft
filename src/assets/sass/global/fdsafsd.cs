using System.Collections.Generic;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            var nGrammDictionary = new Dictionary<string, Dictionary<string, int>>();
            var counterOfDictionary = new Dictionary<string, int>();
            var nGrammMaxCountDictionary = new Dictionary<string, List<string>>();
            for (int i = 0; i < text.Count; i++)
            {
                for (int j = 0; j < text[i].Count - 1; j++)
                {
                    string key, value;
                    if (text[i].Count - 3 >= j)
                    {
                        key = text[i][j] + " " + text[i][j + 1];
                        value = text[i][j + 2];
                        BuildNGramm(key, value, nGrammDictionary, counterOfDictionary, nGrammMaxCountDictionary);
                    }
                    key = text[i][j];
                    value = text[i][j + 1];
                    BuildNGramm(key, value, nGrammDictionary, counterOfDictionary, nGrammMaxCountDictionary);
                }
            }
            FillnGrammMaxCountDictionary(counterOfDictionary, nGrammMaxCountDictionary, nGrammDictionary);
            CompareOrdinal(nGrammMaxCountDictionary, result);
            return result;
        }

        static void BuildNGramm(string key, string value,
								Dictionary<string, Dictionary<string,int>> nGrammDictionary,
								Dictionary<string, int> counterOfDictionary,
								Dictionary<string, List<string>> nGrammMaxCountDictionary)
        {
            if (!nGrammDictionary.ContainsKey(key))
            {
                nGrammDictionary[key] = new Dictionary<string, int>();
                counterOfDictionary[key] = 1;
                nGrammMaxCountDictionary[key] = new List<string>();
            }

            if (!nGrammDictionary[key].ContainsKey(value))
                nGrammDictionary[key].Add(value, 1);
            else
            {
                nGrammDictionary[key][value]++;
                if (nGrammDictionary[key][value] > counterOfDictionary[key])
                    counterOfDictionary[key] = nGrammDictionary[key][value];
            }
        }

        static void CompareOrdinal(Dictionary<string, List<string>> nGrammMaxCountDictionary,
								   Dictionary<string, string> result)
        {
            foreach (var item in nGrammMaxCountDictionary)
            {
                string best = "{";
                if (item.Value.Count == 1)
                    result[item.Key] = item.Value[0];
                else
                {
                    for (int i = 0; i<item.Value.Count; i++)
                        if (string.CompareOrdinal(item.Value[i], best) < 0)
                            best = item.Value[i];
                    result[item.Key] = best;
                }
            }
        }

        static void FillnGrammMaxCountDictionary
			(Dictionary<string, int> counterOfDictionary,
			 Dictionary<string, List<string>> nGrammMaxCountDictionary,
			 Dictionary<string, Dictionary<string, int>> nGrammDictionary)
        {
            foreach (var dictionary in nGrammDictionary)
                foreach (var count in dictionary.Value)
                    if (count.Value == counterOfDictionary[dictionary.Key])
                        nGrammMaxCountDictionary[dictionary.Key].Add(count.Key);
        }
    }
}
