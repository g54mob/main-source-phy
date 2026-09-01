using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace HighlightingSystem
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Camera))]
	public class HighlightingBase : MonoBehaviour
	{
		protected static readonly Color colorClear = new Color(0f, 0f, 0f, 0f);

		protected static readonly string renderBufferName = "HighlightingSystem";

		protected static readonly Matrix4x4 identityMatrix = Matrix4x4.identity;

		protected static readonly string keywordStraightDirections = "STRAIGHT_DIRECTIONS";

		protected static readonly string keywordAllDirections = "ALL_DIRECTIONS";

		protected static readonly string profileHighlightingSystem = "HighlightingSystem";

		protected const CameraEvent queue = CameraEvent.BeforeImageEffectsOpaque;

		protected static Camera currentCamera;

		protected static HashSet<HighlighterRenderer> visibleRenderers = new HashSet<HighlighterRenderer>();

		protected CommandBuffer renderBuffer;

		protected RenderTextureDescriptor cachedDescriptor;

		[SerializeField]
		protected float _fillAlpha;

		[FormerlySerializedAs("downsampleFactor")]
		[SerializeField]
		protected int _downsampleFactor = 4;

		[FormerlySerializedAs("iterations")]
		[SerializeField]
		protected int _iterations = 2;

		[FormerlySerializedAs("blurMinSpread")]
		[SerializeField]
		protected float _blurMinSpread = 0.65f;

		[FormerlySerializedAs("blurSpread")]
		[SerializeField]
		protected float _blurSpread = 0.25f;

		[SerializeField]
		protected float _blurIntensity = 0.3f;

		[SerializeField]
		protected BlurDirections _blurDirections;

		[SerializeField]
		protected HighlightingBlitter _blitter;

		[SerializeField]
		protected AntiAliasing _antiAliasing;

		protected RenderTargetIdentifier highlightingBufferID;

		protected RenderTargetIdentifier blur1ID;

		protected RenderTargetIdentifier blur2ID;

		protected RenderTexture highlightingBuffer;

		protected Camera cam;

		protected const int BLUR = 0;

		protected const int CUT = 1;

		protected const int COMP = 2;

		protected static readonly string[] shaderPaths = new string[3] { "Hidden/Highlighted/Blur", "Hidden/Highlighted/Cut", "Hidden/Highlighted/Composite" };

		protected static Shader[] shaders;

		protected static Material[] materials;

		protected Material blurMaterial;

		protected Material cutMaterial;

		protected Material compMaterial;

		protected static bool initialized = false;

		public bool isSupported => CheckSupported(verbose: false);

		public float fillAlpha
		{
			get
			{
				return _fillAlpha;
			}
			set
			{
				value = Mathf.Clamp01(value);
				if (_fillAlpha != value)
				{
					if (Application.isPlaying)
					{
						cutMaterial.SetFloat(ShaderPropertyID._HighlightingFillAlpha, value);
					}
					_fillAlpha = value;
				}
			}
		}

		public int downsampleFactor
		{
			get
			{
				return _downsampleFactor;
			}
			set
			{
				if (_downsampleFactor != value)
				{
					if (value != 0 && (value & (value - 1)) == 0)
					{
						_downsampleFactor = value;
					}
					else
					{
						Debug.LogWarning("HighlightingSystem : Prevented attempt to set incorrect downsample factor value.");
					}
				}
			}
		}

		public int iterations
		{
			get
			{
				return _iterations;
			}
			set
			{
				if (_iterations != value)
				{
					_iterations = value;
				}
			}
		}

		public float blurMinSpread
		{
			get
			{
				return _blurMinSpread;
			}
			set
			{
				if (_blurMinSpread != value)
				{
					_blurMinSpread = value;
				}
			}
		}

		public float blurSpread
		{
			get
			{
				return _blurSpread;
			}
			set
			{
				if (_blurSpread != value)
				{
					_blurSpread = value;
				}
			}
		}

		public float blurIntensity
		{
			get
			{
				return _blurIntensity;
			}
			set
			{
				if (_blurIntensity != value)
				{
					_blurIntensity = value;
					if (Application.isPlaying)
					{
						blurMaterial.SetFloat(ShaderPropertyID._HighlightingIntensity, _blurIntensity);
					}
				}
			}
		}

		public BlurDirections blurDirections
		{
			get
			{
				return _blurDirections;
			}
			set
			{
				if (_blurDirections != value)
				{
					_blurDirections = value;
					if (Application.isPlaying)
					{
						blurMaterial.SetKeyword(keywordStraightDirections, _blurDirections == BlurDirections.Straight);
						blurMaterial.SetKeyword(keywordAllDirections, _blurDirections == BlurDirections.All);
					}
				}
			}
		}

		public HighlightingBlitter blitter
		{
			get
			{
				return _blitter;
			}
			set
			{
				if (_blitter != value)
				{
					if (_blitter != null)
					{
						_blitter.Unregister(this);
					}
					_blitter = value;
					if (_blitter != null)
					{
						_blitter.Register(this);
					}
				}
			}
		}

		public AntiAliasing antiAliasing
		{
			get
			{
				return _antiAliasing;
			}
			set
			{
				if (_antiAliasing != value)
				{
					_antiAliasing = value;
				}
			}
		}

		protected virtual void OnEnable()
		{
			Initialize();
			if (!CheckSupported(verbose: true))
			{
				base.enabled = false;
				Debug.LogError("HighlightingSystem : Highlighting System has been disabled due to unsupported Unity features on the current platform!");
				return;
			}
			blur1ID = new RenderTargetIdentifier(ShaderPropertyID._HighlightingBlur1);
			blur2ID = new RenderTargetIdentifier(ShaderPropertyID._HighlightingBlur2);
			blurMaterial = new Material(materials[0]);
			cutMaterial = new Material(materials[1]);
			compMaterial = new Material(materials[2]);
			blurMaterial.SetKeyword(keywordStraightDirections, _blurDirections == BlurDirections.Straight);
			blurMaterial.SetKeyword(keywordAllDirections, _blurDirections == BlurDirections.All);
			blurMaterial.SetFloat(ShaderPropertyID._HighlightingIntensity, _blurIntensity);
			cutMaterial.SetFloat(ShaderPropertyID._HighlightingFillAlpha, _fillAlpha);
			renderBuffer = new CommandBuffer();
			renderBuffer.name = renderBufferName;
			cam = GetComponent<Camera>();
			cam.depthTextureMode |= DepthTextureMode.Depth;
			cam.AddCommandBuffer(CameraEvent.BeforeImageEffectsOpaque, renderBuffer);
			if (_blitter != null)
			{
				_blitter.Register(this);
			}
			EndOfFrame.AddListener(OnEndOfFrame);
		}

		protected virtual void OnDisable()
		{
			if (renderBuffer != null)
			{
				cam.RemoveCommandBuffer(CameraEvent.BeforeImageEffectsOpaque, renderBuffer);
				renderBuffer = null;
			}
			if (highlightingBuffer != null && highlightingBuffer.IsCreated())
			{
				highlightingBuffer.Release();
				highlightingBuffer = null;
			}
			if (_blitter != null)
			{
				_blitter.Unregister(this);
			}
			EndOfFrame.RemoveListener(OnEndOfFrame);
		}

		protected virtual void OnPreCull()
		{
			currentCamera = cam;
			visibleRenderers.Clear();
		}

		protected virtual void OnPreRender()
		{
			RenderTextureDescriptor descriptor = GetDescriptor();
			if (highlightingBuffer == null || !Equals(cachedDescriptor, descriptor))
			{
				if (highlightingBuffer != null)
				{
					if (highlightingBuffer.IsCreated())
					{
						highlightingBuffer.Release();
					}
					highlightingBuffer = null;
				}
				cachedDescriptor = descriptor;
				highlightingBuffer = new RenderTexture(cachedDescriptor);
				highlightingBuffer.filterMode = FilterMode.Point;
				highlightingBuffer.wrapMode = TextureWrapMode.Clamp;
				if (!highlightingBuffer.Create())
				{
					Debug.LogError("HighlightingSystem : UpdateHighlightingBuffer() : Failed to create highlightingBuffer RenderTexture!");
				}
				highlightingBufferID = new RenderTargetIdentifier(highlightingBuffer);
				compMaterial.SetTexture(ShaderPropertyID._HighlightingBuffer, highlightingBuffer);
			}
			RebuildCommandBuffer();
		}

		protected virtual void OnRenderImage(RenderTexture src, RenderTexture dst)
		{
			if (blitter == null)
			{
				Blit(src, dst);
			}
			else
			{
				Graphics.Blit(src, dst);
			}
		}

		protected virtual void OnEndOfFrame()
		{
			currentCamera = null;
			visibleRenderers.Clear();
		}

		[ExcludeFromDocs]
		public static void SetVisible(HighlighterRenderer renderer)
		{
			if (!(Camera.current != currentCamera))
			{
				visibleRenderers.Add(renderer);
			}
		}

		[ExcludeFromDocs]
		public static bool GetVisible(HighlighterRenderer renderer)
		{
			return visibleRenderers.Contains(renderer);
		}

		protected static void Initialize()
		{
			if (!initialized)
			{
				int num = shaderPaths.Length;
				shaders = new Shader[num];
				materials = new Material[num];
				for (int i = 0; i < num; i++)
				{
					Shader shader = Shader.Find(shaderPaths[i]);
					shaders[i] = shader;
					Material material = new Material(shader);
					materials[i] = material;
				}
				initialized = true;
			}
		}

		protected virtual RenderTextureDescriptor GetDescriptor()
		{
			RenderTexture targetTexture = cam.targetTexture;
			RenderTextureDescriptor result = ((!(targetTexture != null)) ? new RenderTextureDescriptor(cam.pixelWidth, cam.pixelHeight, RenderTextureFormat.ARGB32, 24) : targetTexture.descriptor);
			result.colorFormat = RenderTextureFormat.ARGB32;
			result.sRGB = QualitySettings.activeColorSpace == ColorSpace.Linear;
			result.useMipMap = false;
			result.msaaSamples = GetAA(targetTexture);
			return result;
		}

		protected virtual bool Equals(RenderTextureDescriptor x, RenderTextureDescriptor y)
		{
			if (x.width == y.width && x.height == y.height)
			{
				return x.msaaSamples == y.msaaSamples;
			}
			return false;
		}

		protected virtual int GetAA(RenderTexture targetTexture)
		{
			int num = 1;
			switch (_antiAliasing)
			{
			case AntiAliasing.QualitySettings:
				if (cam.actualRenderingPath == RenderingPath.DeferredLighting || cam.actualRenderingPath == RenderingPath.DeferredShading)
				{
					num = 1;
				}
				else if (targetTexture == null)
				{
					num = QualitySettings.antiAliasing;
					if (num == 0)
					{
						num = 1;
					}
				}
				else
				{
					num = targetTexture.antiAliasing;
				}
				break;
			case AntiAliasing.Disabled:
				num = 1;
				break;
			case AntiAliasing.MSAA2x:
				num = 2;
				break;
			case AntiAliasing.MSAA4x:
				num = 4;
				break;
			case AntiAliasing.MSAA8x:
				num = 8;
				break;
			}
			return num;
		}

		protected virtual bool CheckSupported(bool verbose)
		{
			bool result = true;
			if (!SystemInfo.supportsImageEffects)
			{
				if (verbose)
				{
					Debug.LogError("HighlightingSystem : Image effects is not supported on this platform!");
				}
				result = false;
			}
			if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB32))
			{
				if (verbose)
				{
					Debug.LogError("HighlightingSystem : RenderTextureFormat.ARGB32 is not supported on this platform!");
				}
				result = false;
			}
			if (!HighlighterCore.opaqueShader.isSupported)
			{
				if (verbose)
				{
					Debug.LogError("HighlightingSystem : HighlightingOpaque shader is not supported on this platform!");
				}
				result = false;
			}
			if (!HighlighterCore.transparentShader.isSupported)
			{
				if (verbose)
				{
					Debug.LogError("HighlightingSystem : HighlightingTransparent shader is not supported on this platform!");
				}
				result = false;
			}
			for (int i = 0; i < shaders.Length; i++)
			{
				Shader shader = shaders[i];
				if (!shader.isSupported)
				{
					if (verbose)
					{
						Debug.LogError("HighlightingSystem : Shader '" + shader.name + "' is not supported on this platform!");
					}
					result = false;
				}
			}
			return result;
		}

		protected virtual void RebuildCommandBuffer()
		{
			renderBuffer.Clear();
			renderBuffer.BeginSample(profileHighlightingSystem);
			renderBuffer.SetRenderTarget(highlightingBufferID);
			renderBuffer.ClearRenderTarget(clearDepth: true, clearColor: true, colorClear);
			HighlighterCore.FillBuffer(renderBuffer);
			RenderTextureDescriptor desc = cachedDescriptor;
			desc.width = highlightingBuffer.width / _downsampleFactor;
			desc.height = highlightingBuffer.height / _downsampleFactor;
			desc.depthBufferBits = 0;
			renderBuffer.GetTemporaryRT(ShaderPropertyID._HighlightingBlur1, desc, FilterMode.Bilinear);
			renderBuffer.GetTemporaryRT(ShaderPropertyID._HighlightingBlur2, desc, FilterMode.Bilinear);
			renderBuffer.Blit(highlightingBufferID, blur1ID);
			bool flag = true;
			for (int i = 0; i < _iterations; i++)
			{
				float value = _blurMinSpread + _blurSpread * (float)i;
				renderBuffer.SetGlobalFloat(ShaderPropertyID._HighlightingBlurOffset, value);
				if (flag)
				{
					renderBuffer.Blit(blur1ID, blur2ID, blurMaterial);
				}
				else
				{
					renderBuffer.Blit(blur2ID, blur1ID, blurMaterial);
				}
				flag = !flag;
			}
			renderBuffer.Blit(flag ? blur1ID : blur2ID, highlightingBufferID, cutMaterial);
			renderBuffer.ReleaseTemporaryRT(ShaderPropertyID._HighlightingBlur1);
			renderBuffer.ReleaseTemporaryRT(ShaderPropertyID._HighlightingBlur2);
			renderBuffer.EndSample(profileHighlightingSystem);
		}

		public virtual void Blit(RenderTexture src, RenderTexture dst)
		{
			Graphics.Blit(src, dst, compMaterial);
		}
	}
}
