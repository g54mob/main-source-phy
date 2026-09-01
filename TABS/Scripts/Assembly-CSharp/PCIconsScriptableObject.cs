using System.Collections.Generic;
using InControl;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/PCIconsScriptableObject", order = 1)]
public class PCIconsScriptableObject : ScriptableObject
{
	[ReorderableList]
	[FormerlySerializedAs("m_pcIcons")]
	public List<MouseKey> m_mouseIcons = new List<MouseKey>();

	[ReorderableList]
	public List<KeyboardKey> m_keyboardIcons = new List<KeyboardKey>();

	public bool TryGetValue(Mouse binding, out int index)
	{
		foreach (MouseKey mouseIcon in m_mouseIcons)
		{
			if (mouseIcon.m_binding == binding)
			{
				index = mouseIcon.m_index;
				return true;
			}
		}
		index = 0;
		return false;
	}

	public bool TryGetValue(Key binding, out int index)
	{
		foreach (KeyboardKey keyboardIcon in m_keyboardIcons)
		{
			if (keyboardIcon.m_binding == binding)
			{
				index = keyboardIcon.m_index;
				return true;
			}
		}
		index = 0;
		return false;
	}
}
