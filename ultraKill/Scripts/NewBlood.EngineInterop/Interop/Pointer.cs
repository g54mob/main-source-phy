using System;

namespace Interop
{
	public readonly struct Pointer : IEquatable<Pointer>, IComparable<Pointer>, IComparable, IPointer
	{
		public unsafe readonly void* Value;

		bool IPointer.IsPointer => true;

		unsafe void* IPointer.Value => Value;

		unsafe Type IPointer.PointerType => typeof(void*);

		public unsafe Pointer(void* value)
		{
			Value = value;
		}

		public unsafe static implicit operator void*(Pointer pointer)
		{
			return pointer.Value;
		}

		public unsafe static implicit operator Pointer(void* pointer)
		{
			return new Pointer(pointer);
		}

		public unsafe override int GetHashCode()
		{
			UIntPtr value = (UIntPtr)Value;
			return value.GetHashCode();
		}

		public unsafe override bool Equals(object obj)
		{
			if (obj is IPointer pointer)
			{
				return Value == pointer.Value;
			}
			return false;
		}

		public unsafe bool Equals(Pointer other)
		{
			return Value == other.Value;
		}

		public unsafe int CompareTo(object obj)
		{
			if (!(obj is IPointer pointer))
			{
				return -1;
			}
			return ((ulong)Value).CompareTo((ulong)pointer.Value);
		}

		public unsafe int CompareTo(Pointer other)
		{
			return ((ulong)Value).CompareTo((ulong)other.Value);
		}
	}
	public readonly struct Pointer<T> : IEquatable<Pointer<T>>, IComparable<Pointer<T>>, IEquatable<Pointer>, IComparable<Pointer>, IComparable, IPointer where T : unmanaged
	{
		public unsafe readonly T* Value;

		bool IPointer.IsPointer => true;

		unsafe void* IPointer.Value => Value;

		unsafe Type IPointer.PointerType => typeof(T*);

		public unsafe Pointer(T* value)
		{
			Value = value;
		}

		public unsafe override int GetHashCode()
		{
			UIntPtr value = (UIntPtr)Value;
			return value.GetHashCode();
		}

		public unsafe override bool Equals(object obj)
		{
			if (obj is IPointer pointer)
			{
				return Value == pointer.Value;
			}
			return false;
		}

		public unsafe bool Equals(Pointer<T> other)
		{
			return Value == other.Value;
		}

		public unsafe bool Equals(Pointer other)
		{
			return Value == other.Value;
		}

		public unsafe int CompareTo(object obj)
		{
			if (!(obj is IPointer pointer))
			{
				return -1;
			}
			return ((ulong)Value).CompareTo((ulong)pointer.Value);
		}

		public unsafe int CompareTo(Pointer<T> other)
		{
			return ((ulong)Value).CompareTo((ulong)other.Value);
		}

		public unsafe int CompareTo(Pointer other)
		{
			return ((ulong)Value).CompareTo((ulong)other.Value);
		}

		public unsafe static implicit operator T*(Pointer<T> pointer)
		{
			return pointer.Value;
		}

		public unsafe static implicit operator Pointer<T>(T* pointer)
		{
			return new Pointer<T>(pointer);
		}

		public unsafe static implicit operator void*(Pointer<T> pointer)
		{
			return pointer.Value;
		}

		public unsafe static implicit operator Pointer(Pointer<T> pointer)
		{
			return new Pointer(pointer.Value);
		}

		public unsafe static explicit operator Pointer<T>(void* pointer)
		{
			return new Pointer<T>((T*)pointer);
		}

		public unsafe static explicit operator Pointer<T>(Pointer pointer)
		{
			return new Pointer<T>((T*)pointer.Value);
		}
	}
}
