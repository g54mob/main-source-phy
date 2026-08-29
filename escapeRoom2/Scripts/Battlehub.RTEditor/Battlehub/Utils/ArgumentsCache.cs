using System;
using System.Reflection;
using UnityEngine;

namespace Battlehub.Utils
{
	public class ArgumentsCache
	{
		private static bool m_isFieldInfoInitialized;

		private static FieldInfo m_boolArgumentFieldInfo;

		private static FieldInfo m_floatArgumentFieldInfo;

		private static FieldInfo m_intArgumentFieldInfo;

		private static FieldInfo m_stringArgumentFieldInfo;

		private static FieldInfo m_objectArgumentFieldInfo;

		private static FieldInfo m_objectArgumentAssemblyTypeNameFieldInfo;

		public bool BoolArgument { get; set; }

		public float FloatArgument { get; set; }

		public int IntArgument { get; set; }

		public string StringArgument { get; set; }

		public UnityEngine.Object ObjectArgument { get; set; }

		public string ObjectArgumentAssemblyTypeName { get; set; }

		internal static void Initialize(Type type)
		{
			if (!m_isFieldInfoInitialized)
			{
				m_boolArgumentFieldInfo = type.GetField("m_BoolArgument", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (m_boolArgumentFieldInfo == null)
				{
					throw new NotSupportedException("m_BoolArgument FieldInfo not found.");
				}
				m_floatArgumentFieldInfo = type.GetField("m_FloatArgument", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (m_floatArgumentFieldInfo == null)
				{
					throw new NotSupportedException("m_FloatArgument FieldInfo not found.");
				}
				m_intArgumentFieldInfo = type.GetField("m_IntArgument", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (m_intArgumentFieldInfo == null)
				{
					throw new NotSupportedException("m_IntArgument FieldInfo not found.");
				}
				m_stringArgumentFieldInfo = type.GetField("m_StringArgument", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (m_stringArgumentFieldInfo == null)
				{
					throw new NotSupportedException("m_StringArgument FieldInfo not found.");
				}
				m_objectArgumentFieldInfo = type.GetField("m_ObjectArgument", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (m_objectArgumentFieldInfo == null)
				{
					throw new NotSupportedException("m_ObjectArgument FieldInfo not found.");
				}
				m_objectArgumentAssemblyTypeNameFieldInfo = type.GetField("m_ObjectArgumentAssemblyTypeName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (m_objectArgumentAssemblyTypeNameFieldInfo == null)
				{
					throw new NotSupportedException("m_ObjectArgumentAssemblyTypeName FieldInfo not found.");
				}
				m_isFieldInfoInitialized = true;
			}
		}

		public void ReadFrom(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			BoolArgument = m_boolArgumentFieldInfo.GetValue<bool>(obj);
			FloatArgument = m_floatArgumentFieldInfo.GetValue<float>(obj);
			IntArgument = m_intArgumentFieldInfo.GetValue<int>(obj);
			StringArgument = m_stringArgumentFieldInfo.GetValue<string>(obj);
			ObjectArgument = m_objectArgumentFieldInfo.GetValue<UnityEngine.Object>(obj);
			ObjectArgumentAssemblyTypeName = Reflection.CleanAssemblyName(m_objectArgumentAssemblyTypeNameFieldInfo.GetValue<string>(obj));
		}

		public void WriteTo(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			m_boolArgumentFieldInfo.SetValue(obj, BoolArgument);
			m_floatArgumentFieldInfo.SetValue(obj, FloatArgument);
			m_intArgumentFieldInfo.SetValue(obj, IntArgument);
			m_stringArgumentFieldInfo.SetValue(obj, StringArgument);
			m_objectArgumentFieldInfo.SetValue(obj, ObjectArgument);
			m_objectArgumentAssemblyTypeNameFieldInfo.SetValue(obj, ObjectArgumentAssemblyTypeName);
		}
	}
}
