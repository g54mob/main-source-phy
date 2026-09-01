using System;
using System.Net;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public abstract class BaseResult
	{
		private JToken rawJson;

		public HttpStatusCode StatusCode { get; set; }

		public JToken JsonObj
		{
			get
			{
				return rawJson;
			}
			internal set
			{
				rawJson = value;
				SetValues(value);
			}
		}

		[DataMember(Name = "error")]
		public Error Error { get; set; }

		public long Limit { get; set; }

		public long Remaining { get; set; }

		public DateTime Reset { get; set; }

		internal virtual void SetValues(JToken source)
		{
		}
	}
}
