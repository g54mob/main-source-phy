using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[SupportsInheritance]
	public struct UserListBase : UserListBase.Interface, IUpCastable<UserListBase>
	{
		public struct Entry
		{
			public unsafe UserListBase* other;

			public int indexInOther;
		}

		public interface Interface : IUpCastable<UserListBase>
		{
		}

		public unsafe Object* m_Target;

		ref UserListBase IUpCastable<UserListBase>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}
	}
}
