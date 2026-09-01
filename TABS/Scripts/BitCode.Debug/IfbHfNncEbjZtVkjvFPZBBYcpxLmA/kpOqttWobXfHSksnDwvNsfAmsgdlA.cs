using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using BitCode.Debug;

namespace IfbHfNncEbjZtVkjvFPZBBYcpxLmA
{
	internal struct kpOqttWobXfHSksnDwvNsfAmsgdlA
	{
		private readonly DebugConsole MSaeGYUSrEUnnjNMigtzkgLuvLFz;

		public readonly string YBLwrmnGpuNNorfGBIOCDXiTtNWV;

		public readonly string RPFxjSkyEPTyMTlnykcAETQhIstB;

		public readonly MethodInfo HlGwOAKaoNMxiiutGrpPqeWSOgBM;

		public readonly Type MzGbjYSINAlUiDjmRZYCXhbDKeTh;

		public readonly Type trZaBdfbWuyVlkBeEHjhbgibLyXpB;

		public readonly bool WfndoufkaPaQMggxgUSCHTGYHHRPB;

		public readonly int UUIgfIwPJCwnlxaFzoYVszeDotDn;

		public readonly ParameterInfo[] bkncIrHDIQIvZUDikxserJxasceeA;

		public kpOqttWobXfHSksnDwvNsfAmsgdlA(DebugConsole P_0, string P_1, MethodInfo P_2, string P_3)
		{
			YBLwrmnGpuNNorfGBIOCDXiTtNWV = P_1;
			RPFxjSkyEPTyMTlnykcAETQhIstB = P_3;
			MSaeGYUSrEUnnjNMigtzkgLuvLFz = P_0;
			HlGwOAKaoNMxiiutGrpPqeWSOgBM = P_2;
			trZaBdfbWuyVlkBeEHjhbgibLyXpB = P_2.ReturnType;
			WfndoufkaPaQMggxgUSCHTGYHHRPB = P_2.IsStatic;
			bkncIrHDIQIvZUDikxserJxasceeA = P_2.GetParameters();
			UUIgfIwPJCwnlxaFzoYVszeDotDn = bkncIrHDIQIvZUDikxserJxasceeA.Length;
			MzGbjYSINAlUiDjmRZYCXhbDKeTh = typeof(void);
			if (WfndoufkaPaQMggxgUSCHTGYHHRPB)
			{
				if (bkncIrHDIQIvZUDikxserJxasceeA.Length != 0 && HlGwOAKaoNMxiiutGrpPqeWSOgBM.IsDefined(typeof(ExtensionAttribute)))
				{
					MzGbjYSINAlUiDjmRZYCXhbDKeTh = bkncIrHDIQIvZUDikxserJxasceeA[0].ParameterType;
				}
			}
			else
			{
				MzGbjYSINAlUiDjmRZYCXhbDKeTh = P_2.DeclaringType;
			}
		}

		public string VkHPAWCWxWnMxPHbIfousxdTCLti()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num3 = default(int);
			ParameterInfo[] array = default(ParameterInfo[]);
			ParameterInfo parameterInfo = default(ParameterInfo);
			while (true)
			{
				int num = 603408541;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x606BF85B)) % 12)
					{
					case 4u:
						break;
					case 0u:
					{
						int num4;
						if (num3 >= array.Length)
						{
							num = 1464213499;
							num4 = num;
						}
						else
						{
							num = 1782312434;
							num4 = num;
						}
						continue;
					}
					case 1u:
					{
						parameterInfo = array[num3];
						int num5;
						if (MSaeGYUSrEUnnjNMigtzkgLuvLFz.BfQDjKlCtjVjTdbJCDBiEejLgcxN.fyvYQTrRmQvhxIUOLoLpfTyocRkI(parameterInfo))
						{
							num = 1459869562;
							num5 = num;
						}
						else
						{
							num = 642131837;
							num5 = num;
						}
						continue;
					}
					case 7u:
						num3 = 0;
						num = (int)(num2 * 1038630869) ^ -1241111626;
						continue;
					case 3u:
						stringBuilder.Append(parameterInfo.HasDefaultValue ? ("[" + parameterInfo.Name + "]") : (parameterInfo.Name ?? ""));
						num = 642131837;
						continue;
					case 9u:
						array = bkncIrHDIQIvZUDikxserJxasceeA;
						num = ((int)num2 * -722310727) ^ -1819283267;
						continue;
					case 5u:
						stringBuilder.Append(' ');
						num = ((int)num2 * -1213195244) ^ -1630315216;
						continue;
					case 10u:
						num3++;
						num = 1932134343;
						continue;
					case 2u:
						num = (int)((num2 * 1809878058) ^ 0x431A21BB);
						continue;
					case 6u:
						stringBuilder.Append(YBLwrmnGpuNNorfGBIOCDXiTtNWV);
						num = ((int)num2 * -1524205608) ^ -447342294;
						continue;
					case 8u:
						stringBuilder.AppendLine();
						stringBuilder.Append('\t');
						num = (int)(num2 * 1153670110) ^ -2099557256;
						continue;
					default:
						stringBuilder.Append(string.IsNullOrWhiteSpace(RPFxjSkyEPTyMTlnykcAETQhIstB) ? "No description" : RPFxjSkyEPTyMTlnykcAETQhIstB);
						return stringBuilder.ToString();
					}
					break;
				}
			}
		}
	}
}
