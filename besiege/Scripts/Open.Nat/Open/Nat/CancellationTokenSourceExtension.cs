using System;
using System.Threading;

namespace Open.Nat
{
	public static class CancellationTokenSourceExtension
	{
		public static void CancelAfter(this CancellationTokenSource source, int millisecondsDelay)
		{
			if (millisecondsDelay < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsDelay");
			}
			Timer timer = new Timer(delegate(object self)
			{
				((Timer)self).Dispose();
				try
				{
					source.Cancel();
				}
				catch (ObjectDisposedException)
				{
				}
			});
			timer.Change(millisecondsDelay, -1);
		}
	}
}
