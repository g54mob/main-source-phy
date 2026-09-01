namespace Mono.CSharp.Linq
{
	public class Where : AQueryClause
	{
		protected override string MethodName
		{
			get
			{
				return "Where";
			}
		}

		public Where(QueryBlock block, Expression expr, Location loc)
			: base(block, expr, loc)
		{
		}

		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
