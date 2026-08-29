using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AOT;
using GLTFast.Schema;
using Unity.Burst;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal static class CachedFunction
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate int GetIndexDelegate(void* baseAddress, int index);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate void GetFloat3Delegate(float3* destination, void* src);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal unsafe delegate int GetIndexValueUInt8_0000049F_0024PostfixBurstDelegate(void* baseAddress, int index);

		internal static class GetIndexValueUInt8_0000049F_0024BurstDirectCall
		{
			private static IntPtr Pointer;

			[BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref IntPtr P_0)
			{
				if (Pointer == (IntPtr)0)
				{
					Pointer = BurstCompiler.CompileFunctionPointer<GetIndexValueUInt8_0000049F_0024PostfixBurstDelegate>(GetIndexValueUInt8).Value;
				}
				P_0 = Pointer;
			}

			private static IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static int Invoke(void* baseAddress, int index)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (IntPtr)0)
					{
						return ((delegate* unmanaged[Cdecl]<void*, int, int>)functionPointer)(baseAddress, index);
					}
				}
				return GetIndexValueUInt8_0024BurstManaged(baseAddress, index);
			}
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal unsafe delegate int GetIndexValueInt8_000004A0_0024PostfixBurstDelegate(void* baseAddress, int index);

		internal static class GetIndexValueInt8_000004A0_0024BurstDirectCall
		{
			private static IntPtr Pointer;

			[BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref IntPtr P_0)
			{
				if (Pointer == (IntPtr)0)
				{
					Pointer = BurstCompiler.CompileFunctionPointer<GetIndexValueInt8_000004A0_0024PostfixBurstDelegate>(GetIndexValueInt8).Value;
				}
				P_0 = Pointer;
			}

			private static IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static int Invoke(void* baseAddress, int index)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (IntPtr)0)
					{
						return ((delegate* unmanaged[Cdecl]<void*, int, int>)functionPointer)(baseAddress, index);
					}
				}
				return GetIndexValueInt8_0024BurstManaged(baseAddress, index);
			}
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal unsafe delegate int GetIndexValueUInt16_000004A1_0024PostfixBurstDelegate(void* baseAddress, int index);

		internal static class GetIndexValueUInt16_000004A1_0024BurstDirectCall
		{
			private static IntPtr Pointer;

			[BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref IntPtr P_0)
			{
				if (Pointer == (IntPtr)0)
				{
					Pointer = BurstCompiler.CompileFunctionPointer<GetIndexValueUInt16_000004A1_0024PostfixBurstDelegate>(GetIndexValueUInt16).Value;
				}
				P_0 = Pointer;
			}

			private static IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static int Invoke(void* baseAddress, int index)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (IntPtr)0)
					{
						return ((delegate* unmanaged[Cdecl]<void*, int, int>)functionPointer)(baseAddress, index);
					}
				}
				return GetIndexValueUInt16_0024BurstManaged(baseAddress, index);
			}
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal unsafe delegate int GetIndexValueInt16_000004A2_0024PostfixBurstDelegate(void* baseAddress, int index);

		internal static class GetIndexValueInt16_000004A2_0024BurstDirectCall
		{
			private static IntPtr Pointer;

			[BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref IntPtr P_0)
			{
				if (Pointer == (IntPtr)0)
				{
					Pointer = BurstCompiler.CompileFunctionPointer<GetIndexValueInt16_000004A2_0024PostfixBurstDelegate>(GetIndexValueInt16).Value;
				}
				P_0 = Pointer;
			}

			private static IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static int Invoke(void* baseAddress, int index)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (IntPtr)0)
					{
						return ((delegate* unmanaged[Cdecl]<void*, int, int>)functionPointer)(baseAddress, index);
					}
				}
				return GetIndexValueInt16_0024BurstManaged(baseAddress, index);
			}
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal unsafe delegate int GetIndexValueUInt32_000004A3_0024PostfixBurstDelegate(void* baseAddress, int index);

		internal static class GetIndexValueUInt32_000004A3_0024BurstDirectCall
		{
			private static IntPtr Pointer;

			[BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref IntPtr P_0)
			{
				if (Pointer == (IntPtr)0)
				{
					Pointer = BurstCompiler.CompileFunctionPointer<GetIndexValueUInt32_000004A3_0024PostfixBurstDelegate>(GetIndexValueUInt32).Value;
				}
				P_0 = Pointer;
			}

			private static IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static int Invoke(void* baseAddress, int index)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (IntPtr)0)
					{
						return ((delegate* unmanaged[Cdecl]<void*, int, int>)functionPointer)(baseAddress, index);
					}
				}
				return GetIndexValueUInt32_0024BurstManaged(baseAddress, index);
			}
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal unsafe delegate void GetFloat3Float_000004A4_0024PostfixBurstDelegate(float3* destination, void* src);

		internal static class GetFloat3Float_000004A4_0024BurstDirectCall
		{
			private static IntPtr Pointer;

			[BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref IntPtr P_0)
			{
				if (Pointer == (IntPtr)0)
				{
					Pointer = BurstCompiler.CompileFunctionPointer<GetFloat3Float_000004A4_0024PostfixBurstDelegate>(GetFloat3Float).Value;
				}
				P_0 = Pointer;
			}

			private static IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(float3* destination, void* src)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<float3*, void*, void>)functionPointer)(destination, src);
						return;
					}
				}
				GetFloat3Float_0024BurstManaged(destination, src);
			}
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal unsafe delegate void GetFloat3Int8_000004A5_0024PostfixBurstDelegate(float3* destination, void* src);

		internal static class GetFloat3Int8_000004A5_0024BurstDirectCall
		{
			private static IntPtr Pointer;

			[BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref IntPtr P_0)
			{
				if (Pointer == (IntPtr)0)
				{
					Pointer = BurstCompiler.CompileFunctionPointer<GetFloat3Int8_000004A5_0024PostfixBurstDelegate>(GetFloat3Int8).Value;
				}
				P_0 = Pointer;
			}

			private static IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(float3* destination, void* src)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<float3*, void*, void>)functionPointer)(destination, src);
						return;
					}
				}
				GetFloat3Int8_0024BurstManaged(destination, src);
			}
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal unsafe delegate void GetFloat3UInt8_000004A6_0024PostfixBurstDelegate(float3* destination, void* src);

		internal static class GetFloat3UInt8_000004A6_0024BurstDirectCall
		{
			private static IntPtr Pointer;

			[BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref IntPtr P_0)
			{
				if (Pointer == (IntPtr)0)
				{
					Pointer = BurstCompiler.CompileFunctionPointer<GetFloat3UInt8_000004A6_0024PostfixBurstDelegate>(GetFloat3UInt8).Value;
				}
				P_0 = Pointer;
			}

			private static IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(float3* destination, void* src)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<float3*, void*, void>)functionPointer)(destination, src);
						return;
					}
				}
				GetFloat3UInt8_0024BurstManaged(destination, src);
			}
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal unsafe delegate void GetFloat3Int16_000004A7_0024PostfixBurstDelegate(float3* destination, void* src);

		internal static class GetFloat3Int16_000004A7_0024BurstDirectCall
		{
			private static IntPtr Pointer;

			[BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref IntPtr P_0)
			{
				if (Pointer == (IntPtr)0)
				{
					Pointer = BurstCompiler.CompileFunctionPointer<GetFloat3Int16_000004A7_0024PostfixBurstDelegate>(GetFloat3Int16).Value;
				}
				P_0 = Pointer;
			}

			private static IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(float3* destination, void* src)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<float3*, void*, void>)functionPointer)(destination, src);
						return;
					}
				}
				GetFloat3Int16_0024BurstManaged(destination, src);
			}
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal unsafe delegate void GetFloat3UInt16_000004A8_0024PostfixBurstDelegate(float3* destination, void* src);

		internal static class GetFloat3UInt16_000004A8_0024BurstDirectCall
		{
			private static IntPtr Pointer;

			[BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref IntPtr P_0)
			{
				if (Pointer == (IntPtr)0)
				{
					Pointer = BurstCompiler.CompileFunctionPointer<GetFloat3UInt16_000004A8_0024PostfixBurstDelegate>(GetFloat3UInt16).Value;
				}
				P_0 = Pointer;
			}

			private static IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(float3* destination, void* src)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<float3*, void*, void>)functionPointer)(destination, src);
						return;
					}
				}
				GetFloat3UInt16_0024BurstManaged(destination, src);
			}
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal unsafe delegate void GetFloat3UInt32_000004A9_0024PostfixBurstDelegate(float3* destination, void* src);

		internal static class GetFloat3UInt32_000004A9_0024BurstDirectCall
		{
			private static IntPtr Pointer;

			[BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref IntPtr P_0)
			{
				if (Pointer == (IntPtr)0)
				{
					Pointer = BurstCompiler.CompileFunctionPointer<GetFloat3UInt32_000004A9_0024PostfixBurstDelegate>(GetFloat3UInt32).Value;
				}
				P_0 = Pointer;
			}

			private static IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(float3* destination, void* src)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<float3*, void*, void>)functionPointer)(destination, src);
						return;
					}
				}
				GetFloat3UInt32_0024BurstManaged(destination, src);
			}
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal unsafe delegate void GetFloat3Int8Normalized_000004AA_0024PostfixBurstDelegate(float3* destination, void* src);

		internal static class GetFloat3Int8Normalized_000004AA_0024BurstDirectCall
		{
			private static IntPtr Pointer;

			[BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref IntPtr P_0)
			{
				if (Pointer == (IntPtr)0)
				{
					Pointer = BurstCompiler.CompileFunctionPointer<GetFloat3Int8Normalized_000004AA_0024PostfixBurstDelegate>(GetFloat3Int8Normalized).Value;
				}
				P_0 = Pointer;
			}

			private static IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(float3* destination, void* src)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<float3*, void*, void>)functionPointer)(destination, src);
						return;
					}
				}
				GetFloat3Int8Normalized_0024BurstManaged(destination, src);
			}
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal unsafe delegate void GetFloat3UInt8Normalized_000004AB_0024PostfixBurstDelegate(float3* destination, void* src);

		internal static class GetFloat3UInt8Normalized_000004AB_0024BurstDirectCall
		{
			private static IntPtr Pointer;

			[BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref IntPtr P_0)
			{
				if (Pointer == (IntPtr)0)
				{
					Pointer = BurstCompiler.CompileFunctionPointer<GetFloat3UInt8Normalized_000004AB_0024PostfixBurstDelegate>(GetFloat3UInt8Normalized).Value;
				}
				P_0 = Pointer;
			}

			private static IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(float3* destination, void* src)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<float3*, void*, void>)functionPointer)(destination, src);
						return;
					}
				}
				GetFloat3UInt8Normalized_0024BurstManaged(destination, src);
			}
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal unsafe delegate void GetFloat3Int16Normalized_000004AC_0024PostfixBurstDelegate(float3* destination, void* src);

		internal static class GetFloat3Int16Normalized_000004AC_0024BurstDirectCall
		{
			private static IntPtr Pointer;

			[BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref IntPtr P_0)
			{
				if (Pointer == (IntPtr)0)
				{
					Pointer = BurstCompiler.CompileFunctionPointer<GetFloat3Int16Normalized_000004AC_0024PostfixBurstDelegate>(GetFloat3Int16Normalized).Value;
				}
				P_0 = Pointer;
			}

			private static IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(float3* destination, void* src)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<float3*, void*, void>)functionPointer)(destination, src);
						return;
					}
				}
				GetFloat3Int16Normalized_0024BurstManaged(destination, src);
			}
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal unsafe delegate void GetFloat3UInt16Normalized_000004AD_0024PostfixBurstDelegate(float3* destination, void* src);

		internal static class GetFloat3UInt16Normalized_000004AD_0024BurstDirectCall
		{
			private static IntPtr Pointer;

			[BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref IntPtr P_0)
			{
				if (Pointer == (IntPtr)0)
				{
					Pointer = BurstCompiler.CompileFunctionPointer<GetFloat3UInt16Normalized_000004AD_0024PostfixBurstDelegate>(GetFloat3UInt16Normalized).Value;
				}
				P_0 = Pointer;
			}

			private static IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(float3* destination, void* src)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<float3*, void*, void>)functionPointer)(destination, src);
						return;
					}
				}
				GetFloat3UInt16Normalized_0024BurstManaged(destination, src);
			}
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal unsafe delegate void GetFloat3UInt32Normalized_000004AE_0024PostfixBurstDelegate(float3* destination, void* src);

		internal static class GetFloat3UInt32Normalized_000004AE_0024BurstDirectCall
		{
			private static IntPtr Pointer;

			[BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref IntPtr P_0)
			{
				if (Pointer == (IntPtr)0)
				{
					Pointer = BurstCompiler.CompileFunctionPointer<GetFloat3UInt32Normalized_000004AE_0024PostfixBurstDelegate>(GetFloat3UInt32Normalized).Value;
				}
				P_0 = Pointer;
			}

			private static IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(float3* destination, void* src)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<float3*, void*, void>)functionPointer)(destination, src);
						return;
					}
				}
				GetFloat3UInt32Normalized_0024BurstManaged(destination, src);
			}
		}

		private static FunctionPointer<GetIndexDelegate> s_GetIndexValueInt8Method;

		private static FunctionPointer<GetIndexDelegate> s_GetIndexValueUInt8Method;

		private static FunctionPointer<GetIndexDelegate> s_GetIndexValueInt16Method;

		private static FunctionPointer<GetIndexDelegate> s_GetIndexValueUInt16Method;

		private static FunctionPointer<GetIndexDelegate> s_GetIndexValueUInt32Method;

		private static FunctionPointer<GetFloat3Delegate> s_GetFloat3FloatMethod;

		private static FunctionPointer<GetFloat3Delegate> s_GetFloat3Int8Method;

		private static FunctionPointer<GetFloat3Delegate> s_GetFloat3UInt8Method;

		private static FunctionPointer<GetFloat3Delegate> s_GetFloat3Int16Method;

		private static FunctionPointer<GetFloat3Delegate> s_GetFloat3UInt16Method;

		private static FunctionPointer<GetFloat3Delegate> s_GetFloat3UInt32Method;

		private static FunctionPointer<GetFloat3Delegate> s_GetFloat3Int8NormalizedMethod;

		private static FunctionPointer<GetFloat3Delegate> s_GetFloat3UInt8NormalizedMethod;

		private static FunctionPointer<GetFloat3Delegate> s_GetFloat3Int16NormalizedMethod;

		private static FunctionPointer<GetFloat3Delegate> s_GetFloat3UInt16NormalizedMethod;

		private static FunctionPointer<GetFloat3Delegate> s_GetFloat3UInt32NormalizedMethod;

		public unsafe static FunctionPointer<GetIndexDelegate> GetIndexConverter(GltfComponentType format)
		{
			switch (format)
			{
			case GltfComponentType.UnsignedByte:
				if (!s_GetIndexValueUInt8Method.IsCreated)
				{
					s_GetIndexValueUInt8Method = BurstCompiler.CompileFunctionPointer<GetIndexDelegate>(GetIndexValueUInt8);
				}
				return s_GetIndexValueUInt8Method;
			case GltfComponentType.Byte:
				if (!s_GetIndexValueInt8Method.IsCreated)
				{
					s_GetIndexValueInt8Method = BurstCompiler.CompileFunctionPointer<GetIndexDelegate>(GetIndexValueInt8);
				}
				return s_GetIndexValueInt8Method;
			case GltfComponentType.UnsignedShort:
				if (!s_GetIndexValueUInt16Method.IsCreated)
				{
					s_GetIndexValueUInt16Method = BurstCompiler.CompileFunctionPointer<GetIndexDelegate>(GetIndexValueUInt16);
				}
				return s_GetIndexValueUInt16Method;
			case GltfComponentType.Short:
				if (!s_GetIndexValueInt16Method.IsCreated)
				{
					s_GetIndexValueInt16Method = BurstCompiler.CompileFunctionPointer<GetIndexDelegate>(GetIndexValueInt16);
				}
				return s_GetIndexValueInt16Method;
			case GltfComponentType.UnsignedInt:
				if (!s_GetIndexValueUInt32Method.IsCreated)
				{
					s_GetIndexValueUInt32Method = BurstCompiler.CompileFunctionPointer<GetIndexDelegate>(GetIndexValueUInt32);
				}
				return s_GetIndexValueUInt32Method;
			default:
				throw new ArgumentOutOfRangeException("format", format, null);
			}
		}

		public unsafe static FunctionPointer<GetFloat3Delegate> GetPositionConverter(GltfComponentType format, bool normalized)
		{
			if (normalized)
			{
				switch (format)
				{
				case GltfComponentType.Byte:
					if (!s_GetFloat3Int8NormalizedMethod.IsCreated)
					{
						s_GetFloat3Int8NormalizedMethod = BurstCompiler.CompileFunctionPointer<GetFloat3Delegate>(GetFloat3Int8Normalized);
					}
					return s_GetFloat3Int8NormalizedMethod;
				case GltfComponentType.UnsignedByte:
					if (!s_GetFloat3UInt8NormalizedMethod.IsCreated)
					{
						s_GetFloat3UInt8NormalizedMethod = BurstCompiler.CompileFunctionPointer<GetFloat3Delegate>(GetFloat3UInt8Normalized);
					}
					return s_GetFloat3UInt8NormalizedMethod;
				case GltfComponentType.Short:
					if (!s_GetFloat3Int16NormalizedMethod.IsCreated)
					{
						s_GetFloat3Int16NormalizedMethod = BurstCompiler.CompileFunctionPointer<GetFloat3Delegate>(GetFloat3Int16Normalized);
					}
					return s_GetFloat3Int16NormalizedMethod;
				case GltfComponentType.UnsignedShort:
					if (!s_GetFloat3UInt16NormalizedMethod.IsCreated)
					{
						s_GetFloat3UInt16NormalizedMethod = BurstCompiler.CompileFunctionPointer<GetFloat3Delegate>(GetFloat3UInt16Normalized);
					}
					return s_GetFloat3UInt16NormalizedMethod;
				case GltfComponentType.UnsignedInt:
					if (!s_GetFloat3UInt32NormalizedMethod.IsCreated)
					{
						s_GetFloat3UInt32NormalizedMethod = BurstCompiler.CompileFunctionPointer<GetFloat3Delegate>(GetFloat3UInt32Normalized);
					}
					return s_GetFloat3UInt32NormalizedMethod;
				}
			}
			switch (format)
			{
			case GltfComponentType.Float:
				if (!s_GetFloat3FloatMethod.IsCreated)
				{
					s_GetFloat3FloatMethod = BurstCompiler.CompileFunctionPointer<GetFloat3Delegate>(GetFloat3Float);
				}
				return s_GetFloat3FloatMethod;
			case GltfComponentType.Byte:
				if (!s_GetFloat3Int8Method.IsCreated)
				{
					s_GetFloat3Int8Method = BurstCompiler.CompileFunctionPointer<GetFloat3Delegate>(GetFloat3Int8);
				}
				return s_GetFloat3Int8Method;
			case GltfComponentType.UnsignedByte:
				if (!s_GetFloat3UInt8Method.IsCreated)
				{
					s_GetFloat3UInt8Method = BurstCompiler.CompileFunctionPointer<GetFloat3Delegate>(GetFloat3UInt8);
				}
				return s_GetFloat3UInt8Method;
			case GltfComponentType.Short:
				if (!s_GetFloat3Int16Method.IsCreated)
				{
					s_GetFloat3Int16Method = BurstCompiler.CompileFunctionPointer<GetFloat3Delegate>(GetFloat3Int16);
				}
				return s_GetFloat3Int16Method;
			case GltfComponentType.UnsignedShort:
				if (!s_GetFloat3UInt16Method.IsCreated)
				{
					s_GetFloat3UInt16Method = BurstCompiler.CompileFunctionPointer<GetFloat3Delegate>(GetFloat3UInt16);
				}
				return s_GetFloat3UInt16Method;
			case GltfComponentType.UnsignedInt:
				if (!s_GetFloat3UInt32Method.IsCreated)
				{
					s_GetFloat3UInt32Method = BurstCompiler.CompileFunctionPointer<GetFloat3Delegate>(GetFloat3UInt32);
				}
				return s_GetFloat3UInt32Method;
			default:
				throw new ArgumentOutOfRangeException("format", format, null);
			}
		}

		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetIndexDelegate))]
		private unsafe static int GetIndexValueUInt8(void* baseAddress, int index)
		{
			return GetIndexValueUInt8_0000049F_0024BurstDirectCall.Invoke(baseAddress, index);
		}

		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetIndexDelegate))]
		private unsafe static int GetIndexValueInt8(void* baseAddress, int index)
		{
			return GetIndexValueInt8_000004A0_0024BurstDirectCall.Invoke(baseAddress, index);
		}

		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetIndexDelegate))]
		private unsafe static int GetIndexValueUInt16(void* baseAddress, int index)
		{
			return GetIndexValueUInt16_000004A1_0024BurstDirectCall.Invoke(baseAddress, index);
		}

		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetIndexDelegate))]
		private unsafe static int GetIndexValueInt16(void* baseAddress, int index)
		{
			return GetIndexValueInt16_000004A2_0024BurstDirectCall.Invoke(baseAddress, index);
		}

		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetIndexDelegate))]
		private unsafe static int GetIndexValueUInt32(void* baseAddress, int index)
		{
			return GetIndexValueUInt32_000004A3_0024BurstDirectCall.Invoke(baseAddress, index);
		}

		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		private unsafe static void GetFloat3Float(float3* destination, void* src)
		{
			GetFloat3Float_000004A4_0024BurstDirectCall.Invoke(destination, src);
		}

		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		private unsafe static void GetFloat3Int8(float3* destination, void* src)
		{
			GetFloat3Int8_000004A5_0024BurstDirectCall.Invoke(destination, src);
		}

		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		private unsafe static void GetFloat3UInt8(float3* destination, void* src)
		{
			GetFloat3UInt8_000004A6_0024BurstDirectCall.Invoke(destination, src);
		}

		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		private unsafe static void GetFloat3Int16(float3* destination, void* src)
		{
			GetFloat3Int16_000004A7_0024BurstDirectCall.Invoke(destination, src);
		}

		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		private unsafe static void GetFloat3UInt16(float3* destination, void* src)
		{
			GetFloat3UInt16_000004A8_0024BurstDirectCall.Invoke(destination, src);
		}

		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		private unsafe static void GetFloat3UInt32(float3* destination, void* src)
		{
			GetFloat3UInt32_000004A9_0024BurstDirectCall.Invoke(destination, src);
		}

		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		private unsafe static void GetFloat3Int8Normalized(float3* destination, void* src)
		{
			GetFloat3Int8Normalized_000004AA_0024BurstDirectCall.Invoke(destination, src);
		}

		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		private unsafe static void GetFloat3UInt8Normalized(float3* destination, void* src)
		{
			GetFloat3UInt8Normalized_000004AB_0024BurstDirectCall.Invoke(destination, src);
		}

		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		private unsafe static void GetFloat3Int16Normalized(float3* destination, void* src)
		{
			GetFloat3Int16Normalized_000004AC_0024BurstDirectCall.Invoke(destination, src);
		}

		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		private unsafe static void GetFloat3UInt16Normalized(float3* destination, void* src)
		{
			GetFloat3UInt16Normalized_000004AD_0024BurstDirectCall.Invoke(destination, src);
		}

		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		private unsafe static void GetFloat3UInt32Normalized(float3* destination, void* src)
		{
			GetFloat3UInt32Normalized_000004AE_0024BurstDirectCall.Invoke(destination, src);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetIndexDelegate))]
		internal unsafe static int GetIndexValueUInt8_0024BurstManaged(void* baseAddress, int index)
		{
			return ((byte*)baseAddress)[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetIndexDelegate))]
		internal unsafe static int GetIndexValueInt8_0024BurstManaged(void* baseAddress, int index)
		{
			return ((sbyte*)baseAddress)[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetIndexDelegate))]
		internal unsafe static int GetIndexValueUInt16_0024BurstManaged(void* baseAddress, int index)
		{
			return ((ushort*)baseAddress)[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetIndexDelegate))]
		internal unsafe static int GetIndexValueInt16_0024BurstManaged(void* baseAddress, int index)
		{
			return ((short*)baseAddress)[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetIndexDelegate))]
		internal unsafe static int GetIndexValueUInt32_0024BurstManaged(void* baseAddress, int index)
		{
			return ((int*)baseAddress)[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		internal unsafe static void GetFloat3Float_0024BurstManaged(float3* destination, void* src)
		{
			destination->x = 0f - *(float*)src;
			destination->y = ((float*)src)[1];
			destination->z = ((float*)src)[2];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		internal unsafe static void GetFloat3Int8_0024BurstManaged(float3* destination, void* src)
		{
			destination->x = -(*(sbyte*)src);
			destination->y = ((sbyte*)src)[1];
			destination->z = ((sbyte*)src)[2];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		internal unsafe static void GetFloat3UInt8_0024BurstManaged(float3* destination, void* src)
		{
			destination->x = -(*(byte*)src);
			destination->y = (int)((byte*)src)[1];
			destination->z = (int)((byte*)src)[2];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		internal unsafe static void GetFloat3Int16_0024BurstManaged(float3* destination, void* src)
		{
			destination->x = -(*(short*)src);
			destination->y = ((short*)src)[1];
			destination->z = ((short*)src)[2];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		internal unsafe static void GetFloat3UInt16_0024BurstManaged(float3* destination, void* src)
		{
			destination->x = -(*(ushort*)src);
			destination->y = (int)((ushort*)src)[1];
			destination->z = (int)((ushort*)src)[2];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		internal unsafe static void GetFloat3UInt32_0024BurstManaged(float3* destination, void* src)
		{
			destination->x = 0L - (long)(uint)(*(int*)src);
			destination->y = ((uint*)src)[1];
			destination->z = ((uint*)src)[2];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		internal unsafe static void GetFloat3Int8Normalized_0024BurstManaged(float3* destination, void* src)
		{
			destination->x = 0f - math.max((float)(*(sbyte*)src) / 127f, -1f);
			destination->y = math.max((float)((sbyte*)src)[1] / 127f, -1f);
			destination->z = math.max((float)((sbyte*)src)[2] / 127f, -1f);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		internal unsafe static void GetFloat3UInt8Normalized_0024BurstManaged(float3* destination, void* src)
		{
			destination->x = (float)(-(*(byte*)src)) / 255f;
			destination->y = (float)(int)((byte*)src)[1] / 255f;
			destination->z = (float)(int)((byte*)src)[2] / 255f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		internal unsafe static void GetFloat3Int16Normalized_0024BurstManaged(float3* destination, void* src)
		{
			destination->x = 0f - math.max((float)(*(short*)src) / 32767f, -1f);
			destination->y = math.max((float)((short*)src)[1] / 32767f, -1f);
			destination->z = math.max((float)((short*)src)[2] / 32767f, -1f);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		internal unsafe static void GetFloat3UInt16Normalized_0024BurstManaged(float3* destination, void* src)
		{
			destination->x = (float)(-(*(ushort*)src)) / 65535f;
			destination->y = (float)(int)((ushort*)src)[1] / 65535f;
			destination->z = (float)(int)((ushort*)src)[2] / 65535f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[BurstCompile]
		[MonoPInvokeCallback(typeof(GetFloat3Delegate))]
		internal unsafe static void GetFloat3UInt32Normalized_0024BurstManaged(float3* destination, void* src)
		{
			destination->x = (float)(0L - (long)(uint)(*(int*)src)) / 4.2949673E+09f;
			destination->y = (float)((uint*)src)[1] / 4.2949673E+09f;
			destination->z = (float)((uint*)src)[2] / 4.2949673E+09f;
		}
	}
}
