using System;
using System.Runtime.InteropServices;

namespace RakNet
{
	public class NatTypeDetectionServer : PluginInterface2
	{
		public enum NATDetectionState
		{
			STATE_NONE = 0,
			STATE_TESTING_NONE_1 = 1,
			STATE_TESTING_NONE_2 = 2,
			STATE_TESTING_FULL_CONE_1 = 3,
			STATE_TESTING_FULL_CONE_2 = 4,
			STATE_TESTING_ADDRESS_RESTRICTED_1 = 5,
			STATE_TESTING_ADDRESS_RESTRICTED_2 = 6,
			STATE_TESTING_PORT_RESTRICTED_1 = 7,
			STATE_TESTING_PORT_RESTRICTED_2 = 8,
			STATE_DONE = 9
		}

		private HandleRef swigCPtr;

		internal NatTypeDetectionServer(IntPtr cPtr, bool cMemoryOwn)
			: base(RakNetPINVOKE.NatTypeDetectionServer_SWIGUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(NatTypeDetectionServer obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~NatTypeDetectionServer()
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
						RakNetPINVOKE.delete_NatTypeDetectionServer(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		public static NatTypeDetectionServer GetInstance()
		{
			IntPtr intPtr = RakNetPINVOKE.NatTypeDetectionServer_GetInstance();
			return (intPtr == IntPtr.Zero) ? null : new NatTypeDetectionServer(intPtr, false);
		}

		public static void DestroyInstance(NatTypeDetectionServer i)
		{
			RakNetPINVOKE.NatTypeDetectionServer_DestroyInstance(getCPtr(i));
		}

		public NatTypeDetectionServer()
			: this(RakNetPINVOKE.new_NatTypeDetectionServer(), true)
		{
		}

		public void Startup(string nonRakNetIP2, string nonRakNetIP3, string nonRakNetIP4)
		{
			RakNetPINVOKE.NatTypeDetectionServer_Startup(swigCPtr, nonRakNetIP2, nonRakNetIP3, nonRakNetIP4);
		}

		public void Shutdown()
		{
			RakNetPINVOKE.NatTypeDetectionServer_Shutdown(swigCPtr);
		}

		public virtual void OnRNS2Recv(SWIGTYPE_p_RNS2RecvStruct recvStruct)
		{
			RakNetPINVOKE.NatTypeDetectionServer_OnRNS2Recv(swigCPtr, SWIGTYPE_p_RNS2RecvStruct.getCPtr(recvStruct));
		}

		public virtual void DeallocRNS2RecvStruct(SWIGTYPE_p_RNS2RecvStruct s, string file, uint line)
		{
			RakNetPINVOKE.NatTypeDetectionServer_DeallocRNS2RecvStruct(swigCPtr, SWIGTYPE_p_RNS2RecvStruct.getCPtr(s), file, line);
		}

		public virtual SWIGTYPE_p_RNS2RecvStruct AllocRNS2RecvStruct(string file, uint line)
		{
			IntPtr intPtr = RakNetPINVOKE.NatTypeDetectionServer_AllocRNS2RecvStruct(swigCPtr, file, line);
			return (intPtr == IntPtr.Zero) ? null : new SWIGTYPE_p_RNS2RecvStruct(intPtr, false);
		}
	}
}
