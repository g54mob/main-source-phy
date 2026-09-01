using System;
using Newtonsoft.Json.Linq;

namespace CloudinaryDotNet
{
	public class ResponsiveBreakpoint : JObject
	{
		private const string CREATEDERIVED = "create_derived";

		private const string TRANSFORMATION = "transformation";

		private const string MAXWIDTH = "max_width";

		private const string MINWIDTH = "min_width";

		private const string BYTESSTEP = "bytes_step";

		private const string MAXIMAGES = "max_images";

		private const string FORMAT = "format";

		public ResponsiveBreakpoint()
		{
			Add("create_derived", true);
		}

		public bool IsCreateDerived()
		{
			return GetValue("create_derived", StringComparison.Ordinal).Value<bool>();
		}

		public ResponsiveBreakpoint CreateDerived(bool createDerived)
		{
			base["create_derived"] = createDerived;
			return this;
		}

		public ResponsiveBreakpoint Transformation(Transformation transformation)
		{
			base["transformation"] = transformation.ToString();
			return this;
		}

		public int MaxWidth()
		{
			return Value<int>("max_width");
		}

		public ResponsiveBreakpoint MaxWidth(int maxWidth)
		{
			base["max_width"] = maxWidth;
			return this;
		}

		public int MinWidth()
		{
			return Value<int>("min_width");
		}

		public ResponsiveBreakpoint MinWidth(int minWidth)
		{
			base["min_width"] = minWidth;
			return this;
		}

		public int BytesStep()
		{
			return Value<int>("bytes_step");
		}

		public ResponsiveBreakpoint BytesStep(int bytesStep)
		{
			base["bytes_step"] = bytesStep;
			return this;
		}

		public int MaxImages()
		{
			return Value<int>("max_images");
		}

		public ResponsiveBreakpoint MaxImages(int maxImages)
		{
			base["max_images"] = maxImages;
			return this;
		}

		public string Format()
		{
			return Value<string>("format");
		}

		public ResponsiveBreakpoint Format(string format)
		{
			base["format"] = format;
			return this;
		}
	}
}
