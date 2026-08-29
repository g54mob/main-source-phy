using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public struct FastBinaryWriter : IDisposable
{
	private struct WriterHandle
	{
		public unsafe byte* BufferPointer;

		public int Position;

		public int Length;

		public int Capacity;

		public int MaxCapacity;

		public Allocator Allocator;

		public bool BufferGrew;
	}

	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct ForPrimitives
	{
	}

	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct ForEnums
	{
	}

	private unsafe WriterHandle* Handle;

	public unsafe bool IsInitialized => Handle != null;

	public unsafe int Position
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return Handle->Position;
		}
	}

	public unsafe int Length
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			if (Handle->Position <= Handle->Length)
			{
				return Handle->Length;
			}
			return Handle->Position;
		}
	}

	public unsafe FastBinaryWriter(int initialSize, int maxSize = -1, Allocator allocator = Allocator.Temp)
	{
		Handle = (WriterHandle*)UnsafeUtility.Malloc(sizeof(WriterHandle) + initialSize, UnsafeUtility.AlignOf<WriterHandle>(), allocator);
		Handle->BufferPointer = (byte*)Handle + sizeof(WriterHandle);
		Handle->Position = 0;
		Handle->Length = 0;
		Handle->Capacity = initialSize;
		Handle->Allocator = allocator;
		Handle->MaxCapacity = ((maxSize < initialSize) ? initialSize : maxSize);
		Handle->BufferGrew = false;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void Seek(int where)
	{
		where = Math.Min(where, Handle->Capacity);
		if (Handle->Position > Handle->Length && where < Handle->Position)
		{
			Handle->Length = Handle->Position;
		}
		Handle->Position = where;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void Reset()
	{
		Handle->Position = 0;
		Handle->Length = 0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe bool TryBeginWrite(int byteCount)
	{
		if (Handle->Position + byteCount > Handle->Capacity)
		{
			if (Handle->Position + byteCount > Handle->MaxCapacity)
			{
				return false;
			}
			if (Handle->Capacity >= Handle->MaxCapacity)
			{
				return false;
			}
			Grow(byteCount);
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe bool TryBeginWriteValue<T>(in T value) where T : unmanaged
	{
		int num = sizeof(T);
		if (Handle->Position + num > Handle->Capacity)
		{
			if (Handle->Position + num > Handle->MaxCapacity)
			{
				return false;
			}
			if (Handle->Capacity >= Handle->MaxCapacity)
			{
				return false;
			}
			Grow(num);
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe bool TryBeginWriteInternal(int bytes)
	{
		if (Handle->Position + bytes > Handle->Capacity)
		{
			if (Handle->Position + bytes > Handle->MaxCapacity)
			{
				return false;
			}
			if (Handle->Capacity >= Handle->MaxCapacity)
			{
				return false;
			}
			Grow(bytes);
		}
		return true;
	}

	private unsafe void Grow(int additionalSizeRequired)
	{
		int num;
		for (num = Handle->Capacity * 2; num < Handle->Position + additionalSizeRequired; num *= 2)
		{
		}
		int num2 = Math.Min(num, Handle->MaxCapacity);
		byte* ptr = (byte*)UnsafeUtility.Malloc(num2, UnsafeUtility.AlignOf<byte>(), Handle->Allocator);
		UnsafeUtility.MemCpy(ptr, Handle->BufferPointer, Length);
		if (Handle->BufferGrew)
		{
			UnsafeUtility.Free(Handle->BufferPointer, Handle->Allocator);
		}
		Handle->BufferGrew = true;
		Handle->BufferPointer = ptr;
		Handle->Capacity = num2;
	}

	public unsafe void Write(string @string)
	{
		if (@string == null)
		{
			Write<int>(-1, default(ForPrimitives));
			return;
		}
		int value = @string.Length;
		Write(in value, default(ForPrimitives));
		if (value != 0)
		{
			fixed (char* value2 = @string)
			{
				WriteBytes((byte*)value2, value * 2);
			}
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Write<T>(in T value, ForPrimitives unused = default(ForPrimitives)) where T : unmanaged
	{
		WriteUnmanaged(in value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Write<T>(T[] value, ForPrimitives unused = default(ForPrimitives)) where T : unmanaged
	{
		WriteUnmanaged(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Write<T>(in T value, ForEnums unused = default(ForEnums)) where T : unmanaged, Enum
	{
		WriteUnmanaged(in value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Write<T>(T[] value, ForEnums unused = default(ForEnums)) where T : unmanaged, Enum
	{
		WriteUnmanaged(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void WriteVector2(in Vector2 vector2)
	{
		WriteUnmanaged(in vector2);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void WriteVector3(in Vector3 vector3)
	{
		WriteUnmanaged(in vector3);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void WriteVector4(in Vector4 vector4)
	{
		WriteUnmanaged(in vector4);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void WriteQuaternion(in Quaternion quaternion)
	{
		WriteUnmanaged(in quaternion);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void WriteColor(in Color color)
	{
		WriteUnmanaged(in color);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void WriteRay(in Ray ray)
	{
		WriteUnmanaged(in ray);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void WriteUnchecked<T>(in T value, ForPrimitives unused = default(ForPrimitives)) where T : unmanaged
	{
		WriteUnmanagedUnchecked(in value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void WriteUnchecked<T>(T[] value, ForPrimitives unused = default(ForPrimitives)) where T : unmanaged
	{
		WriteUnmanagedUnchecked(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void WriteUnchecked<T>(in T value, ForEnums unused = default(ForEnums)) where T : unmanaged, Enum
	{
		WriteUnmanagedUnchecked(in value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void WriteUnchecked<T>(T[] value, ForEnums unused = default(ForEnums)) where T : unmanaged, Enum
	{
		WriteUnmanagedUnchecked(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void WriteVector2Unchecked(in Vector2 vector2)
	{
		WriteUnmanagedUnchecked(in vector2);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void WriteVector3Unchecked(in Vector3 vector3)
	{
		WriteUnmanagedUnchecked(in vector3);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void WriteVector4Unchecked(in Vector4 vector4)
	{
		WriteUnmanagedUnchecked(in vector4);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void WriteQuaternionUnchecked(in Quaternion quaternion)
	{
		WriteUnmanagedUnchecked(in quaternion);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void WriteColorUnchecked(in Color color)
	{
		WriteUnmanagedUnchecked(in color);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe void WriteUnmanaged<T>(in T value) where T : unmanaged
	{
		fixed (T* ptr = &value)
		{
			byte* value2 = (byte*)ptr;
			WriteBytes(value2, sizeof(T));
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe void WriteUnmanaged<T>(T[] value) where T : unmanaged
	{
		WriteUnmanaged<int>(value.Length);
		fixed (T* ptr = value)
		{
			byte* value2 = (byte*)ptr;
			WriteBytes(value2, sizeof(T) * value.Length);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe void WriteUnmanagedUnchecked<T>(in T value) where T : unmanaged
	{
		fixed (T* ptr = &value)
		{
			byte* value2 = (byte*)ptr;
			WriteBytesUnchecked(value2, sizeof(T));
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe void WriteUnmanagedUnchecked<T>(T[] value) where T : unmanaged
	{
		WriteUnmanagedUnchecked<int>(value.Length);
		fixed (T* ptr = value)
		{
			byte* value2 = (byte*)ptr;
			WriteBytesUnchecked(value2, sizeof(T) * value.Length);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void WriteBytes(byte[] value, int size = -1, int offset = 0)
	{
		fixed (byte* value2 = value)
		{
			WriteBytes(value2, (size == -1) ? value.Length : size, offset);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void WriteBytesUnchecked(byte[] value, int size = -1, int offset = 0)
	{
		fixed (byte* value2 = value)
		{
			WriteBytesUnchecked(value2, (size == -1) ? value.Length : size, offset);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe void WriteBytes(byte* value, int size, int offset = 0)
	{
		if (!TryBeginWriteInternal(size))
		{
			throw new OverflowException($"Writing past the end of the buffer, size is {size} bytes but remaining capacity is {Handle->Capacity - Handle->Position} bytes");
		}
		UnsafeUtility.MemCpy(Handle->BufferPointer + Handle->Position, value + offset, size);
		Handle->Position += size;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe void WriteBytesUnchecked(byte* value, int size, int offset = 0)
	{
		UnsafeUtility.MemCpy(Handle->BufferPointer + Handle->Position, value + offset, size);
		Handle->Position += size;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void WriteNativeArray<T>(NativeArray<T> nativeArray) where T : unmanaged
	{
		WriteUnmanaged<int>(nativeArray.Length);
		WriteBytes((byte*)nativeArray.GetUnsafePtr(), sizeof(T) * nativeArray.Length);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void Write(byte value)
	{
		if (!TryBeginWriteInternal(1))
		{
			throw new OverflowException("Writing past the end of the buffer");
		}
		Handle->BufferPointer[Handle->Position++] = value;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void WriteUnchecked(byte value)
	{
		Handle->BufferPointer[Handle->Position++] = value;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void WritePartialValue<T>(T value, int bytesToWrite, int offsetBytes = 0) where T : unmanaged
	{
		byte* source = (byte*)(&value) + offsetBytes;
		UnsafeUtility.MemCpy(Handle->BufferPointer + Handle->Position, source, bytesToWrite);
		Handle->Position += bytesToWrite;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe byte[] ToArray()
	{
		byte[] array = new byte[Length];
		fixed (byte* destination = array)
		{
			UnsafeUtility.MemCpy(destination, Handle->BufferPointer, Length);
		}
		return array;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe Span<byte> AsSpan()
	{
		return new Span<byte>(Handle->BufferPointer, Length);
	}

	public unsafe void Dispose()
	{
		if (Handle != null)
		{
			if (Handle->BufferGrew)
			{
				UnsafeUtility.Free(Handle->BufferPointer, Handle->Allocator);
			}
			UnsafeUtility.Free(Handle, Handle->Allocator);
			Handle = null;
		}
	}
}
