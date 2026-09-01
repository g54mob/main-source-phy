namespace Mono.CSharp
{
	public class UIntLiteral : UIntConstant, ILiteralConstant
	{
		public override bool IsLiteral
		{
			get
			{
				return true;
			}
		}

		public UIntLiteral(BuiltinTypes types, uint l, Location loc)
			: base(types, l, loc)
		{
		}

		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
