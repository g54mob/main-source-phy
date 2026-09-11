using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[SupportsInheritance]
	public struct GameManager : GameManager.Interface, IUpCastable<GameManager>, Object.Interface, IUpCastable<Object>
	{
		public interface Interface : IUpCastable<GameManager>, Object.Interface, IUpCastable<Object>
		{
		}

		[BaseField]
		private Object __Object;

		ref GameManager IUpCastable<GameManager>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref Object IUpCastable<Object>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Object, 1));
		}
	}
}
