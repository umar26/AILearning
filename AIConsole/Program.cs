using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.Azure.CognitiveServices.Search.WebSearch;
using Microsoft.Azure.CognitiveServices.Search.WebSearch.Models;
//using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
//using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Rest;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AIConsole
{
public	class Program
	{
		const string accessKey = "ae958e98f16849f584cae9d1956ad730";
		const string uriBase = "https://api.cognitive.microsoft.com/bing/v7.0/news/";
		//const string searchTerm = "Abhishek";
		const string Endpoint = "https://westus.api.cognitive.microsoft.com/";
		public static readonly List<KeysWithWeightageModel> keywordlist;
		static Program()
		{
			keywordlist= Utility.List();
		}


		private class ApiKeyServiceClientCredentials : ServiceClientCredentials
		{
			public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
			{
				request.Headers.Add("Ocp-Apim-Subscription-Key", accessKey);
				return base.ProcessHttpRequestAsync(request, cancellationToken);
			}
		}

		static void Main(string[] args)
		{
			string str = $"\t \t cognitive services Test ";
			Console.WriteLine(str);
			ConsoleKeyInfo userinput;
			
			do {
				Console.WriteLine("\n Please give your news/string to verify fake news");
				string searchTerm = Console.ReadLine();
				int resultpointer = BingNewsSearch(searchTerm);
				Console.WriteLine($"your result pointer value is {resultpointer} ");
				Console.WriteLine("do you want more to search please feed y/n");
				userinput = Console.ReadKey();
			}
			while (userinput.Key == ConsoleKey.Y);
			//TextAnalysis();
			//string[,] keys= Utility.KeyWordWithWeightage();


			Console.ReadLine();
		}


		static int BingNewsSearch(string searchQuery)
		{

			int resultpointer = 0;
			try
			{
				var client = new WebSearchClient(new ApiKeyServiceClientCredentials())
				{
					Endpoint = Endpoint
				};
				//fake news Pilot Urvisha jariwala Bhulka Bhavan school
				//var webresponse = client.Web.SearchAsync("Proud to inform you that the pilot of today's air strike is a girl  Urvisha jariwala from Bhulka Bhavan school of Surat ").Result;
				//var webresponse = client.Web.SearchAsync("Aam Aadmi Party Rejects Alliance With Congress, Says No Hope Left Now").Result;
				//var webresponse = client.Web.SearchAsync("“Newly appointed Madhya Pradesh Chief Minister Kamal Nath was former Prime Minister Rajiv Gandhi's driver").Result;
				//var webresponse = client.Web.SearchAsync("Be careful not to take the paracetamol that comes written P/500. It is a new, very white and shiny paracetamol, doctors prove to contain ‘Machupo’ virus, considered one of the most dangerous viruses in the world. And with high mortality rate.").Result;
				//var webresponse = client.Web.SearchAsync("After successfully damaged 2000 notes within 2 years Sardar Statue cracking in 2 weeks").Result;
				//var webresponse = client.Web.SearchAsync("“The supreme leader of India will be born in the state of Gujarat His father will sell tea in a shop His first name will be narendus (Narendra)").Result;
				//var webresponse = client.Web.SearchAsync("70 lakh Indian soldiers cannot defeat Azadi gang in Kashmir-Arundhati Roy").Result;
				//var webresponse = client.Web.SearchAsync("Jama Masjid in dark due to non-payment of electricity bills over four crores").Result;
				//Genuine news
				//var webresponse = client.Web.SearchAsync("New device can produce electricity from falling snow").Result;
				//var webresponse = client.Web.SearchAsync("Indian Air Force jets crossed LoC, claims Pakistan").Result;
				var webresponse = client.Web.SearchAsync(searchQuery).Result;
				int i = 0;
				foreach (var item in webresponse.WebPages.Value)
				{
					Console.WriteLine($"=====================Item no {i++}");
					Console.WriteLine(item.WebPageStringData());

				}
				List<WebPage> dd= webresponse.WebPages.Value as List<WebPage>;
				resultpointer= DecisionForFakeNews(dd);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error is {ex.Message} ,, {ex.InnerException} ,, {ex.StackTrace} ");
			}

			return resultpointer;
		}

		//static void TextAnalysis()
		//{
		//	//Microsoft.Azure.CognitiveServices.Language.TextAnalytics.ITextAnalyticsClient;

		//	ITextAnalyticsClient client = new TextAnalyticsClient(new ApiKeyServiceClientCredentials())
		//	{
		//		Endpoint = Endpoint
		//	};

		//	//var sentimentResults= client.SentimentAsync(false,
		//	//	new MultiLanguageBatchInput(new List<MultiLanguageInput>()
		//	//	{
		//	//		new MultiLanguageInput("en","1","I had the best day of my life."),
		//	//		new MultiLanguageInput("en","2", "This was a waste of my time. The speaker put me to sleep."),
		//	//		new MultiLanguageInput("en", "3","Rahul gandhi slap its own party member"),
		//	//		new MultiLanguageInput("en", "4","Narendra modi is arrogant")
		//	//	}
		//	//	)).Result;
		//	//// Printing sentiment results
		//	//foreach (var document in sentimentResults.Documents)
		//	//{
		//	//	Console.WriteLine($"Document ID: {document.Id} , Sentiment Score: {document.Score:0.00}");
		//	//}


		//	//var entitiesResult = client.EntitiesAsync(
		//	//   false,
		//	//   new MultiLanguageBatchInput(
		//	//	   new List<MultiLanguageInput>()
		//	//	   {
		//	//			new MultiLanguageInput("en", "1", "Microsoft was founded by Bill Gates and Paul Allen on April 4, 1975, to develop and sell BASIC interpreters for the Altair 8800."),
		//	//			new MultiLanguageInput("es", "2", "La sede principal de Microsoft se encuentra en la ciudad de Redmond, a 21 kilómetros de Seattle.")
		//	//	   })).Result;

		//	//// Printing entities results
		//	//foreach (var document in entitiesResult.Documents)
		//	//{
		//	//	Console.WriteLine($"Document ID: {document.Id} ");

		//	//	Console.WriteLine("\t Entities:");

		//	//	foreach (var entity in document.Entities)
		//	//	{
		//	//		Console.WriteLine($"\t\tName: {entity.Name},\tType: {entity.Type ?? "N/A"},\tSub-Type: {entity.SubType ?? "N/A"}");
		//	//		foreach (var match in entity.Matches)
		//	//		{
		//	//			Console.WriteLine($"\t\t\tOffset: {match.Offset},\tLength: {match.Length},\tScore: {match.EntityTypeScore:F3}");
		//	//		}
		//	//	}
		//	//}


		//	// Getting key-phrases
		//	Console.WriteLine("\n\n===== KEY-PHRASE EXTRACTION ======");

		//	var kpResults = client.KeyPhrasesAsync(
		//		false,
		//		new MultiLanguageBatchInput(
		//			new List<MultiLanguageInput>
		//			{
					
		//				new MultiLanguageInput("en", "1", "My cat is stiff as a rock."),
		//				new MultiLanguageInput("en", "2", "Dawood Ibrahim’s assets worth 15,000 crores seized in UAE"),
		//				new MultiLanguageInput("en", "3", " Nostradamus had predicted the rise of supreme leader Narendus"),
		//				//new MultiLanguageInput("en", "4", "My cat is stiff as a rock."),

		//			})).Result;

		//	// Printing keyphrases
		//	foreach (var document in kpResults.Documents)
		//	{
		//		Console.WriteLine($"Document ID: {document.Id} ");

		//		Console.WriteLine("\t Key phrases:");

		//		foreach (string keyphrase in document.KeyPhrases)
		//		{
		//			Console.WriteLine($"\t\t{keyphrase}");
		//		}
		//	}
		//}


		static int DecisionForFakeNews(List<WebPage> webPages)
		{
			int totalsum = 0;
			if (webPages.Count < 1)
				return totalsum;

			string toMatchString = string.Empty;
			webPages.ForEach((item) => toMatchString += item.WebPageToMatchData());
			Dictionary<string,int> weightagebyKeys= Utility.FoundKeysWordsWeightage(toMatchString.ToLower(),keywordlist);

			foreach (var item in weightagebyKeys)
			{
				Console.WriteLine($"{item.Key} : {item.Value}");
				totalsum += item.Value;
			}
			totalsum = totalsum / webPages.Count;
			return totalsum;
		}

		
	}

	public static class Extension
	{
		public static string WebPageStringData(this WebPage webpage)
		{
			string result= $"webpage.Id { webpage.Id}  \n webpage.Name { webpage.Name} \n webpage.Snippet { webpage.Snippet}" +
				$" \n webpage.Text { webpage.Text} \n webpage.Description { webpage.Description}" +
				$"\n webpage.BingId: {webpage.BingId}  webpage.DisplayUrl: {webpage.DisplayUrl} \n webpage.ThumbnailUrl: {webpage.ThumbnailUrl}\n" +
				$"webpage.Url: {webpage.Url} \n  webpage.WebSearchUrl: {webpage.WebSearchUrl} ";
			
			string searchtags = string.Empty;
			if( webpage.SearchTags !=null && webpage.SearchTags.Count>0)
			{
				searchtags += $"\t\tSearch tags: \n ";
				foreach (var item in webpage.SearchTags)
				{
					searchtags+=$" item.Name {item.Name} item.Content: {item.Content} ";
				}
			}
			return result + searchtags;
			   
		}

		public static string WebPageToMatchData(this WebPage webpage)
		{
			// currently only Name,Snippet and URL is present . need to verify more search based on that can add more pros to match data
			return  webpage.Name + " " + webpage.Snippet + " " + webpage.Url;
			
		}
	}

	

	


	
}
