using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CloudinaryDotNet.Core;

namespace CloudinaryDotNet.Actions
{
	[Obsolete("One could use List<Rectangle>")]
	public class FaceCoordinates : List<Rectangle>
	{
		public override string ToString()
		{
			return string.Join("|", this.Select((Rectangle r) => string.Format(CultureInfo.InvariantCulture, "{0},{1},{2},{3}", r.X, r.Y, r.Width, r.Height)).ToArray());
		}
	}
}
