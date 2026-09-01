using System.Collections.Generic;
using UnityEngine;

namespace Shapes
{
	internal class MpbQuad : MetaMpb
	{
		internal readonly List<Vector4> a;

		internal readonly List<Vector4> b;

		internal readonly List<Vector4> c;

		internal readonly List<Vector4> colorB;

		internal readonly List<Vector4> colorC;

		internal readonly List<Vector4> colorD;

		internal readonly List<Vector4> d;

		protected override void TransferShapeProperties()
		{
		}
	}
}
