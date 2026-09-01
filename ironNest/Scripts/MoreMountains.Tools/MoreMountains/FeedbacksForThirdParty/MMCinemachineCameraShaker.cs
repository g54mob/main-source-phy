using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.Feedbacks;
using Unity.Cinemachine;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Cinemachine/MMCinemachineCameraShaker")]
	[RequireComponent(typeof(CinemachineCamera))]
	public class MMCinemachineCameraShaker : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CShakeCameraCo_003Ed__27 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMCinemachineCameraShaker _003C_003E4__this;

			public float amplitude;

			public float frequency;

			public bool useUnscaledTime;

			public bool infinite;

			public float duration;

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
			public _003CShakeCameraCo_003Ed__27(int _003C_003E1__state)
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

		[Header("Settings")]
		[Tooltip("whether to listen on a channel defined by an int or by a MMChannel scriptable object. Ints are simple to setup but can get messy and make it harder to remember what int corresponds to what. MMChannel scriptable objects require you to create them in advance, but come with a readable name and are more scalable")]
		public MMChannelModes ChannelMode;

		[Tooltip("the channel to listen to - has to match the one on the feedback")]
		[MMFEnumCondition("ChannelMode", new int[] { 0 })]
		public int Channel;

		[Tooltip("the MMChannel definition asset to use to listen for events. The feedbacks targeting this shaker will have to reference that same MMChannel definition to receive events - to create a MMChannel, right click anywhere in your project (usually in a Data folder) and go MoreMountains > MMChannel, then name it with some unique name")]
		[MMFEnumCondition("ChannelMode", new int[] { 1 })]
		public MMChannel MMChannelDefinition;

		[Tooltip("The default amplitude that will be applied to your shakes if you don't specify one")]
		public float DefaultShakeAmplitude;

		[Tooltip("The default frequency that will be applied to your shakes if you don't specify one")]
		public float DefaultShakeFrequency;

		[Tooltip("the amplitude of the camera's noise when it's idle")]
		[MMFReadOnly]
		public float IdleAmplitude;

		[Tooltip("the frequency of the camera's noise when it's idle")]
		[MMFReadOnly]
		public float IdleFrequency;

		[Tooltip("the speed at which to interpolate the shake")]
		public float LerpSpeed;

		[Header("Test")]
		[Tooltip("a duration (in seconds) to apply when testing this shake via the TestShake button")]
		public float TestDuration;

		[Tooltip("the amplitude to apply when testing this shake via the TestShake button")]
		public float TestAmplitude;

		[Tooltip("the frequency to apply when testing this shake via the TestShake button")]
		public float TestFrequency;

		[MMFInspectorButton("TestShake")]
		public bool TestShakeButton;

		protected TimescaleModes _timescaleMode;

		protected Vector3 _initialPosition;

		protected Quaternion _initialRotation;

		protected CinemachineBasicMultiChannelPerlin _perlin;

		protected CinemachineCamera _virtualCamera;

		protected float _targetAmplitude;

		protected float _targetFrequency;

		private Coroutine _shakeCoroutine;

		public virtual float GetTime()
		{
			return 0f;
		}

		public virtual float GetDeltaTime()
		{
			return 0f;
		}

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void Update()
		{
		}

		public virtual void ShakeCamera(float duration, bool infinite, bool useUnscaledTime = false)
		{
		}

		public virtual void ShakeCamera(float duration, float amplitude, float frequency, bool infinite, bool useUnscaledTime = false)
		{
		}

		[IteratorStateMachine(typeof(_003CShakeCameraCo_003Ed__27))]
		protected virtual IEnumerator ShakeCameraCo(float duration, float amplitude, float frequency, bool infinite, bool useUnscaledTime)
		{
			return null;
		}

		public virtual void CameraReset()
		{
		}

		public virtual void OnCameraShakeEvent(float duration, float amplitude, float frequency, float amplitudeX, float amplitudeY, float amplitudeZ, bool infinite, MMChannelData channelData, bool useUnscaledTime)
		{
		}

		public virtual void OnCameraShakeStopEvent(MMChannelData channelData)
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void TestShake()
		{
		}
	}
}
