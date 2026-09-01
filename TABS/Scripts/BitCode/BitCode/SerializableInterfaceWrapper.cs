using System;
using UnityEngine;

namespace BitCode
{
	[Serializable]
	public abstract class SerializableInterfaceWrapper
	{
		internal const string ObjectReferenceFieldName = "unityObjectReference";

		[HideInInspector]
		[SerializeField]
		protected UnityEngine.Object unityObjectReference;

		internal SerializableInterfaceWrapper()
		{
		}
	}
	[Serializable]
	public abstract class SerializableInterfaceWrapper<T> : SerializableInterfaceWrapper, ISerializationCallbackReceiver
	{
		private T cachedInterfaceReference;

		public T Value => cachedInterfaceReference;

		public bool HasValue => cachedInterfaceReference != null;

		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
			if (unityObjectReference == null)
			{
				goto IL_000e;
			}
			goto IL_004c;
			IL_000e:
			int num = 1506132760;
			goto IL_0013;
			IL_0013:
			T val = default(T);
			UnityEngine.Object obj = default(UnityEngine.Object);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x7A051726)) % 9)
				{
				case 0u:
					break;
				default:
					return;
				case 7u:
					goto IL_004c;
				case 8u:
					return;
				case 2u:
					cachedInterfaceReference = val;
					num = (int)(num2 * 252792393) ^ -714460149;
					continue;
				case 3u:
					cachedInterfaceReference = default(T);
					num = (int)(num2 * 520581757) ^ -183375978;
					continue;
				case 6u:
					val = (T)(object)((obj is T) ? obj : null);
					num = (int)(num2 * 696818825) ^ -1938925314;
					continue;
				case 5u:
					return;
				case 4u:
					Debug.LogError("Serializable interface of type " + typeof(T).Name + " had a reference to object of " + $"unexpected type {unityObjectReference.GetType()}");
					num = 1554469963;
					continue;
				case 1u:
					return;
				}
				break;
			}
			goto IL_000e;
			IL_004c:
			obj = unityObjectReference;
			int num3;
			if (obj is T)
			{
				num = 1152167540;
				num3 = num;
			}
			else
			{
				num = 539103293;
				num3 = num;
			}
			goto IL_0013;
		}
	}
}
