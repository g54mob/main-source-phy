using System.Runtime.InteropServices;

namespace Interop
{
	[SupportsInheritance]
	public struct vector_set<Key> : vector_set<Key>.Interface, IUpCastable<vector_set<Key>>, sorted_vector<Key>.Interface, IUpCastable<sorted_vector<Key>> where Key : unmanaged
	{
		public interface Interface : IUpCastable<vector_set<Key>>, sorted_vector<Key>.Interface, IUpCastable<sorted_vector<Key>>
		{
		}

		[BaseField]
		private sorted_vector<Key> __sorted_vector;

		ref vector_set<Key> IUpCastable<vector_set<Key>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref sorted_vector<Key> IUpCastable<sorted_vector<Key>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __sorted_vector, 1));
		}
	}
	[SupportsInheritance]
	public struct vector_set<Key, Compare, Allocator> : vector_set<Key, Compare, Allocator>.Interface, IUpCastable<vector_set<Key, Compare, Allocator>>, sorted_vector<Key, Compare, Allocator>.Interface, IUpCastable<sorted_vector<Key, Compare, Allocator>> where Key : unmanaged where Compare : unmanaged where Allocator : unmanaged
	{
		public interface Interface : IUpCastable<vector_set<Key, Compare, Allocator>>, sorted_vector<Key, Compare, Allocator>.Interface, IUpCastable<sorted_vector<Key, Compare, Allocator>>
		{
		}

		[BaseField]
		private sorted_vector<Key, Compare, Allocator> __sorted_vector;

		ref vector_set<Key, Compare, Allocator> IUpCastable<vector_set<Key, Compare, Allocator>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref sorted_vector<Key, Compare, Allocator> IUpCastable<sorted_vector<Key, Compare, Allocator>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __sorted_vector, 1));
		}
	}
}
