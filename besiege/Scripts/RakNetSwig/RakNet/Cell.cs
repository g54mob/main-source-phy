using System;
using System.Runtime.InteropServices;

namespace RakNet
{
	public class Cell : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public bool isEmpty
		{
			get
			{
				return RakNetPINVOKE.Cell_isEmpty_get(swigCPtr);
			}
			set
			{
				RakNetPINVOKE.Cell_isEmpty_set(swigCPtr, value);
			}
		}

		public double i
		{
			get
			{
				return RakNetPINVOKE.Cell_i_get(swigCPtr);
			}
			set
			{
				RakNetPINVOKE.Cell_i_set(swigCPtr, value);
			}
		}

		internal Cell(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(Cell obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~Cell()
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
						RakNetPINVOKE.delete_Cell(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		public void Get(out string output)
		{
			string output2 = new string('c', (int)i);
			output = GetHelper(output2);
		}

		public Cell()
			: this(RakNetPINVOKE.new_Cell__SWIG_0(), true)
		{
		}

		public void Clear()
		{
			RakNetPINVOKE.Cell_Clear(swigCPtr);
		}

		public void Set(int input)
		{
			RakNetPINVOKE.Cell_Set__SWIG_0(swigCPtr, input);
		}

		public void Set(uint input)
		{
			RakNetPINVOKE.Cell_Set__SWIG_1(swigCPtr, input);
		}

		public void Set(double input)
		{
			RakNetPINVOKE.Cell_Set__SWIG_2(swigCPtr, input);
		}

		public void Set(string input)
		{
			RakNetPINVOKE.Cell_Set__SWIG_3(swigCPtr, input);
		}

		public void Get(out int output)
		{
			RakNetPINVOKE.Cell_Get__SWIG_0(swigCPtr, out output);
		}

		public void Get(out double output)
		{
			RakNetPINVOKE.Cell_Get__SWIG_1(swigCPtr, out output);
		}

		public RakString ToString(Table.ColumnType columnType)
		{
			return new RakString(RakNetPINVOKE.Cell_ToString(swigCPtr, (int)columnType), true);
		}

		public Cell CopyData(Cell input)
		{
			Cell result = new Cell(RakNetPINVOKE.Cell_CopyData(swigCPtr, getCPtr(input)), false);
			if (RakNetPINVOKE.SWIGPendingException.Pending)
			{
				throw RakNetPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public Cell(Cell input)
			: this(RakNetPINVOKE.new_Cell__SWIG_1(getCPtr(input)), true)
		{
			if (RakNetPINVOKE.SWIGPendingException.Pending)
			{
				throw RakNetPINVOKE.SWIGPendingException.Retrieve();
			}
		}

		public Table.ColumnType EstimateColumnType()
		{
			return (Table.ColumnType)RakNetPINVOKE.Cell_EstimateColumnType(swigCPtr);
		}

		public void Set(byte[] inByteArray, int inputLength)
		{
			RakNetPINVOKE.Cell_Set__SWIG_4(swigCPtr, inByteArray, inputLength);
		}

		public void Get(byte[] inOutByteArray, out int outputLength)
		{
			RakNetPINVOKE.Cell_Get__SWIG_2(swigCPtr, inOutByteArray, out outputLength);
		}

		private string GetHelper(string output)
		{
			return RakNetPINVOKE.Cell_GetHelper(swigCPtr, output);
		}
	}
}
