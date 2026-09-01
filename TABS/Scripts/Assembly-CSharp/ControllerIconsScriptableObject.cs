using System.Collections.Generic;
using InControl;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/ControllerIconsScriptableObject", order = 1)]
public class ControllerIconsScriptableObject : SerializedScriptableObject
{
	[OdinSerialize]
	public Dictionary<InputControlType, int> m_controllerIcons;
}
