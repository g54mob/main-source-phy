using System;

namespace MTAssets.UltimateLODSystem.MeshSimplifier
{
	public sealed class ValidateSimplificationOptionsException : Exception
	{
		private readonly string propertyName;

		public string PropertyName => null;

		public override string Message => null;

		public ValidateSimplificationOptionsException(string propertyName, string message)
		{
		}

		public ValidateSimplificationOptionsException(string propertyName, string message, Exception innerException)
		{
		}
	}
}
