using System;

namespace BitCode.Maths
{
	public class DotNetRandomNumberGenerator : IRandomNumberGenerator
	{
		private readonly Random DwFghfEueBrlcIhNHdNmhXveJcsb;

		public DotNetRandomNumberGenerator()
		{
			DwFghfEueBrlcIhNHdNmhXveJcsb = new Random();
		}

		public DotNetRandomNumberGenerator(int seed)
		{
			DwFghfEueBrlcIhNHdNmhXveJcsb = new Random(seed);
		}

		public double NextDouble()
		{
			return DwFghfEueBrlcIhNHdNmhXveJcsb.NextDouble();
		}
	}
}
