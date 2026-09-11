using System.Runtime.InteropServices;

namespace Interop
{
	[SupportsInheritance]
	public struct BaseRenderer : BaseRenderer.Interface, IUpCastable<BaseRenderer>
	{
		public interface Interface : IUpCastable<BaseRenderer>
		{
		}

		public unsafe void** __vftable;

		public SharedRendererData m_RendererData;

		[NativeTypeName("ShaderPropertySheet *")]
		public unsafe void* m_RendererProperties;

		[NativeTypeName("bool")]
		public byte m_ForceRenderingOff;

		ref BaseRenderer IUpCastable<BaseRenderer>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}
	}
}
