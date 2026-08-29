using System;
using UnityEngine;

namespace GLTFast.Loading
{
	[Serializable]
	public struct HttpHeader
	{
		[SerializeField]
		private string key;

		[SerializeField]
		private string value;

		public string Key => key;

		public string Value => value;

		public HttpHeader(string key, string value)
		{
			this.key = key;
			this.value = value;
		}
	}
}
