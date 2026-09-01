using System;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Nat
{
	public static class TaskExtension
	{
		public static Task Delay(TimeSpan delay, CancellationToken token)
		{
			long num = checked((long)delay.TotalMilliseconds);
			if (num < -1 || num > int.MaxValue)
			{
				throw new ArgumentOutOfRangeException("delay");
			}
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			Timer timer = new Timer(delegate
			{
				tcs.TrySetResult(null);
			}, null, num, -1L);
			token.Register(delegate
			{
				timer.Dispose();
				tcs.TrySetCanceled();
			});
			return tcs.Task;
		}

		public static Task<Task> WhenAny(params Task[] tasks)
		{
			return Task.Factory.ContinueWhenAny(tasks, (Task t) => t);
		}

		public static Task WhenAll(params Task[] tasks)
		{
			return Task.Factory.ContinueWhenAll(tasks, (Task[] t) => t);
		}
	}
}
