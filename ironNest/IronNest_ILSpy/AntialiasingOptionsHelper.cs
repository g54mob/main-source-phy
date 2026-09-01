using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using Kamgam.SettingsGenerator;
using UnityEngine;

public class AntialiasingOptionsHelper : MonoBehaviour
{
	private sealed class _003CDelayedSet_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AntialiasingOptionsHelper _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CDelayedSet_003Ed__7(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0394: Expected I4, but got I8
			//IL_0015: Expected O, but got I4
			//IL_0115: Expected I4, but got I8
			//IL_03cc: Expected I4, but got O
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_0101: Expected I4, but got I8
			//IL_006e: Expected I4, but got I8
			AntialiasingOptionsHelper antialiasingOptionsHelper = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (flag)
					{
						_003C_003E1__state = -1;
						goto IL_015a;
					}
					if ((nint)obj2 != 1)
					{
						goto IL_03f5;
					}
					_003C_003E1__state = -1;
					if ((object)_003C_003E4__this != null && (object)antialiasingOptionsHelper.aaResolver != null)
					{
						antialiasingOptionsHelper.aaResolver.Refresh();
						if ((object)antialiasingOptionsHelper.upscalingResolver != null)
						{
							antialiasingOptionsHelper.upscalingResolver.Refresh();
							goto IL_03f5;
						}
					}
				}
				else
				{
					_003C_003E1__state = -1;
					if ((object)_003C_003E4__this != null)
					{
						if (antialiasingOptionsHelper._settingsProvider != null)
						{
							goto IL_015a;
						}
						_003C_003E2__current = null;
						_003C_003E1__state = 2;
						goto IL_041c;
					}
				}
				goto IL_03be;
			}
			_003C_003E1__state = -1;
			WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
			_003C_003E2__current = waitForEndOfFrame;
			_003C_003E1__state = 1;
			return true;
			IL_0403:
			WaitForEndOfFrame waitForEndOfFrame2 = new WaitForEndOfFrame();
			_003C_003E2__current = waitForEndOfFrame2;
			_003C_003E1__state = 3;
			goto IL_041c;
			IL_015a:
			if ((object)_003C_003E4__this != null && (object)antialiasingOptionsHelper._settingsProvider != null)
			{
				Settings settings = antialiasingOptionsHelper._settingsProvider.Settings;
				if ((object)settings != null)
				{
					SettingOption option = settings.GetOption(antialiasingOptionsHelper.upscalingID);
					if ((object)antialiasingOptionsHelper._settingsProvider != null)
					{
						Settings settings2 = antialiasingOptionsHelper._settingsProvider.Settings;
						if ((object)settings2 != null)
						{
							SettingOption option2 = settings2.GetOption(antialiasingOptionsHelper.aaId);
							if ((object)antialiasingOptionsHelper._settingsProvider != null)
							{
								Settings settings3 = antialiasingOptionsHelper._settingsProvider.Settings;
								if ((object)settings3 != null)
								{
									SettingFloat settingFloat = settings3.GetFloat(antialiasingOptionsHelper.renderScaleId);
									if (option != null)
									{
										int value = option.GetValue();
										if (value == 6)
										{
											goto IL_0403;
										}
										if (option2 != null)
										{
											option2.SetValue(0);
											if (settingFloat != null)
											{
												settingFloat.SetValue(1f);
												goto IL_0403;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			goto IL_03be;
			IL_03f5:
			return false;
			IL_041c:
			return true;
			IL_03be:
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

	private SettingsProvider _settingsProvider;

	private string aaId;

	private string renderScaleId;

	private string upscalingID;

	private OptionsButtonUGUIResolver aaResolver;

	private SliderUGUIResolver upscalingResolver;

	public void UpdateAntialiasing()
	{
		_003CDelayedSet_003Ed__7 obj = new _003CDelayedSet_003Ed__7(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private IEnumerator DelayedSet()
	{
		_003CDelayedSet_003Ed__7 obj = new _003CDelayedSet_003Ed__7(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}
}
