using System;
using System.Reflection;
using ThisOtherThing.UI.Shapes;
using UnityEngine;

namespace ThisOtherThing.Utils.Animation
{
	[ExecuteInEditMode]
	public class ValueSetter : MonoBehaviour
	{
		public static BindingFlags binding = BindingFlags.Instance | BindingFlags.Public;

		public int FieldType;

		public bool IsInArray;

		public int ArrayItemIndex;

		public bool IsInClass;

		public string TargetTypeName;

		public string TargetFieldName;

		public string FieldName;

		public string ArrayFieldName;

		public string TargetClassFieldName;

		public string ClassFieldName;

		public float FloatValue;

		public Color ColorValue;

		private float cachedFloatValue = float.NegativeInfinity;

		private Color cachedColorValue;

		private bool cachedBoolValue;

		private IShape target;

		private object targetField;

		private FieldInfo fieldInfo;

		private void OnValidate()
		{
			UpdateCachedReferences();
		}

		private void Start()
		{
			UpdateCachedReferences();
		}

		private void Update()
		{
			if (targetField != null && fieldInfo != null && (!cachedFloatValue.Equals(FloatValue) || !cachedColorValue.Equals(ColorValue)))
			{
				if (FieldType == 0)
				{
					fieldInfo.SetValue(targetField, FloatValue);
				}
				else if (FieldType == 1)
				{
					fieldInfo.SetValue(targetField, FloatValue >= 0.99f);
				}
				else if (FieldType == 2)
				{
					fieldInfo.SetValue(targetField, (Color32)ColorValue);
				}
				target.ForceMeshUpdate();
				cachedFloatValue = FloatValue;
				cachedColorValue = ColorValue;
			}
		}

		private void UpdateCachedReferences()
		{
			if (TargetTypeName == null || TargetFieldName == null)
			{
				return;
			}
			if (target == null)
			{
				target = base.gameObject.GetComponent<IShape>();
			}
			targetField = target.GetType().GetField(TargetFieldName, binding).GetValue(target);
			if (IsInArray)
			{
				FieldInfo field = targetField.GetType().GetField(FieldName);
				if (field == null)
				{
					return;
				}
				if (field.FieldType.GetElementType() != null)
				{
					fieldInfo = targetField.GetType().GetField(FieldName, binding).FieldType.GetElementType().GetField(ArrayFieldName, binding);
					Array array = (Array)targetField.GetType().GetField(FieldName, binding).GetValue(targetField);
					targetField = array.GetValue(ArrayItemIndex);
				}
			}
			else
			{
				fieldInfo = Type.GetType(TargetTypeName).GetField(FieldName, BindingFlags.Instance | BindingFlags.Public);
			}
			if (IsInClass && TargetClassFieldName.Length != 0 && ClassFieldName.Length != 0)
			{
				FieldInfo field2 = targetField.GetType().GetField(TargetClassFieldName, binding);
				if (!(field2 == null))
				{
					targetField = field2.GetValue(targetField);
					fieldInfo = targetField.GetType().GetField(ClassFieldName, binding);
				}
			}
		}
	}
}
