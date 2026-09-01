using System.Collections.Generic;
using UnityEngine;

namespace Shapes;

internal class MpbTexture : MetaMpb
{
	internal Texture texture;

	internal readonly List<Vector4> rect;

	internal readonly List<Vector4> uvs;

	protected unsafe override void TransferShapeProperties()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected Ref, but got Unknown
		Transfer(ShapesMaterialUtils.propRect, rect);
		Transfer(ShapesMaterialUtils.propUvs, uvs);
		Transfer(ShapesMaterialUtils.propMainTex, ref *(Texture*)(this + 200));
	}

	public MpbTexture()
	{
		List<Vector4> list = MetaMpb.InitList<Vector4>();
		rect = list;
		List<Vector4> list2 = MetaMpb.InitList<Vector4>();
		uvs = list2;
		base._002Ector();
	}
}
