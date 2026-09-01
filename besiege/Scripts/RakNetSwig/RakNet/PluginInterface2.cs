using System;
using System.Runtime.InteropServices;

namespace RakNet
{
	public class PluginInterface2 : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		internal PluginInterface2(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(PluginInterface2 obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~PluginInterface2()
		{
			Dispose();
		}

		public virtual void Dispose()
		{
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						RakNetPINVOKE.delete_PluginInterface2(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		public PluginInterface2()
			: this(RakNetPINVOKE.new_PluginInterface2(), true)
		{
		}

		public virtual bool UsesReliabilityLayer()
		{
			return RakNetPINVOKE.PluginInterface2_UsesReliabilityLayer(swigCPtr);
		}

		public virtual void OnReliabilityLayerNotification(string errorMessage, uint bitsUsed, SystemAddress remoteSystemAddress, bool isError)
		{
			RakNetPINVOKE.PluginInterface2_OnReliabilityLayerNotification(swigCPtr, errorMessage, bitsUsed, SystemAddress.getCPtr(remoteSystemAddress), isError);
			if (RakNetPINVOKE.SWIGPendingException.Pending)
			{
				throw RakNetPINVOKE.SWIGPendingException.Retrieve();
			}
		}

		public RakPeerInterface GetRakPeerInterface()
		{
			IntPtr intPtr = RakNetPINVOKE.PluginInterface2_GetRakPeerInterface(swigCPtr);
			return (intPtr == IntPtr.Zero) ? null : new RakPeerInterface(intPtr, false);
		}

		public RakNetGUID GetMyGUIDUnified()
		{
			return new RakNetGUID(RakNetPINVOKE.PluginInterface2_GetMyGUIDUnified(swigCPtr), true);
		}

		public void SetTCPInterface(SWIGTYPE_p_RakNet__TCPInterface ptr)
		{
			RakNetPINVOKE.PluginInterface2_SetTCPInterface(swigCPtr, SWIGTYPE_p_RakNet__TCPInterface.getCPtr(ptr));
		}
	}
}
