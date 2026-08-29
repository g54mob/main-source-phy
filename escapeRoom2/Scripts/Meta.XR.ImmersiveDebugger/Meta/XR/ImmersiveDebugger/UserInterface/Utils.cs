using System.Text.RegularExpressions;
using UnityEngine;

namespace Meta.XR.ImmersiveDebugger.UserInterface
{
	internal static class Utils
	{
		private const int MaxLetterCountForTitle = 22;

		public const int DropDownMenuSortOrder = 5;

		public const int CursorSortOrder = 31000;

		internal static string ToDisplayText(this string input)
		{
			string input2 = Regex.Replace(input, "([a-z])([A-Z])", "$1 $2");
			input2 = Regex.Replace(input2, "([A-Z]+)([A-Z][a-z])", "$1 $2");
			input2 = input2.Replace("_", " ");
			input2 = char.ToUpper(input2[0]) + input2.Substring(1);
			if (input2.Length > 22)
			{
				input2 = input2.Substring(0, 22);
			}
			return input2;
		}

		internal static Vector3 LerpPosition(Vector3 current, Vector3 target, float lerpSpeed)
		{
			if (Vector3.Distance(current, target) < 0.01f)
			{
				return target;
			}
			current = Vector3.Lerp(current, target, Time.deltaTime * lerpSpeed);
			return current;
		}

		internal static string ClampText(string text, int limit)
		{
			if (text.Length <= limit)
			{
				return text;
			}
			return text.Substring(0, limit);
		}
	}
}
