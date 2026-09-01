using UnityEngine;

namespace Shapes;

public sealed class ShapesColorFieldAttribute : PropertyAttribute
{
	public readonly bool showAlpha = true;

	public ShapesColorFieldAttribute(bool showAlpha)
	{
		this.showAlpha = showAlpha;
	}
}
