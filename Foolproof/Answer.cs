using System;

namespace Foolproof
{
	public class Answer
	{
		string VerbalAnswer{get; set;}
		string ImageUrl{get; set;}
		
		public Answer ()
		{
			
		}

		public Answer (string VerbalAnswer, string ImageUrl)
		{
			this.VerbalAnswer = VerbalAnswer;
			this.ImageUrl = ImageUrl;
		}
	}
}