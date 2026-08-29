using System;

namespace MagicaCloth2
{
	[Serializable]
	public class MagicaClothProcessingException : Exception
	{
		public MagicaClothProcessingException()
		{
		}

		public MagicaClothProcessingException(string message)
			: base(message)
		{
		}

		public MagicaClothProcessingException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
