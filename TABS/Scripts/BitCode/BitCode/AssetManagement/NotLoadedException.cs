using System;
using System.Runtime.CompilerServices;

namespace BitCode.AssetManagement
{
	public class NotLoadedException : InvalidOperationException
	{
		[CompilerGenerated]
		private readonly string WbLDAEFEUVHkHCraSIAxoWJNIEWo;

		public string ResourceName
		{
			[CompilerGenerated]
			get
			{
				return WbLDAEFEUVHkHCraSIAxoWJNIEWo;
			}
		}

		public NotLoadedException()
		{
		}

		public NotLoadedException(string message)
			: base(message)
		{
		}

		public NotLoadedException(string message, string resourceName)
			: base(message)
		{
			while (true)
			{
				int num = 284431736;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x9BE353F)) % 3)
					{
					case 2u:
						break;
					default:
						return;
					case 1u:
						goto IL_0029;
					case 0u:
						return;
					}
					break;
					IL_0029:
					WbLDAEFEUVHkHCraSIAxoWJNIEWo = resourceName;
					num = ((int)num2 * -656434155) ^ 0x49837B8A;
				}
			}
		}

		public NotLoadedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
