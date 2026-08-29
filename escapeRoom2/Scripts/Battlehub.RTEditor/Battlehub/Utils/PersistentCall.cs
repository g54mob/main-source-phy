using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace Battlehub.Utils
{
	public class PersistentCall
	{
		private static bool m_isFieldInfoInitialized;

		private static FieldInfo m_argumentsFieldInfo;

		private static FieldInfo m_callStateFieldInfo;

		private static FieldInfo m_methodNameFieldInfo;

		private static FieldInfo m_modeFieldInfo;

		private static FieldInfo m_targetFieldInfo;

		private string m_methodName;

		private UnityEngine.Object m_target;

		public ArgumentsCache ArgumentsCache { get; set; }

		public UnityEventCallState CallState { get; set; }

		public string MethodName
		{
			get
			{
				return m_methodName;
			}
			set
			{
				if (m_methodName != value)
				{
					m_methodName = value;
					Mode = PersistentListenerMode.EventDefined;
				}
			}
		}

		public PersistentListenerMode Mode { get; set; }

		public UnityEngine.Object Target
		{
			get
			{
				return m_target;
			}
			set
			{
				if (m_target != value)
				{
					m_target = value;
					MethodName = string.Empty;
				}
			}
		}

		public static PersistentCall CreateNew()
		{
			return new PersistentCall
			{
				ArgumentsCache = new ArgumentsCache(),
				CallState = UnityEventCallState.RuntimeOnly,
				Mode = PersistentListenerMode.EventDefined,
				MethodName = string.Empty
			};
		}

		internal static void Initialize(Type type)
		{
			if (!m_isFieldInfoInitialized)
			{
				m_argumentsFieldInfo = type.GetField("m_Arguments", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (m_argumentsFieldInfo == null)
				{
					throw new NotSupportedException("m_Arguments FieldInfo not found.");
				}
				m_callStateFieldInfo = type.GetField("m_CallState", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (m_callStateFieldInfo == null)
				{
					throw new NotSupportedException("m_CallState FieldInfo not found.");
				}
				m_methodNameFieldInfo = type.GetField("m_MethodName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (m_methodNameFieldInfo == null)
				{
					throw new NotSupportedException("m_MethodName FieldInfo not found.");
				}
				m_modeFieldInfo = type.GetField("m_Mode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (m_modeFieldInfo == null)
				{
					throw new NotSupportedException("m_Mode FieldInfo not found.");
				}
				m_targetFieldInfo = type.GetField("m_Target", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (m_targetFieldInfo == null)
				{
					throw new NotSupportedException("m_Target FieldInfo not found.");
				}
				m_isFieldInfoInitialized = true;
				ArgumentsCache.Initialize(m_argumentsFieldInfo.FieldType);
			}
		}

		public void ReadFrom(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			object value = m_argumentsFieldInfo.GetValue(obj);
			if (value != null)
			{
				ArgumentsCache = new ArgumentsCache();
				ArgumentsCache.ReadFrom(value);
			}
			else
			{
				ArgumentsCache = null;
			}
			m_target = m_targetFieldInfo.GetValue<UnityEngine.Object>(obj);
			CallState = m_callStateFieldInfo.GetValue<UnityEventCallState>(obj);
			m_methodName = m_methodNameFieldInfo.GetValue<string>(obj);
			Mode = m_modeFieldInfo.GetValue<PersistentListenerMode>(obj);
		}

		public void WriteTo(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			object obj2 = null;
			if (ArgumentsCache != null)
			{
				obj2 = Activator.CreateInstance(m_argumentsFieldInfo.FieldType);
				ArgumentsCache.WriteTo(obj2);
			}
			m_argumentsFieldInfo.SetValue(obj, obj2);
			m_callStateFieldInfo.SetValue(obj, CallState);
			m_methodNameFieldInfo.SetValue(obj, MethodName);
			m_modeFieldInfo.SetValue(obj, Mode);
			m_targetFieldInfo.SetValue(obj, Target);
		}
	}
}
