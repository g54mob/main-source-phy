using System;
using System.Runtime.CompilerServices;

namespace BitCode
{
	public class MissingDependencyException : InvalidOperationException
	{
		private const string smyDojPsXHMLcKUcMUvehoBEdZjX = "{0} is required by {1} but is missing.";

		[CompilerGenerated]
		private readonly string sHDosmZnCBuxaaCvVITUQkvGjqmL;

		[CompilerGenerated]
		private readonly string kasjSfSztAIPhRoEPcGSjZnOQjXmA;

		public string DependantName
		{
			[CompilerGenerated]
			get
			{
				return sHDosmZnCBuxaaCvVITUQkvGjqmL;
			}
		}

		private string BHQGwTJTloDFmrjithWuaEWbhAOYA
		{
			[CompilerGenerated]
			get
			{
				return kasjSfSztAIPhRoEPcGSjZnOQjXmA;
			}
		}

		public MissingDependencyException(string dependencyName, string dependantName)
			: base($"{dependencyName} is required by {dependantName} but is missing.")
		{
			while (true)
			{
				int num = 1416538386;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x3128F4C3)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 2u:
						goto IL_0034;
					case 1u:
						return;
					}
					break;
					IL_0034:
					sHDosmZnCBuxaaCvVITUQkvGjqmL = dependantName;
					kasjSfSztAIPhRoEPcGSjZnOQjXmA = dependencyName;
					num = ((int)num2 * -603111058) ^ -1932794826;
				}
			}
		}

		public MissingDependencyException(string dependencyName, string dependantName, Exception innerException)
			: base($"{dependencyName} is required by {dependantName} but is missing.", innerException)
		{
			while (true)
			{
				int num = -464446039;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -746017006)) % 3)
					{
					case 2u:
						break;
					case 1u:
						goto IL_0035;
					default:
						kasjSfSztAIPhRoEPcGSjZnOQjXmA = dependencyName;
						return;
					}
					break;
					IL_0035:
					sHDosmZnCBuxaaCvVITUQkvGjqmL = dependantName;
					num = (int)(num2 * 19936521) ^ -1111765081;
				}
			}
		}
	}
}
