using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class WindowModePlatformSpecificConnection : WindowModeConnection
{
	public override List<string> GetOptionLabels()
	{
		if (CollectionExtensions.IsNullOrEmpty(_labels))
		{
			List<string> labels = new List<string>();
			_labels = labels;
			if (_labels != null)
			{
				_labels.Add("Full Screen");
				if (_labels != null)
				{
					_labels.Add("Window");
					if (_labels != null)
					{
						_labels.Add("Exclusive (Windows)");
						goto IL_00b0;
					}
				}
			}
			return (List<string>)(object)new NullReferenceException();
		}
		goto IL_00b0;
		IL_00b0:
		return _labels;
	}

	protected unsafe override List<FullScreenMode> getWindowOptions()
	{
		if (CollectionExtensions.IsNullOrEmpty(_values))
		{
			List<FullScreenMode> values = new List<FullScreenMode>();
			_values = values;
			if (_values != null)
			{
				object obj = default(object);
				_values.Add((FullScreenMode)(int)(&obj));
				if (_values != null)
				{
					_values.Add((FullScreenMode)(int)(&obj));
					if (_values != null)
					{
						_values.Add((FullScreenMode)(int)(&obj));
						goto IL_00ad;
					}
				}
			}
			return (List<FullScreenMode>)(object)new NullReferenceException();
		}
		goto IL_00ad;
		IL_00ad:
		return _values;
	}
}
