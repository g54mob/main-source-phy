using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	public class MMFloatingTextSpawner : MMMonoBehaviour
	{
		public enum PoolerModes
		{
			Simple = 0,
			Multiple = 1
		}

		public enum AlignmentModes
		{
			Fixed = 0,
			MatchInitialDirection = 1,
			MatchMovementDirection = 2
		}

		[CompilerGenerated]
		private sealed class _003CTestSpawnManyCo_003Ed__77 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMFloatingTextSpawner _003C_003E4__this;

			private float _003ClastSpawnAt_003E5__2;

			private float _003Cinterval_003E5__3;

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
			public _003CTestSpawnManyCo_003Ed__77(int _003C_003E1__state)
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

		[MMInspectorGroup("General Settings", true, 10, false)]
		[Tooltip("whether to listen on a channel defined by an int or by a MMChannel scriptable object. Ints are simple to setup but can get messy and make it harder to remember what int corresponds to what. MMChannel scriptable objects require you to create them in advance, but come with a readable name and are more scalable")]
		public MMChannelModes ChannelMode;

		[Tooltip("the channel to listen to - has to match the one on the feedback")]
		[MMEnumCondition("ChannelMode", new int[] { 0 })]
		public int Channel;

		[Tooltip("the MMChannel definition asset to use to listen for events. The feedbacks targeting this shaker will have to reference that same MMChannel definition to receive events - to create a MMChannel, right click anywhere in your project (usually in a Data folder) and go MoreMountains > MMChannel, then name it with some unique name")]
		[MMEnumCondition("ChannelMode", new int[] { 1 })]
		public MMChannel MMChannelDefinition;

		[Tooltip("whether or not this spawner can spawn at this time")]
		public bool CanSpawn;

		[Tooltip("whether or not this spawner should spawn objects on unscaled time")]
		public bool UseUnscaledTime;

		[MMInspectorGroup("Pooler", true, 24, false)]
		[Tooltip("the selected pooler mode (single prefab or multiple ones)")]
		public PoolerModes PoolerMode;

		[Tooltip("the prefab to spawn (ignored if in multiple mode)")]
		public MMFloatingText PooledSimpleMMFloatingText;

		[Tooltip("the prefabs to spawn (ignored if in simple mode)")]
		public List<MMFloatingText> PooledMultipleMMFloatingText;

		[Tooltip("the amount of objects to pool to avoid having to instantiate them at runtime. Should be bigger than the max amount of texts you plan on having on screen at any given moment")]
		public int PoolSize;

		[Tooltip("whether or not to nest the waiting pools")]
		public bool NestWaitingPool;

		[Tooltip("whether or not to mutualize the waiting pools")]
		public bool MutualizeWaitingPools;

		[Tooltip("whether or not the text pool can expand if the pool is empty")]
		public bool PoolCanExpand;

		[MMInspectorGroup("Spawn Settings", true, 14, false)]
		[Tooltip("the random min and max lifetime duration for the spawned texts (in seconds)")]
		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 Lifetime;

		[Header("Spawn Position Offset")]
		[Tooltip("the random min position at which to spawn the text, relative to its intended spawn position")]
		public Vector3 SpawnOffsetMin;

		[Tooltip("the random max position at which to spawn the text, relative to its intended spawn position")]
		public Vector3 SpawnOffsetMax;

		[MMInspectorGroup("Animate Position", true, 15, false)]
		[Header("Movement")]
		[Tooltip("whether or not to animate the movement of spawned texts")]
		public bool AnimateMovement;

		[Tooltip("whether or not to animate the X movement of spawned texts")]
		public bool AnimateX;

		[Tooltip("the value to which the x movement curve's zero should be remapped to, randomized between its min and max - put the same value in both min and max if you don't want any randomness")]
		[MMCondition("AnimateX", true)]
		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 RemapXZero;

		[Tooltip("the value to which the x movement curve's one should be remapped to, randomized between its min and max - put the same value in both min and max if you don't want any randomness")]
		[MMCondition("AnimateX", true)]
		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 RemapXOne;

		[Tooltip("the curve on which to animate the x movement")]
		[MMCondition("AnimateX", true)]
		public AnimationCurve AnimateXCurve;

		[Tooltip("whether or not to animate the Y movement of spawned texts")]
		public bool AnimateY;

		[Tooltip("the value to which the y movement curve's zero should be remapped to, randomized between its min and max - put the same value in both min and max if you don't want any randomness")]
		[MMCondition("AnimateY", true)]
		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 RemapYZero;

		[Tooltip("the value to which the y movement curve's one should be remapped to, randomized between its min and max - put the same value in both min and max if you don't want any randomness")]
		[MMCondition("AnimateY", true)]
		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 RemapYOne;

		[Tooltip("the curve on which to animate the y movement")]
		[MMCondition("AnimateY", true)]
		public AnimationCurve AnimateYCurve;

		[Tooltip("whether or not to animate the Z movement of spawned texts")]
		public bool AnimateZ;

		[Tooltip("the value to which the z movement curve's zero should be remapped to, randomized between its min and max - put the same value in both min and max if you don't want any randomness")]
		[MMCondition("AnimateZ", true)]
		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 RemapZZero;

		[Tooltip("the value to which the z movement curve's one should be remapped to, randomized between its min and max - put the same value in both min and max if you don't want any randomness")]
		[MMCondition("AnimateZ", true)]
		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 RemapZOne;

		[Tooltip("the curve on which to animate the z movement")]
		[MMCondition("AnimateZ", true)]
		public AnimationCurve AnimateZCurve;

		[MMInspectorGroup("Facing Directions", true, 16, false)]
		[Header("Alignment")]
		[Tooltip("the selected alignment mode (whether the spawned text should have a fixed alignment, orient to match the initial spawn direction, or its movement curve)")]
		public AlignmentModes AlignmentMode;

		[Tooltip("when in fixed mode, the direction in which to keep the spawned texts")]
		[MMEnumCondition("AlignmentMode", new int[] { 0 })]
		public Vector3 FixedAlignment;

		[Header("Billboard")]
		[Tooltip("whether or not spawned texts should always face the camera")]
		public bool AlwaysFaceCamera;

		[Tooltip("whether or not this spawner should automatically grab the main camera on start")]
		[MMCondition("AlwaysFaceCamera", true)]
		public bool AutoGrabMainCameraOnStart;

		[Tooltip("if not in auto grab mode, the camera to use for billboards")]
		[MMCondition("AlwaysFaceCamera", true)]
		public Camera TargetCamera;

		[MMInspectorGroup("Animate Scale", true, 46, false)]
		[Tooltip("whether or not to animate the scale of spawned texts")]
		public bool AnimateScale;

		[Tooltip("the value to which the scale curve's zero should be remapped to")]
		[MMCondition("AnimateScale", true)]
		public Vector2 RemapScaleZero;

		[Tooltip("the value to which the scale curve's one should be remapped to")]
		[MMCondition("AnimateScale", true)]
		public Vector2 RemapScaleOne;

		[Tooltip("the curve on which to animate the scale")]
		[MMCondition("AnimateScale", true)]
		public AnimationCurve AnimateScaleCurve;

		[MMInspectorGroup("Animate Color", true, 55, false)]
		[Tooltip("whether or not to animate the spawned text's color over time")]
		public bool AnimateColor;

		[Tooltip("the gradient over which to animate the spawned text's color over time")]
		[GradientUsage(true)]
		public Gradient AnimateColorGradient;

		[MMInspectorGroup("Animate Opacity", true, 45, false)]
		[Tooltip("whether or not to animate the opacity of the spawned texts")]
		public bool AnimateOpacity;

		[Tooltip("the value to which the opacity curve's zero should be remapped to")]
		[MMCondition("AnimateOpacity", true)]
		public Vector2 RemapOpacityZero;

		[Tooltip("the value to which the opacity curve's one should be remapped to")]
		[MMCondition("AnimateOpacity", true)]
		public Vector2 RemapOpacityOne;

		[Tooltip("the curve on which to animate the opacity")]
		[MMCondition("AnimateOpacity", true)]
		public AnimationCurve AnimateOpacityCurve;

		[MMInspectorGroup("Intensity Multipliers", true, 45, false)]
		[Tooltip("whether or not the intensity multiplier should impact lifetime")]
		public bool IntensityImpactsLifetime;

		[Tooltip("when getting an intensity multiplier, the value by which to multiply the lifetime")]
		[MMCondition("IntensityImpactsLifetime", true)]
		public float IntensityLifetimeMultiplier;

		[Tooltip("whether or not the intensity multiplier should impact movement")]
		public bool IntensityImpactsMovement;

		[Tooltip("when getting an intensity multiplier, the value by which to multiply the movement values")]
		[MMCondition("IntensityImpactsMovement", true)]
		public float IntensityMovementMultiplier;

		[Tooltip("whether or not the intensity multiplier should impact scale")]
		public bool IntensityImpactsScale;

		[Tooltip("when getting an intensity multiplier, the value by which to multiply the scale values")]
		[MMCondition("IntensityImpactsScale", true)]
		public float IntensityScaleMultiplier;

		[MMInspectorGroup("Debug", true, 12, false)]
		[Tooltip("a random value to display when pressing the TestSpawnOne button")]
		public Vector2Int DebugRandomValue;

		[Tooltip("the min and max bounds within which to pick a value to output when pressing the TestSpawnMany button")]
		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 DebugInterval;

		[Tooltip("a button used to test the spawn of one text")]
		[MMInspectorButton("TestSpawnOne")]
		public bool TestSpawnOneBtn;

		[Tooltip("a button used to start/stop the spawn of texts at regular intervals")]
		[MMInspectorButton("TestSpawnMany")]
		public bool TestSpawnManyBtn;

		protected MMObjectPooler _pooler;

		protected MMFloatingText _floatingText;

		protected Coroutine _testSpawnCoroutine;

		protected float _lifetime;

		protected float _speed;

		protected Vector3 _spawnOffset;

		protected Vector3 _direction;

		protected Gradient _colorGradient;

		protected bool _animateColor;

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void InstantiateObjectPool()
		{
		}

		protected virtual void InstantiateSimplePool()
		{
		}

		protected virtual void InstantiateMultiplePool()
		{
		}

		protected virtual void GrabMainCamera()
		{
		}

		protected virtual void Spawn(string value, Vector3 position, Vector3 direction, float intensity = 1f, bool forceLifetime = false, float lifetime = 1f, bool forceColor = false, Gradient animateColorGradient = null)
		{
		}

		public virtual void OnMMFloatingTextSpawnEvent(MMChannelData channelData, Vector3 spawnPosition, string value, Vector3 direction, float intensity, bool forceLifetime = false, float lifetime = 1f, bool forceColor = false, Gradient animateColorGradient = null, bool useUnscaledTime = false)
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void TestSpawnOne()
		{
		}

		protected virtual void TestSpawnMany()
		{
		}

		[IteratorStateMachine(typeof(_003CTestSpawnManyCo_003Ed__77))]
		protected virtual IEnumerator TestSpawnManyCo()
		{
			return null;
		}
	}
}
