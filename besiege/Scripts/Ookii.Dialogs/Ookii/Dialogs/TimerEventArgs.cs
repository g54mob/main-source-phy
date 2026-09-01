using System;

namespace Ookii.Dialogs
{
	public class TimerEventArgs : EventArgs
	{
		private int _tickCount;

		private bool _resetTickCount;

		public bool ResetTickCount
		{
			get
			{
				return _resetTickCount;
			}
			set
			{
				_resetTickCount = value;
			}
		}

		public int TickCount
		{
			get
			{
				return _tickCount;
			}
		}

		public TimerEventArgs(int tickCount)
		{
			_tickCount = tickCount;
		}
	}
}
