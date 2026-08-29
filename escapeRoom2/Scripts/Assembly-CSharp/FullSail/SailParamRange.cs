using System;

namespace FullSail
{
	[Serializable]
	public class SailParamRange
	{
		public SailParamID id;

		public bool active;

		public float fmin;

		public float fmax;

		public int seed;
	}
}
