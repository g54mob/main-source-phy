using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[SupportsInheritance]
	public struct Motion : Motion.Interface, IUpCastable<Motion>, NamedObject.Interface, IUpCastable<NamedObject>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
	{
		public interface Interface : IUpCastable<Motion>, NamedObject.Interface, IUpCastable<NamedObject>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
		{
		}

		[BaseField]
		private NamedObject __NamedObject;

		public AnimationAsset m_AnimationAsset;

		public UserList m_ObjectUsers;

		ref Motion IUpCastable<Motion>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref NamedObject IUpCastable<NamedObject>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __NamedObject, 1));
		}

		ref EditorExtension IUpCastable<EditorExtension>.Cast()
		{
			return ref CastOperations.UpCast<NamedObject, EditorExtension>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __NamedObject, 1)));
		}

		ref Object IUpCastable<Object>.Cast()
		{
			return ref CastOperations.UpCast<NamedObject, Object>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __NamedObject, 1)));
		}
	}
}
