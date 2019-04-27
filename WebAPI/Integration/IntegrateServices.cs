using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AzureBingSearch;
using NaturalLanguageProcessing;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using Microsoft.Azure.CognitiveServices.ContentModerator.Models;
using System.Text.RegularExpressions;

namespace WebAPI.Integration
{
	public class IntegrateServices
	{
		private static string _appPath => Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
		private static string _filePath => Path.Combine(_appPath, "..", "..", "TestSample\\");

		public static int IntegratedResult(string queryterms )
		{
			NLPHelper nlp = new NLPHelper();
			int finalPoint = 0;
			//string text = File.ReadAllText(_filePath + "sample.txt");
			KeyValuePair<string,int> resultFromNLP= nlp.POSIndirectResult(queryterms);

			if(resultFromNLP.Value>=80) //  this is news
			{
				string newterms= RemoveFakeTerm(queryterms);
				 finalPoint = BingSearch.BingNewsSearch(newterms);
			}
			return finalPoint;
			
		}

		public static string EvaluateImage(string urlToProcess)
		{

			var client = Clients.NewClient();
			EvaluationData imageData = ImageToText.EvaluateImage(client, urlToProcess);
		return	imageData.TextDetection.Text;

		}

		private int WordCount(string input)
		{
			string pattern = "[^\\w]";
			//get all spaces and other signs, like: '.' '?' '!'
			//string input = "This is a nice day. What about this? This tastes good. I saw a dog!";
			string[] words = null;
			int i = 0;
			int count = 0;
			Console.WriteLine(input);
			words = Regex.Split(input, pattern, RegexOptions.IgnoreCase);
			for (i = words.GetLowerBound(0); i <= words.GetUpperBound(0); i++)
			{
				if (words[i].ToString() == string.Empty)
					count = count - 1;
				count = count + 1;
			}
			return count;
			//Console.WriteLine("Count of words:" + count.ToString());
		}

		public static string RemoveFakeTerm( string input)
		{
			//int wordcount = WordCount(input);
			string removedfakewords = input.ToLower().Replace("fake", "");
			return removedfakewords;
		}


	}

	public class FakeTemplate
	{
		public string url { get; set; }// we will use same for text also
		public bool isProcessed { get; set; }
		public bool isFake { get; set; }

	}



}