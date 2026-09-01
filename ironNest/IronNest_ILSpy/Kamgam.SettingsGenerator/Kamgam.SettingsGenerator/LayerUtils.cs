using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public static class LayerUtils
{
	public static int GetIndexOfFirstLayerInMask(LayerMask mask, int defaultIndex = -1)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected I4, but got Unknown
		//IL_00a0: Expected O, but got I4
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Expected O, but got Unknown
		int num = 0;
		object obj = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
			int num2 = 1 << num;
			int num3 = num2 & obj;
			bool flag = num3 == 0;
			bool flag2 = num3 < 0;
			bool flag3 = !flag2;
			object obj2 = !flag;
			object obj3 = flag3 & obj2;
			if (obj3 != null)
			{
				break;
			}
			num++;
			if (num >= 32)
			{
				return defaultIndex;
			}
		}
		return num;
	}
}
