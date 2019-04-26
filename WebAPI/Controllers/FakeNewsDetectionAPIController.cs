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
	//[RoutePrefix("api/FakeNewsDetection")]
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class FakeNewsDetectionAPIController : ApiController
	{
		// GET api/<controller>
		//[Route("")]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<controller>/5
		//[Route("GetByString/{querystring}")]
		public FakeTemplate Get(string querystring)
		{
			int finalscore = 0;
			finalscore= IntegrateServices.IntegratedResult(querystring);
			FakeTemplate result = new FakeTemplate();
			result.url = querystring;
			result.isFake = finalscore >= 5;
			
			return result;
		}

		
		// POST api/<controller>
		public void Post([FromBody]string value)
		{
		}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/<controller>/5
		public void Delete(int id)
		{
		}
	}

	public class FakeTemplate
	{
		public string url { get; set; }// we will use same for text also
		public bool isProcessed { get; set; }
		public bool isFake { get; set; }

	}
}