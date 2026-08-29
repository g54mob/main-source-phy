using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Events;

namespace Battlehub.Utils
{
	public static class UnityEventBaseExtensions
	{
		private static readonly FieldInfo m_persistentCallGroupInfo;

		private static readonly FieldInfo m_callsInfo;

		private static readonly Type m_callType;

		static UnityEventBaseExtensions()
		{
			m_persistentCallGroupInfo = typeof(UnityEventBase).GetField("m_PersistentCalls", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic);
			if (m_persistentCallGroupInfo == null)
			{
				throw new NotSupportedException("m_PersistentCalls FieldInfo not found.");
			}
			m_callsInfo = m_persistentCallGroupInfo.FieldType.GetField("m_Calls", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (m_callsInfo == null)
			{
				throw new NotSupportedException("m_Calls FieldInfo not found. ");
			}
			Type fieldType = m_callsInfo.FieldType;
			if (!fieldType.IsGenericType() || fieldType.GetGenericTypeDefinition() != typeof(List<>))
			{
				throw new NotSupportedException("m_callsInfo.FieldType is not a generic List<>");
			}
			m_callType = fieldType.GetGenericArguments()[0];
			PersistentCall.Initialize(m_callType);
		}

		public static void AddPersistentCall(this UnityEventBase obj, PersistentCall call)
		{
			PersistentCall[] array = obj.GetPersistentCalls();
			Array.Resize(ref array, array.Length + 1);
			array[^1] = call;
			obj.SetPersistentCalls(array);
		}

		public static void RemovePersistentCall(this UnityEventBase obj, int index)
		{
			PersistentCall[] array = obj.GetPersistentCalls();
			for (int i = index; i < array.Length - 1; i++)
			{
				array[i] = array[i + 1];
			}
			Array.Resize(ref array, array.Length - 1);
			obj.SetPersistentCalls(array);
		}

		public static PersistentCall[] GetPersistentCalls(this UnityEventBase obj)
		{
			object value = m_persistentCallGroupInfo.GetValue(obj);
			if (value == null)
			{
				return new PersistentCall[0];
			}
			object value2 = m_callsInfo.GetValue(value);
			if (value2 == null)
			{
				return new PersistentCall[0];
			}
			IList list = (IList)value2;
			PersistentCall[] array = new PersistentCall[list.Count];
			for (int i = 0; i < array.Length; i++)
			{
				object obj2 = list[i];
				if (obj2 != null)
				{
					PersistentCall persistentCall = new PersistentCall();
					persistentCall.ReadFrom(obj2);
					array[i] = persistentCall;
				}
			}
			return array;
		}

		public static void SetPersistentCalls(this UnityEventBase obj, PersistentCall[] persistentCalls)
		{
			object obj2 = Activator.CreateInstance(m_persistentCallGroupInfo.FieldType);
			IList list = (IList)Activator.CreateInstance(m_callsInfo.FieldType);
			for (int i = 0; i < persistentCalls.Length; i++)
			{
				object obj3 = null;
				PersistentCall persistentCall = persistentCalls[i];
				if (persistentCall != null)
				{
					obj3 = Activator.CreateInstance(m_callType);
					persistentCall.WriteTo(obj3);
				}
				list.Add(obj3);
			}
			m_callsInfo.SetValue(obj2, list);
			m_persistentCallGroupInfo.SetValue(obj, obj2);
		}
	}
}
