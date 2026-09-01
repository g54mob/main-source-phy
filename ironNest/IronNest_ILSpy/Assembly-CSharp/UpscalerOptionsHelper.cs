using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using Kamgam.SettingsGenerator;
using UnityEngine;

public class UpscalerOptionsHelper : MonoBehaviour
{
	private sealed class _003CDelayedSet_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UpscalerOptionsHelper _003C_003E4__this;

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
			//IL_03a8: Expected I4, but got I8
			//IL_0015: Expected O, but got I4
			//IL_00e5: Expected I4, but got I8
			//IL_03e0: Expected I4, but got O
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_00d1: Expected I4, but got I8
			//IL_006e: Expected I4, but got I8
			//IL_0316: Unknown result type (might be due to invalid IL or missing references)
			//IL_031b: Expected O, but got Unknown
			//IL_0324: Invalid comparison between O and F4
			UpscalerOptionsHelper upscalerOptionsHelper = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (!flag)
					{
						if ((nint)obj2 == 1)
						{
							_003C_003E1__state = -1;
							if ((object)_003C_003E4__this == null || (object)upscalerOptionsHelper.upscalingResolver == null)
							{
								goto IL_03d2;
							}
							upscalerOptionsHelper.upscalingResolver.Refresh();
						}
						return false;
					}
					_003C_003E1__state = -1;
				}
				else
				{
					_003C_003E1__state = -1;
					if ((object)_003C_003E4__this == null)
					{
						goto IL_03d2;
					}
					if (!(upscalerOptionsHelper._settingsProvider != null))
					{
						_003C_003E2__current = null;
						_003C_003E1__state = 2;
						goto IL_0430;
					}
				}
				if ((object)_003C_003E4__this != null && (object)upscalerOptionsHelper._settingsProvider != null)
				{
					Settings settings = upscalerOptionsHelper._settingsProvider.Settings;
					if ((object)settings != null)
					{
						SettingOption option = settings.GetOption(upscalerOptionsHelper.upscalingId);
						if ((object)upscalerOptionsHelper._settingsProvider != null)
						{
							Settings settings2 = upscalerOptionsHelper._settingsProvider.Settings;
							if ((object)settings2 != null)
							{
								SettingOption option2 = settings2.GetOption(upscalerOptionsHelper.aaID);
								if ((object)upscalerOptionsHelper._settingsProvider != null)
								{
									Settings settings3 = upscalerOptionsHelper._settingsProvider.Settings;
									if ((object)settings3 != null)
									{
										SettingFloat settingFloat = settings3.GetFloat(upscalerOptionsHelper.renderScaleId);
										if (option2 != null)
										{
											int value = option2.GetValue();
											if (value <= 0)
											{
												if (settingFloat == null)
												{
													goto IL_03d2;
												}
												float value2 = settingFloat.GetValue();
												object obj3 = default(object);
												float num = (float)obj3 - 1f;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
												object obj4 = num & 0;
												if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.01f))
												{
													goto IL_0417;
												}
											}
											if (option != null)
											{
												option.SetValue(6);
												goto IL_0417;
											}
										}
									}
								}
							}
						}
					}
				}
				goto IL_03d2;
			}
			_003C_003E1__state = -1;
			WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
			_003C_003E2__current = waitForEndOfFrame;
			_003C_003E1__state = 1;
			return true;
			IL_0417:
			WaitForEndOfFrame waitForEndOfFrame2 = new WaitForEndOfFrame();
			_003C_003E2__current = waitForEndOfFrame2;
			_003C_003E1__state = 3;
			goto IL_0430;
			IL_03d2:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0430:
			return true;
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

	private sealed class _003COnRenderScaleChanged_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float scale;

		public UpscalerOptionsHelper _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003COnRenderScaleChanged_003Ed__8(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0300: Expected I4, but got I8
			//IL_0015: Expected O, but got I4
			//IL_019d: Expected I4, but got I8
			//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c9: Expected O, but got Unknown
			//IL_01d2: Invalid comparison between O and F4
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_00d1: Expected I4, but got I8
			//IL_0338: Expected I4, but got O
			//IL_006e: Expected I4, but got I8
			UpscalerOptionsHelper upscalerOptionsHelper = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (!flag)
					{
						if ((nint)obj2 == 1)
						{
							_003C_003E1__state = -1;
							if ((object)_003C_003E4__this == null || (object)upscalerOptionsHelper.upscalingResolver == null)
							{
								goto IL_032a;
							}
							upscalerOptionsHelper.upscalingResolver.Refresh();
						}
						return false;
					}
					_003C_003E1__state = -1;
					if ((object)_003C_003E4__this != null && (object)upscalerOptionsHelper._settingsProvider != null)
					{
						Settings settings = upscalerOptionsHelper._settingsProvider.Settings;
						if ((object)settings != null)
						{
							SettingFloat settingFloat = settings.GetFloat(upscalerOptionsHelper.renderScaleId);
							if (settingFloat != null)
							{
								settingFloat.SetValue(scale);
								goto IL_01e6;
							}
						}
					}
				}
				else
				{
					_003C_003E1__state = -1;
					float num = scale - 1f;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
					object obj3 = num & 0;
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.01f))
					{
						goto IL_01e6;
					}
					if ((object)_003C_003E4__this != null && (object)upscalerOptionsHelper._settingsProvider != null)
					{
						Settings settings2 = upscalerOptionsHelper._settingsProvider.Settings;
						if ((object)settings2 != null)
						{
							SettingOption option = settings2.GetOption(upscalerOptionsHelper.upscalingId);
							if (option != null)
							{
								option.SetValue(6);
								WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
								_003C_003E2__current = waitForEndOfFrame;
								_003C_003E1__state = 2;
								return true;
							}
						}
					}
				}
				goto IL_032a;
			}
			_003C_003E1__state = -1;
			WaitForEndOfFrame waitForEndOfFrame2 = new WaitForEndOfFrame();
			_003C_003E2__current = waitForEndOfFrame2;
			_003C_003E1__state = 1;
			return true;
			IL_01e6:
			WaitForEndOfFrame waitForEndOfFrame3 = new WaitForEndOfFrame();
			_003C_003E2__current = waitForEndOfFrame3;
			_003C_003E1__state = 3;
			return true;
			IL_032a:
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

	private string upscalingId;

	private string aaID;

	private string renderScaleId;

	private OptionsButtonUGUIResolver upscalingResolver;

	public void UpdateUpscaling()
	{
		_003CDelayedSet_003Ed__7 obj = new _003CDelayedSet_003Ed__7(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
	}

	public void RenderScaleChanged(float newScale)
	{
		_003COnRenderScaleChanged_003Ed__8 obj = new _003COnRenderScaleChanged_003Ed__8(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.scale = newScale;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private IEnumerator DelayedSet()
	{
		_003CDelayedSet_003Ed__7 obj = new _003CDelayedSet_003Ed__7(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator OnRenderScaleChanged(float scale)
	{
		_003COnRenderScaleChanged_003Ed__8 obj = new _003COnRenderScaleChanged_003Ed__8(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.scale = scale;
		return obj;
	}
}
