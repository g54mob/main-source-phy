using System.Runtime.InteropServices;
using Interop.core;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[SupportsInheritance]
	public struct UserList : UserList.Interface, IUpCastable<UserList>, UserListBase.Interface, IUpCastable<UserListBase>
	{
		public interface Interface : IUpCastable<UserList>, UserListBase.Interface, IUpCastable<UserListBase>
		{
		}

		[BaseField]
		private UserListBase __UserListBase;

		public vector<UserListBase.Entry> m_Entries;

		ref UserList IUpCastable<UserList>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref UserListBase IUpCastable<UserListBase>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __UserListBase, 1));
		}
	}
}
