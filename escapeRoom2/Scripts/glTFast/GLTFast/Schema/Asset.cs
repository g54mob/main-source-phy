using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class Asset : NamedObject
	{
		public string copyright;

		public string generator;

		public string version;

		public string minVersion;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.OpenBrackets();
			if (!string.IsNullOrEmpty(version))
			{
				writer.AddProperty("version", version);
			}
			if (!string.IsNullOrEmpty(generator))
			{
				writer.AddPropertySafe("generator", generator);
			}
			if (!string.IsNullOrEmpty(copyright))
			{
				writer.AddPropertySafe("copyright", copyright);
			}
			if (!string.IsNullOrEmpty(minVersion))
			{
				writer.AddProperty("minVersion", minVersion);
			}
			writer.Close();
		}
	}
}
