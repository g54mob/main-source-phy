using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Pb
{
	public class Debug
	{
		public enum CodeAnnotation
		{
			DuplicatedComputations = 0,
			UnreachableCode = 1
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message)
		{
			AssertImpl(condition, message);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Conditional("DEBUG")]
		public static void Assert(object obj, bool condition, string message)
		{
			AssertImpl(obj, condition, message);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Conditional("DEBUG")]
		public static void LogAssertion(object obj, string message)
		{
			LogAssertionImpl(obj, message);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Conditional("DEBUG")]
		public static void AnnotateCode(CodeAnnotation annotation)
		{
			UnityEngine.Debug.Log(annotation.ToString());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Conditional("DEBUG")]
		public static void NotImplemented()
		{
			UnityEngine.Debug.Log("Not implemented");
			UnityEngine.Debug.Break();
		}

		private static void AssertImpl(bool condition, string message)
		{
		}

		private static void AssertImpl(object obj, bool condition, string message)
		{
			if (!condition)
			{
				obj?.ToString();
			}
		}

		private static void LogAssertionImpl(object obj, string message)
		{
			obj?.ToString();
		}
	}
}
