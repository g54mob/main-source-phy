using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Vertx.Debugging
{
	public static class DrawPhysicsSettings
	{
		public static float Duration
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set;
		}

		[Conditional("UNITY_EDITOR")]
		public static void SetDuration(float duration)
		{
			Duration = duration;
		}

		[Conditional("UNITY_EDITOR")]
		public static void ResetDuration()
		{
			Duration = 0f;
		}
	}
}
