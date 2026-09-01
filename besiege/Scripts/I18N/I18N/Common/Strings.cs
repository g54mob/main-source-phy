using System;

namespace I18N.Common
{
	public sealed class Strings
	{
		public static string GetString(string tag)
		{
			switch (tag)
			{
			case "ArgRange_Array":
				return "Argument index is out of array range.";
			case "Arg_InsufficientSpace":
				return "Insufficient space in the argument array.";
			case "ArgRange_NonNegative":
				return "Non-negative value is expected.";
			case "NotSupp_MissingCodeTable":
				return "This encoding is not supported. Code table is missing.";
			case "ArgRange_StringIndex":
				return "String index is out of range.";
			case "ArgRange_StringRange":
				return "String length is out of range.";
			default:
				throw new ArgumentException(string.Format("Unexpected error tag name:  {0}", tag));
			}
		}
	}
}
