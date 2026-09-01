using System;
using System.Runtime.InteropServices;

namespace RakNet
{
	public class AddressOrGUID : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public RakNetGUID rakNetGuid
		{
			get
			{
				IntPtr intPtr = RakNetPINVOKE.AddressOrGUID_rakNetGuid_get(swigCPtr);
				return (intPtr == IntPtr.Zero) ? null : new RakNetGUID(intPtr, false);
			}
			set
			{
				RakNetPINVOKE.AddressOrGUID_rakNetGuid_set(swigCPtr, RakNetGUID.getCPtr(value));
			}
		}

		public SystemAddress systemAddress
		{
			get
			{
				IntPtr intPtr = RakNetPINVOKE.AddressOrGUID_systemAddress_get(swigCPtr);
				return (intPtr == IntPtr.Zero) ? null : new SystemAddress(intPtr, false);
			}
			set
			{
				RakNetPINVOKE.AddressOrGUID_systemAddress_set(swigCPtr, SystemAddress.getCPtr(value));
			}
		}

		internal AddressOrGUID(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(AddressOrGUID obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~AddressOrGUID()
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
						RakNetPINVOKE.delete_AddressOrGUID(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		public static implicit operator AddressOrGUID(SystemAddress systemAddress)
		{
			return new AddressOrGUID(systemAddress);
		}

		public static implicit operator AddressOrGUID(RakNetGUID guid)
		{
			return new AddressOrGUID(guid);
		}

		public ushort GetSystemIndex()
		{
			return RakNetPINVOKE.AddressOrGUID_GetSystemIndex(swigCPtr);
		}

		public bool IsUndefined()
		{
			return RakNetPINVOKE.AddressOrGUID_IsUndefined(swigCPtr);
		}

		public void SetUndefined()
		{
			RakNetPINVOKE.AddressOrGUID_SetUndefined(swigCPtr);
		}

		public static uint ToInteger(AddressOrGUID aog)
		{
			uint result = RakNetPINVOKE.AddressOrGUID_ToInteger(getCPtr(aog));
			if (RakNetPINVOKE.SWIGPendingException.Pending)
			{
				throw RakNetPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public string ToString(bool writePort)
		{
			return RakNetPINVOKE.AddressOrGUID_ToString(swigCPtr, writePort);
		}

		public AddressOrGUID()
			: this(RakNetPINVOKE.new_AddressOrGUID__SWIG_0(), true)
		{
		}

		public AddressOrGUID(AddressOrGUID input)
			: this(RakNetPINVOKE.new_AddressOrGUID__SWIG_1(getCPtr(input)), true)
		{
			if (RakNetPINVOKE.SWIGPendingException.Pending)
			{
				throw RakNetPINVOKE.SWIGPendingException.Retrieve();
			}
		}

		public AddressOrGUID(SystemAddress input)
			: this(RakNetPINVOKE.new_AddressOrGUID__SWIG_2(SystemAddress.getCPtr(input)), true)
		{
			if (RakNetPINVOKE.SWIGPendingException.Pending)
			{
				throw RakNetPINVOKE.SWIGPendingException.Retrieve();
			}
		}

		public AddressOrGUID(Packet packet)
			: this(RakNetPINVOKE.new_AddressOrGUID__SWIG_3(Packet.getCPtr(packet)), true)
		{
		}

		public AddressOrGUID(RakNetGUID input)
			: this(RakNetPINVOKE.new_AddressOrGUID__SWIG_4(RakNetGUID.getCPtr(input)), true)
		{
			if (RakNetPINVOKE.SWIGPendingException.Pending)
			{
				throw RakNetPINVOKE.SWIGPendingException.Retrieve();
			}
		}

		public AddressOrGUID CopyData(AddressOrGUID input)
		{
			AddressOrGUID result = new AddressOrGUID(RakNetPINVOKE.AddressOrGUID_CopyData__SWIG_0(swigCPtr, getCPtr(input)), false);
			if (RakNetPINVOKE.SWIGPendingException.Pending)
			{
				throw RakNetPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public AddressOrGUID CopyData(SystemAddress input)
		{
			AddressOrGUID result = new AddressOrGUID(RakNetPINVOKE.AddressOrGUID_CopyData__SWIG_1(swigCPtr, SystemAddress.getCPtr(input)), false);
			if (RakNetPINVOKE.SWIGPendingException.Pending)
			{
				throw RakNetPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public AddressOrGUID CopyData(RakNetGUID input)
		{
			AddressOrGUID result = new AddressOrGUID(RakNetPINVOKE.AddressOrGUID_CopyData__SWIG_2(swigCPtr, RakNetGUID.getCPtr(input)), false);
			if (RakNetPINVOKE.SWIGPendingException.Pending)
			{
				throw RakNetPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public bool Equals(AddressOrGUID right)
		{
			bool result = RakNetPINVOKE.AddressOrGUID_Equals(swigCPtr, getCPtr(right));
			if (RakNetPINVOKE.SWIGPendingException.Pending)
			{
				throw RakNetPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	}
}
