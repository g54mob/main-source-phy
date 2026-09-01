using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CloudinaryDotNet
{
	public class Expression : BaseExpression<Expression>
	{
		public const string VARIABLE_NAME_REGEX = "^\\$[a-zA-Z][a-zA-Z0-9]*$";

		public Expression()
		{
		}

		public Expression(string name)
			: this()
		{
			if (!string.IsNullOrEmpty(name))
			{
				m_expressions.Add(name);
			}
		}

		public static Expression Variable(string name, object value)
		{
			CheckVariableName(name);
			return new Expression(name)
			{
				m_expressions = { value.ToString() }
			};
		}

		public static void CheckVariableName(string name)
		{
			if (string.IsNullOrWhiteSpace(name) || !Regex.IsMatch(name, "^\\$[a-zA-Z][a-zA-Z0-9]*$"))
			{
				throw new ArgumentException("The name `" + name + "` can include only alphanumeric characters and must begin with a letter.");
			}
		}

		public static bool ValueContainsVariable(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				if (value.IndexOf("$", StringComparison.Ordinal) == -1)
				{
					return BaseExpression<Expression>.parameters.Any((KeyValuePair<string, string> v) => value.Contains("_" + v.Value) || value.Contains(v.Value + "_"));
				}
				return true;
			}
			return false;
		}

		public static Expression Width()
		{
			return new Expression("w");
		}

		public static Expression Height()
		{
			return new Expression("h");
		}

		public static Expression InitialWidth()
		{
			return new Expression("iw");
		}

		public static Expression InitialHeight()
		{
			return new Expression("ih");
		}

		public static Expression PageCount()
		{
			return new Expression("pc");
		}

		public static Expression FaceCount()
		{
			return new Expression("fc");
		}

		public static Expression IllustrationScore()
		{
			return new Expression("ils");
		}

		public static Expression CurrentPageIndex()
		{
			return new Expression("cp");
		}

		public static Expression Tags()
		{
			return new Expression("tags");
		}

		public static Expression XOffset()
		{
			return new Expression("px");
		}

		public static Expression YOffset()
		{
			return new Expression("py");
		}

		public static Expression AspectRatio()
		{
			return new Expression("ar");
		}

		public static Expression AspectRatioOfInitialImage()
		{
			return new Expression("iar");
		}

		public static Expression Duration()
		{
			return new Expression("du");
		}

		public static Expression InitialDuration()
		{
			return new Expression("idu");
		}
	}
}
