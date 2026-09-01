using System;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.TimerElapsed" /> event.</summary>
	public class TimerElapsedEventArgs : EventArgs
	{
		private IntPtr mytimerId;

		/// <summary>Gets the ID number for the timer.</summary>
		/// <returns>The ID number for the timer.</returns>
		public IntPtr TimerId
		{
			get
			{
				return mytimerId;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.TimerElapsedEventArgs" /> class.</summary>
		/// <param name="timerId">The ID number for the timer. </param>
		public TimerElapsedEventArgs(IntPtr timerId)
		{
			mytimerId = timerId;
		}
	}
}
