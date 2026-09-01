using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace LevelCreator
{
	public class DataTable<T> : ScriptableObject, IGenericDataTable where T : DataTableRow
	{
		[SerializeField]
		private List<string> Keys = new List<string>();

		[SerializeField]
		private List<T> Values = new List<T>();

		DataTableRow IGenericDataTable.AddRow(string key)
		{
			return AddRow(key);
		}

		public T AddRow(string key)
		{
			if (Keys.Find((string x) => x == key) != null)
			{
				return default(T);
			}
			T val = (T)Activator.CreateInstance(GetTableType());
			Keys.Add(key);
			Values.Add(val);
			return val;
		}

		public T AddRow(string key, T row)
		{
			if (Keys.Find((string x) => x == key) != null)
			{
				return default(T);
			}
			T val = (T)Activator.CreateInstance(GetTableType());
			FieldInfo[] fields = row.GetType().GetFields();
			for (int num = 0; num < fields.Length; num++)
			{
				row.GetType().GetField(fields[num].Name).SetValue(val, fields[num].GetValue(row));
			}
			Keys.Add(key);
			Values.Add(val);
			return val;
		}

		public void RemoveRow(string key)
		{
			RemoveRow(Keys.FindIndex((string x) => x == key));
		}

		public void RemoveRow(int index)
		{
			if (index >= 0 && index < Keys.Count())
			{
				Keys.RemoveAt(index);
				Values.RemoveAt(index);
			}
		}

		int IGenericDataTable.Nudge(string key, NudgeDirection direction)
		{
			return Nudge(Keys.FindIndex((string x) => x == key), direction);
		}

		public int Nudge(int index, NudgeDirection direction)
		{
			if (index < 0 || index >= Keys.Count())
			{
				return index;
			}
			int num = index + ((direction == NudgeDirection.Down) ? 1 : (-1));
			if (num < 0 || num >= Keys.Count())
			{
				return index;
			}
			string value = Keys[index];
			Keys[index] = Keys[num];
			Keys[num] = value;
			T value2 = Values[index];
			Values[index] = Values[num];
			Values[num] = value2;
			return num;
		}

		public int Size()
		{
			return Keys.Count();
		}

		DataTableRow IGenericDataTable.GetRowValue(string key)
		{
			return GetRowValue(key);
		}

		public T GetRowValue(string key)
		{
			int num = Keys.FindIndex((string x) => x == key);
			if (num < 0 || num >= Keys.Count())
			{
				return default(T);
			}
			return Values[num];
		}

		DataTableRow[] IGenericDataTable.GetRowValues()
		{
			return GetRowValues() as DataTableRow[];
		}

		public T[] GetRowValues()
		{
			return Values.ToArray();
		}

		public string[] GetKeys()
		{
			return Keys.ToArray();
		}

		public void SetKey(string key, string newKey)
		{
			for (int i = 0; i < Keys.Count; i++)
			{
				if (Keys[i] == key)
				{
					Keys[i] = newKey;
				}
			}
		}

		public Type GetTableType()
		{
			return typeof(T);
		}

		public void ForEachRow(Action<string, T> callback)
		{
			for (int i = 0; i < Keys.Count(); i++)
			{
				callback(Keys[i], Values[i]);
			}
		}
	}
}
