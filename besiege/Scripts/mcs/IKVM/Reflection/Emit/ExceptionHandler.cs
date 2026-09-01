using System;

namespace IKVM.Reflection.Emit
{
	public struct ExceptionHandler : IEquatable<ExceptionHandler>
	{
		private readonly int tryOffset;

		private readonly int tryLength;

		private readonly int filterOffset;

		private readonly int handlerOffset;

		private readonly int handlerLength;

		private readonly ExceptionHandlingClauseOptions kind;

		private readonly int exceptionTypeToken;

		public int TryOffset
		{
			get
			{
				return tryOffset;
			}
		}

		public int TryLength
		{
			get
			{
				return tryLength;
			}
		}

		public int FilterOffset
		{
			get
			{
				return filterOffset;
			}
		}

		public int HandlerOffset
		{
			get
			{
				return handlerOffset;
			}
		}

		public int HandlerLength
		{
			get
			{
				return handlerLength;
			}
		}

		public ExceptionHandlingClauseOptions Kind
		{
			get
			{
				return kind;
			}
		}

		public int ExceptionTypeToken
		{
			get
			{
				return exceptionTypeToken;
			}
		}

		public ExceptionHandler(int tryOffset, int tryLength, int filterOffset, int handlerOffset, int handlerLength, ExceptionHandlingClauseOptions kind, int exceptionTypeToken)
		{
			if (tryOffset < 0 || tryLength < 0 || filterOffset < 0 || handlerOffset < 0 || handlerLength < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.tryOffset = tryOffset;
			this.tryLength = tryLength;
			this.filterOffset = filterOffset;
			this.handlerOffset = handlerOffset;
			this.handlerLength = handlerLength;
			this.kind = kind;
			this.exceptionTypeToken = exceptionTypeToken;
		}

		public bool Equals(ExceptionHandler other)
		{
			if (tryOffset == other.tryOffset && tryLength == other.tryLength && filterOffset == other.filterOffset && handlerOffset == other.handlerOffset && handlerLength == other.handlerLength && kind == other.kind)
			{
				return exceptionTypeToken == other.exceptionTypeToken;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			ExceptionHandler? exceptionHandler = obj as ExceptionHandler?;
			if (exceptionHandler.HasValue)
			{
				return Equals(exceptionHandler.Value);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return tryOffset ^ (tryLength * 33) ^ (filterOffset * 333) ^ (handlerOffset * 3333) ^ (handlerLength * 33333);
		}

		public static bool operator ==(ExceptionHandler left, ExceptionHandler right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(ExceptionHandler left, ExceptionHandler right)
		{
			return !left.Equals(right);
		}
	}
}
