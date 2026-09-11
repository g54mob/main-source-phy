using System.Runtime.InteropServices;

namespace Interop
{
	[SupportsInheritance]
	public struct vector_set_empty_comparer<Key, Allocator> : vector_set_empty_comparer<Key, Allocator>.Interface, IUpCastable<vector_set_empty_comparer<Key, Allocator>>, sorted_vector_empty_comparer<Key, Allocator>.Interface, IUpCastable<sorted_vector_empty_comparer<Key, Allocator>> where Key : unmanaged where Allocator : unmanaged
	{
		public interface Interface : IUpCastable<vector_set_empty_comparer<Key, Allocator>>, sorted_vector_empty_comparer<Key, Allocator>.Interface, IUpCastable<sorted_vector_empty_comparer<Key, Allocator>>
		{
		}

		[BaseField]
		private sorted_vector_empty_comparer<Key, Allocator> __sorted_vector;

		ref vector_set_empty_comparer<Key, Allocator> IUpCastable<vector_set_empty_comparer<Key, Allocator>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref sorted_vector_empty_comparer<Key, Allocator> IUpCastable<sorted_vector_empty_comparer<Key, Allocator>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __sorted_vector, 1));
		}
	}
}
