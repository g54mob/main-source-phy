using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	[MMRequiresConstantRepaint]
	[AddComponentMenu("More Mountains/Tools/Property Controllers/ShaderController")]
	public class ShaderController : MMMonoBehaviour
	{
		public enum TargetTypes
		{
			Renderer = 0,
			Image = 1,
			RawImage = 2,
			Text = 3
		}

		public enum PropertyTypes
		{
			Bool = 0,
			Float = 1,
			Int = 2,
			Vector = 3,
			Keyword = 4,
			Color = 5
		}

		public enum ControlModes
		{
			PingPong = 0,
			Random = 1,
			OneTime = 2,
			AudioAnalyzer = 3,
			ToDestination = 4,
			Driven = 5,
			Loop = 6
		}

		public enum ColorModes
		{
			TwoColors = 0,
			ColorRamp = 1
		}

		[Header("Target")]
		[Tooltip("the type of renderer to pilot")]
		public TargetTypes TargetType;

		[Tooltip("the renderer with the shader you want to control")]
		[MMEnumCondition("TargetType", new int[] { 0 })]
		public Renderer TargetRenderer;

		[Tooltip("the ID of the material in the Materials array on the target renderer (usually 0)")]
		[MMEnumCondition("TargetType", new int[] { 0 })]
		public int TargetMaterialID;

		[Tooltip("the Image with the shader you want to control")]
		[MMEnumCondition("TargetType", new int[] { 1 })]
		public Image TargetImage;

		[Tooltip("if this is true, the 'materialForRendering' for this Image will be used, instead of the regular material")]
		[MMEnumCondition("TargetType", new int[] { 1 })]
		public bool UseMaterialForRendering;

		[Tooltip("the RawImage with the shader you want to control")]
		[MMEnumCondition("TargetType", new int[] { 2 })]
		public RawImage TargetRawImage;

		[Tooltip("the Text with the shader you want to control")]
		[MMEnumCondition("TargetType", new int[] { 3 })]
		public Text TargetText;

		[Tooltip("if this is true, material will be cached on Start")]
		public bool CacheMaterial;

		[Tooltip("if this is true, an instance of the material will be created on start so that this controller only affects its target")]
		public bool CreateMaterialInstance;

		[Tooltip("the EXACT name of the property to affect")]
		public string TargetPropertyName;

		[Tooltip("the type of the property to affect")]
		public PropertyTypes PropertyType;

		[Tooltip("whether or not to affect its x component")]
		[MMEnumCondition("PropertyType", new int[] { 3 })]
		public bool X;

		[Tooltip("whether or not to affect its y component")]
		[MMEnumCondition("PropertyType", new int[] { 3 })]
		public bool Y;

		[Tooltip("whether or not to affect its z component")]
		[MMEnumCondition("PropertyType", new int[] { 3 })]
		public bool Z;

		[Tooltip("whether or not to affect its w component")]
		[MMEnumCondition("PropertyType", new int[] { 3 })]
		public bool W;

		[Header("Color")]
		[Tooltip("whether to move from a color to another, or to evalute colors on a ramp")]
		public ColorModes ColorMode;

		[Tooltip("the ramp along which to lerp when in ramp color mode")]
		[GradientUsage(true)]
		public Gradient ColorRamp;

		[Tooltip("the color to lerp from")]
		[ColorUsage(true, true)]
		public Color FromColor;

		[Tooltip("the color to lerp to")]
		[ColorUsage(true, true)]
		public Color ToColor;

		[Header("Global Settings")]
		[Tooltip("the control mode (ping pong or random)")]
		public ControlModes ControlMode;

		[Tooltip("whether or not the updated value should be added to the initial one")]
		public bool AddToInitialValue;

		[Tooltip("whether or not to use unscaled time")]
		public bool UseUnscaledTime;

		[Tooltip("whether or not you want to revert to the InitialValue after the control ends")]
		public bool RevertToInitialValueAfterEnd;

		[Tooltip("if this is true, this component will use material property blocks instead of working on an instance of the material.")]
		[MMEnumCondition("TargetType", new int[] { 0 })]
		public bool UseMaterialPropertyBlocks;

		[Tooltip("if using material property blocks on a sprite renderer, you'll want to make sure the sprite texture gets passed to the block when updating it. For that, you need to specify your sprite's material's shader's texture property name. If you're not working with a sprite renderer, you can safely ignore this.")]
		[MMCondition("UseMaterialPropertyBlocks", true)]
		public string SpriteRendererTextureProperty;

		[Tooltip("whether or not to perform extra safety checks (safer, more costly)")]
		public bool SafeMode;

		[Header("Ping Pong")]
		[Tooltip("the curve to apply to the tween")]
		public MMTweenType Curve;

		[Tooltip("the minimum value for the ping pong")]
		public float MinValue;

		[Tooltip("the maximum value for the ping pong")]
		public float MaxValue;

		[Tooltip("the duration of one ping (or pong)")]
		public float Duration;

		[Tooltip("the duration of the pause between two ping (or pongs) (in seconds)")]
		public float PingPongPauseDuration;

		[Header("Loop")]
		[Tooltip("the curve to apply to the tween")]
		public MMTweenType LoopCurve;

		[Tooltip("the start value for the loop tween")]
		public float LoopStartValue;

		[Tooltip("the end value for the loop tween")]
		public float LoopEndValue;

		[Tooltip("the duration of one loop")]
		public float LoopDuration;

		[Tooltip("the duration of the pause between two loops (in seconds)")]
		public float LoopPauseDuration;

		[Header("Driven")]
		[Tooltip("the value that will be applied to the controlled float in driven mode")]
		public float DrivenLevel;

		[Header("Random")]
		[Tooltip("the noise amplitude")]
		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 Amplitude;

		[Tooltip("the noise frequency")]
		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 Frequency;

		[Tooltip("the noise shift")]
		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 Shift;

		[Tooltip("if this is true, will let you remap the noise value (without amplitude) to the bounds you've specified")]
		public bool RemapNoiseValues;

		[Tooltip("the value to which to remap the random's zero bound")]
		[MMCondition("RemapNoiseValues", true)]
		public float RemapNoiseZero;

		[Tooltip("the value to which to remap the random's one bound")]
		[MMCondition("RemapNoiseValues", true)]
		public float RemapNoiseOne;

		[Header("OneTime")]
		[Tooltip("the duration of the One Time shake")]
		public float OneTimeDuration;

		[Tooltip("the amplitude of the One Time shake (this will be multiplied by the curve's height)")]
		public float OneTimeAmplitude;

		[Tooltip("the low value to remap the normalized curve value to")]
		public float OneTimeRemapMin;

		[Tooltip("the high value to remap the normalized curve value to")]
		public float OneTimeRemapMax;

		[Tooltip("the curve to apply to the one time shake")]
		public AnimationCurve OneTimeCurve;

		[MMInspectorButton("OneTime")]
		[Tooltip("a test button for the one time shake")]
		public bool OneTimeButton;

		[Tooltip("whether or not this controller should go back to sleep after a OneTime")]
		public bool DisableAfterOneTime;

		[Tooltip("whether or not this controller should go back to sleep after a OneTime")]
		public bool DisableGameObjectAfterOneTime;

		[Tooltip("whether or not to initialize the initial value to the current value on a OneTime play")]
		public bool GetInitialValueOnOneTime;

		[Header("AudioAnalyzer")]
		[Tooltip("the bound audio analyzer used to drive this controller")]
		public MMAudioAnalyzer AudioAnalyzer;

		[Tooltip("the ID of the selected beat on the analyzer")]
		public int BeatID;

		[Tooltip("the multiplier to apply to the value out of the analyzer")]
		public float AudioAnalyzerMultiplier;

		[Tooltip("the offset to apply to the value out of the analyzer")]
		public float AudioAnalyzerOffset;

		[Tooltip("the speed at which to lerp the value")]
		public float AudioAnalyzerLerp;

		[Header("ToDestination")]
		[Tooltip("the value to go to when in ToDestination mode")]
		public float ToDestinationValue;

		[Tooltip("the duration of the ToDestination tween")]
		public float ToDestinationDuration;

		[Tooltip("the curve to use to tween to the ToDestination value")]
		public AnimationCurve ToDestinationCurve;

		[Tooltip("a test button for the one time shake")]
		[MMInspectorButton("ToDestination")]
		public bool ToDestinationButton;

		[Tooltip("whether or not this controller should go back to sleep after a OneTime")]
		public bool DisableAfterToDestination;

		[Header("Debug")]
		[Tooltip("the initial value of the controlled float")]
		[MMReadOnly]
		public float InitialValue;

		[Tooltip("the current value of the controlled float")]
		[MMReadOnly]
		public float CurrentValue;

		[Tooltip("the current value of the controlled float, normalized")]
		[MMReadOnly]
		public float CurrentValueNormalized;

		[Tooltip("the current value of the controlled float")]
		[MMReadOnly]
		public Color InitialColor;

		[Tooltip("the ID of the property")]
		[MMReadOnly]
		public int PropertyID;

		[Tooltip("whether or not the property got found")]
		[MMReadOnly]
		public bool PropertyFound;

		[Tooltip("the target material")]
		[MMReadOnly]
		public Material TargetMaterial;

		[HideInInspector]
		public float PingPong;

		[HideInInspector]
		public float LoopTime;

		protected float _randomAmplitude;

		protected float _randomFrequency;

		protected float _randomShift;

		protected float _elapsedTime;

		protected bool _shaking;

		protected float _startedTimestamp;

		protected float _remappedTimeSinceStart;

		protected Color _currentColor;

		protected Vector4 _vectorValue;

		protected float _pingPongDirection;

		protected float _lastPingPongPauseAt;

		protected float _lastLoopPauseAt;

		protected float _initialValue;

		protected Color _fromColorStorage;

		protected bool _activeLastFrame;

		protected MaterialPropertyBlock _propertyBlock;

		protected SpriteRenderer _spriteRenderer;

		protected Texture2D _spriteRendererTexture;

		protected bool SpriteRendererIsNull;

		public virtual bool FindShaderProperty(string propertyName)
		{
			return false;
		}

		protected virtual void Awake()
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual bool RendererIsNull()
		{
			return false;
		}

		public virtual void Initialization()
		{
		}

		public virtual void StoreSpriteRenderer()
		{
		}

		public virtual void StoreSpriteRendererTexture()
		{
		}

		protected virtual void SetStoredSpriteRendererTexture(MaterialPropertyBlock block)
		{
		}

		public virtual void SetDrivenLevelAbsolute(float level)
		{
		}

		public virtual void SetDrivenLevelNormalized(float normalizedLevel, float remapZero, float remapOne)
		{
		}

		public virtual void OneTime()
		{
		}

		public virtual void ToDestination()
		{
		}

		public void SetFromColor(Color newColor)
		{
		}

		public void SetToColor(Color newColor)
		{
		}

		public virtual void SetRemapOneTimeMin(float newValue)
		{
		}

		public virtual void SetRemapOneTimeMax(float newValue)
		{
		}

		public virtual void SetToDestinationValue(float newValue)
		{
		}

		protected float GetDeltaTime()
		{
			return 0f;
		}

		protected float GetTime()
		{
			return 0f;
		}

		protected virtual void Update()
		{
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void UpdateValue()
		{
		}

		protected virtual float GetInitialValue()
		{
			return 0f;
		}

		protected virtual void SetValue(float newValue)
		{
		}

		public virtual void Stop()
		{
		}

		public virtual void RestoreInitialValues()
		{
		}
	}
}
