using System.Collections.Generic;

namespace Shapes
{
	internal class MpbTorus : MetaMpb
	{
		internal readonly List<float> angleEnd;

		internal readonly List<float> angleStart;

		internal readonly List<float> radius;

		internal readonly List<float> radiusSpace;

		internal readonly List<float> scaleMode;

		internal readonly List<float> thickness;

		internal readonly List<float> thicknessSpace;

		protected override void TransferShapeProperties()
		{
		}
	}
}
