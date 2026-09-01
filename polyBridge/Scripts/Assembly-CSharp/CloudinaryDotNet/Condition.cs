using System.Globalization;

namespace CloudinaryDotNet
{
	public class Condition : BaseExpression<Condition>
	{
		public Condition()
		{
		}

		public Condition(string condition)
			: this()
		{
			if (!string.IsNullOrEmpty(condition))
			{
				m_expressions.Add(BaseExpression<Condition>.Normalize(condition));
			}
		}

		public Transformation Then()
		{
			base.Parent.IfCondition(Serialize());
			return base.Parent;
		}

		public Condition Width(string @operator, object value)
		{
			return Predicate("w", @operator, value);
		}

		public Condition InitialWidth(string @operator, object value)
		{
			return Predicate("iw", @operator, value);
		}

		public Condition Height(string @operator, object value)
		{
			return Predicate("h", @operator, value);
		}

		public Condition InitialHeight(string @operator, object value)
		{
			return Predicate("ih", @operator, value);
		}

		public Condition AspectRatio(string @operator, string value)
		{
			return Predicate("ar", @operator, value);
		}

		public Condition FaceCount(string @operator, object value)
		{
			return Predicate("fc", @operator, value);
		}

		public Condition PageCount(string @operator, object value)
		{
			return Predicate("pc", @operator, value);
		}

		public Condition Duration(string @operator, object value)
		{
			return Predicate("du", @operator, value);
		}

		public Condition InitialDuration(string @operator, object value)
		{
			return Predicate("idu", @operator, value);
		}

		protected Condition Predicate(string name, string @operator, object value)
		{
			if (BaseExpression<Condition>.operators.ContainsKey(@operator))
			{
				@operator = BaseExpression<Condition>.operators[@operator];
			}
			m_expressions.Add(string.Format(CultureInfo.InvariantCulture, "{0}_{1}_{2}", name, @operator, value));
			return this;
		}
	}
}
