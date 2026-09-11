using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[IncompleteType]
	[SupportsInheritance]
	public struct PrefabInstance : PrefabInstance.Interface, IUpCastable<PrefabInstance>, Object.Interface, IUpCastable<Object>
	{
		public interface Interface : IUpCastable<PrefabInstance>, Object.Interface, IUpCastable<Object>
		{
		}

		[BaseField]
		private Object __Object;

		ref PrefabInstance IUpCastable<PrefabInstance>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref Object IUpCastable<Object>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Object, 1));
		}
	}
}
