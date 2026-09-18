using System;
using System.Collections.Generic;

[Serializable]
public class SerializableDicList<T>
{
	public List<T> list;

	public SerializableDicList()
	{
		list = new List<T>();
	}

	public SerializableDicList(List<T> l)
	{
		list = new List<T>(l);
	}
}
