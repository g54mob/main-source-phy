using System.Runtime.InteropServices;
using Interop.Unity;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[IncompleteType]
	[SupportsInheritance]
	public struct Renderer : Renderer.Interface, IUpCastable<Renderer>, Component.Interface, IUpCastable<Component>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>, BaseRenderer.Interface, IUpCastable<BaseRenderer>
	{
		public interface Interface : IUpCastable<Renderer>, Component.Interface, IUpCastable<Component>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>, BaseRenderer.Interface, IUpCastable<BaseRenderer>
		{
		}

		[BaseField]
		private Component __Component;

		[BaseField]
		private BaseRenderer __BaseRenderer;

		ref Renderer IUpCastable<Renderer>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref Component IUpCastable<Component>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Component, 1));
		}

		ref EditorExtension IUpCastable<EditorExtension>.Cast()
		{
			return ref CastOperations.UpCast<Component, EditorExtension>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Component, 1)));
		}

		ref Object IUpCastable<Object>.Cast()
		{
			return ref CastOperations.UpCast<Component, Object>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Component, 1)));
		}

		ref BaseRenderer IUpCastable<BaseRenderer>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __BaseRenderer, 1));
		}
	}
}
