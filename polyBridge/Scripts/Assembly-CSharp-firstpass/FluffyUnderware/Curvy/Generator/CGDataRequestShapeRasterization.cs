using System.Linq;

namespace FluffyUnderware.Curvy.Generator
{
	public class CGDataRequestShapeRasterization : CGDataRequestRasterization
	{
		public float[] PathF;

		public CGDataRequestShapeRasterization(float[] pathF, float start, float rasterizedRelativeLength, int resolution, float angle, ModeEnum mode = ModeEnum.Even)
			: base(start, rasterizedRelativeLength, resolution, angle, mode)
		{
			PathF = pathF;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is CGDataRequestShapeRasterization cGDataRequestShapeRasterization))
			{
				return false;
			}
			if (base.Equals(obj) && cGDataRequestShapeRasterization.PathF.Length == PathF.Length)
			{
				return cGDataRequestShapeRasterization.PathF.SequenceEqual(PathF);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (base.GetHashCode() * 397) ^ ((PathF != null) ? PathF.GetHashCode() : 0);
		}
	}
}
