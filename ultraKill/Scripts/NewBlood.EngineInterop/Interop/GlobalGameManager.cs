using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[SupportsInheritance]
	public struct GlobalGameManager : GlobalGameManager.Interface, IUpCastable<GlobalGameManager>, GameManager.Interface, IUpCastable<GameManager>, Object.Interface, IUpCastable<Object>
	{
		public interface Interface : IUpCastable<GlobalGameManager>, GameManager.Interface, IUpCastable<GameManager>, Object.Interface, IUpCastable<Object>
		{
		}

		[BaseField]
		private GameManager __GameManager;

		ref GlobalGameManager IUpCastable<GlobalGameManager>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref GameManager IUpCastable<GameManager>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __GameManager, 1));
		}

		ref Object IUpCastable<Object>.Cast()
		{
			return ref CastOperations.UpCast<GameManager, Object>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __GameManager, 1)));
		}
	}
}
