namespace FMOD
{
	public struct DSP_METERING_INFO
	{
		public struct LEVEL_ARRAY
		{
			private float ch0;

			private float ch1;

			private float ch2;

			private float ch3;

			private float ch4;

			private float ch5;

			private float ch6;

			private float ch7;

			private float ch8;

			private float ch9;

			private float ch10;

			private float ch11;

			private float ch12;

			private float ch13;

			private float ch14;

			private float ch15;

			private float ch16;

			private float ch17;

			private float ch18;

			private float ch19;

			private float ch20;

			private float ch21;

			private float ch22;

			private float ch23;

			private float ch24;

			private float ch25;

			private float ch26;

			private float ch27;

			private float ch28;

			private float ch29;

			private float ch30;

			private float ch31;

			public float this[int index] => 0f;

			public readonly int Length => 0;

			public static implicit operator float[](LEVEL_ARRAY levels)
			{
				return null;
			}

			public void CopyTo(float[] buffer)
			{
			}
		}

		public int numsamples;

		public LEVEL_ARRAY peaklevel;

		public LEVEL_ARRAY rmslevel;

		public short numchannels;
	}
}
