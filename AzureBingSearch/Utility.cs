using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace AzureBingSearch
{
	public class Utility
	{

		public static string[,] KeyWordWithWeightage()
		{
			Console.WriteLine("current exexuting path " + Environment.CurrentDirectory);
			string filepath = AppDomain.CurrentDomain.BaseDirectory + "bin\\FakeNewsSynonyms.txt";
			//string[] lines = File.ReadAllLines(@"C:\Users\v-abumar\source\repos\AIConsole\AzureBingSearch\FakeNewsSynonyms.txt");
			string[] lines = File.ReadAllLines(filepath);
			int i = 0;
			string[,] s = new string[lines.Length, 2];
			foreach (string line in lines)
			{
				// Use a tab to indent each line of the file.
				//Console.WriteLine($"{i} : {line}");
				if (string.IsNullOrEmpty(line))
					continue;
				string[] splitdata = line.Trim().Split('|');
				if (splitdata.Length > 1)
				{
					s[i, 0] = splitdata[0];
					s[i, 1] = splitdata[1];
					i++;
				}
			}

			return s;

		}

		public static List<KeysWithWeightageModel> List()
		{
			string[,] keyswithweightage = KeyWordWithWeightage();
			List<KeysWithWeightageModel> KeysData = new List<KeysWithWeightageModel>();
			for (int i = 0; i < keyswithweightage.GetLength(0); i++)
			{
				if (!string.IsNullOrEmpty(keyswithweightage[i, 0]))
				{
					KeysData.Add(new KeysWithWeightageModel()
					{
						Key = keyswithweightage[i, 0].ToLower(),
						Weightage = Convert.ToInt32(keyswithweightage[i, 1])
					});
				}
			}

			return KeysData;

		}

		public static Dictionary<string, int> FoundKeysWordsWeightage(string str, List<KeysWithWeightageModel> keywordslist)
		{
			Dictionary<string, int> WeightageDict = new Dictionary<string, int>();
			foreach (var item in keywordslist)
			{
				int matchcount = Regex.Matches(str, item.Key.Trim()).Count;
				if (matchcount > 0)
				{
					WeightageDict.Add(item.Key, item.Weightage * matchcount);
				}
			}

			return WeightageDict;
		}
	}

	public class KeysWithWeightageModel
	{
		public string Key { get; set; }
		public int Weightage { get; set; }
	}
}
