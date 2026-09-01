using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CloudinaryDotNet
{
	public abstract class BaseExpression
	{
	}
	public abstract class BaseExpression<T> : BaseExpression where T : BaseExpression<T>
	{
		protected static Dictionary<string, string> operators = new Dictionary<string, string>
		{
			{ "=", "eq" },
			{ "!=", "ne" },
			{ "<", "lt" },
			{ ">", "gt" },
			{ "<=", "lte" },
			{ ">=", "gte" },
			{ "&&", "and" },
			{ "||", "or" },
			{ "*", "mul" },
			{ "/", "div" },
			{ "+", "add" },
			{ "-", "sub" },
			{ "^", "pow" }
		};

		protected static Dictionary<string, string> parameters = new Dictionary<string, string>
		{
			{ "width", "w" },
			{ "height", "h" },
			{ "initial_width", "iw" },
			{ "initialWidth", "iw" },
			{ "initial_height", "ih" },
			{ "initialHeight", "ih" },
			{ "aspect_ratio", "ar" },
			{ "aspectRatio", "ar" },
			{ "initial_aspect_ratio", "iar" },
			{ "initialAspectRatio", "iar" },
			{ "page_count", "pc" },
			{ "pageCount", "pc" },
			{ "face_count", "fc" },
			{ "faceCount", "fc" },
			{ "illustration_score", "ils" },
			{ "illustrationScore", "ils" },
			{ "current_page", "cp" },
			{ "currentPage", "cp" },
			{ "tags", "tags" },
			{ "pageX", "px" },
			{ "pageY", "py" },
			{ "duration", "du" }
		};

		protected List<string> m_expressions;

		protected Transformation Parent { get; private set; }

		protected BaseExpression()
		{
			m_expressions = new List<string>();
		}

		public static string Normalize(string expression)
		{
			if (string.IsNullOrEmpty(expression))
			{
				return null;
			}
			expression = Regex.Replace(expression, "[ _]+", "_");
			string pattern = GetPattern();
			Match match = new Regex("\\$_*[^_]+", RegexOptions.IgnoreCase).Match(expression);
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (match.Success)
			{
				Group obj = match.Groups[0];
				string input = expression.Substring(num, obj.Index - num);
				stringBuilder.Append(Regex.Replace(input, pattern, (Match m) => GetOperatorReplacement(m.Value)));
				stringBuilder.Append(obj.Value);
				num = obj.Index + obj.Length;
				match = match.NextMatch();
			}
			string input2 = expression.Substring(num);
			stringBuilder.Append(Regex.Replace(input2, pattern, (Match m) => GetOperatorReplacement(m.Value)));
			return stringBuilder.ToString();
		}

		public T SetParent(Transformation parent)
		{
			Parent = parent;
			return (T)this;
		}

		public override string ToString()
		{
			return Serialize();
		}

		public virtual T Value(object value)
		{
			m_expressions.Add(Convert.ToString(value, CultureInfo.InvariantCulture));
			return (T)this;
		}

		public T Mul(object value)
		{
			return Mul().Value(value);
		}

		public T Mul()
		{
			m_expressions.Add("mul");
			return (T)this;
		}

		public T Gt(object value)
		{
			return Gt().Value(value);
		}

		public T Gt()
		{
			m_expressions.Add("gt");
			return (T)this;
		}

		public T And(object value)
		{
			return And().Value(value);
		}

		public T And()
		{
			m_expressions.Add("and");
			return (T)this;
		}

		public T Or(object value)
		{
			return Or().Value(value);
		}

		public T Or()
		{
			m_expressions.Add("or");
			return (T)this;
		}

		public T Eq(object value)
		{
			return Eq().Value(value);
		}

		public T Eq()
		{
			m_expressions.Add("eq");
			return (T)this;
		}

		public T Ne(object value)
		{
			return Ne().Value(value);
		}

		public T Ne()
		{
			m_expressions.Add("ne");
			return (T)this;
		}

		public T Lt(object value)
		{
			return Lt().Value(value);
		}

		public T Lt()
		{
			m_expressions.Add("lt");
			return (T)this;
		}

		public T Lte(object value)
		{
			return Lte().Value(value);
		}

		public T Lte()
		{
			m_expressions.Add("lte");
			return (T)this;
		}

		public T Gte(object value)
		{
			return Gte().Value(value);
		}

		public T Gte()
		{
			m_expressions.Add("gte");
			return (T)this;
		}

		public T Div(object value)
		{
			return Div().Value(value);
		}

		public T Div()
		{
			m_expressions.Add("div");
			return (T)this;
		}

		public T Add(object value)
		{
			return Add().Value(value);
		}

		public T Add()
		{
			m_expressions.Add("add");
			return (T)this;
		}

		public T Sub(object value)
		{
			return Sub().Value(value);
		}

		public T Sub()
		{
			m_expressions.Add("sub");
			return (T)this;
		}

		public T In()
		{
			m_expressions.Add("in");
			return (T)this;
		}

		public T In(object value)
		{
			return In().Value(value);
		}

		public T Nin()
		{
			m_expressions.Add("nin");
			return (T)this;
		}

		public T Nin(object value)
		{
			return Nin().Value(value);
		}

		public T Pow()
		{
			m_expressions.Add("pow");
			return (T)this;
		}

		public T Pow(object value)
		{
			return Pow().Value(value);
		}

		protected static string GetOperatorReplacement(string value)
		{
			if (operators.ContainsKey(value))
			{
				return operators[value];
			}
			if (!parameters.ContainsKey(value))
			{
				return value;
			}
			return parameters[value];
		}

		protected string Serialize()
		{
			return Normalize(string.Join("_", m_expressions));
		}

		private static string GetPattern()
		{
			List<string> list = new List<string>(operators.Keys);
			list.Reverse();
			StringBuilder stringBuilder = new StringBuilder("((");
			foreach (string item in list)
			{
				stringBuilder.Append(Regex.Escape(item)).Append('|');
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			stringBuilder.Append(")(?=[ _])|(?<![\\$:])(").Append(string.Join("|", parameters.Keys.ToArray())).Append("))");
			return stringBuilder.ToString();
		}
	}
}
