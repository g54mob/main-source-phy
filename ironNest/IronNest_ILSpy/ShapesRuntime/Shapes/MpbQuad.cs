using System.Collections.Generic;
using UnityEngine;

namespace Shapes;

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
		Transfer(ShapesMaterialUtils.propA, a);
		Transfer(ShapesMaterialUtils.propB, b);
		Transfer(ShapesMaterialUtils.propC, c);
		Transfer(ShapesMaterialUtils.propColorB, colorB);
		Transfer(ShapesMaterialUtils.propColorC, colorC);
		Transfer(ShapesMaterialUtils.propColorD, colorD);
		Transfer(ShapesMaterialUtils.propD, d);
	}

	public MpbQuad()
	{
		List<Vector4> list = MetaMpb.InitList<Vector4>();
		a = list;
		List<Vector4> list2 = MetaMpb.InitList<Vector4>();
		b = list2;
		c = MetaMpb.InitList<Vector4>();
		colorB = MetaMpb.InitList<Vector4>();
		colorC = MetaMpb.InitList<Vector4>();
		colorD = MetaMpb.InitList<Vector4>();
		d = MetaMpb.InitList<Vector4>();
		base._002Ector();
	}
}
