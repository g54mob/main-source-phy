using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XblMultiplayerActivityDetails
	{
		[StructLayout(LayoutKind.Sequential, Size = 40)]
		[CompilerGenerated]
		[UnsafeValueType]
		public struct _003CHandleId_003E__FixedBuffer12
		{
			public byte FixedElementField;
		}

		internal readonly XblMultiplayerSessionReference SessionReference;

		private _003CHandleId_003E__FixedBuffer12 HandleId;

		internal readonly uint TitleId;

		internal readonly XblMultiplayerSessionVisibility Visibility;

		internal readonly XblMultiplayerSessionRestriction JoinRestriction;

		internal readonly NativeBool Closed;

		internal readonly ulong OwnerXuid;

		internal readonly uint MaxMembersCount;

		internal readonly uint MembersCount;

		internal readonly UTF8StringPtr CustomSessionPropertiesJson;

		internal unsafe XblMultiplayerActivityDetails(XGamingRuntime.XblMultiplayerActivityDetails publicObject, DisposableCollection disposableCollection)
		{
			SessionReference = new XblMultiplayerSessionReference(publicObject.SessionReference);
			fixed (byte* handleId = &HandleId.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.HandleId, handleId, 40);
			}
			TitleId = publicObject.TitleId;
			Visibility = publicObject.Visibility;
			JoinRestriction = publicObject.JoinRestriction;
			Closed = new NativeBool(publicObject.Closed);
			OwnerXuid = publicObject.OwnerXuid;
			MaxMembersCount = publicObject.MaxMembersCount;
			MembersCount = publicObject.MembersCount;
			CustomSessionPropertiesJson = new UTF8StringPtr(publicObject.CustomSessionPropertiesJson, disposableCollection);
		}

		internal unsafe string GetHandleId()
		{
			fixed (byte* handleId = &HandleId.FixedElementField)
			{
				return Converters.BytePointerToString(handleId, 40);
			}
		}
	}
}
