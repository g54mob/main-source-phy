using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblPreferredColor
	{
		public string PrimaryColor { get; private set; }

		public string SecondaryColor { get; private set; }

		public string TertiaryColor { get; private set; }

		internal XblPreferredColor(XGamingRuntime.Interop.XblPreferredColor interopPreferredColor)
		{
			PrimaryColor = Converters.ByteArrayToString(interopPreferredColor.primaryColor);
			SecondaryColor = Converters.ByteArrayToString(interopPreferredColor.secondaryColor);
			TertiaryColor = Converters.ByteArrayToString(interopPreferredColor.tertiaryColor);
		}
	}
}
