using UnityEngine;

namespace Shapes
{
	public interface IFillable
	{
		GradientFill Fill { get; set; }

		bool UseFill { get; set; }

		FillType FillType { get; set; }

		FillSpace FillSpace { get; set; }

		Vector3 FillRadialOrigin { get; set; }

		float FillRadialRadius { get; set; }

		Vector3 FillLinearStart { get; set; }

		Vector3 FillLinearEnd { get; set; }

		Color FillColorStart { get; set; }

		Color FillColorEnd { get; set; }
	}
}
