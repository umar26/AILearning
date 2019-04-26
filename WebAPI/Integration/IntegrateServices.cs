using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AzureBingSearch;
using NaturalLanguageProcessing;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using Microsoft.Azure.CognitiveServices.ContentModerator.Models;
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

			if(resultFromNLP.Value>=90) //  this is news
			{
				 finalPoint = BingSearch.BingNewsSearch(queryterms);
			}
			return finalPoint;
			
		}

		public static string EvaluateImage(string urlToProcess)
		{

			var client = Clients.NewClient();
			EvaluationData imageData = ImageToText.EvaluateImage(client, urlToProcess);
		return	imageData.TextDetection.Text;

		}

		
	}

	public class FakeTemplate
	{
		public string url { get; set; }// we will use same for text also
		public bool isProcessed { get; set; }
		public bool isFake { get; set; }

	}



}