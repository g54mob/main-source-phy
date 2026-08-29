using UnityEngine;
using UnityEngine.Rendering;

namespace INab.Common
{
	[ExecuteAlways]
	public class InteractiveEffectMask : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Configuration settings for the mask's visual guides and collision detection.")]
		public InteractiveEffectMaskSettings maskSettings;

		[SerializeField]
		[Tooltip("Determines the mask's shape and associated behavior.")]
		private InteractiveEffectMaskType type = InteractiveEffectMaskType.Ellipse;

		[SerializeField]
		[Tooltip("Enables a visual preview of the mask in the scene.")]
		protected bool usePreview;

		[SerializeField]
		[Tooltip("Disables the mask preview when the application is playing.")]
		protected bool onlyEditorPreview;

		public MeshFilter maskPreviewFilter;

		public MeshRenderer maskPreviewRenderer;

		public InteractiveEffectMaskType Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
				if (usePreview)
				{
					UpdatePreview();
				}
			}
		}

		public bool UsePreview
		{
			get
			{
				return usePreview;
			}
			set
			{
				usePreview = value;
			}
		}

		public bool HasMaskPreview
		{
			get
			{
				if (maskPreviewRenderer != null)
				{
					return true;
				}
				return false;
			}
			private set
			{
			}
		}

		public bool OnlyEditorPreview
		{
			get
			{
				return onlyEditorPreview;
			}
			set
			{
				onlyEditorPreview = value;
			}
		}

		private void Start()
		{
			if (onlyEditorPreview && Application.isPlaying)
			{
				if ((bool)maskPreviewRenderer)
				{
					maskPreviewRenderer.enabled = false;
				}
			}
			else if ((bool)maskPreviewRenderer)
			{
				maskPreviewRenderer.enabled = true;
			}
		}

		private void Update()
		{
			if (!HasMaskPreview && UsePreview)
			{
				UpdatePreview();
			}
		}

		public void UpdatePreview()
		{
			DestroyPreview();
			if (base.gameObject.GetComponent<MeshFilter>() != null)
			{
				maskPreviewFilter = base.gameObject.GetComponent<MeshFilter>();
			}
			else
			{
				maskPreviewFilter = base.gameObject.AddComponent<MeshFilter>();
			}
			if (base.gameObject.GetComponent<MeshRenderer>() != null)
			{
				maskPreviewRenderer = base.gameObject.GetComponent<MeshRenderer>();
			}
			else
			{
				maskPreviewRenderer = base.gameObject.AddComponent<MeshRenderer>();
			}
			maskPreviewRenderer.material = maskSettings.PreviewMaterial;
			maskPreviewRenderer.shadowCastingMode = ShadowCastingMode.Off;
			maskPreviewRenderer.receiveShadows = false;
			switch (type)
			{
			case InteractiveEffectMaskType.Plane:
				maskPreviewFilter.sharedMesh = maskSettings.PreviewPlaneMesh;
				break;
			case InteractiveEffectMaskType.Box:
				maskPreviewFilter.sharedMesh = maskSettings.PreviewBoxMesh;
				break;
			case InteractiveEffectMaskType.Ellipse:
				maskPreviewFilter.sharedMesh = maskSettings.PreviewSphereMesh;
				break;
			}
		}

		public void DestroyPreview()
		{
			if (Application.isPlaying)
			{
				Object.Destroy(maskPreviewFilter);
				Object.Destroy(maskPreviewRenderer);
			}
			else
			{
				Object.DestroyImmediate(maskPreviewFilter);
				Object.DestroyImmediate(maskPreviewRenderer);
			}
			maskPreviewFilter = null;
			maskPreviewRenderer = null;
		}
	}
}
