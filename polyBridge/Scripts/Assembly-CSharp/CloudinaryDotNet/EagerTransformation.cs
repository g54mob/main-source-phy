using System.Collections.Generic;
using System.Linq;

namespace CloudinaryDotNet
{
	public class EagerTransformation : Transformation
	{
		public string Format { get; set; }

		public EagerTransformation(params Transformation[] transforms)
			: base(transforms.ToList())
		{
		}

		public EagerTransformation(List<Transformation> transforms)
			: base(transforms)
		{
		}

		public EagerTransformation()
		{
		}

		public EagerTransformation SetFormat(string format)
		{
			Format = format;
			return this;
		}

		public override string Generate()
		{
			string text = base.Generate();
			if (!string.IsNullOrEmpty(Format))
			{
				text = text + "/" + Format;
			}
			return text;
		}
	}
}
