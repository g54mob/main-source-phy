using System;
using System.Collections.Generic;

namespace ch.sycoforge.Decal
{
	public class EnumHelper
	{
		public static List<T> GetAllEnums<T>() where T : struct
		{
			if (typeof(T).BaseType != typeof(Enum))
			{
				throw new ArgumentException("T must be an Enum type");
			}
			T[] array = Enum.GetValues(typeof(T)) as T[];
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = (int)(object)array[i];
			}
			List<T> list = new List<T>();
			int num = 0;
			for (int j = 0; j < array2.Length; j++)
			{
				num |= array2[j];
			}
			for (int k = 0; k <= num; k++)
			{
				int num2 = k;
				for (int l = 0; l < array2.Length; l++)
				{
					num2 &= ~array2[l];
					if (num2 == 0)
					{
						list.Add((T)(object)k);
						break;
					}
				}
			}
			try
			{
				if (string.IsNullOrEmpty(Enum.GetName(typeof(T), (T)(object)0)))
				{
					list.Remove((T)(object)0);
				}
			}
			catch
			{
				list.Remove((T)(object)0);
			}
			return list;
		}
	}
}
