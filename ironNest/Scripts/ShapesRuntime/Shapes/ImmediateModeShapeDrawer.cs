using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes
{
	public class ImmediateModeShapeDrawer : MonoBehaviour
	{
		[Tooltip("When enabled, shapes will only draw in cameras that can see the layer of this GameObject")]
		public bool useCullingMasks;

		public virtual void DrawShapes(Camera cam)
		{
		}

		private void OnCameraPreRender(Camera cam)
		{
		}

		public virtual void OnEnable()
		{
		}

		public virtual void OnDisable()
		{
		}

		private void DrawShapesSRP(ScriptableRenderContext ctx, Camera cam)
		{
		}
	}
}
