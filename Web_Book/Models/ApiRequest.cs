﻿using Microsoft.AspNetCore.Mvc;
using static Web_Book.StaticDetails;

namespace Web_Book.Models
{
	public class ApiRequest
	{
		public ApiType ApiType { get; set; }
		public string URL { get; set; }

		public object Data { get; set; }
		public string AccessToken { get; set; }
	}
}
