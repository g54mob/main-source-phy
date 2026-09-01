using UnityEngine;

namespace Shapes
{
	[ExecuteAlways]
	public class ShapesTextPool : ShapesObjPool<TextMeshProShapes, ShapesTextPool>
	{
		public override string PoolTypeName => null;

		public override void OnCreatedNewComponent(TextMeshProShapes comp)
		{
		}
	}
}
