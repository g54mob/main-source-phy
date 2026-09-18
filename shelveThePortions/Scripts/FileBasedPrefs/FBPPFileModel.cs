using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
internal class FBPPFileModel
{
	[Serializable]
	public class StringItem
	{
		public string Key;

		public string Value;

		public StringItem(string K, string V)
		{
			Key = K;
			Value = V;
		}
	}

	[Serializable]
	public class IntItem
	{
		public string Key;

		public int Value;

		public IntItem(string K, int V)
		{
			Key = K;
			Value = V;
		}
	}

	[Serializable]
	public class FloatItem
	{
		public string Key;

		public float Value;

		public FloatItem(string K, float V)
		{
			Key = K;
			Value = V;
		}
	}

	[Serializable]
	public class BoolItem
	{
		public string Key;

		public bool Value;

		public BoolItem(string K, bool V)
		{
			Key = K;
			Value = V;
		}
	}

	public StringItem[] StringData = new StringItem[0];

	public IntItem[] IntData = new IntItem[0];

	public FloatItem[] FloatData = new FloatItem[0];

	public BoolItem[] BoolData = new BoolItem[0];

	public object GetValueForKey(string key, object defaultValue)
	{
		if (defaultValue is string)
		{
			for (int i = 0; i < StringData.Length; i++)
			{
				if (StringData[i].Key.Equals(key))
				{
					return StringData[i].Value;
				}
			}
		}
		if (defaultValue is int)
		{
			for (int j = 0; j < IntData.Length; j++)
			{
				if (IntData[j].Key.Equals(key))
				{
					return IntData[j].Value;
				}
			}
		}
		if (defaultValue is float)
		{
			for (int k = 0; k < FloatData.Length; k++)
			{
				if (FloatData[k].Key.Equals(key))
				{
					return FloatData[k].Value;
				}
			}
		}
		if (defaultValue is bool)
		{
			for (int l = 0; l < BoolData.Length; l++)
			{
				if (BoolData[l].Key.Equals(key))
				{
					return BoolData[l].Value;
				}
			}
		}
		return defaultValue;
	}

	public void UpdateOrAddData(string key, object value)
	{
		if (HasKeyFromObject(key, value))
		{
			SetValueForExistingKey(key, value);
		}
		else
		{
			SetValueForNewKey(key, value);
		}
	}

	private void SetValueForNewKey(string key, object value)
	{
		if (value is string)
		{
			List<StringItem> list = StringData.ToList();
			list.Add(new StringItem(key, (string)value));
			StringData = list.ToArray();
		}
		if (value is int)
		{
			List<IntItem> list2 = IntData.ToList();
			list2.Add(new IntItem(key, (int)value));
			IntData = list2.ToArray();
		}
		if (value is float)
		{
			List<FloatItem> list3 = FloatData.ToList();
			list3.Add(new FloatItem(key, (float)value));
			FloatData = list3.ToArray();
		}
		if (value is bool)
		{
			List<BoolItem> list4 = BoolData.ToList();
			list4.Add(new BoolItem(key, (bool)value));
			BoolData = list4.ToArray();
		}
	}

	private void SetValueForExistingKey(string key, object value)
	{
		if (value is string)
		{
			for (int i = 0; i < StringData.Length; i++)
			{
				if (StringData[i].Key.Equals(key))
				{
					StringData[i].Value = (string)value;
				}
			}
		}
		if (value is int)
		{
			for (int j = 0; j < IntData.Length; j++)
			{
				if (IntData[j].Key.Equals(key))
				{
					IntData[j].Value = (int)value;
				}
			}
		}
		if (value is float)
		{
			for (int k = 0; k < FloatData.Length; k++)
			{
				if (FloatData[k].Key.Equals(key))
				{
					FloatData[k].Value = (float)value;
				}
			}
		}
		if (!(value is bool))
		{
			return;
		}
		for (int l = 0; l < BoolData.Length; l++)
		{
			if (BoolData[l].Key.Equals(key))
			{
				BoolData[l].Value = (bool)value;
			}
		}
	}

	public bool HasKeyFromObject(string key, object value)
	{
		if (value is string)
		{
			for (int i = 0; i < StringData.Length; i++)
			{
				if (StringData[i].Key.Equals(key))
				{
					return true;
				}
			}
		}
		if (value is int)
		{
			for (int j = 0; j < IntData.Length; j++)
			{
				if (IntData[j].Key.Equals(key))
				{
					return true;
				}
			}
		}
		if (value is float)
		{
			for (int k = 0; k < FloatData.Length; k++)
			{
				if (FloatData[k].Key.Equals(key))
				{
					return true;
				}
			}
		}
		if (value is bool)
		{
			for (int l = 0; l < BoolData.Length; l++)
			{
				if (BoolData[l].Key.Equals(key))
				{
					return true;
				}
			}
		}
		return false;
	}

	public void DeleteKey(string key)
	{
		for (int i = 0; i < StringData.Length; i++)
		{
			if (StringData[i].Key.Equals(key))
			{
				List<StringItem> list = StringData.ToList();
				list.RemoveAt(i);
				StringData = list.ToArray();
			}
		}
		for (int j = 0; j < IntData.Length; j++)
		{
			if (IntData[j].Key.Equals(key))
			{
				List<IntItem> list2 = IntData.ToList();
				list2.RemoveAt(j);
				IntData = list2.ToArray();
			}
		}
		for (int k = 0; k < FloatData.Length; k++)
		{
			if (FloatData[k].Key.Equals(key))
			{
				List<FloatItem> list3 = FloatData.ToList();
				list3.RemoveAt(k);
				FloatData = list3.ToArray();
			}
		}
		for (int l = 0; l < BoolData.Length; l++)
		{
			if (BoolData[l].Key.Equals(key))
			{
				List<BoolItem> list4 = BoolData.ToList();
				list4.RemoveAt(l);
				BoolData = list4.ToArray();
			}
		}
	}

	public void DeleteString(string key)
	{
		for (int i = 0; i < StringData.Length; i++)
		{
			if (StringData[i].Key.Equals(key))
			{
				List<StringItem> list = StringData.ToList();
				list.RemoveAt(i);
				StringData = list.ToArray();
			}
		}
	}

	public void DeleteInt(string key)
	{
		for (int i = 0; i < IntData.Length; i++)
		{
			if (IntData[i].Key.Equals(key))
			{
				List<IntItem> list = IntData.ToList();
				list.RemoveAt(i);
				IntData = list.ToArray();
			}
		}
	}

	public void DeleteFloat(string key)
	{
		for (int i = 0; i < FloatData.Length; i++)
		{
			if (FloatData[i].Key.Equals(key))
			{
				List<FloatItem> list = FloatData.ToList();
				list.RemoveAt(i);
				FloatData = list.ToArray();
			}
		}
	}

	public void DeleteBool(string key)
	{
		for (int i = 0; i < BoolData.Length; i++)
		{
			if (BoolData[i].Key.Equals(key))
			{
				List<BoolItem> list = BoolData.ToList();
				list.RemoveAt(i);
				BoolData = list.ToArray();
			}
		}
	}

	public bool HasKey(string key)
	{
		for (int i = 0; i < StringData.Length; i++)
		{
			if (StringData[i].Key.Equals(key))
			{
				return true;
			}
		}
		for (int j = 0; j < IntData.Length; j++)
		{
			if (IntData[j].Key.Equals(key))
			{
				return true;
			}
		}
		for (int k = 0; k < FloatData.Length; k++)
		{
			if (FloatData[k].Key.Equals(key))
			{
				return true;
			}
		}
		for (int l = 0; l < BoolData.Length; l++)
		{
			if (BoolData[l].Key.Equals(key))
			{
				return true;
			}
		}
		return false;
	}
}
