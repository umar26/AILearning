using Microsoft.Azure.CognitiveServices.Search.WebSearch;
using Microsoft.Azure.CognitiveServices.Search.WebSearch.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureBingSearch
{
    public class BingSearch
    {
		const string accessKey = "ae958e98f16849f584cae9d1956ad730";
		const string uriBase = "https://api.cognitive.microsoft.com/bing/v7.0/news/";
		//const string searchTerm = "Abhishek";
		 //const string endpoint = "
		public static readonly List<KeysWithWeightageModel> keywordlist;
		private static string endPoint= "https://westus.api.cognitive.microsoft.com/";

		static BingSearch()
		{
			keywordlist = Utility.List();
		}
	public	static int BingNewsSearch(string searchQuery)
		{

			int resultpointer = 0;
			try
			{
				var client = new WebSearchClient(new ApiKeyServiceClientCredentials())
				{
					Endpoint = endPoint
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
				List<WebPage> dd = webresponse.WebPages.Value as List<WebPage>;
				resultpointer = DecisionForFakeNews(dd);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error is {ex.Message} ,, {ex.InnerException} ,, {ex.StackTrace} ");
			}

			return resultpointer;
		}

		static int DecisionForFakeNews(List<WebPage> webPages)
		{
			int totalsum = 0;
			if (webPages.Count < 1)
				return totalsum;

			string toMatchString = string.Empty;
			webPages.ForEach((item) => toMatchString += item.WebPageToMatchData());
			Dictionary<string, int> weightagebyKeys = Utility.FoundKeysWordsWeightage(toMatchString.ToLower(), keywordlist);

			foreach (var item in weightagebyKeys)
			{
				Console.WriteLine($"{item.Key} : {item.Value}");
				totalsum += item.Value;
			}
			totalsum=(int)Math.Ceiling(((decimal)totalsum / (decimal)webPages.Count));
			//totalsum = totalsum / webPages.Count;
			return totalsum;
		}

		private class ApiKeyServiceClientCredentials : ServiceClientCredentials
		{
			public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
			{
				request.Headers.Add("Ocp-Apim-Subscription-Key", accessKey);
				return base.ProcessHttpRequestAsync(request, cancellationToken);
			}
		}
	}

	public static class Extension
	{
		public static string WebPageStringData(this WebPage webpage)
		{
			string result = $"webpage.Id { webpage.Id}  \n webpage.Name { webpage.Name} \n webpage.Snippet { webpage.Snippet}" +
				$" \n webpage.Text { webpage.Text} \n webpage.Description { webpage.Description}" +
				$"\n webpage.BingId: {webpage.BingId}  webpage.DisplayUrl: {webpage.DisplayUrl} \n webpage.ThumbnailUrl: {webpage.ThumbnailUrl}\n" +
				$"webpage.Url: {webpage.Url} \n  webpage.WebSearchUrl: {webpage.WebSearchUrl} ";

			string searchtags = string.Empty;
			if (webpage.SearchTags != null && webpage.SearchTags.Count > 0)
			{
				searchtags += $"\t\tSearch tags: \n ";
				foreach (var item in webpage.SearchTags)
				{
					searchtags += $" item.Name {item.Name} item.Content: {item.Content} ";
				}
			}
			return result + searchtags;

		}

		public static string WebPageToMatchData(this WebPage webpage)
		{
			// currently only Name,Snippet and URL is present . need to verify more search based on that can add more pros to match data
			return webpage.Name + " " + webpage.Snippet + " " + webpage.Url;

		}
	}


	//fake news 1 A blog titled “Nostradamus and India” has been published on the Times of India website on March 28, 2017. Francois Gautier, the author of this blog, claims to have chanced upon some hidden manuscripts according to which Nostradamus predicted that a supreme leader will be born in the state of Gujarat and his first name would be Narendrus (Narendra). Fracois Gautier claims that these manuscripts were discovered in 2012
}
