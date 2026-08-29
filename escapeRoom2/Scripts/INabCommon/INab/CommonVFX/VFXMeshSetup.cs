using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace INab.CommonVFX
{
	public static class VFXMeshSetup
	{
		public static string MeshProperty = "Mesh Renderer";

		public static string SkinnedMeshProperty = "Skinned Renderer";

		public static string UseSkinnedMeshProperty = "Use Skinned Mesh";

		public static void SetupPropertyBinder(VFXPropertyBinder propertyBinder, Transform transform)
		{
			IEnumerable<VFXLossyTransformBinder> propertyBinders = propertyBinder.GetPropertyBinders<VFXLossyTransformBinder>();
			VFXLossyTransformBinder vFXLossyTransformBinder = ((propertyBinders.Count() != 0) ? propertyBinders.First() : propertyBinder.AddPropertyBinder<VFXLossyTransformBinder>());
			if ((bool)transform)
			{
				vFXLossyTransformBinder.Target = transform;
			}
		}

		public static void SetupRenderer(Renderer renderer, VisualEffect visualEffect)
		{
			if (!(visualEffect.visualEffectAsset == null))
			{
				bool b = false;
				if (renderer is SkinnedMeshRenderer)
				{
					visualEffect.SetSkinnedMeshRenderer(SkinnedMeshProperty, renderer as SkinnedMeshRenderer);
					b = true;
				}
				else
				{
					MeshFilter component = renderer.GetComponent<MeshFilter>();
					visualEffect.SetMesh(MeshProperty, component.sharedMesh);
				}
				visualEffect.SetBool(UseSkinnedMeshProperty, b);
			}
		}
	}
}
