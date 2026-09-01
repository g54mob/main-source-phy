using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace cakeslice
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Camera))]
	public class OutlineEffect : MonoBehaviour
	{
		private static OutlineEffect m_instance;

		private readonly LinkedSet<Outline> outlines = new LinkedSet<Outline>();

		[Range(1f, 6f)]
		public float lineThickness = 1.25f;

		[Range(0f, 10f)]
		public float lineIntensity = 0.5f;

		[Range(0f, 1f)]
		public float fillAmount = 0.2f;

		private float originalFillAmount;

		[HideInInspector]
		public Color lineColor0;

		[HideInInspector]
		public Color lineColor1;

		[HideInInspector]
		public Color lineColor2;

		private int currentTargetType = -1;

		public bool additiveRendering;

		public bool backfaceCulling = true;

		[Header("These settings can affect performance!")]
		public bool cornerOutlines;

		public bool addLinesBetweenColors;

		[Header("Advanced settings")]
		public bool scaleWithScreenSize = true;

		[Range(0.1f, 0.9f)]
		public float alphaCutoff = 0.5f;

		public bool flipY;

		public Camera sourceCamera;

		[HideInInspector]
		public Camera outlineCamera;

		private Material outline1Material;

		private Material outline2Material;

		private Material outline3Material;

		private Material outlineEraseMaterial;

		private Shader outlineShader;

		private Shader outlineBufferShader;

		[HideInInspector]
		public Material outlineShaderMaterial;

		[HideInInspector]
		public RenderTexture renderTexture;

		[HideInInspector]
		public RenderTexture extraRenderTexture;

		private CommandBuffer commandBuffer;

		private Material m;

		private Texture plainTexture;

		private int outlineCount;

		private Dictionary<Texture, Dictionary<int, Material>> materialBufferDir = new Dictionary<Texture, Dictionary<int, Material>>();

		private Dictionary<int, Material> colorDir;

		public static OutlineEffect Instance
		{
			get
			{
				if (object.Equals(m_instance, null))
				{
					return m_instance = UnityEngine.Object.FindObjectOfType(typeof(OutlineEffect)) as OutlineEffect;
				}
				return m_instance;
			}
		}

		private OutlineEffect()
		{
		}

		private Material GetMaterialFromID(int ID)
		{
			switch (ID)
			{
			case 0:
				return outline1Material;
			case 1:
				return outline2Material;
			default:
				return outline3Material;
			}
		}

		private Material CreateMaterial(Color emissionColor)
		{
			Material material = new Material(outlineBufferShader);
			material.SetColor("_OutlineColor", emissionColor);
			material.SetInt("_SrcBlend", 5);
			material.SetInt("_DstBlend", 10);
			material.SetInt("_ZWrite", 0);
			material.DisableKeyword("_ALPHATEST_ON");
			material.EnableKeyword("_ALPHABLEND_ON");
			material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
			material.renderQueue = 3000;
			return material;
		}

		private void Awake()
		{
			m_instance = this;
		}

		private void Start()
		{
			ChangeTargetType(0);
			CreateMaterialsIfNeeded();
			UpdateMaterialsPublicProperties();
			if (sourceCamera == null)
			{
				sourceCamera = GetComponent<Camera>();
				if (sourceCamera == null)
				{
					sourceCamera = Camera.main;
				}
			}
			if (outlineCamera == null)
			{
				GameObject gameObject = new GameObject("Outline Camera");
				gameObject.transform.parent = sourceCamera.transform;
				outlineCamera = gameObject.AddComponent<Camera>();
				outlineCamera.enabled = false;
			}
			renderTexture = new RenderTexture(sourceCamera.pixelWidth, sourceCamera.pixelHeight, 16, RenderTextureFormat.Default);
			extraRenderTexture = new RenderTexture(sourceCamera.pixelWidth, sourceCamera.pixelHeight, 16, RenderTextureFormat.Default);
			UpdateOutlineCameraFromSource();
			commandBuffer = new CommandBuffer();
			outlineCamera.AddCommandBuffer(CameraEvent.BeforeImageEffects, commandBuffer);
			plainTexture = Texture2D.whiteTexture;
			originalFillAmount = fillAmount;
			base.enabled = false;
			ReferenceMaster.onMachineSimulation = (Action<Machine, bool>)Delegate.Combine(ReferenceMaster.onMachineSimulation, new Action<Machine, bool>(SimToggle));
		}

		private void SimToggle(Machine m, bool toggle)
		{
			if (m == Machine.Active())
			{
				Instance.enabled = !toggle;
			}
		}

		public void ResetFillAmount()
		{
			fillAmount = originalFillAmount;
		}

		public void OnPreRender()
		{
			if (commandBuffer == null)
			{
				return;
			}
			CreateMaterialsIfNeeded();
			if (renderTexture == null || renderTexture.width != sourceCamera.pixelWidth || renderTexture.height != sourceCamera.pixelHeight)
			{
				renderTexture = new RenderTexture(sourceCamera.pixelWidth, sourceCamera.pixelHeight, 16, RenderTextureFormat.Default);
				extraRenderTexture = new RenderTexture(sourceCamera.pixelWidth, sourceCamera.pixelHeight, 16, RenderTextureFormat.Default);
				outlineCamera.targetTexture = renderTexture;
			}
			UpdateMaterialsPublicProperties();
			UpdateOutlineCameraFromSource();
			outlineCamera.targetTexture = renderTexture;
			commandBuffer.SetRenderTarget(renderTexture);
			commandBuffer.Clear();
			bool flag = true;
			if (outlines != null)
			{
				LayerMask layerMask = sourceCamera.cullingMask;
				foreach (Outline outline in outlines)
				{
					if (object.ReferenceEquals(outline, null) || (int)layerMask != ((int)layerMask | 1))
					{
						continue;
					}
					for (int i = 0; i < outline.materialAmount; i++)
					{
						m = null;
						Material objA = outline.materialArray[i];
						if (!object.ReferenceEquals(objA, null) && !object.ReferenceEquals(outline.Tex, null))
						{
							colorDir = null;
							if ((!outline.ignoreAlpha) ? materialBufferDir.TryGetValue(outline.Tex, out colorDir) : materialBufferDir.TryGetValue(plainTexture, out colorDir))
							{
								if (!colorDir.TryGetValue(outline.color, out m))
								{
									if (outline.eraseRenderer)
									{
										m = new Material(outlineEraseMaterial);
									}
									else
									{
										m = new Material(GetMaterialFromID(outline.color));
									}
									m.mainTexture = ((!outline.ignoreAlpha) ? outline.Tex : plainTexture);
									colorDir.Add(outline.color, m);
								}
							}
							else
							{
								if (outline.eraseRenderer)
								{
									m = new Material(outlineEraseMaterial);
								}
								else
								{
									m = new Material(GetMaterialFromID(outline.color));
								}
								m.mainTexture = ((!outline.ignoreAlpha) ? outline.Tex : plainTexture);
								colorDir = new Dictionary<int, Material>();
								colorDir.Add(outline.color, m);
								materialBufferDir.Add((!outline.ignoreAlpha) ? outline.Tex : plainTexture, colorDir);
							}
						}
						else if (outline.eraseRenderer)
						{
							m = outlineEraseMaterial;
						}
						else
						{
							m = GetMaterialFromID(outline.color);
						}
						if (outline.useFill)
						{
							flag = false;
						}
						commandBuffer.DrawRenderer(outline.Renderer, m, 0, 0);
						if (outline.hasFilter)
						{
							for (int j = 1; j < outline.Filter.mesh.subMeshCount; j++)
							{
								commandBuffer.DrawRenderer(outline.Renderer, m, j, 0);
							}
						}
						else if (outline.isSkinned)
						{
							SkinnedMeshRenderer skinnedMeshRenderer = outline.Renderer as SkinnedMeshRenderer;
							for (int k = 1; k < skinnedMeshRenderer.sharedMesh.subMeshCount; k++)
							{
								commandBuffer.DrawRenderer(outline.Renderer, m, k, 0);
							}
						}
					}
				}
			}
			if (flag)
			{
				outlineShaderMaterial.SetFloat("_FillAmount", 0f);
			}
			else
			{
				outlineShaderMaterial.SetFloat("_FillAmount", fillAmount);
			}
			outlineCamera.Render();
		}

		private void OnDestroy()
		{
			if (renderTexture != null)
			{
				renderTexture.Release();
			}
			if (extraRenderTexture != null)
			{
				extraRenderTexture.Release();
			}
			DestroyMaterials();
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			outlineShaderMaterial.SetTexture("_OutlineSource", renderTexture);
			if (addLinesBetweenColors)
			{
				Graphics.Blit(source, extraRenderTexture, outlineShaderMaterial, 0);
				outlineShaderMaterial.SetTexture("_OutlineSource", extraRenderTexture);
			}
			Graphics.Blit(source, destination, outlineShaderMaterial, 1);
		}

		private void CreateMaterialsIfNeeded()
		{
			if (outlineShader == null)
			{
				outlineShader = Resources.Load<Shader>("OutlineShader");
			}
			if (outlineBufferShader == null)
			{
				outlineBufferShader = Resources.Load<Shader>("OutlineBufferShader");
			}
			if (outlineShaderMaterial == null)
			{
				outlineShaderMaterial = new Material(outlineShader);
				outlineShaderMaterial.hideFlags = HideFlags.HideAndDontSave;
				UpdateMaterialsPublicProperties();
			}
			if (outlineEraseMaterial == null)
			{
				outlineEraseMaterial = CreateMaterial(new Color(0f, 0f, 0f, 0f));
			}
			if (outline1Material == null)
			{
				outline1Material = CreateMaterial(new Color(1f, 0f, 0f, 0f));
			}
			if (outline2Material == null)
			{
				outline2Material = CreateMaterial(new Color(0f, 1f, 0f, 0f));
			}
			if (outline3Material == null)
			{
				outline3Material = CreateMaterial(new Color(0f, 0f, 1f, 0f));
			}
		}

		private void DestroyMaterials()
		{
			foreach (Dictionary<int, Material> value in materialBufferDir.Values)
			{
				foreach (Material value2 in value.Values)
				{
					UnityEngine.Object.DestroyImmediate(value2);
				}
			}
			materialBufferDir.Clear();
			UnityEngine.Object.DestroyImmediate(outlineShaderMaterial);
			UnityEngine.Object.DestroyImmediate(outlineEraseMaterial);
			UnityEngine.Object.DestroyImmediate(outline1Material);
			UnityEngine.Object.DestroyImmediate(outline2Material);
			UnityEngine.Object.DestroyImmediate(outline3Material);
			outlineShader = null;
			outlineBufferShader = null;
			outlineShaderMaterial = null;
			outlineEraseMaterial = null;
			outline1Material = null;
			outline2Material = null;
			outline3Material = null;
		}

		public void UpdateMaterialsPublicProperties()
		{
			if ((bool)outlineShaderMaterial)
			{
				float num = 1f;
				if (scaleWithScreenSize)
				{
					num = (float)Screen.height / 360f;
				}
				if (scaleWithScreenSize && num < 1f)
				{
					outlineShaderMaterial.SetFloat("_LineThicknessX", 0.001f * (1f / (float)Screen.width) * 1000f);
					outlineShaderMaterial.SetFloat("_LineThicknessY", 0.001f * (1f / (float)Screen.height) * 1000f);
				}
				else
				{
					outlineShaderMaterial.SetFloat("_LineThicknessX", num * (lineThickness / 1000f) * (1f / (float)Screen.width) * 1000f);
					outlineShaderMaterial.SetFloat("_LineThicknessY", num * (lineThickness / 1000f) * (1f / (float)Screen.height) * 1000f);
				}
				outlineShaderMaterial.SetFloat("_LineIntensity", lineIntensity);
				outlineShaderMaterial.SetFloat("_FillAmount", fillAmount);
				outlineShaderMaterial.SetColor("_LineColor1", lineColor0 * lineColor0);
				outlineShaderMaterial.SetColor("_LineColor2", lineColor1 * lineColor1);
				outlineShaderMaterial.SetColor("_LineColor3", lineColor2 * lineColor2);
				if (flipY)
				{
					outlineShaderMaterial.SetInt("_FlipY", 1);
				}
				else
				{
					outlineShaderMaterial.SetInt("_FlipY", 0);
				}
				if (!additiveRendering)
				{
					outlineShaderMaterial.SetInt("_Dark", 1);
				}
				else
				{
					outlineShaderMaterial.SetInt("_Dark", 0);
				}
				if (cornerOutlines)
				{
					outlineShaderMaterial.SetInt("_CornerOutlines", 1);
				}
				else
				{
					outlineShaderMaterial.SetInt("_CornerOutlines", 0);
				}
				Shader.SetGlobalFloat("_OutlineAlphaCutoff", alphaCutoff);
				if (backfaceCulling)
				{
					outlineShaderMaterial.SetInt("_Culling", 2);
				}
				else
				{
					outlineShaderMaterial.SetInt("_Culling", 0);
				}
			}
		}

		private void UpdateOutlineCameraFromSource()
		{
			outlineCamera.CopyFrom(sourceCamera);
			outlineCamera.renderingPath = RenderingPath.Forward;
			outlineCamera.backgroundColor = new Color(0f, 0f, 0f, 0f);
			outlineCamera.clearFlags = CameraClearFlags.Color;
			outlineCamera.rect = new Rect(0f, 0f, 1f, 1f);
			outlineCamera.cullingMask = 0;
			outlineCamera.targetTexture = renderTexture;
			outlineCamera.enabled = false;
			outlineCamera.hdr = false;
		}

		public void AddOutline(Outline outline)
		{
			if (!outlines.Contains(outline))
			{
				outlines.Add(outline);
			}
		}

		public void RemoveOutline(Outline outline)
		{
			if (outlines.Contains(outline))
			{
				outlines.Remove(outline);
			}
		}

		public void ChangeTargetType(int type)
		{
			if (currentTargetType == type)
			{
				return;
			}
			ReferenceMaster instance = ReferenceMaster.Instance;
			currentTargetType = type;
			if ((bool)instance)
			{
				switch (type)
				{
				case 0:
					lineColor0 = (lineColor1 = (lineColor2 = instance.ObjectSelectionColor));
					addLinesBetweenColors = false;
					break;
				case 1:
					lineColor0 = (lineColor2 = instance.ObjectSelectionColor);
					lineColor1 = instance.BlockHighlightColor;
					addLinesBetweenColors = instance.colorBetweenBlockOutlines;
					break;
				default:
					lineColor0 = (lineColor1 = (lineColor2 = instance.ObjectSelectionColor));
					addLinesBetweenColors = false;
					break;
				}
			}
			else
			{
				Debug.LogWarning("Missing ReferenceMaster");
			}
		}

		public static void ToggleOutline(bool toggle)
		{
			if (!StatMaster.showOutline)
			{
				Instance.outlineCount = 0;
				Instance.enabled = false;
			}
			else if (toggle)
			{
				if (Instance.outlineCount == 0)
				{
					Instance.enabled = true;
				}
				Instance.outlineCount++;
			}
			else
			{
				Instance.outlineCount = Mathf.Max(Instance.outlineCount - 1, 0);
				if (Instance.outlineCount <= 0)
				{
					Instance.enabled = false;
				}
			}
		}
	}
}
