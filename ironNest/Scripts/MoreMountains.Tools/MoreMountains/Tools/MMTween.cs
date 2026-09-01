using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMTween : MonoBehaviour
	{
		public enum MMTweenCurve
		{
			LinearTween = 0,
			EaseInQuadratic = 1,
			EaseOutQuadratic = 2,
			EaseInOutQuadratic = 3,
			EaseInCubic = 4,
			EaseOutCubic = 5,
			EaseInOutCubic = 6,
			EaseInQuartic = 7,
			EaseOutQuartic = 8,
			EaseInOutQuartic = 9,
			EaseInQuintic = 10,
			EaseOutQuintic = 11,
			EaseInOutQuintic = 12,
			EaseInSinusoidal = 13,
			EaseOutSinusoidal = 14,
			EaseInOutSinusoidal = 15,
			EaseInBounce = 16,
			EaseOutBounce = 17,
			EaseInOutBounce = 18,
			EaseInOverhead = 19,
			EaseOutOverhead = 20,
			EaseInOutOverhead = 21,
			EaseInExponential = 22,
			EaseOutExponential = 23,
			EaseInOutExponential = 24,
			EaseInElastic = 25,
			EaseOutElastic = 26,
			EaseInOutElastic = 27,
			EaseInCircular = 28,
			EaseOutCircular = 29,
			EaseInOutCircular = 30,
			AntiLinearTween = 31,
			AlmostIdentity = 32
		}

		public delegate float TweenDelegate(float currentTime);

		[CompilerGenerated]
		private sealed class _003CMoveRectTransformCo_003Ed__61 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delayDuration;

			public WaitForSeconds delay;

			public float duration;

			public RectTransform targetTransform;

			public Vector3 origin;

			public Vector3 destination;

			public MMTweenCurve curve;

			public bool ignoreTimescale;

			private float _003CtimeLeft_003E5__2;

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
			public _003CMoveRectTransformCo_003Ed__61(int _003C_003E1__state)
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

		[CompilerGenerated]
		private sealed class _003CMoveTransformCo_003Ed__62 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delayDuration;

			public WaitForSeconds delay;

			public float duration;

			public Transform targetTransform;

			public Vector3 origin;

			public Vector3 destination;

			public MMTweenCurve curve;

			public bool ignoreTimescale;

			private float _003CtimeLeft_003E5__2;

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
			public _003CMoveTransformCo_003Ed__62(int _003C_003E1__state)
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

		[CompilerGenerated]
		private sealed class _003CMoveTransformCo_003Ed__63 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delayDuration;

			public WaitForSeconds delay;

			public float duration;

			public bool updatePosition;

			public Transform targetTransform;

			public Transform origin;

			public Transform destination;

			public MMTweenCurve curve;

			public bool updateRotation;

			public bool ignoreTimescale;

			private float _003CtimeLeft_003E5__2;

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
			public _003CMoveTransformCo_003Ed__63(int _003C_003E1__state)
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

		[CompilerGenerated]
		private sealed class _003CRotateTransformAroundCo_003Ed__64 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delayDuration;

			public WaitForSeconds delay;

			public Transform targetTransform;

			public float duration;

			public float angle;

			public MMTweenCurve curve;

			public Transform center;

			public bool ignoreTimescale;

			public Transform destination;

			private Vector3 _003CinitialRotationPosition_003E5__2;

			private float _003CtimeSpent_003E5__3;

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
			public _003CRotateTransformAroundCo_003Ed__64(int _003C_003E1__state)
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

		public static TweenDelegate[] TweenDelegateArray;

		public static float Tween(float currentTime, float initialTime, float endTime, float startValue, float endValue, MMTweenCurve curve)
		{
			return 0f;
		}

		public static long Tween(float currentTime, float initialTime, float endTime, long startValue, long endValue, MMTweenCurve curve)
		{
			return 0L;
		}

		public static float Evaluate(float t, MMTweenCurve curve)
		{
			return 0f;
		}

		public static float Evaluate(float t, MMTweenType tweenType)
		{
			return 0f;
		}

		public static float LinearTween(float currentTime)
		{
			return 0f;
		}

		public static float AntiLinearTween(float currentTime)
		{
			return 0f;
		}

		public static float EaseInQuadratic(float currentTime)
		{
			return 0f;
		}

		public static float EaseOutQuadratic(float currentTime)
		{
			return 0f;
		}

		public static float EaseInOutQuadratic(float currentTime)
		{
			return 0f;
		}

		public static float EaseInCubic(float currentTime)
		{
			return 0f;
		}

		public static float EaseOutCubic(float currentTime)
		{
			return 0f;
		}

		public static float EaseInOutCubic(float currentTime)
		{
			return 0f;
		}

		public static float EaseInQuartic(float currentTime)
		{
			return 0f;
		}

		public static float EaseOutQuartic(float currentTime)
		{
			return 0f;
		}

		public static float EaseInOutQuartic(float currentTime)
		{
			return 0f;
		}

		public static float EaseInQuintic(float currentTime)
		{
			return 0f;
		}

		public static float EaseOutQuintic(float currentTime)
		{
			return 0f;
		}

		public static float EaseInOutQuintic(float currentTime)
		{
			return 0f;
		}

		public static float EaseInSinusoidal(float currentTime)
		{
			return 0f;
		}

		public static float EaseOutSinusoidal(float currentTime)
		{
			return 0f;
		}

		public static float EaseInOutSinusoidal(float currentTime)
		{
			return 0f;
		}

		public static float EaseInBounce(float currentTime)
		{
			return 0f;
		}

		public static float EaseOutBounce(float currentTime)
		{
			return 0f;
		}

		public static float EaseInOutBounce(float currentTime)
		{
			return 0f;
		}

		public static float EaseInOverhead(float currentTime)
		{
			return 0f;
		}

		public static float EaseOutOverhead(float currentTime)
		{
			return 0f;
		}

		public static float EaseInOutOverhead(float currentTime)
		{
			return 0f;
		}

		public static float EaseInExponential(float currentTime)
		{
			return 0f;
		}

		public static float EaseOutExponential(float currentTime)
		{
			return 0f;
		}

		public static float EaseInOutExponential(float currentTime)
		{
			return 0f;
		}

		public static float EaseInElastic(float currentTime)
		{
			return 0f;
		}

		public static float EaseOutElastic(float currentTime)
		{
			return 0f;
		}

		public static float EaseInOutElastic(float currentTime)
		{
			return 0f;
		}

		public static float EaseInCircular(float currentTime)
		{
			return 0f;
		}

		public static float EaseOutCircular(float currentTime)
		{
			return 0f;
		}

		public static float EaseInOutCircular(float currentTime)
		{
			return 0f;
		}

		public static float AlmostIdentity(float currentTime)
		{
			return 0f;
		}

		public static TweenDelegate GetTweenMethod(MMTweenCurve tween)
		{
			return null;
		}

		public static Vector2 Tween(float currentTime, float initialTime, float endTime, Vector2 startValue, Vector2 endValue, MMTweenCurve curve)
		{
			return default(Vector2);
		}

		public static Vector3 Tween(float currentTime, float initialTime, float endTime, Vector3 startValue, Vector3 endValue, MMTweenCurve curve)
		{
			return default(Vector3);
		}

		public static Vector4 Tween(float currentTime, float initialTime, float endTime, Vector4 startValue, Vector4 endValue, MMTweenCurve curve)
		{
			return default(Vector4);
		}

		public static Quaternion Tween(float currentTime, float initialTime, float endTime, Quaternion startValue, Quaternion endValue, MMTweenCurve curve)
		{
			return default(Quaternion);
		}

		public static float Tween(float currentTime, float initialTime, float endTime, float startValue, float endValue, AnimationCurve curve)
		{
			return 0f;
		}

		public static long Tween(float currentTime, float initialTime, float endTime, long startValue, long endValue, AnimationCurve curve)
		{
			return 0L;
		}

		public static Vector2 Tween(float currentTime, float initialTime, float endTime, Vector2 startValue, Vector2 endValue, AnimationCurve curve)
		{
			return default(Vector2);
		}

		public static Vector3 Tween(float currentTime, float initialTime, float endTime, Vector3 startValue, Vector3 endValue, AnimationCurve curve)
		{
			return default(Vector3);
		}

		public static Vector4 Tween(float currentTime, float initialTime, float endTime, Vector4 startValue, Vector4 endValue, AnimationCurve curve)
		{
			return default(Vector4);
		}

		public static Quaternion Tween(float currentTime, float initialTime, float endTime, Quaternion startValue, Quaternion endValue, AnimationCurve curve)
		{
			return default(Quaternion);
		}

		public static float Tween(float currentTime, float initialTime, float endTime, float startValue, float endValue, MMTweenType tweenType)
		{
			return 0f;
		}

		public static long Tween(float currentTime, float initialTime, float endTime, long startValue, long endValue, MMTweenType tweenType)
		{
			return 0L;
		}

		public static Vector2 Tween(float currentTime, float initialTime, float endTime, Vector2 startValue, Vector2 endValue, MMTweenType tweenType)
		{
			return default(Vector2);
		}

		public static Vector3 Tween(float currentTime, float initialTime, float endTime, Vector3 startValue, Vector3 endValue, MMTweenType tweenType)
		{
			return default(Vector3);
		}

		public static Vector4 Tween(float currentTime, float initialTime, float endTime, Vector4 startValue, Vector4 endValue, MMTweenType tweenType)
		{
			return default(Vector4);
		}

		public static Quaternion Tween(float currentTime, float initialTime, float endTime, Quaternion startValue, Quaternion endValue, MMTweenType tweenType)
		{
			return default(Quaternion);
		}

		public static Coroutine MoveTransform(MonoBehaviour mono, Transform targetTransform, Vector3 origin, Vector3 destination, WaitForSeconds delay, float delayDuration, float duration, MMTweenCurve curve, bool ignoreTimescale = false)
		{
			return null;
		}

		public static Coroutine MoveRectTransform(MonoBehaviour mono, RectTransform targetTransform, Vector3 origin, Vector3 destination, WaitForSeconds delay, float delayDuration, float duration, MMTweenCurve curve, bool ignoreTimescale = false)
		{
			return null;
		}

		public static Coroutine MoveTransform(MonoBehaviour mono, Transform targetTransform, Transform origin, Transform destination, WaitForSeconds delay, float delayDuration, float duration, MMTweenCurve curve, bool updatePosition = true, bool updateRotation = true, bool ignoreTimescale = false)
		{
			return null;
		}

		public static Coroutine RotateTransformAround(MonoBehaviour mono, Transform targetTransform, Transform center, Transform destination, float angle, WaitForSeconds delay, float delayDuration, float duration, MMTweenCurve curve, bool ignoreTimescale = false)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CMoveRectTransformCo_003Ed__61))]
		protected static IEnumerator MoveRectTransformCo(RectTransform targetTransform, Vector3 origin, Vector3 destination, WaitForSeconds delay, float delayDuration, float duration, MMTweenCurve curve, bool ignoreTimescale = false)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CMoveTransformCo_003Ed__62))]
		protected static IEnumerator MoveTransformCo(Transform targetTransform, Vector3 origin, Vector3 destination, WaitForSeconds delay, float delayDuration, float duration, MMTweenCurve curve, bool ignoreTimescale = false)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CMoveTransformCo_003Ed__63))]
		protected static IEnumerator MoveTransformCo(Transform targetTransform, Transform origin, Transform destination, WaitForSeconds delay, float delayDuration, float duration, MMTweenCurve curve, bool updatePosition = true, bool updateRotation = true, bool ignoreTimescale = false)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CRotateTransformAroundCo_003Ed__64))]
		protected static IEnumerator RotateTransformAroundCo(Transform targetTransform, Transform center, Transform destination, float angle, WaitForSeconds delay, float delayDuration, float duration, MMTweenCurve curve, bool ignoreTimescale = false)
		{
			return null;
		}
	}
}
