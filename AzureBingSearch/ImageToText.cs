using Microsoft.Azure.CognitiveServices.ContentModerator;
using Microsoft.Azure.CognitiveServices.ContentModerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureBingSearch
{
	public class ImageToText
	{
		

		

		//;

		public static EvaluationData EvaluateImage(
			ContentModeratorClient client, string imageUrl)
		{
			var url = new BodyModel("URL", imageUrl.Trim());

			var imageData = new EvaluationData();

			imageData.ImageUrl = url.Value;

			// Evaluate for adult and racy content.
			//imageData.ImageModeration =
			//	client.ImageModeration.EvaluateUrlInput("application/json", url, true);
			//Thread.Sleep(1000);

			// Detect and extract text.
			imageData.TextDetection =
				client.ImageModeration.OCRUrlInput("eng", "application/json", url, true);
			Thread.Sleep(1000);

			// Detect faces.
			//imageData.FaceDetection =
			//	client.ImageModeration.FindFacesUrlInput("application/json", url, true);
			//Thread.Sleep(1000);

			return imageData;
		}

	}

	// Wraps the creation and configuration of a Content Moderator client.
	public static class Clients
	{
		// The region/location for your Content Moderator account, 
		// for example, westus.
		private static readonly string AzureRegion = "westcentralus";

		// The base URL fragment for Content Moderator calls.
		private static readonly string AzureBaseURL =
			$"https://{AzureRegion}.api.cognitive.microsoft.com";

		// Your Content Moderator subscription key.
		private static readonly string CMSubscriptionKey = "b88da68873e84bd399ec5e51b6876a8b";

		// Returns a new Content Moderator client for your subscription.
		public static ContentModeratorClient NewClient()
		{
			// Create and initialize an instance of the Content Moderator API wrapper.
			ContentModeratorClient client = new ContentModeratorClient(new ApiKeyServiceClientCredentials(CMSubscriptionKey));

			client.Endpoint = AzureBaseURL;
			return client;
		}
	}

	// Contains the image moderation results for an image, 
	// including text and face detection results.
	public class EvaluationData
	{
		// The URL of the evaluated image.
		public string ImageUrl;

		// The image moderation results.
		//public Evaluate ImageModeration;

		// The text detection results.
		public OCR TextDetection;

		// The face detection results;
		//public FoundFaces FaceDetection;
	}

}
