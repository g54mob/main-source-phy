using System.Collections.Generic;
using UnityEngine;

namespace Shapes
{
	internal class MpbTexture : MetaMpb
	{
		internal Texture texture;

		internal readonly List<Vector4> rect;

		internal readonly List<Vector4> uvs;

		protected override void TransferShapeProperties()
		{
		}
	}
}
