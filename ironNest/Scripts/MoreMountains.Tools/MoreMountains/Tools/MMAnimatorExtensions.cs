using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	public static class MMAnimatorExtensions
	{
		public static bool MMHasParameterOfType(this Animator self, string name, AnimatorControllerParameterType type)
		{
			return false;
		}

		public static void AddAnimatorParameterIfExists(Animator animator, string parameterName, out int parameter, AnimatorControllerParameterType type, HashSet<int> parameterList)
		{
			parameter = default(int);
		}

		public static void AddAnimatorParameterIfExists(Animator animator, string parameterName, AnimatorControllerParameterType type, HashSet<string> parameterList)
		{
		}

		public static void UpdateAnimatorBool(Animator animator, string parameterName, bool value)
		{
		}

		public static void UpdateAnimatorInteger(Animator animator, string parameterName, int value)
		{
		}

		public static void UpdateAnimatorFloat(Animator animator, string parameterName, float value, bool performSanityCheck = true)
		{
		}

		public static bool UpdateAnimatorBool(Animator animator, int parameter, bool value, HashSet<int> parameterList, bool performSanityCheck = true)
		{
			return false;
		}

		public static bool UpdateAnimatorTrigger(Animator animator, int parameter, HashSet<int> parameterList, bool performSanityCheck = true)
		{
			return false;
		}

		public static bool SetAnimatorTrigger(Animator animator, int parameter, HashSet<int> parameterList, bool performSanityCheck = true)
		{
			return false;
		}

		public static bool UpdateAnimatorFloat(Animator animator, int parameter, float value, HashSet<int> parameterList, bool performSanityCheck = true)
		{
			return false;
		}

		public static bool UpdateAnimatorInteger(Animator animator, int parameter, int value, HashSet<int> parameterList, bool performSanityCheck = true)
		{
			return false;
		}

		public static void UpdateAnimatorBool(Animator animator, string parameterName, bool value, HashSet<string> parameterList, bool performSanityCheck = true)
		{
		}

		public static void UpdateAnimatorTrigger(Animator animator, string parameterName, HashSet<string> parameterList, bool performSanityCheck = true)
		{
		}

		public static void SetAnimatorTrigger(Animator animator, string parameterName, HashSet<string> parameterList, bool performSanityCheck = true)
		{
		}

		public static void UpdateAnimatorFloat(Animator animator, string parameterName, float value, HashSet<string> parameterList, bool performSanityCheck = true)
		{
		}

		public static void UpdateAnimatorInteger(Animator animator, string parameterName, int value, HashSet<string> parameterList, bool performSanityCheck = true)
		{
		}

		public static void UpdateAnimatorBoolIfExists(Animator animator, string parameterName, bool value, bool performSanityCheck = true)
		{
		}

		public static void UpdateAnimatorTriggerIfExists(Animator animator, string parameterName, bool performSanityCheck = true)
		{
		}

		public static void SetAnimatorTriggerIfExists(Animator animator, string parameterName, bool performSanityCheck = true)
		{
		}

		public static void UpdateAnimatorFloatIfExists(Animator animator, string parameterName, float value, bool performSanityCheck = true)
		{
		}

		public static void UpdateAnimatorIntegerIfExists(Animator animator, string parameterName, int value, bool performSanityCheck = true)
		{
		}
	}
}
