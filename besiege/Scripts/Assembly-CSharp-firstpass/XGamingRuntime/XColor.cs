using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XColor
	{
		public byte A { get; private set; }

		public byte R { get; private set; }

		public byte G { get; private set; }

		public byte B { get; private set; }

		public uint Value { get; private set; }

		internal XColor(XGamingRuntime.Interop.XColor interopStruct)
		{
			A = interopStruct.A;
			R = interopStruct.R;
			G = interopStruct.G;
			B = interopStruct.B;
			Value = BitConverter.ToUInt32(new byte[4] { interopStruct.A, interopStruct.R, interopStruct.G, interopStruct.B }, 0);
		}
	}
}
