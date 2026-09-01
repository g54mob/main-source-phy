using System;
using System.Runtime.InteropServices;

namespace RakNet
{
	public class ThreadsafePacketLogger : PacketLogger
	{
		private HandleRef swigCPtr;

		internal ThreadsafePacketLogger(IntPtr cPtr, bool cMemoryOwn)
			: base(RakNetPINVOKE.ThreadsafePacketLogger_SWIGUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(ThreadsafePacketLogger obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~ThreadsafePacketLogger()
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
						RakNetPINVOKE.delete_ThreadsafePacketLogger(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		public ThreadsafePacketLogger()
			: this(RakNetPINVOKE.new_ThreadsafePacketLogger(), true)
		{
		}
	}
}
