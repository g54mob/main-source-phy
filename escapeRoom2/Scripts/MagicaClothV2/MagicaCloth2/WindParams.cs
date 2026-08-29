namespace MagicaCloth2
{
	public struct WindParams : IValid
	{
		public float influence;

		public float frequency;

		public float turbulence;

		public float blend;

		public float synchronization;

		public float depthWeight;

		public float movingWind;

		public void Convert(WindSettings sdata, ClothProcess.ClothType clothType)
		{
			influence = sdata.influence;
			frequency = sdata.frequency;
			turbulence = sdata.turbulence;
			blend = sdata.blend;
			synchronization = sdata.synchronization;
			depthWeight = sdata.depthWeight;
			movingWind = sdata.movingWind;
		}

		public bool IsValid()
		{
			return influence > 1E-08f;
		}
	}
}
