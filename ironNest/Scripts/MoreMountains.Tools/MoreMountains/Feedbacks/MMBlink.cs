using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Various/MMBlink")]
	public class MMBlink : MMMonoBehaviour
	{
		public enum States
		{
			On = 0,
			Off = 1
		}

		public enum Methods
		{
			SetGameObjectActive = 0,
			MaterialAlpha = 1,
			MaterialEmissionIntensity = 2,
			ShaderFloatValue = 3
		}

		[MMInspectorGroup("Blink Method", true, 17, false)]
		[Tooltip("the selected method to blink the target object")]
		public Methods Method;

		[Tooltip("the object to set active/inactive if that method was chosen")]
		[MMFEnumCondition("Method", new int[] { 0 })]
		public GameObject TargetGameObject;

		[Tooltip("the target renderer to work with")]
		[MMFEnumCondition("Method", new int[] { 1, 2, 3 })]
		public Renderer TargetRenderer;

		[Tooltip("the material index to target")]
		[MMFEnumCondition("Method", new int[] { 1, 2, 3 })]
		public int MaterialIndex;

		[Tooltip("the shader property to alter a float on")]
		[MMFEnumCondition("Method", new int[] { 1, 2, 3 })]
		public string ShaderPropertyName;

		[Tooltip("the value to apply when blinking is off")]
		[MMFEnumCondition("Method", new int[] { 1, 2, 3 })]
		public float OffValue;

		[Tooltip("the value to apply when blinking is on")]
		[MMFEnumCondition("Method", new int[] { 1, 2, 3 })]
		public float OnValue;

		[Tooltip("whether to lerp these values or not")]
		[MMFEnumCondition("Method", new int[] { 1, 2, 3 })]
		public bool LerpValue;

		[Tooltip("the curve to apply to the lerping")]
		[MMFEnumCondition("Method", new int[] { 1, 2, 3 })]
		public AnimationCurve Curve;

		[Tooltip("if this is true, this component will use material property blocks instead of working on an instance of the material.")]
		public bool UseMaterialPropertyBlocks;

		[MMInspectorGroup("Extra Targets", true, 12, false)]
		[Tooltip("a list of optional extra renderers and their material index to target")]
		public List<BlinkTargetRenderer> ExtraRenderers;

		[Tooltip("a list of optional extra game objects to target")]
		public List<GameObject> ExtraGameObjects;

		[MMInspectorGroup("State", true, 18, false)]
		[Tooltip("whether the object should blink or not")]
		public bool Blinking;

		[Tooltip("whether or not to force a certain state on exit")]
		public bool ForceStateOnExit;

		[Tooltip("the state to apply on exit")]
		[MMFCondition("ForceStateOnExit", true)]
		public States StateOnExit;

		[MMInspectorGroup("TimeScale", true, 120, false)]
		[Tooltip("whether or not this MMBlink should operate on unscaled time")]
		public TimescaleModes TimescaleMode;

		[MMInspectorGroup("Sequence", true, 121, false)]
		[Tooltip("how many times the sequence should repeat (-1 : infinite)")]
		public int RepeatCount;

		[Tooltip("The list of phases to apply blinking with")]
		public List<BlinkPhase> Phases;

		[MMInspectorGroup("Debug", true, 122, false)]
		[MMInspectorButtonBar(new string[] { "ToggleBlinking", "StartBlinking", "StopBlinking" }, new string[] { "ToggleBlinking", "StartBlinking", "StopBlinking" }, new bool[] { true, true, true }, new string[] { "main-call-to-action", null, null })]
		public bool DebugToolbar;

		[Tooltip("is the blinking object in an active state right now?")]
		[MMFReadOnly]
		public bool Active;

		[Tooltip("the index of the phase we're currently in")]
		[MMFReadOnly]
		public int CurrentPhaseIndex;

		protected float _lastBlinkAt;

		protected float _currentPhaseStartedAt;

		protected float _currentBlinkDuration;

		protected float _currentLerpDuration;

		protected int _propertyID;

		protected float _initialShaderFloatValue;

		protected Color _initialColor;

		protected Color _currentColor;

		protected int _repeatCount;

		protected MaterialPropertyBlock _propertyBlock;

		protected List<MaterialPropertyBlock> _extraPropertyBlocks;

		protected List<Color> _extraInitialColors;

		public virtual float Duration => 0f;

		public virtual float GetTime()
		{
			return 0f;
		}

		public virtual float GetDeltaTime()
		{
			return 0f;
		}

		public virtual void ToggleBlinking()
		{
		}

		public virtual void StartBlinking()
		{
		}

		public virtual void StopBlinking()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void DetermineState()
		{
		}

		protected virtual void Blink()
		{
		}

		protected virtual void ApplyBlink(bool active, float value)
		{
		}

		protected virtual void ApplyFloatValue(Renderer targetRenderer, float value)
		{
		}

		protected virtual void ApplyCurrentColor(Renderer targetRenderer)
		{
		}

		protected virtual void DetermineCurrentPhase()
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void InitializeBlinkProperties()
		{
		}

		protected virtual void GetInitialColor()
		{
		}

		protected virtual void GetInitialFloatValue()
		{
		}

		protected virtual void ResetBlinkProperties()
		{
		}

		protected void OnDisable()
		{
		}
	}
}
