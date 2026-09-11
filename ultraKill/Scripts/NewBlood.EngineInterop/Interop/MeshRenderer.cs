using System.Runtime.InteropServices;
using Interop.Unity;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[IncompleteType]
	[SupportsInheritance]
	public struct MeshRenderer : MeshRenderer.Interface, IUpCastable<MeshRenderer>, Renderer.Interface, IUpCastable<Renderer>, Component.Interface, IUpCastable<Component>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>, BaseRenderer.Interface, IUpCastable<BaseRenderer>
	{
		public interface Interface : IUpCastable<MeshRenderer>, Renderer.Interface, IUpCastable<Renderer>, Component.Interface, IUpCastable<Component>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>, BaseRenderer.Interface, IUpCastable<BaseRenderer>
		{
		}

		[BaseField]
		private Renderer __Renderer;

		ref MeshRenderer IUpCastable<MeshRenderer>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref Renderer IUpCastable<Renderer>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Renderer, 1));
		}

		ref Component IUpCastable<Component>.Cast()
		{
			return ref CastOperations.UpCast<Renderer, Component>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Renderer, 1)));
		}

		ref EditorExtension IUpCastable<EditorExtension>.Cast()
		{
			return ref CastOperations.UpCast<Renderer, EditorExtension>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Renderer, 1)));
		}

		ref Object IUpCastable<Object>.Cast()
		{
			return ref CastOperations.UpCast<Renderer, Object>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Renderer, 1)));
		}

		ref BaseRenderer IUpCastable<BaseRenderer>.Cast()
		{
			return ref CastOperations.UpCast<Renderer, BaseRenderer>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Renderer, 1)));
		}
	}
}
