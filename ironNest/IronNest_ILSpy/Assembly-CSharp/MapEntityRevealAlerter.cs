using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class MapEntityRevealAlerter : MonoBehaviour
{
	private sealed class _003CInternal_AlertEntities_003Ed__4 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MapEntityRevealAlerter _003C_003E4__this;

		public EntityLocation[] entities;

		private float _003CfinishTime_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CInternal_AlertEntities_003Ed__4(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_0090: Expected I4, but got I8
			//IL_01d6: Expected I4, but got O
			//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ce: Expected O, but got Unknown
			//IL_00d7: Expected O, but got I4
			//IL_00e0: Expected O, but got I4
			//IL_0114: Unknown result type (might be due to invalid IL or missing references)
			//IL_0119: Expected O, but got Unknown
			//IL_0122: Unknown result type (might be due to invalid IL or missing references)
			//IL_0127: Expected O, but got Unknown
			MapEntityRevealAlerter mapEntityRevealAlerter = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				float time = Time.time;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_01c8;
				}
				float num = time + mapEntityRevealAlerter.AlertForSeconds;
				_003CfinishTime_003E5__2 = num;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_018e;
				}
				_003C_003E1__state = -1;
			}
			float time2 = Time.time;
			if (!(_003CfinishTime_003E5__2 > time2))
			{
				goto IL_018e;
			}
			EntityLocation[] array = entities;
			if (entities != null)
			{
				object obj = entities + 32;
				object obj2 = 0;
				object obj3 = 0;
				while (true)
				{
					if ((nint)obj3 < array.Length)
					{
						if (obj == null)
						{
							break;
						}
						((EntityLocation)obj).StartScanWindow();
						obj2++;
						obj += 8;
						obj3 = obj2;
						continue;
					}
					if ((object)_003C_003E4__this == null)
					{
						break;
					}
					WaitForSeconds waitForSeconds = new WaitForSeconds(mapEntityRevealAlerter.AltertInterval);
					_003C_003E2__current = waitForSeconds;
					_003C_003E1__state = 1;
					return true;
				}
			}
			goto IL_01c8;
			IL_018e:
			return false;
			IL_01c8:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	public float AlertForSeconds = 9f;

	public float AltertInterval = 1f;

	private void OnEnable()
	{
		EntityLocation[] entities = UnityEngine.Object.FindObjectsByType<EntityLocation>(FindObjectsSortMode.None);
		_003CInternal_AlertEntities_003Ed__4 obj = new _003CInternal_AlertEntities_003Ed__4(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.entities = entities;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}

	public IEnumerator Internal_AlertEntities(EntityLocation[] entities)
	{
		_003CInternal_AlertEntities_003Ed__4 obj = new _003CInternal_AlertEntities_003Ed__4(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.entities = entities;
		return obj;
	}
}
