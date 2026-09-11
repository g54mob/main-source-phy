using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[IncompleteType]
	[SupportsInheritance]
	public struct Prefab : Prefab.Interface, IUpCastable<Prefab>, Object.Interface, IUpCastable<Object>
	{
		public interface Interface : IUpCastable<Prefab>, Object.Interface, IUpCastable<Object>
		{
		}

		[BaseField]
		private Object __Object;

		ref Prefab IUpCastable<Prefab>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref Object IUpCastable<Object>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Object, 1));
		}
	}
}
