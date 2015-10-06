using System;
using System.Json;
using System.Threading.Tasks;
using System.Net;
using System.IO;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;

namespace Foolproof
{
	[Activity (Label = "Foolproof", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button btn_decide = FindViewById<Button> (Resource.Id.btn_decide);
			
			btn_decide.Click += DecideClicked;
		}

		private async void DecideClicked (object sender, EventArgs e)
		{
			JsonValue json = await FetchAnswerAsync ("http://yesno.wtf/api");
			ParseAndDisplay (json);			
		}

		private void ParseAndDisplay (JsonValue json)
		{
			WebView wv_answer = FindViewById<WebView> (Resource.Id.wv_answer);
			wv_answer.LoadUrl(json ["image"]);

		}

		private async Task<JsonValue> FetchAnswerAsync (string url)
		{
			// Create an HTTP web request using the URL:
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new Uri (url));
			request.ContentType = "application/json";
			request.Method = "GET";

			// Send the request to the server and wait for the response:
			using (WebResponse response = await request.GetResponseAsync ())
			{
				// Get a stream representation of the HTTP web response:
				using (Stream stream = response.GetResponseStream ())
				{
					// Use this stream to build a JSON document object:
					JsonValue jsonDoc = await Task.Run (() => JsonObject.Load (stream));
					Console.Out.WriteLine("Response: {0}", jsonDoc.ToString ());

					// Return the JSON document:
					return jsonDoc;
				}
			}
		}
	}
}