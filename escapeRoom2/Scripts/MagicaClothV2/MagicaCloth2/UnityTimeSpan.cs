using UnityEngine;

namespace MagicaCloth2
{
	public class UnityTimeSpan
	{
		private string name = string.Empty;

		private float stime;

		private float etime;

		private bool isFinish;

		public UnityTimeSpan(string name)
		{
			this.name = name;
			stime = Time.realtimeSinceStartup;
		}

		public void Finish()
		{
			if (!isFinish)
			{
				etime = Time.realtimeSinceStartup;
				isFinish = true;
			}
		}

		public float TotalSeconds()
		{
			Finish();
			return etime - stime;
		}

		public float TotalMilliSeconds()
		{
			Finish();
			return (etime - stime) * 1000f;
		}

		public override string ToString()
		{
			return $"UnityTimeSpan [{name}] : {TotalMilliSeconds()}(ms)";
		}

		public void DebugLog()
		{
			Debug.Log(this);
		}
	}
}
