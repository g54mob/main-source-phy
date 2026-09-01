using System;
using System.Runtime.InteropServices;

namespace RakNet
{
	public class UDPProxyCoordinator : PluginInterface2
	{
		private HandleRef swigCPtr;

		internal UDPProxyCoordinator(IntPtr cPtr, bool cMemoryOwn)
			: base(RakNetPINVOKE.UDPProxyCoordinator_SWIGUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(UDPProxyCoordinator obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~UDPProxyCoordinator()
		{
			Dispose();
		}

		public override void Dispose()
		{
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						RakNetPINVOKE.delete_UDPProxyCoordinator(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		public static UDPProxyCoordinator GetInstance()
		{
			IntPtr intPtr = RakNetPINVOKE.UDPProxyCoordinator_GetInstance();
			return (intPtr == IntPtr.Zero) ? null : new UDPProxyCoordinator(intPtr, false);
		}

		public static void DestroyInstance(UDPProxyCoordinator i)
		{
			RakNetPINVOKE.UDPProxyCoordinator_DestroyInstance(getCPtr(i));
		}

		public UDPProxyCoordinator()
			: this(RakNetPINVOKE.new_UDPProxyCoordinator(), true)
		{
		}

		public void SetRemoteLoginPassword(RakString password)
		{
			RakNetPINVOKE.UDPProxyCoordinator_SetRemoteLoginPassword(swigCPtr, RakString.getCPtr(password));
			if (RakNetPINVOKE.SWIGPendingException.Pending)
			{
				throw RakNetPINVOKE.SWIGPendingException.Retrieve();
			}
		}
	}
}
