using System.Diagnostics;

namespace Poly.Timers
{
	[DebuggerDisplay("{id} | {isBeginning?\"Begin\":\"End\",nq} : {(elapsedTicks - referenceTicks)/10} μs")]
	public struct TimerInfo
	{
		public TimerId id;

		public bool isBeginning;

		public uint timestamp;

		public static uint referenceTimestamp;
	}
}
