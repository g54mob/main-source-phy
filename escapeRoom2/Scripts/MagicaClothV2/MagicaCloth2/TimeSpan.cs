using System;

namespace MagicaCloth2
{
	public class TimeSpan
	{
		private string name = string.Empty;

		private DateTime stime;

		private DateTime etime;

		private bool isFinish;

		public TimeSpan()
		{
		}

		public TimeSpan(string name)
		{
			this.name = name;
			stime = DateTime.Now;
			isFinish = false;
		}

		public void Start()
		{
			stime = DateTime.Now;
			isFinish = false;
		}

		public void Finish()
		{
			if (!isFinish)
			{
				etime = DateTime.Now;
				isFinish = true;
			}
		}

		public double TotalSeconds()
		{
			Finish();
			return (etime - stime).TotalSeconds;
		}

		public double TotalMilliSeconds()
		{
			Finish();
			return (etime - stime).TotalMilliseconds;
		}

		public override string ToString()
		{
			return $"TimeSpan [{name}] : {TotalMilliSeconds()}(ms)";
		}

		public void DebugLog()
		{
		}

		public void Log()
		{
			object mes = this;
			Develop.Log(in mes);
		}
	}
}
