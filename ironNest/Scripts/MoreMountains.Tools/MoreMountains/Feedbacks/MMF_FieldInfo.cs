using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	public static class MMF_FieldInfo
	{
		public static Dictionary<int, List<FieldInfo>> FieldInfoList;

		public static int GetFieldInfo(MMF_Feedback target, out List<FieldInfo> fieldInfoList)
		{
			fieldInfoList = null;
			return 0;
		}

		public static int GetFieldInfo(UnityEngine.Object target, out List<FieldInfo> fieldInfoList)
		{
			fieldInfoList = null;
			return 0;
		}

		public static IList<Type> GetBaseTypes(this Type t)
		{
			return null;
		}
	}
}
