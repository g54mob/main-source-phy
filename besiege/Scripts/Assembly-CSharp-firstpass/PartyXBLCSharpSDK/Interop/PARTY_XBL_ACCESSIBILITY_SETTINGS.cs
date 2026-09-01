using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using PartyCSharpSDK;

namespace PartyXBLCSharpSDK.Interop
{
	internal struct PARTY_XBL_ACCESSIBILITY_SETTINGS
	{
		[StructLayout(LayoutKind.Sequential, Size = 85)]
		[UnsafeValueType]
		[CompilerGenerated]
		public struct _003ClanguageCode_003E__FixedBuffer38
		{
			public byte FixedElementField;
		}

		internal readonly byte speechToTextEnabled;

		internal readonly byte textToSpeechEnabled;

		private _003ClanguageCode_003E__FixedBuffer38 languageCode;

		internal readonly PARTY_GENDER gender;

		internal unsafe PARTY_XBL_ACCESSIBILITY_SETTINGS(PartyXBLCSharpSDK.PARTY_XBL_ACCESSIBILITY_SETTINGS publicObject)
		{
			speechToTextEnabled = publicObject.SpeechToTextEnabled;
			textToSpeechEnabled = publicObject.TextToSpeechEnabled;
			fixed (byte* bytePointer = &languageCode.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.LanguageCode, bytePointer, 85);
			}
			gender = publicObject.Gender;
		}

		internal unsafe string GetLanguageCode()
		{
			fixed (byte* bytePointer = &languageCode.FixedElementField)
			{
				return Converters.BytePointerToString(bytePointer, 85);
			}
		}
	}
}
