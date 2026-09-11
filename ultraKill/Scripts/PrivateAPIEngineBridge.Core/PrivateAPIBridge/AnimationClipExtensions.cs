using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PrivateAPIBridge
{
	public static class AnimationClipExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_002487180E789F687AD90B53AC62A3910666
		{
			[SpecialName]
			public static class _003CM_003E_0024BD837BEF03958B3A9462A2BDF922989D
			{
			}

			[ExtensionMarker("<M>$BD837BEF03958B3A9462A2BDF922989D")]
			public float startTime
			{
				[ExtensionMarker("<M>$BD837BEF03958B3A9462A2BDF922989D")]
				get
				{
					throw new NotSupportedException();
				}
			}

			[ExtensionMarker("<M>$BD837BEF03958B3A9462A2BDF922989D")]
			public float stopTime
			{
				[ExtensionMarker("<M>$BD837BEF03958B3A9462A2BDF922989D")]
				get
				{
					throw new NotSupportedException();
				}
			}

			[ExtensionMarker("<M>$BD837BEF03958B3A9462A2BDF922989D")]
			public bool hasRootMotion
			{
				[ExtensionMarker("<M>$BD837BEF03958B3A9462A2BDF922989D")]
				get
				{
					throw new NotSupportedException();
				}
			}
		}

		public static float get_startTime(AnimationClip @this)
		{
			return @this.startTime;
		}

		public static float get_stopTime(AnimationClip @this)
		{
			return @this.stopTime;
		}

		public static bool get_hasRootMotion(AnimationClip @this)
		{
			return @this.hasRootMotion;
		}
	}
}
