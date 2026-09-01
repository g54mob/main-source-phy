using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	public struct XblMultiplayerSessionReference
	{
		[StructLayout(LayoutKind.Sequential, Size = 40)]
		[CompilerGenerated]
		[UnsafeValueType]
		public struct _003CScid_003E__FixedBuffer17
		{
			public byte FixedElementField;
		}

		[StructLayout(LayoutKind.Sequential, Size = 100)]
		[UnsafeValueType]
		[CompilerGenerated]
		public struct _003CSessionTemplateName_003E__FixedBuffer18
		{
			public byte FixedElementField;
		}

		[StructLayout(LayoutKind.Sequential, Size = 100)]
		[CompilerGenerated]
		[UnsafeValueType]
		public struct _003CSessionName_003E__FixedBuffer19
		{
			public byte FixedElementField;
		}

		private _003CScid_003E__FixedBuffer17 Scid;

		private _003CSessionTemplateName_003E__FixedBuffer18 SessionTemplateName;

		private _003CSessionName_003E__FixedBuffer19 SessionName;

		public unsafe XblMultiplayerSessionReference(XGamingRuntime.XblMultiplayerSessionReference publicObject)
		{
			fixed (byte* scid = &Scid.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.Scid, scid, 40);
			}
			fixed (byte* sessionTemplateName = &SessionTemplateName.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.SessionTemplateName, sessionTemplateName, 100);
			}
			fixed (byte* sessionName = &SessionName.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.SessionName, sessionName, 100);
			}
		}

		public unsafe string GetScid()
		{
			fixed (byte* scid = &Scid.FixedElementField)
			{
				return Converters.BytePointerToString(scid, 40);
			}
		}

		public unsafe string GetSessionTemplateName()
		{
			fixed (byte* sessionTemplateName = &SessionTemplateName.FixedElementField)
			{
				return Converters.BytePointerToString(sessionTemplateName, 100);
			}
		}

		public unsafe string GetSessionName()
		{
			fixed (byte* sessionName = &SessionName.FixedElementField)
			{
				return Converters.BytePointerToString(sessionName, 100);
			}
		}
	}
}
