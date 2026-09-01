using System;
using System.Runtime.InteropServices;

namespace RakNet
{
	public class ByteQueue : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		internal ByteQueue(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(ByteQueue obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~ByteQueue()
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
						RakNetPINVOKE.delete_ByteQueue(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		public byte[] PeekContiguousBytes(out uint outLength)
		{
			return PeekContiguousBytesHelper(out outLength);
		}

		public ByteQueue()
			: this(RakNetPINVOKE.new_ByteQueue(), true)
		{
		}

		public uint GetBytesWritten()
		{
			return RakNetPINVOKE.ByteQueue_GetBytesWritten(swigCPtr);
		}

		public void IncrementReadOffset(uint length)
		{
			RakNetPINVOKE.ByteQueue_IncrementReadOffset(swigCPtr, length);
		}

		public void DecrementReadOffset(uint length)
		{
			RakNetPINVOKE.ByteQueue_DecrementReadOffset(swigCPtr, length);
		}

		public void Clear(string file, uint line)
		{
			RakNetPINVOKE.ByteQueue_Clear(swigCPtr, file, line);
		}

		public void Print()
		{
			RakNetPINVOKE.ByteQueue_Print(swigCPtr);
		}

		public void WriteBytes(byte[] inByteArray, uint length, string file, uint line)
		{
			RakNetPINVOKE.ByteQueue_WriteBytes(swigCPtr, inByteArray, length, file, line);
		}

		public bool ReadBytes(byte[] inOutByteArray, uint maxLengthToRead, bool peek)
		{
			return RakNetPINVOKE.ByteQueue_ReadBytes(swigCPtr, inOutByteArray, maxLengthToRead, peek);
		}

		private byte[] PeekContiguousBytesHelper(out uint outLength)
		{
			IntPtr source = RakNetPINVOKE.ByteQueue_PeekContiguousBytesHelper(swigCPtr, out outLength);
			int num = (int)outLength;
			if (num <= 0)
			{
				return null;
			}
			byte[] array = new byte[num];
			Marshal.Copy(source, array, 0, num);
			return array;
		}
	}
}
