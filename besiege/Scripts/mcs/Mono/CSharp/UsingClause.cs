namespace Mono.CSharp
{
	public class UsingClause
	{
		private readonly ATypeNameExpression expr;

		private readonly Location loc;

		protected FullNamedExpression resolved;

		public virtual SimpleMemberName Alias
		{
			get
			{
				return null;
			}
		}

		public Location Location
		{
			get
			{
				return loc;
			}
		}

		public ATypeNameExpression NamespaceExpression
		{
			get
			{
				return expr;
			}
		}

		public FullNamedExpression ResolvedExpression
		{
			get
			{
				return resolved;
			}
		}

		public UsingClause(ATypeNameExpression expr, Location loc)
		{
			this.expr = expr;
			this.loc = loc;
		}

		public string GetSignatureForError()
		{
			return expr.GetSignatureForError();
		}

		public virtual void Define(NamespaceContainer ctx)
		{
			resolved = expr.ResolveAsTypeOrNamespace(ctx, false);
		}

		public override string ToString()
		{
			return resolved.ToString();
		}
	}
}
