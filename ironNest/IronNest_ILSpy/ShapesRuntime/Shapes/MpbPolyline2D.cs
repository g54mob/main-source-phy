using System.Collections.Generic;

namespace Shapes;

internal class MpbPolyline2D : MetaMpb
{
	internal readonly List<float> alignment;

	internal readonly List<float> scaleMode;

	internal readonly List<float> thickness;

	internal readonly List<float> thicknessSpace;

	protected override void TransferShapeProperties()
	{
		Transfer(ShapesMaterialUtils.propAlignment, alignment);
		Transfer(ShapesMaterialUtils.propScaleMode, scaleMode);
		Transfer(ShapesMaterialUtils.propThickness, thickness);
		Transfer(ShapesMaterialUtils.propThicknessSpace, thicknessSpace);
	}

	public MpbPolyline2D()
	{
		List<float> list = MetaMpb.InitList<float>();
		alignment = list;
		List<float> list2 = MetaMpb.InitList<float>();
		scaleMode = list2;
		thickness = MetaMpb.InitList<float>();
		thicknessSpace = MetaMpb.InitList<float>();
		base._002Ector();
	}
}
