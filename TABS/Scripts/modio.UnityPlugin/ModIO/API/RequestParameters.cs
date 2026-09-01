using System.Collections.Generic;

namespace ModIO.API
{
	public class RequestParameters
	{
		public List<StringValueParameter> stringValues = new List<StringValueParameter>();

		public List<BinaryDataParameter> binaryData = new List<BinaryDataParameter>();

		public void SetStringValue<T>(string key, T value)
		{
			foreach (StringValueParameter stringValue in stringValues)
			{
				if (stringValue.key == key)
				{
					stringValue.value = value.ToString();
					return;
				}
			}
			stringValues.Add(StringValueParameter.Create(key, value));
		}

		public void SetStringArrayValue<T>(string key, T[] valueArray)
		{
			int num = 0;
			while (num < stringValues.Count)
			{
				if (stringValues[num].key.Equals(key))
				{
					stringValues.RemoveAt(num);
				}
				else
				{
					num++;
				}
			}
			for (int i = 0; i < valueArray.Length; i++)
			{
				T val = valueArray[i];
				stringValues.Add(StringValueParameter.Create(key, val.ToString()));
			}
		}

		public void SetBinaryData(string key, string fileName, byte[] data)
		{
			foreach (BinaryDataParameter binaryDatum in binaryData)
			{
				if (binaryDatum.key == key)
				{
					binaryDatum.fileName = fileName;
					binaryDatum.contents = data;
					return;
				}
			}
			binaryData.Add(BinaryDataParameter.Create(key, fileName, null, data));
		}
	}
}
