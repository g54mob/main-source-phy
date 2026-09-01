using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class MultiColorConnectionSO : ColorOptionConnectionSO
{
	protected MultiColorConnection _connection;

	public unsafe override IConnectionWithOptions<Color> GetConnection()
	{
		//IL_0051: Expected O, but got Ref
		//IL_005e: Expected O, but got Ref
		//IL_0070: Expected O, but got Ref
		//IL_007d: Expected O, but got Ref
		//IL_008f: Expected O, but got Ref
		if (_connection == null)
		{
			MultiColorConnection multiColorConnection = (MultiColorConnection)new ConnectionWithOptions<Color>();
			List<Color> list = new List<Color>();
			if (list == null)
			{
				return (IConnectionWithOptions<Color>)new NullReferenceException();
			}
			object obj = default(object);
			list.Add((Color)(&obj));
			object obj2 = default(object);
			list.Add((Color)(&obj2));
			object obj3 = default(object);
			list.Add((Color)(&obj3));
			object obj4 = default(object);
			list.Add((Color)(&obj4));
			object obj5 = default(object);
			list.Add((Color)(&obj5));
			multiColorConnection._colors = list;
			multiColorConnection._selectedIndex = 0;
			_connection = multiColorConnection;
		}
		return _connection;
	}

	public unsafe void Create()
	{
		//IL_0012: Expected O, but got Ref
		//IL_001f: Expected O, but got Ref
		//IL_0031: Expected O, but got Ref
		//IL_003e: Expected O, but got Ref
		//IL_0050: Expected O, but got Ref
		MultiColorConnection multiColorConnection = (MultiColorConnection)new ConnectionWithOptions<Color>();
		List<Color> list = new List<Color>();
		object obj = default(object);
		list.Add((Color)(&obj));
		object obj2 = default(object);
		list.Add((Color)(&obj2));
		object obj3 = default(object);
		list.Add((Color)(&obj3));
		object obj4 = default(object);
		list.Add((Color)(&obj4));
		object obj5 = default(object);
		list.Add((Color)(&obj5));
		multiColorConnection._colors = list;
		multiColorConnection._selectedIndex = 0;
		_connection = multiColorConnection;
	}

	public override void DestroyConnection()
	{
		if (_connection != null)
		{
			_connection.Destroy();
		}
		_connection = null;
	}
}
