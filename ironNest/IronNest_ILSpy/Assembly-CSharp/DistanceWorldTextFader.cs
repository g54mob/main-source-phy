using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DistanceWorldTextFader : MonoBehaviour
{
	[Serializable]
	public class TextToRendererPair
	{
		private TMP_Text _text;

		private Renderer _renderer;

		public TMP_Text Text => _text;

		public Renderer Renderer => _renderer;
	}

	private sealed class _003CFadeInCoroutine_003Ed__13 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DistanceWorldTextFader _003C_003E4__this;

		private float _003Cspeed_003E5__2;

		private float _003Calpha_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CFadeInCoroutine_003Ed__13(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0045: Expected I4, but got I8
			//IL_0281: Expected I4, but got I8
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Expected O, but got Unknown
			//IL_00f8: Invalid comparison between F4 and I4
			//IL_0318: Unknown result type (might be due to invalid IL or missing references)
			//IL_031d: Expected O, but got Unknown
			//IL_0326: Expected O, but got I4
			//IL_032f: Expected O, but got I4
			//IL_03f1: Expected I4, but got O
			//IL_012c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0131: Expected O, but got Unknown
			//IL_013a: Expected O, but got I4
			//IL_0143: Expected O, but got I4
			//IL_00c1: Expected O, but got I
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Expected O, but got Unknown
			//IL_037d: Expected O, but got I
			//IL_0386: Unknown result type (might be due to invalid IL or missing references)
			//IL_038b: Expected O, but got Unknown
			//IL_0394: Unknown result type (might be due to invalid IL or missing references)
			//IL_0399: Expected O, but got Unknown
			//IL_0194: Expected O, but got I
			//IL_019d: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a2: Expected O, but got Unknown
			//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b0: Expected O, but got Unknown
			int num = _003C_003E1__state;
			DistanceWorldTextFader distanceWorldTextFader = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				TextToRendererPair[] texts = distanceWorldTextFader._texts;
				object obj = distanceWorldTextFader._texts + 32;
				int num2 = 0;
				while (num < texts.Length)
				{
					if (num2 < texts.Length)
					{
						object obj2 = obj;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v227 @ rax_v24+18]");
						((Renderer)0).enabled = true;
						num2++;
						obj += 8;
						num = num2;
						continue;
					}
					goto IL_03e3;
				}
				if (!(distanceWorldTextFader._fadeDuration > 0f))
				{
					TextToRendererPair[] texts2 = distanceWorldTextFader._texts;
					object obj3 = distanceWorldTextFader._texts + 32;
					object obj4 = 0;
					object obj5 = 0;
					while ((nint)obj5 < texts2.Length)
					{
						if ((nint)obj4 < texts2.Length)
						{
							object obj6 = obj3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rdx_v16+10]");
							distanceWorldTextFader.SetTextAlpha((TMP_Text)0, distanceWorldTextFader._visibleAlpha);
							obj4++;
							obj3 += 8;
							obj5 = obj4;
							continue;
						}
						goto IL_03e3;
					}
					goto IL_03c6;
				}
				float num3 = distanceWorldTextFader._visibleAlpha / distanceWorldTextFader._fadeDuration;
				_003Cspeed_003E5__2 = num3;
				TextToRendererPair[] texts3 = distanceWorldTextFader._texts;
				if (texts3.Length <= 0)
				{
					goto IL_03e3;
				}
				TextToRendererPair textToRendererPair = texts3[0];
				float currentAlpha = distanceWorldTextFader.GetCurrentAlpha(textToRendererPair._text);
				_003Calpha_003E5__3 = currentAlpha;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_03d5;
				}
				_003C_003E1__state = -1;
			}
			if (!(distanceWorldTextFader._visibleAlpha > _003Calpha_003E5__3))
			{
				goto IL_03c6;
			}
			float deltaTime = Time.deltaTime;
			float num4 = _003Cspeed_003E5__2 * deltaTime;
			float num5 = num4 + _003Calpha_003E5__3;
			if (!(distanceWorldTextFader._visibleAlpha > num5))
			{
				num5 = distanceWorldTextFader._visibleAlpha;
			}
			_003Calpha_003E5__3 = num5;
			TextToRendererPair[] texts4 = distanceWorldTextFader._texts;
			object obj7 = distanceWorldTextFader._texts + 32;
			object obj8 = 0;
			object obj9 = 0;
			while (true)
			{
				if ((nint)obj9 < texts4.Length)
				{
					if ((nint)obj8 >= texts4.Length)
					{
						break;
					}
					object obj10 = obj7;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v92 @ rdx_v10+10]");
					distanceWorldTextFader.SetTextAlpha((TMP_Text)0, _003Calpha_003E5__3);
					obj8++;
					obj7 += 8;
					obj9 = obj8;
					continue;
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			goto IL_03e3;
			IL_03c6:
			distanceWorldTextFader._fadeCoroutine = null;
			goto IL_03d5;
			IL_03d5:
			return false;
			IL_03e3:
			IndexOutOfRangeException ex = new IndexOutOfRangeException();
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

	private sealed class _003CFadeOutCoroutine_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DistanceWorldTextFader _003C_003E4__this;

		private float _003Cspeed_003E5__2;

		private float _003Calpha_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CFadeOutCoroutine_003Ed__12(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0045: Expected I4, but got I8
			//IL_0340: Expected I4, but got I8
			//IL_0058: Invalid comparison between F4 and I4
			//IL_0595: Invalid comparison between F4 and I4
			//IL_0489: Unknown result type (might be due to invalid IL or missing references)
			//IL_048e: Expected O, but got Unknown
			//IL_0497: Expected O, but got I4
			//IL_04a0: Expected O, but got I4
			//IL_0379: Invalid comparison between F4 and I4
			//IL_053d: Expected I4, but got O
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Expected O, but got Unknown
			//IL_0396: Expected F4, but got I4
			//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c0: Expected O, but got Unknown
			//IL_03c9: Expected O, but got I4
			//IL_03d2: Expected O, but got I4
			//IL_04e9: Expected O, but got I
			//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f7: Expected O, but got Unknown
			//IL_0500: Unknown result type (might be due to invalid IL or missing references)
			//IL_0505: Expected O, but got Unknown
			//IL_00e4: Expected O, but got I
			//IL_0420: Expected O, but got I
			//IL_0429: Unknown result type (might be due to invalid IL or missing references)
			//IL_042e: Expected O, but got Unknown
			//IL_0437: Unknown result type (might be due to invalid IL or missing references)
			//IL_043c: Expected O, but got Unknown
			//IL_0554: Unknown result type (might be due to invalid IL or missing references)
			//IL_0559: Expected O, but got Unknown
			//IL_0194: Expected O, but got I
			//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a9: Expected O, but got Unknown
			//IL_01c6: Expected O, but got I
			//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fe: Expected O, but got Unknown
			//IL_0207: Expected O, but got I4
			//IL_0276: Expected O, but got I
			//IL_0239: Unknown result type (might be due to invalid IL or missing references)
			//IL_023e: Expected O, but got Unknown
			//IL_0254: Unknown result type (might be due to invalid IL or missing references)
			//IL_0259: Expected O, but got Unknown
			int num = _003C_003E1__state;
			DistanceWorldTextFader distanceWorldTextFader = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if (!(distanceWorldTextFader._fadeDuration > 0f))
				{
					TextToRendererPair[] texts = distanceWorldTextFader._texts;
					object obj = distanceWorldTextFader._texts + 32;
					for (int i = 0; num < texts.Length; i++, obj += 8, num = i)
					{
						if (i < texts.Length)
						{
							object obj2 = obj;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v324 @ rax_v25+10]");
							TMP_TextInfo textInfo = ((TMP_Text)0).textInfo;
							if (textInfo.characterCount == 0)
							{
								continue;
							}
							TMP_CharacterInfo[] characterInfo = textInfo.characterInfo;
							if (characterInfo.Length > 0)
							{
								TMP_MeshInfo[] meshInfo = textInfo.meshInfo;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ rcx_v18 (TMPro.TMP_CharacterInfo[])+50]");
								if ((nint)0 < (nint)meshInfo.Length)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ rcx_v18 (TMPro.TMP_CharacterInfo[])+50]");
									object obj3 = (nint)0 * (nint)4;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ rcx_v18 (TMPro.TMP_CharacterInfo[])+50]");
									object obj4 = 0 + obj3;
									object obj5 = obj4 + obj4;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ rdx_v19 (TMPro.TMP_MeshInfo[])+58+v99 @ rcx_v20*8]");
									TMP_MeshInfo tMP_MeshInfo = (TMP_MeshInfo)0;
									if ((nint)tMP_MeshInfo.normals > 0)
									{
										object obj6 = tMP_MeshInfo + 32;
										object obj7 = 0;
										while (true)
										{
											object obj8 = obj7;
											Vector3[] normals = tMP_MeshInfo.normals;
											if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals))
											{
												break;
											}
											object obj9 = obj7;
											Vector3[] normals2 = tMP_MeshInfo.normals;
											if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9) < System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals2))
											{
												obj7++;
												obj6 = tMP_MeshInfo.tangents;
												obj6 += 4;
												continue;
											}
											goto IL_052f;
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v324 @ rax_v25+10]");
										((TMP_Text)0).UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
										continue;
									}
								}
							}
						}
						goto IL_052f;
					}
					goto IL_0469;
				}
				float num2 = distanceWorldTextFader._visibleAlpha / distanceWorldTextFader._fadeDuration;
				_003Cspeed_003E5__2 = num2;
				TextToRendererPair[] texts2 = distanceWorldTextFader._texts;
				if (texts2.Length <= 0)
				{
					goto IL_052f;
				}
				TextToRendererPair textToRendererPair = texts2[0];
				float currentAlpha = distanceWorldTextFader.GetCurrentAlpha(textToRendererPair._text);
				_003Calpha_003E5__3 = currentAlpha;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_0521;
				}
				_003C_003E1__state = -1;
			}
			if (!(_003Calpha_003E5__3 > 0f))
			{
				goto IL_0469;
			}
			float deltaTime = Time.deltaTime;
			float num3 = deltaTime * _003Cspeed_003E5__2;
			float num4 = _003Calpha_003E5__3 - num3;
			if (!(num4 > 0f))
			{
				num4 = 0f;
			}
			_003Calpha_003E5__3 = num4;
			TextToRendererPair[] texts3 = distanceWorldTextFader._texts;
			object obj10 = distanceWorldTextFader._texts + 32;
			object obj11 = 0;
			object obj12 = 0;
			while (true)
			{
				if ((nint)obj12 < texts3.Length)
				{
					if ((nint)obj11 >= texts3.Length)
					{
						break;
					}
					object obj13 = obj10;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v114 @ rdx_v5+10]");
					distanceWorldTextFader.SetTextAlpha((TMP_Text)0, _003Calpha_003E5__3);
					obj11++;
					obj10 += 8;
					obj12 = obj11;
					continue;
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			goto IL_052f;
			IL_052f:
			IndexOutOfRangeException ex = new IndexOutOfRangeException();
			return (byte)(int)ex != 0;
			IL_0521:
			return false;
			IL_0469:
			TextToRendererPair[] texts4 = distanceWorldTextFader._texts;
			object obj14 = distanceWorldTextFader._texts + 32;
			object obj15 = 0;
			object obj16 = 0;
			while ((nint)obj16 < texts4.Length)
			{
				if ((nint)obj15 < texts4.Length)
				{
					object obj17 = obj14;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v332 @ rax_v20+18]");
					((Renderer)0).enabled = false;
					obj15++;
					obj14 += 8;
					obj16 = obj15;
					continue;
				}
				goto IL_052f;
			}
			distanceWorldTextFader._fadeCoroutine = null;
			goto IL_0521;
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

	private Transform _measuredTransform;

	private float _distanceToShow = 10f;

	private float _fadeDuration = 1f;

	private TextToRendererPair[] _texts;

	private Transform _cameraTransform;

	private Coroutine _fadeCoroutine;

	private float _visibleAlpha;

	private bool _isVisible = true;

	private void Start()
	{
		Camera main = Camera.main;
		bool flag = (object)main == null;
		Transform cameraTransform = (Transform)(object)main;
		if (!flag)
		{
			cameraTransform = main.transform;
		}
		_cameraTransform = cameraTransform;
		TextToRendererPair[] texts = _texts;
		if (texts.Length != 0 && texts[0] != null)
		{
			TextToRendererPair textToRendererPair = texts[0];
			if (textToRendererPair._text != null)
			{
				TextToRendererPair[] texts2 = _texts;
				TextToRendererPair textToRendererPair2 = texts2[0];
				float alpha = textToRendererPair2._text.alpha;
				_visibleAlpha = alpha;
				return;
			}
		}
		Debug.LogError("[DistanceWorldTextFader] Wrong texts setup", this);
	}

	private void Update()
	{
		//IL_010f: Expected O, but got I4
		//IL_0132: Expected O, but got I4
		//IL_01a8: Expected I, but got O
		//IL_0195: Expected I, but got O
		Vector3 position = _cameraTransform.position;
		Vector3 position2 = _measuredTransform.position;
		float num = position.z - position2.z;
		float num2 = position.x - position2.x;
		object obj2 = default(object);
		object obj3 = default(object);
		object obj = obj2 - obj3;
		float num3 = _distanceToShow * _distanceToShow;
		float num4 = num2 * num2;
		float num5 = num * num;
		object obj4 = obj * obj;
		float num6 = (float)obj4 + num4;
		float num7 = num6 + num5;
		bool flag = num3 < num7;
		bool flag2 = !flag;
		if (flag2 != _isVisible)
		{
			bool flag3 = _fadeCoroutine == null;
			object obj5 = 0;
			if (!flag3)
			{
				StopCoroutine(_fadeCoroutine);
				obj5 = 0;
			}
			if (flag2)
			{
				nint num8 = (nint)typeof(_003CFadeInCoroutine_003Ed__13);
			}
			else
			{
				nint num8 = (nint)typeof(_003CFadeOutCoroutine_003Ed__12);
			}
			IEnumerator routine = null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
			_ = 0;
			Coroutine fadeCoroutine = StartCoroutine(routine);
			_fadeCoroutine = fadeCoroutine;
			_isVisible = flag2;
		}
	}

	private bool ShouldBeVisible()
	{
		//IL_0123: Expected I4, but got O
		if ((object)_cameraTransform != null)
		{
			Vector3 position = _cameraTransform.position;
			if ((object)_measuredTransform != null)
			{
				Vector3 position2 = _measuredTransform.position;
				float num = position.z - position2.z;
				float num2 = position.x - position2.x;
				object obj2 = default(object);
				object obj3 = default(object);
				object obj = obj2 - obj3;
				float num3 = num * num;
				float num4 = num2 * num2;
				float num5 = _distanceToShow * _distanceToShow;
				object obj4 = obj * obj;
				float num6 = (float)obj4 + num4;
				float num7 = num6 + num3;
				bool flag = num5 < num7;
				return !flag;
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private IEnumerator FadeOutCoroutine()
	{
		_003CFadeOutCoroutine_003Ed__12 obj = new _003CFadeOutCoroutine_003Ed__12(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator FadeInCoroutine()
	{
		_003CFadeInCoroutine_003Ed__13 obj = new _003CFadeInCoroutine_003Ed__13(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private unsafe float GetCurrentAlpha(TMP_Text text)
	{
		//IL_03f6: Expected F4, but got I4
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
		//IL_00e1: Expected O, but got Ref
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Expected O, but got Unknown
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Expected O, but got Unknown
		//IL_015b: Expected O, but got I
		//IL_0170: Expected O, but got I
		//IL_0185: Expected O, but got I
		//IL_01b7: Expected O, but got Ref
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Expected O, but got Unknown
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Expected O, but got Unknown
		//IL_053b: Expected O, but got I4
		//IL_0225: Expected F4, but got I
		//IL_023a: Expected O, but got I
		//IL_024f: Expected O, but got I
		//IL_0264: Expected O, but got I
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Expected O, but got Unknown
		//IL_02df: Expected O, but got I
		//IL_02e8: Expected O, but got Ref
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Expected O, but got Unknown
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Expected O, but got Unknown
		//IL_034b: Expected F4, but got I
		//IL_0360: Expected O, but got I
		//IL_0375: Expected O, but got I
		//IL_038a: Expected O, but got I
		if ((object)text != null)
		{
			TMP_TextInfo textInfo = text.textInfo;
			if (textInfo != null)
			{
				if (textInfo.characterCount <= 0)
				{
					goto IL_03f0;
				}
				TMP_CharacterInfo[] characterInfo = textInfo.characterInfo;
				if (textInfo.characterInfo != null)
				{
					if (characterInfo.Length > 0)
					{
						DistanceWorldTextFader distanceWorldTextFader = (DistanceWorldTextFader)(textInfo.characterInfo + 32);
						nint num = 2;
						object obj = default(object);
						TMP_Text tMP_Text = (TMP_Text)(&obj);
						DistanceWorldTextFader distanceWorldTextFader2 = distanceWorldTextFader;
						IntPtr intPtr = default(IntPtr);
						num = intPtr;
						tMP_Text = text;
						DistanceWorldTextFader distanceWorldTextFader3 = default(DistanceWorldTextFader);
						distanceWorldTextFader2 = distanceWorldTextFader3;
						do
						{
							tMP_Text = (TMP_Text)(tMP_Text + 128);
							distanceWorldTextFader2 = (DistanceWorldTextFader)(distanceWorldTextFader2 + 128);
							_ = ((UnityEngine.Object)distanceWorldTextFader2).m_CachedPtr;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v240 @ rax_v9 (DistanceWorldTextFader)-60]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v240 @ rax_v9 (DistanceWorldTextFader)-50]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v240 @ rax_v9 (DistanceWorldTextFader)-40]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v240 @ rax_v9 (DistanceWorldTextFader)-30]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v240 @ rax_v9 (DistanceWorldTextFader)-20]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v240 @ rax_v9 (DistanceWorldTextFader)-10]");
							_ = 0;
							num--;
						}
						while (characterInfo.Length != 0);
						tMP_Text = (TMP_Text)(object)distanceWorldTextFader2;
						((UnityEngine.Object)tMP_Text).m_CachedPtr = ((UnityEngine.Object)distanceWorldTextFader2).m_CachedPtr;
						((Graphic)tMP_Text).m_Material = (Material)(object)distanceWorldTextFader2._measuredTransform;
						_ = distanceWorldTextFader2._texts;
						_ = distanceWorldTextFader2._fadeCoroutine;
						TMP_Text tMP_Text2 = tMP_Text;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v240 @ rax_v9 (DistanceWorldTextFader)+50]");
						((Graphic)tMP_Text2).m_RectTransform = (RectTransform)0;
						TMP_Text tMP_Text3 = tMP_Text;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v240 @ rax_v9 (DistanceWorldTextFader)+60]");
						((Graphic)tMP_Text3).m_Canvas = (Canvas)0;
						TMP_Text tMP_Text4 = tMP_Text;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v240 @ rax_v9 (DistanceWorldTextFader)+70]");
						((Graphic)tMP_Text4).m_OnDirtyLayoutCallback = (UnityAction)0;
						object obj2 = default(object);
						bool flag = (nint)obj2 == num;
						if (flag)
						{
							goto IL_03f0;
						}
						TMP_MeshInfo[] meshInfo = textInfo.meshInfo;
						TMP_Text tMP_Text5 = (TMP_Text)(&obj);
						DistanceWorldTextFader distanceWorldTextFader4 = distanceWorldTextFader;
						tMP_Text5 = text;
						distanceWorldTextFader4 = distanceWorldTextFader3;
						object obj3;
						do
						{
							tMP_Text5 = (TMP_Text)(tMP_Text5 + 128);
							distanceWorldTextFader4 = (DistanceWorldTextFader)(distanceWorldTextFader4 + 128);
							_ = ((UnityEngine.Object)distanceWorldTextFader4).m_CachedPtr;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ rax_v13 (DistanceWorldTextFader)-60]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ rax_v13 (DistanceWorldTextFader)-50]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ rax_v13 (DistanceWorldTextFader)-40]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ rax_v13 (DistanceWorldTextFader)-30]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ rax_v13 (DistanceWorldTextFader)-20]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ rax_v13 (DistanceWorldTextFader)-10]");
							_ = 0;
							obj3 = !flag;
						}
						while (obj3 != null);
						tMP_Text5 = (TMP_Text)(object)distanceWorldTextFader4;
						((UnityEngine.Object)tMP_Text5).m_CachedPtr = ((UnityEngine.Object)distanceWorldTextFader4).m_CachedPtr;
						((Graphic)tMP_Text5).m_Material = (Material)(object)distanceWorldTextFader4._measuredTransform;
						_ = distanceWorldTextFader4._texts;
						_ = distanceWorldTextFader4._fadeCoroutine;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ rax_v13 (DistanceWorldTextFader)+60]");
						float num2 = 0f;
						TMP_Text tMP_Text6 = tMP_Text5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ rax_v13 (DistanceWorldTextFader)+50]");
						((Graphic)tMP_Text6).m_RectTransform = (RectTransform)0;
						TMP_Text tMP_Text7 = tMP_Text5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ rax_v13 (DistanceWorldTextFader)+60]");
						((Graphic)tMP_Text7).m_Canvas = (Canvas)0;
						TMP_Text tMP_Text8 = tMP_Text5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ rax_v13 (DistanceWorldTextFader)+70]");
						((Graphic)tMP_Text8).m_OnDirtyLayoutCallback = (UnityAction)0;
						if (textInfo.meshInfo == null)
						{
							goto IL_03f6;
						}
						object obj4 = default(object);
						if ((nint)obj4 < meshInfo.Length)
						{
							object obj5 = obj4 * 4;
							object obj6 = obj4 + obj5;
							object obj7 = obj6 + obj6;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v25 @ r10_v7 (TMPro.TMP_MeshInfo[])+58+v281 @ rax_v18*8]");
							TMP_MeshInfo tMP_MeshInfo = (TMP_MeshInfo)0;
							TMP_Text tMP_Text9 = (TMP_Text)(&obj);
							distanceWorldTextFader = this;
							tMP_Text9 = text;
							do
							{
								tMP_Text9 = (TMP_Text)(tMP_Text9 + 128);
								distanceWorldTextFader = (DistanceWorldTextFader)(distanceWorldTextFader + 128);
								_ = ((UnityEngine.Object)distanceWorldTextFader).m_CachedPtr;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v289 @ rcx_v7 (DistanceWorldTextFader)-60]");
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v289 @ rcx_v7 (DistanceWorldTextFader)-50]");
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v289 @ rcx_v7 (DistanceWorldTextFader)-40]");
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v289 @ rcx_v7 (DistanceWorldTextFader)-30]");
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v289 @ rcx_v7 (DistanceWorldTextFader)-20]");
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v289 @ rcx_v7 (DistanceWorldTextFader)-10]");
								_ = 0;
							}
							while ((nint)obj4 != meshInfo.Length);
							tMP_Text9 = (TMP_Text)(object)distanceWorldTextFader;
							((UnityEngine.Object)tMP_Text9).m_CachedPtr = ((UnityEngine.Object)distanceWorldTextFader).m_CachedPtr;
							((Graphic)tMP_Text9).m_Material = (Material)(object)distanceWorldTextFader._measuredTransform;
							_ = distanceWorldTextFader._texts;
							_ = distanceWorldTextFader._fadeCoroutine;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v289 @ rcx_v7 (DistanceWorldTextFader)+60]");
							num2 = 0f;
							TMP_Text tMP_Text10 = tMP_Text9;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v289 @ rcx_v7 (DistanceWorldTextFader)+50]");
							((Graphic)tMP_Text10).m_RectTransform = (RectTransform)0;
							TMP_Text tMP_Text11 = tMP_Text9;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v289 @ rcx_v7 (DistanceWorldTextFader)+60]");
							((Graphic)tMP_Text11).m_Canvas = (Canvas)0;
							TMP_Text tMP_Text12 = tMP_Text9;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v289 @ rcx_v7 (DistanceWorldTextFader)+70]");
							((Graphic)tMP_Text12).m_OnDirtyLayoutCallback = (UnityAction)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v25 @ r10_v7 (TMPro.TMP_MeshInfo[])+58+v281 @ rax_v18*8]");
							if ((nint)0 == 0)
							{
								goto IL_03f6;
							}
							Vector3[] normals = tMP_MeshInfo.normals;
							object obj8 = default(object);
							if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8) < System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals))
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r8_v7 (TMPro.TMP_MeshInfo)+23+v302 @ stack_-144*4]");
								return 0f / 255f;
							}
						}
					}
					throw new IndexOutOfRangeException();
				}
			}
		}
		goto IL_03f6;
		IL_03f0:
		return 0f;
		IL_03f6:
		throw new NullReferenceException();
	}

	private void SetTextAlpha(TMP_Text text, float alpha)
	{
		//IL_0073: Expected O, but got I
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		//IL_00a5: Expected O, but got I
		//IL_00c0: Expected O, but got I
		//IL_00c9: Expected O, but got I4
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Expected O, but got Unknown
		TMP_TextInfo textInfo = text.textInfo;
		if (textInfo.characterCount == 0)
		{
			return;
		}
		TMP_MeshInfo[] meshInfo = textInfo.meshInfo;
		TMP_CharacterInfo[] characterInfo = textInfo.characterInfo;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si r8d,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rax_v7 (TMPro.TMP_CharacterInfo[])+50]");
		object obj = (nint)0 * (nint)4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rax_v7 (TMPro.TMP_CharacterInfo[])+50]");
		object obj2 = 0 + obj;
		object obj3 = obj2 + obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdx_v5 (TMPro.TMP_MeshInfo[])+58+v33 @ rcx_v7*8]");
		TMP_MeshInfo tMP_MeshInfo = (TMP_MeshInfo)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdx_v5 (TMPro.TMP_MeshInfo[])+58+v33 @ rcx_v7*8]");
		object obj4 = (nint)0 + (nint)32;
		object obj5 = 0;
		while (true)
		{
			object obj6 = obj5;
			Vector3[] normals = tMP_MeshInfo.normals;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals))
			{
				break;
			}
			obj5++;
			obj4 = tMP_MeshInfo.tangents;
			obj4 += 4;
		}
		text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
	}

	private void Reset()
	{
		if (_measuredTransform == null)
		{
			Transform measuredTransform = base.transform;
			_measuredTransform = measuredTransform;
		}
	}
}
