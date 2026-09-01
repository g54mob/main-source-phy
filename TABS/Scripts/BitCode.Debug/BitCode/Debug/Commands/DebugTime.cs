using System.Reflection;
using BitCode.Attributes;
using BitCode.Debug.MemberWrappers;
using DdQbeCzwvEdCSCHcDJqhScymDgUBA;
using UnityEngine;

namespace BitCode.Debug.Commands
{
	public sealed class DebugTime
	{
		private static readonly DebugTime unkCMXdDaHlgFnStRuNbxzrbnMID = new DebugTime();

		[DebugCommand(Name = "Time", Description = "Push the Time context onto the stack.")]
		public static DebugTime PushTime()
		{
			return unkCMXdDaHlgFnStRuNbxzrbnMID;
		}

		[DebugCommand(Description = "Gets or sets Time.timeScale.")]
		public IPropertyWrapper TimeScale()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(Time), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "timeScale");
		}

		[DebugCommand(Description = "Gets or sets Time.fixedDeltaTime.")]
		public IPropertyWrapper FixedDeltaTime()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(Time), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "fixedDeltaTime");
		}

		[DebugCommand(Description = "Gets or sets Time.maximumDeltaTime.")]
		public IPropertyWrapper MaximumDeltaTime()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(Time), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "maximumDeltaTime");
		}

		[DebugCommand(Description = "Gets or sets Time.captureFramerate.")]
		public IPropertyWrapper CaptureFramerate()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(Time), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "captureFramerate");
		}
	}
}
