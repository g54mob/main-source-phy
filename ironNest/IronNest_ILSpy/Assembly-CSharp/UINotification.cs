using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINotification : MonoBehaviour
{
	private sealed class _003CLifetimeRoutine_003Ed__14 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UINotification _003C_003E4__this;

		public float lifetime;

		private float _003CfadeTime_003E5__2;

		private float _003Ctime_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CLifetimeRoutine_003Ed__14(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_03c6: Expected I4, but got I8
			//IL_0423: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_0236: Expected I4, but got I8
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_045b: Expected I4, but got I8
			//IL_0302: Invalid comparison between F4 and I4
			//IL_0512: Invalid comparison between I4 and F4
			//IL_0524: Expected F4, but got I4
			//IL_013f: Invalid comparison between I4 and F4
			//IL_018a: Expected F4, but got I4
			UINotification uINotification = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (!flag)
					{
						if ((nint)obj2 != 1)
						{
							goto IL_005c;
						}
					}
					else
					{
						_003Ctime_003E5__3 = 0f;
					}
					_003C_003E1__state = -1;
					if (_003CfadeTime_003E5__2 > _003Ctime_003E5__3)
					{
						if ((object)_003C_003E4__this != null)
						{
							float num = ((!uINotification.useUnscaledTime) ? Time.deltaTime : Time.unscaledDeltaTime);
							float num2 = num + _003Ctime_003E5__3;
							_003Ctime_003E5__3 = num2;
							if (uINotification.canvasGroup != null)
							{
								float num3 = _003Ctime_003E5__3 / _003CfadeTime_003E5__2;
								if (!(0f > num3))
								{
									if (num3 > 1f)
									{
										num3 = 1f;
									}
								}
								else
								{
									num3 = 0f;
								}
								if ((object)uINotification.canvasGroup == null)
								{
									goto IL_0415;
								}
								float num4 = num3 * -1f;
								float alpha = num4 + 1f;
								uINotification.canvasGroup.alpha = alpha;
							}
							_003C_003E2__current = null;
							_003C_003E1__state = 3;
							return true;
						}
					}
					else if ((object)_003C_003E4__this != null)
					{
						GameObject gameObject = _003C_003E4__this.gameObject;
						UnityEngine.Object.Destroy(gameObject);
						goto IL_005c;
					}
				}
				else
				{
					_003C_003E1__state = -1;
					if ((object)_003C_003E4__this != null)
					{
						Transform transform = _003C_003E4__this.transform;
						bool flag2 = (object)transform == null;
						UnityEngine.Object obj3 = null;
						if (!flag2)
						{
							bool flag3 = (object)transform.GetType() != typeof(RectTransform);
							obj3 = null;
							if (!flag3)
							{
								obj3 = transform;
							}
						}
						if (obj3 != null)
						{
							LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)obj3);
						}
						if (uINotification.disableLayoutAfterBuild)
						{
							_003C_003E4__this.DisableLayout();
						}
						float num5 = ((!(lifetime > 0f)) ? uINotification.defaultLifetime : lifetime);
						bool flag4 = !(0.01f < uINotification.defaultFadeTime);
						float num6 = 0.01f;
						if (!flag4)
						{
							num6 = uINotification.defaultFadeTime;
						}
						_003CfadeTime_003E5__2 = num6;
						bool flag5 = !(0f < num5);
						float num7 = 0f;
						if (!flag5)
						{
							num7 = num5;
						}
						WaitForSeconds waitForSeconds;
						if (uINotification.useUnscaledTime)
						{
							WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(num7);
							waitForSeconds = (WaitForSeconds)(object)waitForSecondsRealtime;
						}
						else
						{
							WaitForSeconds waitForSeconds2 = new WaitForSeconds(num7);
							waitForSeconds = waitForSeconds2;
						}
						_003C_003E2__current = waitForSeconds;
						_003C_003E1__state = 2;
						return true;
					}
				}
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					_003C_003E4__this.EnableLayout();
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
			}
			goto IL_0415;
			IL_0415:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_005c:
			return false;
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

	public TMP_Text titleText;

	public TMP_Text descriptionText;

	public Image borderImage;

	public CanvasGroup canvasGroup;

	public float defaultLifetime = 4f;

	public float defaultFadeTime = 0.5f;

	public bool useUnscaledTime = true;

	public bool disableLayoutAfterBuild;

	private LayoutGroup[] layoutGroups;

	private ContentSizeFitter[] contentSizeFitters;

	private Coroutine routine;

	private void Awake()
	{
		CacheReferences();
	}

	private void OnDisable()
	{
		if (routine != null)
		{
			StopCoroutine(routine);
			routine = null;
		}
	}

	public void Show(string title, string description, float lifetime, Color? borderColor)
	{
		//IL_0066: Expected O, but got I
		//IL_0076: Expected O, but got I
		//IL_00f8: Expected O, but got I
		//IL_0108: Expected O, but got I
		CacheReferences();
		if (titleText != null)
		{
			bool flag = title != null;
			string text = title;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v125 @ rax_v35+B8]");
				object obj2 = 0;
				text = (string)obj2;
			}
			titleText.text = text;
		}
		if (descriptionText != null)
		{
			bool flag2 = description != null;
			string text2 = description;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v307 @ rax_v28+B8]");
				object obj4 = 0;
				text2 = (string)obj4;
			}
			descriptionText.text = text2;
		}
		if (canvasGroup != null)
		{
			canvasGroup.alpha = 1f;
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
		}
		if (routine != null)
		{
			StopCoroutine(routine);
		}
		_003CLifetimeRoutine_003Ed__14 obj5 = new _003CLifetimeRoutine_003Ed__14(0);
		obj5._003C_003E1__state = 0;
		obj5._003C_003E4__this = this;
		obj5.lifetime = lifetime;
		Coroutine coroutine = StartCoroutine(obj5);
		routine = coroutine;
	}

	private IEnumerator LifetimeRoutine(float lifetime)
	{
		_003CLifetimeRoutine_003Ed__14 obj = new _003CLifetimeRoutine_003Ed__14(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.lifetime = lifetime;
		return obj;
	}

	private void CacheReferences()
	{
		if (layoutGroups == null)
		{
			LayoutGroup[] componentsInChildren = GetComponentsInChildren<LayoutGroup>(includeInactive: true);
			layoutGroups = componentsInChildren;
		}
		if (contentSizeFitters == null)
		{
			ContentSizeFitter[] componentsInChildren2 = GetComponentsInChildren<ContentSizeFitter>(includeInactive: true);
			contentSizeFitters = componentsInChildren2;
		}
	}

	private void EnableLayout()
	{
		//IL_0018: Expected O, but got I4
		//IL_0021: Expected O, but got I4
		//IL_002a: Expected O, but got I4
		//IL_00fb: Expected O, but got I4
		//IL_0104: Expected O, but got I4
		//IL_010d: Expected O, but got I4
		//IL_0055: Expected O, but got I
		//IL_0138: Expected O, but got I
		//IL_008c: Expected O, but got I
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_016f: Expected O, but got I
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Expected O, but got Unknown
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Expected O, but got Unknown
		if (layoutGroups != null)
		{
			LayoutGroup[] array = layoutGroups;
			object obj = 32;
			object obj2 = 0;
			object obj3 = 0;
			while ((nint)obj3 < array.Length)
			{
				LayoutGroup[] array2 = layoutGroups;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ r15_v6+v172 @ rax_v20 (UnityEngine.UI.LayoutGroup[])]");
				if ((UnityEngine.Object)0 != null)
				{
					LayoutGroup[] array3 = layoutGroups;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ r15_v6+v276 @ rax_v25 (UnityEngine.UI.LayoutGroup[])]");
					((Behaviour)0).enabled = true;
				}
				array = layoutGroups;
				obj2++;
				obj += 8;
				obj3 = obj2;
			}
		}
		if (contentSizeFitters == null)
		{
			return;
		}
		ContentSizeFitter[] array4 = contentSizeFitters;
		object obj4 = 32;
		object obj5 = 0;
		object obj6 = 0;
		bool flag;
		do
		{
			if ((nint)obj6 < array4.Length)
			{
				ContentSizeFitter[] array5 = contentSizeFitters;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ r14_v6+v277 @ rax_v10 (UnityEngine.UI.ContentSizeFitter[])]");
				if ((UnityEngine.Object)0 != null)
				{
					ContentSizeFitter[] array6 = contentSizeFitters;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ r14_v6+v278 @ rax_v15 (UnityEngine.UI.ContentSizeFitter[])]");
					((Behaviour)0).enabled = true;
				}
				array4 = contentSizeFitters;
				obj5++;
				obj4 += 8;
				flag = contentSizeFitters != null;
				obj6 = obj5;
				continue;
			}
			return;
		}
		while (flag);
		throw new NullReferenceException();
	}

	private void DisableLayout()
	{
		//IL_0018: Expected O, but got I4
		//IL_0021: Expected O, but got I4
		//IL_002a: Expected O, but got I4
		//IL_00fb: Expected O, but got I4
		//IL_0104: Expected O, but got I4
		//IL_010d: Expected O, but got I4
		//IL_0055: Expected O, but got I
		//IL_0138: Expected O, but got I
		//IL_008c: Expected O, but got I
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_016f: Expected O, but got I
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Expected O, but got Unknown
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Expected O, but got Unknown
		if (layoutGroups != null)
		{
			LayoutGroup[] array = layoutGroups;
			object obj = 32;
			object obj2 = 0;
			object obj3 = 0;
			while ((nint)obj3 < array.Length)
			{
				LayoutGroup[] array2 = layoutGroups;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ r15_v6+v172 @ rax_v20 (UnityEngine.UI.LayoutGroup[])]");
				if ((UnityEngine.Object)0 != null)
				{
					LayoutGroup[] array3 = layoutGroups;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ r15_v6+v276 @ rax_v25 (UnityEngine.UI.LayoutGroup[])]");
					((Behaviour)0).enabled = false;
				}
				array = layoutGroups;
				obj2++;
				obj += 8;
				obj3 = obj2;
			}
		}
		if (contentSizeFitters == null)
		{
			return;
		}
		ContentSizeFitter[] array4 = contentSizeFitters;
		object obj4 = 32;
		object obj5 = 0;
		object obj6 = 0;
		bool flag;
		do
		{
			if ((nint)obj6 < array4.Length)
			{
				ContentSizeFitter[] array5 = contentSizeFitters;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ r14_v6+v277 @ rax_v10 (UnityEngine.UI.ContentSizeFitter[])]");
				if ((UnityEngine.Object)0 != null)
				{
					ContentSizeFitter[] array6 = contentSizeFitters;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ r14_v6+v278 @ rax_v15 (UnityEngine.UI.ContentSizeFitter[])]");
					((Behaviour)0).enabled = false;
				}
				array4 = contentSizeFitters;
				obj5++;
				obj4 += 8;
				flag = contentSizeFitters != null;
				obj6 = obj5;
				continue;
			}
			return;
		}
		while (flag);
		throw new NullReferenceException();
	}

	private void RebuildLayout()
	{
		Transform transform = base.transform;
		bool flag = (object)transform == null;
		UnityEngine.Object obj = null;
		if (!flag)
		{
			bool flag2 = (object)transform.GetType() != typeof(RectTransform);
			obj = null;
			if (!flag2)
			{
				obj = transform;
			}
		}
		if (obj != null)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)obj);
		}
	}

	private float DeltaTime()
	{
		if (useUnscaledTime)
		{
			return Time.unscaledDeltaTime;
		}
		return Time.deltaTime;
	}

	private object Wait(float seconds)
	{
		//IL_004b: Invalid comparison between I4 and F4
		//IL_005d: Expected F4, but got I4
		bool flag = !(0f < seconds);
		float num = 0f;
		if (!flag)
		{
			num = seconds;
		}
		if (useUnscaledTime)
		{
			return new WaitForSecondsRealtime(num);
		}
		return new WaitForSeconds(num);
	}
}
