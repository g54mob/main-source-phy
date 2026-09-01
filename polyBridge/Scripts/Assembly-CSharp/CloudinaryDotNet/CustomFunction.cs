using CloudinaryDotNet.Core;

namespace CloudinaryDotNet
{
	public class CustomFunction : ICloneable
	{
		private readonly string parameters;

		private CustomFunction(params string[] components)
		{
			parameters = string.Join(":", components);
		}

		public static CustomFunction Wasm(string publicId)
		{
			return new CustomFunction("wasm", publicId);
		}

		public static CustomFunction Render(string manifest)
		{
			return new CustomFunction("render", manifest);
		}

		public static CustomFunction Remote(string url)
		{
			return new CustomFunction("remote", Utils.EncodeUrlSafe(url));
		}

		public override string ToString()
		{
			return parameters;
		}

		public object Clone()
		{
			return MemberwiseClone();
		}
	}
}
