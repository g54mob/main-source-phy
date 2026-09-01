namespace Mono.CSharp.Linq
{
	public class ThenByAscending : OrderByAscending
	{
		protected override string MethodName
		{
			get
			{
				return "ThenBy";
			}
		}

		public ThenByAscending(QueryBlock block, Expression expr)
			: base(block, expr)
		{
		}

		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
