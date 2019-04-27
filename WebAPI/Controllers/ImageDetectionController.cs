using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AzureBingSearch;
using NaturalLanguageProcessing;
using WebAPI.Integration;
namespace WebAPI.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class ImageDetectionController : ApiController
    {
        // GET: api/ImageDetection
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ImageDetection/5
        public FakeTemplate Get(string imageUrl)
        {
			int finalscore = 0;
			imageUrl = imageUrl.Replace("__", "&").Replace("_2f_", "%2F");
			if (!string.IsNullOrEmpty(imageUrl))
			{
				string texttosearch = IntegrateServices.EvaluateImage(imageUrl);
				if (texttosearch.Length >= 50)
				{
					finalscore = IntegrateServices.IntegratedResult(texttosearch);
				}
				//int result=  BingSearch.BingNewsSearch(querystring);
			}
			FakeTemplate result = new FakeTemplate();
			result.url = imageUrl;
			result.fakePoints = finalscore;
			result.isFake = finalscore >= 5;
			return result;
		}

        // POST: api/ImageDetection
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ImageDetection/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ImageDetection/5
        public void Delete(int id)
        {
        }
    }
}
