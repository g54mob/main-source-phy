using System.Collections.Generic;
using UnityEngine;

namespace Shapes
{
	internal class MpbCuboid : MetaMpb
	{
		internal readonly List<Vector4> size;

		internal readonly List<float> sizeSpace;

		protected override void TransferShapeProperties()
		{
		}
	}
}
