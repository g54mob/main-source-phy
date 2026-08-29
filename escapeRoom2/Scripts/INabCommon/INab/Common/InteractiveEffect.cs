using System.Collections;
using System.Collections.Generic;
using INab.CommonVFX;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace INab.Common
{
	[ExecuteAlways]
	public class InteractiveEffect : MonoBehaviour
	{
		private static List<string> typeKeywords = new List<string> { "_TYPE_PLANE", "_TYPE_BOX", "_TYPE_ELLIPSE" };

		[SerializeField]
		[Tooltip("Indicates whether to use VFX Graph Effect.")]
		public bool useVFXGraphEffect = true;

		[SerializeField]
		[Tooltip("Reference to the Visual Effect component.")]
		public VisualEffect visualEffect;

		[SerializeField]
		[Tooltip("Reference to the VFX Property Binder component.")]
		public VFXPropertyBinder propertyBinder;

		[SerializeField]
		[Tooltip("Transform of the mesh used for the effect.")]
		public Transform meshTransform;

		[SerializeField]
		[Tooltip("Renderer of the mesh used for the effect.")]
		public Renderer meshRenderer;

		[SerializeField]
		[Tooltip("Instance of the VFX Uniform Mesh Baker.")]
		public VFXUniformMeshBaker meshBaker = new VFXUniformMeshBaker();

		[Range(0.01f, 10f)]
		[SerializeField]
		[Tooltip("Multiply sample count by this value to control density of the particles. Keep this as low as possible.")]
		public float sampleCountMultiplier = 1f;

		public Coroutine EffectCoroutine;

		public Coroutine EditorCoroutine;

		[SerializeField]
		[Tooltip("Interactive effect mask.")]
		public InteractiveEffectMask mask;

		[SerializeField]
		[Tooltip("Type of SDF mask. Choose from Plane, Box, Sphere, etc.")]
		private InteractiveEffectMaskType maskType = InteractiveEffectMaskType.Ellipse;

		[SerializeField]
		[Tooltip("Automatically updates material properties from the materials list if enabled.")]
		private bool controlMaterialsProperties;

		[SerializeField]
		[Tooltip("Effect shader style. For 'Smooth', set material to transparent.")]
		private ShaderType shaderType;

		[SerializeField]
		[Tooltip("List of materials used by the interactive effect.")]
		public List<Material> materials = new List<Material>();

		[SerializeField]
		[Tooltip("Effect animation curve.")]
		public AnimationCurve effectCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		[SerializeField]
		[Tooltip("Duration of the effect.")]
		public float duration = 2f;

		[SerializeField]
		[Tooltip("Time that the VFX graphs will be turned off after the effect is finished.")]
		public float vfxEventOffset = 2f;

		[SerializeField]
		[Tooltip("Change the effect stage to 0 on initial.")]
		public bool resetOnInitial = true;

		[SerializeField]
		[Tooltip("Ensures that the materials are only affected on the selected mesh renderer. This may breaks SRP batching.")]
		public bool useInstancedMaterials;

		public bool isFinished;

		[Range(0f, 1f)]
		[SerializeField]
		[Tooltip("Lerp value for transforms.")]
		private float effectStateTest = 0.5f;

		[SerializeField]
		[Tooltip("Indicates if position transform is used.")]
		public bool usePositionTransform;

		[SerializeField]
		[Tooltip("Initial position for the transform.")]
		public Vector3 initialPosition;

		[SerializeField]
		[Tooltip("Final position for the transform.")]
		public Vector3 finalPosition;

		[SerializeField]
		[Tooltip("Indicates if scale transform is used.")]
		public bool useScaleTransform;

		[SerializeField]
		[Tooltip("Initial scale for the transform.")]
		public Vector3 initialScale;

		[SerializeField]
		[Tooltip("Final scale for the transform.")]
		public Vector3 finalScale;

		[SerializeField]
		[Tooltip("Indicates if rotation transform is used.")]
		public bool useRotationTransform;

		[SerializeField]
		[Tooltip("Initial rotation for the transform.")]
		public Vector3 initialRotation;

		[SerializeField]
		[Tooltip("Final rotation for the transform.")]
		public Vector3 finalRotation;

		[SerializeField]
		[Tooltip("Rate at which the properties are scaled.")]
		private float propertiesScaleRate = 1f;

		[SerializeField]
		[Tooltip("Inverts the effect effect.")]
		private bool invert;

		[SerializeField]
		[Tooltip("Guide texture for the effect pattern.")]
		private Texture2D guideTexture;

		[SerializeField]
		[Tooltip("Tiling rate of the guide texture.")]
		private float guideTiling = 1f;

		[SerializeField]
		[Range(0f, 1f)]
		[Tooltip("Intensity of the guide texture effect.")]
		private float guideStrength = 0.5f;

		[SerializeField]
		[Tooltip("Enables a background color for the effect.")]
		private bool useBackColor;

		[SerializeField]
		[ColorUsage(true, true)]
		[Tooltip("Background color for the effect effect.")]
		private Color backColor = Color.black;

		[SerializeField]
		[Range(0f, 1f)]
		[Tooltip("Controls the sharpness of the burn effect.")]
		private float burnHardness = 0.5f;

		[SerializeField]
		[Range(0f, 2f)]
		[Tooltip("Determines the offset of the burn effect.")]
		private float burnOffset = 0.5f;

		[SerializeField]
		[Range(0f, 1f)]
		[Tooltip("Offset for the ember effect within the burn area.")]
		private float emberOffset = 0.1f;

		[SerializeField]
		[Range(0f, 1f)]
		[Tooltip("Smoothness of the ember edges.")]
		private float emberSmoothness = 0.1f;

		[SerializeField]
		[Range(0f, 1f)]
		[Tooltip("Width of the ember effect.")]
		private float emberWidth;

		[SerializeField]
		[ColorUsage(true, true)]
		[Tooltip("Color of the ember effect.")]
		private Color emberColor = new Color(10f, 1.8f, 0.2f);

		[SerializeField]
		[ColorUsage(true, true)]
		[Tooltip("Primary color for the burn effect.")]
		private Color burnColor = Color.black;

		[SerializeField]
		[Tooltip("Whether to use dithering to fake transparency.")]
		private bool useDithering;

		[SerializeField]
		[ColorUsage(true, true)]
		[Tooltip("Color of the edge.")]
		private Color edgeColor = Color.blue;

		[SerializeField]
		[Range(0f, 0.2f)]
		[Tooltip("Width of the edge.")]
		private float edgeWidth;

		[SerializeField]
		[Range(0f, 1f)]
		[Tooltip("Smoothness of the edge.")]
		private float edgeSmoothness = 0.05f;

		[SerializeField]
		[Tooltip("Determines if the albedo is affected.")]
		private bool affectAlbedo;

		[SerializeField]
		[ColorUsage(true, true)]
		[Tooltip("Color of the glare effect.")]
		private Color glareColor = Color.blue;

		[SerializeField]
		[Range(0f, 3f)]
		[Tooltip("Strength of the guide texture for the glare effect.")]
		private float glareGuideStrength = 1f;

		[SerializeField]
		[Range(0f, 1f)]
		[Tooltip("Width of the glare effect.")]
		private float glareWidth;

		[SerializeField]
		[Range(0f, 2f)]
		[Tooltip("Smoothness of the glare effect.")]
		private float glareSmoothness = 0.3f;

		[SerializeField]
		[Range(-2f, 2f)]
		[Tooltip("Offset for the glare effect.")]
		private float glareOffset;

		private Vector4 position;

		private Vector4 scaleVector;

		private Vector4 upVector;

		private Vector4 forwardVector;

		private Vector4 rightVector;

		public bool IsSkinnedMesh
		{
			get
			{
				return meshRenderer is SkinnedMeshRenderer;
			}
			private set
			{
			}
		}

		public bool IsMeshReadable
		{
			get
			{
				if (meshRenderer == null)
				{
					return true;
				}
				if (IsSkinnedMesh)
				{
					return (meshRenderer as SkinnedMeshRenderer).sharedMesh.isReadable;
				}
				return meshRenderer.GetComponent<MeshFilter>().sharedMesh.isReadable;
			}
			private set
			{
			}
		}

		public InteractiveEffectMaskType MaskType
		{
			get
			{
				return maskType;
			}
			private set
			{
			}
		}

		public bool ControlMaterialsProperties
		{
			get
			{
				return controlMaterialsProperties;
			}
			private set
			{
			}
		}

		public ShaderType ShaderType
		{
			get
			{
				return shaderType;
			}
			private set
			{
			}
		}

		protected float initialLerpValue { get; }

		public float EffectStateTest
		{
			get
			{
				return effectStateTest;
			}
			private set
			{
			}
		}

		public float PropertiesScaleRate
		{
			get
			{
				return propertiesScaleRate;
			}
			set
			{
				propertiesScaleRate = value;
				UpdateAllMaterialProperties();
			}
		}

		public bool Invert
		{
			get
			{
				return invert;
			}
			set
			{
				invert = value;
				UpdateAllMaterialProperties("Invert", Invert);
			}
		}

		public Texture2D GuideTexture
		{
			get
			{
				return guideTexture;
			}
			set
			{
				guideTexture = value;
				UpdateAllMaterialProperties("GuideTexture", GuideTexture);
			}
		}

		public float GuideTiling
		{
			get
			{
				return guideTiling;
			}
			set
			{
				guideTiling = value;
				UpdateAllMaterialProperties("GuideTiling", GuideTiling);
			}
		}

		public float GuideStrength
		{
			get
			{
				return guideStrength;
			}
			set
			{
				guideStrength = value;
				UpdateAllMaterialProperties("GuideStrength", GuideStrength);
			}
		}

		public bool UseBackColor
		{
			get
			{
				return useBackColor;
			}
			set
			{
				useBackColor = value;
				UpdateAllMaterialProperties("UseBackColor", UseBackColor);
			}
		}

		public Color BackColor
		{
			get
			{
				return backColor;
			}
			set
			{
				backColor = value;
				UpdateAllMaterialProperties("BackColor", BackColor);
			}
		}

		public float BurnHardness
		{
			get
			{
				return burnHardness;
			}
			set
			{
				burnHardness = value;
				UpdateAllMaterialProperties("BurnHardness", BurnHardness);
			}
		}

		public float BurnOffset
		{
			get
			{
				return burnOffset;
			}
			set
			{
				burnOffset = value;
				UpdateAllMaterialProperties("BurnOffset", BurnOffset);
			}
		}

		public float EmberOffset
		{
			get
			{
				return emberOffset;
			}
			set
			{
				emberOffset = value;
				UpdateAllMaterialProperties("EmberOffset", EmberOffset);
			}
		}

		public float EmberSmoothness
		{
			get
			{
				return emberSmoothness;
			}
			set
			{
				emberSmoothness = value;
				UpdateAllMaterialProperties("EmberSmoothness", EmberSmoothness);
			}
		}

		public float EmberWidth
		{
			get
			{
				return emberWidth;
			}
			set
			{
				emberWidth = value;
				UpdateAllMaterialProperties("EmberWidth", EmberWidth);
			}
		}

		public Color EmberColor
		{
			get
			{
				return emberColor;
			}
			set
			{
				emberColor = value;
				UpdateAllMaterialProperties("EmberColor", EmberColor);
			}
		}

		public Color BurnColor
		{
			get
			{
				return burnColor;
			}
			set
			{
				burnColor = value;
				UpdateAllMaterialProperties("BurnColor", BurnColor);
			}
		}

		public bool UseDithering
		{
			get
			{
				return useDithering;
			}
			set
			{
				useDithering = value;
				UpdateAllMaterialProperties("UseDithering", UseDithering);
			}
		}

		public Color EdgeColor
		{
			get
			{
				return edgeColor;
			}
			set
			{
				edgeColor = value;
				UpdateAllMaterialProperties("EdgeColor", EdgeColor);
			}
		}

		public float EdgeWidth
		{
			get
			{
				return edgeWidth;
			}
			set
			{
				edgeWidth = value;
				UpdateAllMaterialProperties("EdgeWidth", EdgeWidth);
			}
		}

		public float EdgeSmoothness
		{
			get
			{
				return edgeSmoothness;
			}
			set
			{
				edgeSmoothness = value;
				UpdateAllMaterialProperties("EdgeSmoothness", EdgeSmoothness);
			}
		}

		public bool AffectAlbedo
		{
			get
			{
				return affectAlbedo;
			}
			set
			{
				affectAlbedo = value;
				UpdateAllMaterialProperties("AffectAlbedo", AffectAlbedo);
			}
		}

		public Color GlareColor
		{
			get
			{
				return glareColor;
			}
			set
			{
				glareColor = value;
				UpdateAllMaterialProperties("GlareColor", GlareColor);
			}
		}

		public float GlareGuideStrength
		{
			get
			{
				return glareGuideStrength;
			}
			set
			{
				glareGuideStrength = value;
				UpdateAllMaterialProperties("GlareGuideStrength", GlareGuideStrength);
			}
		}

		public float GlareWidth
		{
			get
			{
				return glareWidth;
			}
			set
			{
				glareWidth = value;
				UpdateAllMaterialProperties("GlareWidth", GlareWidth);
			}
		}

		public float GlareSmoothness
		{
			get
			{
				return glareSmoothness;
			}
			set
			{
				glareSmoothness = value;
				UpdateAllMaterialProperties("GlareSmoothness", GlareSmoothness);
			}
		}

		public float GlareOffset
		{
			get
			{
				return glareOffset;
			}
			set
			{
				glareOffset = value;
				UpdateAllMaterialProperties("GlareOffset", GlareOffset);
			}
		}

		private void UpdateAllMaterialProperties(string propertyName, Color color)
		{
			foreach (Material material in materials)
			{
				material.SetColor("_" + propertyName, color);
			}
		}

		private void UpdateAllMaterialProperties(string propertyName, float vector)
		{
			foreach (Material material in materials)
			{
				material.SetFloat("_" + propertyName, vector);
			}
		}

		private void UpdateAllMaterialProperties(string propertyName, Vector3 vector)
		{
			foreach (Material material in materials)
			{
				material.SetVector("_" + propertyName, vector);
			}
		}

		private void UpdateAllMaterialProperties(string propertyName, bool value)
		{
			foreach (Material material in materials)
			{
				material.SetInt("_" + propertyName, value ? 1 : 0);
			}
		}

		private void UpdateAllMaterialProperties(string propertyName, Texture value)
		{
			if (value == null)
			{
				return;
			}
			foreach (Material material in materials)
			{
				material.SetTexture("_" + propertyName, value);
			}
		}

		private void UpdateAllMaterialProperties()
		{
			foreach (Material material in materials)
			{
				material.SetInt("_Invert", Invert ? 1 : 0);
				material.SetTexture("_GuideTexture", GuideTexture);
				material.SetFloat("_GuideTiling", GuideTiling / propertiesScaleRate);
				material.SetFloat("_GuideStrength", GuideStrength * propertiesScaleRate);
				material.SetInt("_UseBackColor", UseBackColor ? 1 : 0);
				material.SetColor("_BackColor", BackColor);
			}
			if (shaderType == ShaderType.Burn)
			{
				foreach (Material material2 in materials)
				{
					material2.SetFloat("_BurnHardness", BurnHardness);
					material2.SetFloat("_BurnOffset", BurnOffset * propertiesScaleRate);
					material2.SetFloat("_EmberOffset", EmberOffset * propertiesScaleRate);
					material2.SetFloat("_EmberSmoothness", Mathf.Clamp01(EmberSmoothness * propertiesScaleRate));
					material2.SetFloat("_EmberWidth", EmberWidth * propertiesScaleRate);
					material2.SetColor("_EmberColor", EmberColor);
					material2.SetColor("_BurnColor", BurnColor);
				}
				return;
			}
			if (shaderType != ShaderType.Smooth)
			{
				return;
			}
			foreach (Material material3 in materials)
			{
				material3.SetInt("_UseDithering", UseDithering ? 1 : 0);
				material3.SetColor("_EdgeColor", EdgeColor);
				material3.SetFloat("_EdgeWidth", EdgeWidth * propertiesScaleRate);
				material3.SetFloat("_EdgeSmoothness", Mathf.Clamp01(EdgeSmoothness * propertiesScaleRate));
				material3.SetInt("_AffectAlbedo", AffectAlbedo ? 1 : 0);
				material3.SetColor("_GlareColor", GlareColor);
				material3.SetFloat("_GlareGuideStrength", GlareGuideStrength);
				material3.SetFloat("_GlareWidth", GlareWidth * propertiesScaleRate);
				material3.SetFloat("_GlareSmoothness", GlareSmoothness * propertiesScaleRate);
				material3.SetFloat("_GlareOffset", GlareOffset * propertiesScaleRate);
			}
		}

		private void DisableMaterialTypeKeyword(Material material)
		{
			LocalKeyword[] enabledKeywords = material.enabledKeywords;
			for (int i = 0; i < enabledKeywords.Length; i++)
			{
				LocalKeyword keyword = enabledKeywords[i];
				if (typeKeywords.Contains(keyword.name))
				{
					material.DisableKeyword(in keyword);
				}
			}
		}

		private void EnableMaterialTypeKeyword(Material material)
		{
			material.EnableKeyword(typeKeywords[(int)maskType]);
		}

		private void ResetSdfTransformToDefault(ref Vector4 position, ref Vector4 forward, ref Vector4 right, ref Vector4 up)
		{
			position = new Vector4(0f, 999999f, 0f, 0f);
			forward = Vector3.forward;
			up = Vector3.up;
			right = Vector3.right;
		}

		private void SetSdfTransform(InteractiveEffectMask mask, ref Vector4 position, ref Vector4 scale, ref Vector4 forward, ref Vector4 right, ref Vector4 up)
		{
			Transform transform = mask.transform;
			position = transform.position;
			scale = new Vector4(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z, 0f);
			switch (maskType)
			{
			case InteractiveEffectMaskType.Plane:
				up = transform.up;
				break;
			case InteractiveEffectMaskType.Box:
				forward = transform.forward;
				up = transform.up;
				right = transform.right;
				scale *= 0.5f;
				break;
			case InteractiveEffectMaskType.Ellipse:
			{
				forward = transform.forward;
				up = transform.up;
				right = transform.right;
				float num = 0.01f;
				if (scale.x < num && scale.x > 0f - num)
				{
					scale.x = num;
				}
				if (scale.y < num && scale.y > 0f - num)
				{
					scale.y = num;
				}
				if (scale.z < num && scale.z > 0f - num)
				{
					scale.z = num;
				}
				break;
			}
			}
		}

		private void UpdateSDFTransformProperties()
		{
			foreach (Material material in materials)
			{
				if (material == null)
				{
					break;
				}
				UpdateCommonTransformProperties(material);
				switch (maskType)
				{
				case InteractiveEffectMaskType.Plane:
					UpdateUpVectorProperties(material);
					break;
				case InteractiveEffectMaskType.Box:
					UpdateRotationProperties(material);
					break;
				case InteractiveEffectMaskType.Ellipse:
					UpdateRotationProperties(material);
					break;
				}
			}
		}

		private void UpdateCommonTransformProperties(Material material)
		{
			material.SetVector("_PositionVector", position);
			material.SetVector("_ScaleVector", scaleVector);
		}

		private void UpdateRotationProperties(Material material)
		{
			material.SetVector("_UpVector", upVector);
			material.SetVector("_RightVector", rightVector);
			material.SetVector("_ForwardVector", forwardVector);
		}

		private void UpdateUpVectorProperties(Material material)
		{
			material.SetVector("_UpVector", upVector);
		}

		private void UpdateCommonVFXProperties(VisualEffect particleEffect)
		{
			particleEffect.SetVector4("Mask Position", position);
			particleEffect.SetVector4("Mask Scale", scaleVector);
		}

		private void UpdateRotationVFXProperties(VisualEffect particleEffect)
		{
			particleEffect.SetVector4("Mask Up", upVector);
			particleEffect.SetVector4("Mask Right", rightVector);
			particleEffect.SetVector4("Mask Forward", forwardVector);
		}

		private void UpdateUpVectorVFXProperties(VisualEffect particleEffect)
		{
			particleEffect.SetVector4("Mask Up", upVector);
		}

		private void UpdateVFXSDFTransformProperties()
		{
			if (!(visualEffect == null) && useVFXGraphEffect && visualEffect.HasGraphicsBuffer(VFXUniformMeshBaker.GraphicsBufferName))
			{
				UpdateCommonVFXProperties(visualEffect);
				switch (MaskType)
				{
				case InteractiveEffectMaskType.Plane:
					UpdateUpVectorVFXProperties(visualEffect);
					break;
				case InteractiveEffectMaskType.Box:
					UpdateRotationVFXProperties(visualEffect);
					break;
				case InteractiveEffectMaskType.Ellipse:
					UpdateRotationVFXProperties(visualEffect);
					break;
				}
			}
		}

		private void UpdateSdfParameters()
		{
			if (mask != null)
			{
				SetSdfTransform(mask, ref position, ref scaleVector, ref forwardVector, ref rightVector, ref upVector);
			}
		}

		private void UpdateMaskType()
		{
			if (mask == null)
			{
				return;
			}
			mask.Type = MaskType;
			if (!(visualEffect == null) && useVFXGraphEffect)
			{
				bool b = false;
				bool b2 = false;
				bool b3 = false;
				switch (MaskType)
				{
				case InteractiveEffectMaskType.Plane:
					b = true;
					break;
				case InteractiveEffectMaskType.Box:
					b2 = true;
					break;
				case InteractiveEffectMaskType.Ellipse:
					b3 = true;
					break;
				}
				visualEffect.SetBool("Use Plane", b);
				visualEffect.SetBool("Use Box", b2);
				visualEffect.SetBool("Use Ellipse", b3);
			}
		}

		private void ForceUpdateMaskParameters()
		{
			UpdateSdfParameters();
			UpdateSDFTransformProperties();
			UpdateVFXSDFTransformProperties();
		}

		protected void Start()
		{
			if (resetOnInitial)
			{
				StartTransformCheck(initialLerpValue);
			}
			if (useInstancedMaterials && Application.isPlaying)
			{
				GetRendererMaterials(instancedMaterials: true);
			}
			UpdateAndSetupEffect();
		}

		protected void Update()
		{
			if (useVFXGraphEffect && (bool)visualEffect && (bool)meshRenderer)
			{
				meshBaker.Update(visualEffect, meshRenderer);
			}
		}

		protected void LateUpdate()
		{
			ForceUpdateMaskParameters();
		}

		protected void OnDisable()
		{
			meshBaker.OnDisable();
		}

		protected void OnValidate()
		{
			if (base.enabled && base.gameObject.activeSelf)
			{
				if (useVFXGraphEffect)
				{
					meshBaker.SampleCountMultiplier = sampleCountMultiplier;
				}
				else
				{
					SendStopEvent();
				}
				if (controlMaterialsProperties)
				{
					UpdateAllMaterialProperties();
				}
			}
		}

		public void _BakeUniformMesh()
		{
			meshBaker.Bake(visualEffect, meshRenderer);
		}

		public void _SetGraphicsBuffer()
		{
			meshBaker.SetGraphicsBuffer(visualEffect);
		}

		public void _ClearMaterialsList()
		{
			materials.Clear();
		}

		public void _FindRendererInParent()
		{
			meshRenderer = GetComponentInParent<Renderer>();
			if (meshRenderer == null)
			{
				meshRenderer = base.gameObject.transform.parent.GetComponentInChildren<Renderer>();
			}
			if (meshRenderer == null)
			{
				Debug.LogWarning("No renderer could be found.");
			}
		}

		public void _SetupVfxGraphGameObject()
		{
			GameObject gameObject = new GameObject("Vfx Graph");
			gameObject.transform.parent = base.transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			visualEffect = gameObject.AddComponent<VisualEffect>();
			visualEffect.initialEventName = "";
			propertyBinder = gameObject.AddComponent<VFXPropertyBinder>();
		}

		public void _SetupMaskGameObject()
		{
			GameObject gameObject = new GameObject("Mask");
			gameObject.transform.parent = base.transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			mask = gameObject.AddComponent<InteractiveEffectMask>();
		}

		public void _SetInitialMaskTransform()
		{
			Transform transform = mask.transform;
			initialPosition = transform.localPosition;
			initialRotation = transform.localRotation.eulerAngles;
			initialScale = transform.localScale;
		}

		public void _SetFinalMaskTransform()
		{
			Transform transform = mask.transform;
			finalPosition = transform.localPosition;
			finalRotation = transform.localRotation.eulerAngles;
			finalScale = transform.localScale;
		}

		public bool _DoMaterialsUseProperShaders()
		{
			bool result = true;
			foreach (Material material in materials)
			{
				string text = material.shader.name;
				if (text[..text.LastIndexOf(' ')] != "INab Studio/Interactive Effect")
				{
					result = false;
				}
			}
			return result;
		}

		public virtual void UpdateAndSetupEffect()
		{
			if (useVFXGraphEffect)
			{
				VFXMeshSetup.SetupRenderer(meshRenderer, visualEffect);
				VFXMeshSetup.SetupPropertyBinder(propertyBinder, meshTransform);
			}
			ForceUpdateMaskParameters();
			ChangeMaskType(maskType);
			OnValidate();
			if (controlMaterialsProperties)
			{
				UpdateAllMaterialProperties();
			}
			PassMaterialPropertiesToGraph();
			if (useVFXGraphEffect)
			{
				_BakeUniformMesh();
			}
		}

		public void ChangeMaskType(InteractiveEffectMaskType newType)
		{
			maskType = newType;
			foreach (Material material in materials)
			{
				DisableMaterialTypeKeyword(material);
				EnableMaterialTypeKeyword(material);
			}
			UpdateMaskType();
		}

		public void GetRendererMaterials(bool instancedMaterials = false)
		{
			materials.Clear();
			if (instancedMaterials)
			{
				materials.AddRange(meshRenderer.materials);
			}
			else
			{
				materials.AddRange(meshRenderer.sharedMaterials);
			}
		}

		public void PlayEffect()
		{
			EffectCoroutine = StartCoroutine(EffectEnumerator());
			if (useInstancedMaterials && Application.isPlaying)
			{
				GetRendererMaterials(instancedMaterials: true);
			}
		}

		public void ReverseEffect(float customDuration)
		{
			EffectCoroutine = StartCoroutine(EffectEnumerator(useParticles: false, inversed: true, customDuration));
			if (useInstancedMaterials && Application.isPlaying)
			{
				GetRendererMaterials(instancedMaterials: true);
			}
		}

		public void ReverseEffect()
		{
			ReverseEffect(duration);
		}

		private void StartTransformCheck(float lerpValue)
		{
			UpdateMaskTransform(lerpValue);
			ForceUpdateMaskParameters();
		}

		public void UpdateMaskTransform(float effectLerp)
		{
			if (!(mask == null))
			{
				Transform transform = mask.transform;
				if (usePositionTransform)
				{
					Vector3 localPosition = Vector3.Lerp(initialPosition, finalPosition, effectLerp);
					transform.localPosition = localPosition;
				}
				if (useScaleTransform)
				{
					Vector3 localScale = Vector3.Lerp(initialScale, finalScale, effectLerp);
					transform.localScale = localScale;
				}
				if (useRotationTransform)
				{
					Quaternion localRotation = Quaternion.Lerp(Quaternion.Euler(initialRotation), Quaternion.Euler(finalRotation), effectLerp);
					transform.localRotation = localRotation;
				}
			}
		}

		private IEnumerator EffectEnumerator(bool useParticles = true, bool inversed = false, float customDuration = 1f)
		{
			if (EffectCoroutine != null)
			{
				StopCoroutine(EffectCoroutine);
			}
			if (useVFXGraphEffect && useParticles)
			{
				SendPlayEvent();
			}
			if (inversed)
			{
				SendStopEvent();
			}
			else
			{
				customDuration = duration;
			}
			float elapsedTime = 0f;
			while (elapsedTime < customDuration)
			{
				elapsedTime += Time.deltaTime;
				float num = effectCurve.Evaluate(elapsedTime / customDuration);
				if (inversed)
				{
					num = 1f - num;
				}
				UpdateMaskTransform(num);
				yield return null;
			}
			isFinished = true;
			float elapsedTimeVFX = 0f;
			while (elapsedTimeVFX < vfxEventOffset)
			{
				elapsedTimeVFX += Time.deltaTime;
				yield return null;
			}
			if (useVFXGraphEffect && useParticles)
			{
				SendStopEvent();
			}
		}

		public IEnumerator AutoEffectCoroutine()
		{
			float coroutnieTimeOffset = 0.8f;
			float timeLasted = duration;
			isFinished = false;
			PlayEffect();
			while (true)
			{
				timeLasted -= Time.deltaTime;
				if (timeLasted < 0f - coroutnieTimeOffset)
				{
					isFinished = true;
					PlayEffect();
					timeLasted = duration;
				}
				yield return null;
			}
		}

		public void SendPlayEvent()
		{
			if ((bool)visualEffect)
			{
				visualEffect.Play();
			}
		}

		public void SendStopEvent()
		{
			if ((bool)visualEffect)
			{
				visualEffect.Stop();
			}
		}

		public void PassMaterialPropertiesToGraph()
		{
			if (materials.Count < 1)
			{
				Debug.LogWarning("There is no materials to copy properties from in the materials list.");
				return;
			}
			Material material = materials[0];
			if (material == null)
			{
				Debug.LogWarning("First material in materials list is null.");
			}
			else if (visualEffect != null && useVFXGraphEffect)
			{
				if ((bool)material.GetTexture("_GuideTexture"))
				{
					visualEffect.SetTexture("Guide Texture", material.GetTexture("_GuideTexture"));
					visualEffect.SetFloat("Guide Strength", material.GetFloat("_GuideStrength"));
				}
				else
				{
					visualEffect.SetFloat("Guide Strength", 0f);
				}
				visualEffect.SetFloat("Guide Tiling", material.GetFloat("_GuideTiling"));
				if ((bool)material.GetTexture("_BaseMap"))
				{
					visualEffect.SetTexture("Texture", material.GetTexture("_BaseMap"));
				}
				visualEffect.SetVector2("Tiling", material.GetVector("_Tiling"));
			}
		}
	}
}
