using System.Collections.Generic;

namespace Shapes;

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
		Transfer(ShapesMaterialUtils.propAngEnd, angleEnd);
		Transfer(ShapesMaterialUtils.propAngStart, angleStart);
		Transfer(ShapesMaterialUtils.propRadius, radius);
		Transfer(ShapesMaterialUtils.propRadiusSpace, radiusSpace);
		Transfer(ShapesMaterialUtils.propScaleMode, scaleMode);
		Transfer(ShapesMaterialUtils.propThickness, thickness);
		Transfer(ShapesMaterialUtils.propThicknessSpace, thicknessSpace);
	}

	public MpbTorus()
	{
		List<float> list = MetaMpb.InitList<float>();
		angleEnd = list;
		List<float> list2 = MetaMpb.InitList<float>();
		angleStart = list2;
		radius = MetaMpb.InitList<float>();
		radiusSpace = MetaMpb.InitList<float>();
		scaleMode = MetaMpb.InitList<float>();
		thickness = MetaMpb.InitList<float>();
		thicknessSpace = MetaMpb.InitList<float>();
		base._002Ector();
	}
}
