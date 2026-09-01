using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/GUI/MMHealthBar")]
	public class MMHealthBar : MonoBehaviour
	{
		public enum HealthBarTypes
		{
			Prefab = 0,
			Drawn = 1,
			Existing = 2
		}

		public enum TimeScales
		{
			UnscaledTime = 0,
			Time = 1
		}

		[CompilerGenerated]
		private sealed class _003CFinalHideBar_003Ed__54 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMHealthBar _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CFinalHideBar_003Ed__54(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[MMInformation("Add this component to an object and it'll add a healthbar next to it to reflect its health level in real time. You can decide here whether the health bar should be drawn automatically or use a prefab.", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("whether the healthbar uses a prefab or is drawn automatically")]
		public HealthBarTypes HealthBarType;

		[Tooltip("defines whether the bar will work on scaled or unscaled time (whether or not it'll keep moving if time is slowed down for example)")]
		public TimeScales TimeScale;

		[Header("Select a Prefab")]
		[MMInformation("Select a prefab with a progress bar script on it. There is one example of such a prefab in Common/Prefabs/GUI.", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("the prefab to use as the health bar")]
		public MMProgressBar HealthBarPrefab;

		[Header("Existing MMProgressBar")]
		[Tooltip("the MMProgressBar this health bar should update")]
		public MMProgressBar TargetProgressBar;

		[Header("Drawn Healthbar Settings ")]
		[MMInformation("Set the size (in world units), padding, back and front colors of the healthbar.", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("if the healthbar is drawn, its size in world units")]
		public Vector2 Size;

		[Tooltip("if the healthbar is drawn, the padding to apply to the foreground, in world units")]
		public Vector2 BackgroundPadding;

		[Tooltip("the rotation to apply to the MMHealthBarContainer when drawing it")]
		public Vector3 InitialRotationAngles;

		[Tooltip("if the healthbar is drawn, the color of its foreground")]
		public Gradient ForegroundColor;

		[Tooltip("if the healthbar is drawn, the color of its delayed bar")]
		public Gradient DelayedColor;

		[Tooltip("if the healthbar is drawn, the color of its border")]
		public Gradient BorderColor;

		[Tooltip("if the healthbar is drawn, the color of its background")]
		public Gradient BackgroundColor;

		[Tooltip("the name of the sorting layer to put this health bar on")]
		public string SortingLayerName;

		[Tooltip("the delay to apply to the delayed bar if drawn")]
		public float Delay;

		[Tooltip("whether or not the front bar should lerp")]
		public bool LerpFrontBar;

		[Tooltip("the speed at which the front bar lerps")]
		public float LerpFrontBarSpeed;

		[Tooltip("whether or not the delayed bar should lerp")]
		public bool LerpDelayedBar;

		[Tooltip("the speed at which the delayed bar lerps")]
		public float LerpDelayedBarSpeed;

		[Tooltip("if this is true, bumps the scale of the healthbar when its value changes")]
		public bool BumpScaleOnChange;

		[Tooltip("the duration of the bump animation")]
		public float BumpDuration;

		[Tooltip("the animation curve to map the bump animation on")]
		public AnimationCurve BumpAnimationCurve;

		[Tooltip("the mode the bar should follow the target in")]
		public MMFollowTarget.UpdateModes FollowTargetMode;

		[Tooltip("if this is true, the drawn health bar will adapt its rotation to match the one of its target")]
		public bool FollowRotation;

		[Tooltip("if this is true, the drawn health bar will adapt its scale to match the one of its target")]
		public bool FollowScale;

		[Tooltip("if this is true, the drawn health bar will be nested below the MMHealthBar")]
		public bool NestDrawnHealthBar;

		[Tooltip("if this is true, a MMBillboard component will be added to the progress bar to make sure it always looks towards the camera")]
		public bool Billboard;

		[Header("Death")]
		[Tooltip("a gameobject (usually a particle system) to instantiate when the healthbar reaches zero")]
		public GameObject InstantiatedOnDeath;

		[Header("Offset")]
		[MMInformation("Set the offset (in world units), relative to the object's center, to which the health bar will be displayed.", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("the offset to apply to the healthbar compared to the object's center")]
		public Vector3 HealthBarOffset;

		[Header("Display")]
		[MMInformation("Here you can define whether or not the healthbar should always be visible. If not, you can set here how long after a hit it'll remain visible.", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("whether or not the bar should be permanently displayed")]
		public bool AlwaysVisible;

		[Tooltip("the duration (in seconds) during which to display the bar")]
		public float DisplayDurationOnHit;

		[Tooltip("if this is set to true the bar will hide itself when it reaches zero")]
		public bool HideBarAtZero;

		[Tooltip("the delay (in seconds) after which to hide the bar")]
		public float HideBarAtZeroDelay;

		[Header("Test")]
		[Tooltip("a test value to use when pressing the TestUpdateHealth button")]
		public float TestMinHealth;

		[Tooltip("a test value to use when pressing the TestUpdateHealth button")]
		public float TestMaxHealth;

		[Tooltip("a test value to use when pressing the TestUpdateHealth button")]
		public float TestCurrentHealth;

		[MMInspectorButton("TestUpdateHealth")]
		public bool TestUpdateHealthButton;

		protected MMProgressBar _progressBar;

		protected MMFollowTarget _followTransform;

		protected float _lastShowTimestamp;

		protected bool _showBar;

		protected Image _backgroundImage;

		protected Image _borderImage;

		protected Image _foregroundImage;

		protected Image _delayedImage;

		protected bool _finalHideStarted;

		protected virtual void Awake()
		{
		}

		protected void OnEnable()
		{
		}

		public virtual void SetInitialActiveState()
		{
		}

		public virtual void ShowBar(bool state)
		{
		}

		public virtual bool BarIsShown()
		{
			return false;
		}

		public virtual void Initialization()
		{
		}

		protected virtual void DrawHealthBar()
		{
		}

		protected virtual void Update()
		{
		}

		[IteratorStateMachine(typeof(_003CFinalHideBar_003Ed__54))]
		protected virtual IEnumerator FinalHideBar()
		{
			return null;
		}

		protected virtual void UpdateDrawnColors()
		{
		}

		public virtual void UpdateBar(float currentHealth, float minHealth, float maxHealth, bool show)
		{
		}

		protected virtual void TestUpdateHealth()
		{
		}
	}
}
