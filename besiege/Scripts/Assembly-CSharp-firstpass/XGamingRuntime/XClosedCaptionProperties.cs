using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XClosedCaptionProperties
	{
		public XColor BackgroundColor { get; private set; }

		public XColor FontColor { get; private set; }

		public XColor WindowColor { get; private set; }

		public XClosedCaptionFontEdgeAttribute FontEdgeAttribute { get; private set; }

		public XClosedCaptionFontStyle FontStyle { get; private set; }

		public float FontScale { get; private set; }

		public bool Enabled { get; private set; }

		internal XClosedCaptionProperties(XGamingRuntime.Interop.XClosedCaptionProperties interopStruct)
		{
			BackgroundColor = new XColor(interopStruct.BackgroundColor);
			FontColor = new XColor(interopStruct.FontColor);
			WindowColor = new XColor(interopStruct.WindowColor);
			FontEdgeAttribute = interopStruct.FontEdgeAttribute;
			FontStyle = interopStruct.FontStyle;
			FontScale = interopStruct.FontScale;
			Enabled = interopStruct.Enabled.Value;
		}
	}
}
