using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class TextValueLoader : MonoBehaviour
{
	[Serializable]
	public class ValueSource
	{
		public GameObject sourceObject;

		public Component sourceComponent;

		public string propertyName;

		[NonSerialized]
		public PropertyInfo cachedProperty;

		[NonSerialized]
		public Type cachedComponentType;

		[NonSerialized]
		public string cachedPropertyName;
	}

	[Header("Target")]
	public TMP_Text targetText;

	public string format;

	[Header("Values")]
	public List<ValueSource> values;

	[Header("Refresh")]
	public bool refreshEveryFrame;

	private void OnEnable()
	{
	}

	private void Update()
	{
	}

	public void UpdateUI()
	{
	}

	private object GetValue(ValueSource source)
	{
		return null;
	}
}
