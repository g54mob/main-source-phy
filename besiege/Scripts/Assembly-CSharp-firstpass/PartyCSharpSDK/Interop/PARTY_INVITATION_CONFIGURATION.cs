using System;

namespace PartyCSharpSDK.Interop
{
	internal struct PARTY_INVITATION_CONFIGURATION
	{
		internal readonly UTF8StringPtr identifier;

		internal readonly PARTY_INVITATION_REVOCABILITY revocability;

		internal readonly uint entityIdCount;

		private unsafe readonly UTF8StringPtr* entityIds;

		internal unsafe PARTY_INVITATION_CONFIGURATION(PartyCSharpSDK.PARTY_INVITATION_CONFIGURATION publicObject, DisposableCollection disposableCollection)
		{
			identifier = new UTF8StringPtr(publicObject.Identifier, disposableCollection);
			revocability = publicObject.Revocability;
			SizeT arrayCount;
			entityIds = (UTF8StringPtr*)(void*)Converters.ClassArrayToPtr(publicObject.EntityIds, (string x, DisposableCollection d) => new UTF8StringPtr(x, d), disposableCollection, out arrayCount);
			entityIdCount = arrayCount.ToUInt32();
		}

		internal unsafe T[] GetEntityIds<T>(Func<UTF8StringPtr, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)entityIds, entityIdCount, ctor);
		}
	}
}
