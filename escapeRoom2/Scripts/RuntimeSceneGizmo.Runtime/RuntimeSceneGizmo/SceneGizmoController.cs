using System;
using UnityEngine;

namespace RuntimeSceneGizmo
{
	public class SceneGizmoController : MonoBehaviour
	{
		private const int GIZMOS_LAYER = 24;

		[SerializeField]
		private Camera gizmoCameraBlack;

		[SerializeField]
		private Camera gizmoCameraWhite;

		private Transform gizmoCamParent;

		[SerializeField]
		private Renderer[] gizmoComponents;

		[SerializeField]
		private TextMesh[] labels;

		private Transform[] labelsTR;

		private Transform m_referenceTransform;

		private Vector3 prevForward;

		private Material gizmoNormalMaterial;

		private Material gizmoFadeMaterial;

		private Material gizmoHighlightMaterial;

		private int gizmoMaterialFadeProperty;

		private GizmoComponent highlightedComponent = GizmoComponent.None;

		private GizmoComponent fadingComponent = GizmoComponent.None;

		private bool isFadingToZero;

		private float fadeT = 1f;

		private bool updateTargetTexture;

		[NonSerialized]
		public RenderTexture TargetTextureBlack;

		[NonSerialized]
		public RenderTexture TargetTextureWhite;

		public Camera gizmoCamera => gizmoCameraBlack;

		public Renderer[] gizmoRenderers => gizmoComponents;

		public TextMesh[] gizmoLabels => labels;

		public Transform ReferenceTransform
		{
			get
			{
				return m_referenceTransform;
			}
			set
			{
				if (value == null || value.Equals(null))
				{
					value = Camera.main.transform;
				}
				if (value != m_referenceTransform)
				{
					m_referenceTransform = value;
					Camera component = m_referenceTransform.GetComponent<Camera>();
					if (component != null)
					{
						component.cullingMask &= -16777217;
					}
				}
			}
		}

		private void Awake()
		{
			gizmoCamParent = gizmoCameraBlack.transform.parent;
			labelsTR = new Transform[labels.Length];
			int gizmoResolution = Mathf.Min(Mathf.NextPowerOfTwo(Mathf.Max(Screen.width, Screen.height) / 6), 512);
			setupCamera(gizmoCameraBlack, ref TargetTextureBlack);
			setupCamera(gizmoCameraWhite, ref TargetTextureWhite);
			gizmoNormalMaterial = gizmoComponents[0].sharedMaterial;
			gizmoFadeMaterial = new Material(gizmoNormalMaterial);
			gizmoMaterialFadeProperty = Shader.PropertyToID("_AlphaVal");
			gizmoHighlightMaterial = new Material(gizmoNormalMaterial);
			gizmoHighlightMaterial.EnableKeyword("HIGHLIGHT_ON");
			gizmoHighlightMaterial.color = Color.yellow;
			for (int i = 0; i < gizmoComponents.Length; i++)
			{
				gizmoComponents[i].gameObject.layer = 24;
			}
			for (int j = 0; j < labelsTR.Length; j++)
			{
				labels[j].gameObject.layer = 24;
				labelsTR[j] = labels[j].transform;
			}
			void setupCamera(Camera gizmoCamera, ref RenderTexture TargetTexture)
			{
				TargetTexture = new RenderTexture(gizmoResolution, gizmoResolution, 16);
				gizmoCamera.aspect = 1f;
				gizmoCamera.targetTexture = TargetTexture;
				gizmoCamera.cullingMask = 16777216;
				gizmoCamera.eventMask = 0;
				gizmoCamera.enabled = false;
			}
		}

		private void OnEnable()
		{
			if (highlightedComponent != GizmoComponent.None)
			{
				gizmoComponents[(int)highlightedComponent].sharedMaterial = gizmoNormalMaterial;
				highlightedComponent = GizmoComponent.None;
			}
			SetHiddenComponent(GizmoComponent.None);
			updateTargetTexture = true;
		}

		private void OnDestroy()
		{
			if (TargetTextureBlack != null)
			{
				TargetTextureBlack.Release();
				UnityEngine.Object.Destroy(TargetTextureBlack);
			}
			if (TargetTextureWhite != null)
			{
				TargetTextureWhite.Release();
				UnityEngine.Object.Destroy(TargetTextureWhite);
			}
		}

		private void LateUpdate()
		{
			if (!m_referenceTransform)
			{
				ReferenceTransform = Camera.main.transform;
				if (!m_referenceTransform)
				{
					Debug.LogError("ReferenceTransform mustn't be null!");
					return;
				}
			}
			Vector3 forward = m_referenceTransform.forward;
			if (prevForward != forward)
			{
				float num = ((forward.x < 0f) ? (0f - forward.x) : forward.x);
				float num2 = ((forward.y < 0f) ? (0f - forward.y) : forward.y);
				float num3 = ((forward.z < 0f) ? (0f - forward.z) : forward.z);
				GizmoComponent component;
				float num4;
				if (num > num2)
				{
					if (num > num3)
					{
						component = ((!(forward.x > 0f)) ? GizmoComponent.XNegative : GizmoComponent.XPositive);
						num4 = Vector3.Dot(forward, new Vector3(1f, 0f, 0f));
					}
					else
					{
						component = ((forward.z > 0f) ? GizmoComponent.ZPositive : GizmoComponent.ZNegative);
						num4 = Vector3.Dot(forward, new Vector3(0f, 0f, 1f));
					}
				}
				else if (num2 > num3)
				{
					component = ((forward.y > 0f) ? GizmoComponent.YPositive : GizmoComponent.YNegative);
					num4 = Vector3.Dot(forward, new Vector3(0f, 1f, 0f));
				}
				else
				{
					component = ((forward.z > 0f) ? GizmoComponent.ZPositive : GizmoComponent.ZNegative);
					num4 = Vector3.Dot(forward, new Vector3(0f, 0f, 1f));
				}
				if (num4 < 0f)
				{
					num4 = 0f - num4;
				}
				if (num4 >= 0.92f)
				{
					SetHiddenComponent(GetOppositeComponent(component));
				}
				else
				{
					SetHiddenComponent(GizmoComponent.None);
				}
				Quaternion rotation = m_referenceTransform.rotation;
				gizmoCamParent.localRotation = rotation;
				float num5 = (num - 0.15f) * 0.65f;
				float num6 = (num2 - 0.15f) * 0.65f;
				float num7 = (num3 - 0.15f) * 0.65f;
				if (num5 < 0f)
				{
					num5 = 0f;
				}
				if (num6 < 0f)
				{
					num6 = 0f;
				}
				if (num7 < 0f)
				{
					num7 = 0f;
				}
				labelsTR[0].localPosition = new Vector3(0f, 0f, num5);
				labelsTR[1].localPosition = new Vector3(0f, 0f, num6);
				labelsTR[2].localPosition = new Vector3(0f, 0f, num7);
				labelsTR[0].rotation = rotation;
				labelsTR[1].rotation = rotation;
				labelsTR[2].rotation = rotation;
				updateTargetTexture = true;
				prevForward = forward;
			}
			if (fadeT < 1f)
			{
				fadeT += Time.unscaledDeltaTime * 4f;
				if (fadeT >= 1f)
				{
					fadeT = 1f;
				}
				SetAlphaOf(fadingComponent, isFadingToZero ? (1f - fadeT) : fadeT);
				if (fadeT >= 1f)
				{
					if (!isFadingToZero)
					{
						SetMaterialOf(fadingComponent, gizmoNormalMaterial);
						fadingComponent = GizmoComponent.None;
					}
					else
					{
						gizmoComponents[(int)fadingComponent].gameObject.SetActive(value: false);
						gizmoComponents[(int)GetOppositeComponent(fadingComponent)].gameObject.SetActive(value: false);
					}
				}
				updateTargetTexture = true;
			}
			if (updateTargetTexture)
			{
				updateTargetTexture = false;
			}
		}

		public GizmoComponent Raycast(Vector3 normalizedPosition)
		{
			if (Physics.Raycast(gizmoCameraBlack.ViewportPointToRay(normalizedPosition), out var hitInfo, gizmoCameraBlack.farClipPlane, 16777216, QueryTriggerInteraction.Collide))
			{
				GameObject gameObject = hitInfo.collider.transform.gameObject;
				for (int i = 0; i < gizmoComponents.Length; i++)
				{
					if (gizmoComponents[i].gameObject == gameObject)
					{
						return (GizmoComponent)i;
					}
				}
			}
			return GizmoComponent.None;
		}

		public void OnPointerHover(Vector3 normalizedPosition)
		{
			GizmoComponent gizmoComponent = Raycast(normalizedPosition);
			if (gizmoComponent != GizmoComponent.None)
			{
				if (gizmoComponent != highlightedComponent)
				{
					if (highlightedComponent != GizmoComponent.None)
					{
						gizmoComponents[(int)highlightedComponent].sharedMaterial = gizmoNormalMaterial;
					}
					if (gizmoComponent != fadingComponent)
					{
						highlightedComponent = gizmoComponent;
						gizmoComponents[(int)highlightedComponent].sharedMaterial = gizmoHighlightMaterial;
					}
					else
					{
						highlightedComponent = GizmoComponent.None;
					}
					updateTargetTexture = true;
				}
			}
			else if (highlightedComponent != GizmoComponent.None)
			{
				gizmoComponents[(int)highlightedComponent].sharedMaterial = gizmoNormalMaterial;
				highlightedComponent = GizmoComponent.None;
				updateTargetTexture = true;
			}
		}

		private void SetHiddenComponent(GizmoComponent component)
		{
			if (component != GizmoComponent.None)
			{
				if (component != fadingComponent)
				{
					if (fadingComponent != GizmoComponent.None)
					{
						SetMaterialOf(fadingComponent, gizmoNormalMaterial);
						SetAlphaOf(fadingComponent, 1f);
						gizmoComponents[(int)fadingComponent].gameObject.SetActive(value: true);
						gizmoComponents[(int)GetOppositeComponent(fadingComponent)].gameObject.SetActive(value: true);
					}
					fadingComponent = component;
					SetMaterialOf(fadingComponent, gizmoFadeMaterial);
					isFadingToZero = true;
					fadeT = 0f;
				}
			}
			else if (fadingComponent != GizmoComponent.None && fadeT >= 1f)
			{
				gizmoComponents[(int)fadingComponent].gameObject.SetActive(value: true);
				gizmoComponents[(int)GetOppositeComponent(fadingComponent)].gameObject.SetActive(value: true);
				isFadingToZero = false;
				fadeT = 0f;
			}
		}

		private void SetAlphaOf(GizmoComponent component, float alpha)
		{
			if (component != GizmoComponent.None)
			{
				gizmoFadeMaterial.SetFloat(gizmoMaterialFadeProperty, alpha);
				switch (component)
				{
				case GizmoComponent.XNegative:
				case GizmoComponent.XPositive:
					labels[0].color = new Color(1f, 1f, 1f, alpha);
					break;
				case GizmoComponent.ZNegative:
				case GizmoComponent.ZPositive:
					labels[2].color = new Color(1f, 1f, 1f, alpha);
					break;
				default:
					labels[1].color = new Color(1f, 1f, 1f, alpha);
					break;
				}
			}
		}

		private void SetMaterialOf(GizmoComponent component, Material material)
		{
			if (component != GizmoComponent.None)
			{
				GizmoComponent oppositeComponent = GetOppositeComponent(component);
				if (component == highlightedComponent || oppositeComponent == highlightedComponent)
				{
					highlightedComponent = GizmoComponent.None;
				}
				gizmoComponents[(int)component].sharedMaterial = material;
				gizmoComponents[(int)oppositeComponent].sharedMaterial = material;
			}
		}

		private GizmoComponent GetOppositeComponent(GizmoComponent component)
		{
			if (component == GizmoComponent.None || component == GizmoComponent.Center)
			{
				return component;
			}
			if ((int)component % 2 == 0)
			{
				return component - 1;
			}
			return component + 1;
		}
	}
}
