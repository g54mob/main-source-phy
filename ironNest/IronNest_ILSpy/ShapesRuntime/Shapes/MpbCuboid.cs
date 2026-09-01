using System.Collections.Generic;
using UnityEngine;

namespace Shapes;

internal class MpbCuboid : MetaMpb
{
	internal readonly List<Vector4> size;

	internal readonly List<float> sizeSpace;

	protected override void TransferShapeProperties()
	{
		Transfer(ShapesMaterialUtils.propSize, size);
		Transfer(ShapesMaterialUtils.propSizeSpace, sizeSpace);
	}

	public MpbCuboid()
	{
		List<Vector4> list = MetaMpb.InitList<Vector4>();
		size = list;
		List<float> list2 = MetaMpb.InitList<float>();
		sizeSpace = list2;
		base._002Ector();
	}
}
