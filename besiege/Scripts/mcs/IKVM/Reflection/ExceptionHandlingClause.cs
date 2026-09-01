using IKVM.Reflection.Reader;

namespace IKVM.Reflection
{
	public sealed class ExceptionHandlingClause
	{
		private readonly int flags;

		private readonly int tryOffset;

		private readonly int tryLength;

		private readonly int handlerOffset;

		private readonly int handlerLength;

		private readonly Type catchType;

		private readonly int filterOffset;

		public Type CatchType
		{
			get
			{
				return catchType;
			}
		}

		public int FilterOffset
		{
			get
			{
				return filterOffset;
			}
		}

		public ExceptionHandlingClauseOptions Flags
		{
			get
			{
				return (ExceptionHandlingClauseOptions)flags;
			}
		}

		public int HandlerLength
		{
			get
			{
				return handlerLength;
			}
		}

		public int HandlerOffset
		{
			get
			{
				return handlerOffset;
			}
		}

		public int TryLength
		{
			get
			{
				return tryLength;
			}
		}

		public int TryOffset
		{
			get
			{
				return tryOffset;
			}
		}

		internal ExceptionHandlingClause(ModuleReader module, int flags, int tryOffset, int tryLength, int handlerOffset, int handlerLength, int classTokenOrfilterOffset, IGenericContext context)
		{
			this.flags = flags;
			this.tryOffset = tryOffset;
			this.tryLength = tryLength;
			this.handlerOffset = handlerOffset;
			this.handlerLength = handlerLength;
			catchType = ((flags == 0 && classTokenOrfilterOffset != 0) ? module.ResolveType(classTokenOrfilterOffset, context) : null);
			filterOffset = ((flags == 1) ? classTokenOrfilterOffset : 0);
		}
	}
}
