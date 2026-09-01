using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Property Controllers/FloatController")]
	[MMRequiresConstantRepaint]
	public class FloatController : MMMonoBehaviour
	{
		public enum ControlModes
		{
			PingPong = 0,
			Random = 1,
			OneTime = 2,
			AudioAnalyzer = 3,
			ToDestination = 4,
			Driven = 5
		}

		public enum AudioAnalyzerModes
		{
			Beat = 0,
			NormalizedBufferedBandLevels = 1
		}

		[Header("Target")]
		public MonoBehaviour TargetObject;

		[Header("Global Settings")]
		public ControlModes ControlMode;

		public bool AddToInitialValue;

		public bool UseUnscaledTime;

		public bool RevertToInitialValueAfterEnd;

		[Header("Driven")]
		public float DrivenLevel;

		[Header("Ping Pong")]
		public MMTweenType Curve;

		public float MinValue;

		public float MaxValue;

		public float Duration;

		public float PingPongPauseDuration;

		[Header("Random")]
		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 Amplitude;

		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 Frequency;

		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 Shift;

		public bool RemapNoiseValues;

		[MMCondition("RemapNoiseValues", true)]
		public float RemapNoiseZero;

		[MMCondition("RemapNoiseValues", true)]
		public float RemapNoiseOne;

		[Header("OneTime")]
		public float OneTimeDuration;

		public float OneTimeAmplitude;

		public float OneTimeRemapMin;

		public float OneTimeRemapMax;

		public AnimationCurve OneTimeCurve;

		public bool DisableAfterOneTime;

		public bool DisableGameObjectAfterOneTime;

		[MMInspectorButton("OneTime")]
		public bool OneTimeButton;

		[Header("ToDestination")]
		public float ToDestinationDuration;

		public float ToDestinationValue;

		public AnimationCurve ToDestinationCurve;

		public bool DisableAfterToDestination;

		[MMInspectorButton("ToDestination")]
		public bool ToDestinationButton;

		[Header("AudioAnalyzer")]
		public MMAudioAnalyzer AudioAnalyzer;

		public AudioAnalyzerModes AudioAnalyzerMode;

		public int BeatID;

		public int NormalizedLevelID;

		public float AudioAnalyzerMultiplier;

		[Header("Debug")]
		[MMReadOnly]
		public float InitialValue;

		[MMReadOnly]
		public float CurrentValue;

		[MMReadOnly]
		public float CurrentValueNormalized;

		[HideInInspector]
		public float PingPong;

		[HideInInspector]
		public MonoAttribute TargetAttribute;

		[HideInInspector]
		public string[] AttributeNames;

		[HideInInspector]
		public string PropertyName;

		[HideInInspector]
		public int ChoiceIndex;

		public const string _undefinedString = "<Undefined Attribute>";

		protected List<string> _attributesNamesTempList;

		protected PropertyInfo[] _propertyReferences;

		protected FieldInfo[] _fieldReferences;

		protected bool _attributeFound;

		protected float _randomAmplitude;

		protected float _randomFrequency;

		protected float _randomShift;

		protected float _elapsedTime;

		protected bool _shaking;

		protected float _shakeStartTimestamp;

		protected float _remappedTimeSinceStart;

		protected float _pingPongDirection;

		protected float _lastPingPongPauseAt;

		protected float _initialValue;

		protected MonoBehaviour _targetObjectLastFrame;

		protected MonoAttribute _targetAttributeLastFrame;

		public virtual bool FindAttribute(string propertyName)
		{
			return false;
		}

		protected virtual void Awake()
		{
		}

		protected virtual void OnEnable()
		{
		}

		public virtual void Initialization()
		{
		}

		protected virtual float GetInitialValue()
		{
			return 0f;
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

		protected virtual void OnValidate()
		{
		}

		protected virtual void OnDisable()
		{
		}

		public virtual void Stop()
		{
		}

		public virtual void FillDropDownList()
		{
		}

		public virtual void RestoreInitialValues()
		{
		}
	}
}
