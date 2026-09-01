using System.Collections.Generic;

namespace Shapes
{
	internal class MpbPolyline2D : MetaMpb
	{
		internal readonly List<float> alignment;

		internal readonly List<float> scaleMode;

		internal readonly List<float> thickness;

		internal readonly List<float> thicknessSpace;

		protected override void TransferShapeProperties()
		{
		}
	}
}
