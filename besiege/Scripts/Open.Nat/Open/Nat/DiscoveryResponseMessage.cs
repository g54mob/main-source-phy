using System;
using System.Collections.Generic;
using System.Linq;

namespace Open.Nat
{
	internal class DiscoveryResponseMessage
	{
		private readonly IDictionary<string, string> _headers;

		public string this[string key]
		{
			get
			{
				return _headers[key.ToUpperInvariant()];
			}
		}

		public DiscoveryResponseMessage(string message)
		{
			string[] source = message.Split(new string[1] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			var source2 = from h in source.Skip(1)
				let c = h.Split(':')
				let key = c[0]
				let value = (c.Length > 1) ? string.Join(":", c.Skip(1).ToArray()) : string.Empty
				select new
				{
					Key = key,
					Value = value.Trim()
				};
			_headers = source2.ToDictionary(x => x.Key.ToUpperInvariant(), x => x.Value);
		}
	}
}
