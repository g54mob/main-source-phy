namespace Mono.CSharp.Linq
{
	public class OrderByDescending : AQueryClause
	{
		protected override string MethodName
		{
			get
			{
				return "OrderByDescending";
			}
		}

		public OrderByDescending(QueryBlock block, Expression expr)
			: base(block, expr, expr.Location)
		{
		}

		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
