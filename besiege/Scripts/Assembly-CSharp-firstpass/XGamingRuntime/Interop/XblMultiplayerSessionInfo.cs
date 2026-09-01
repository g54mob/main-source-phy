using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	public struct XblMultiplayerSessionInfo
	{
		[StructLayout(LayoutKind.Sequential, Size = 40)]
		[UnsafeValueType]
		[CompilerGenerated]
		public struct _003CBranch_003E__FixedBuffer13
		{
			public byte FixedElementField;
		}

		[StructLayout(LayoutKind.Sequential, Size = 40)]
		[CompilerGenerated]
		[UnsafeValueType]
		public struct _003CCorrelationId_003E__FixedBuffer14
		{
			public byte FixedElementField;
		}

		[StructLayout(LayoutKind.Sequential, Size = 40)]
		[UnsafeValueType]
		[CompilerGenerated]
		public struct _003CSearchHandleId_003E__FixedBuffer15
		{
			public byte FixedElementField;
		}

		internal readonly uint ContractVersion;

		private _003CBranch_003E__FixedBuffer13 Branch;

		internal readonly ulong ChangeNumber;

		private _003CCorrelationId_003E__FixedBuffer14 CorrelationId;

		internal readonly TimeT StartTime;

		internal readonly TimeT NextTimer;

		private _003CSearchHandleId_003E__FixedBuffer15 SearchHandleId;

		internal unsafe XblMultiplayerSessionInfo(XGamingRuntime.XblMultiplayerSessionInfo publicObject)
		{
			ContractVersion = publicObject.ContractVersion;
			fixed (byte* branch = &Branch.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.Branch, branch, 40);
			}
			ChangeNumber = publicObject.ChangeNumber;
			fixed (byte* correlationId = &CorrelationId.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.CorrelationId, correlationId, 40);
			}
			StartTime = new TimeT(publicObject.StartTime);
			NextTimer = new TimeT(publicObject.NextTimer);
			fixed (byte* searchHandleId = &SearchHandleId.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.SearchHandleId, searchHandleId, 40);
			}
		}

		internal unsafe string GetBranch()
		{
			fixed (byte* branch = &Branch.FixedElementField)
			{
				return Converters.BytePointerToString(branch, 40);
			}
		}

		internal unsafe string GetCorrelationId()
		{
			fixed (byte* correlationId = &CorrelationId.FixedElementField)
			{
				return Converters.BytePointerToString(correlationId, 40);
			}
		}

		public unsafe string GetSearchHandleId()
		{
			fixed (byte* searchHandleId = &SearchHandleId.FixedElementField)
			{
				return Converters.BytePointerToString(searchHandleId, 40);
			}
		}
	}
}
