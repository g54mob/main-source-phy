using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Cpp2ILInjected;
using Localisation;
using TMPro;
using UnityEngine;

public class CreditsSection : MonoBehaviour
{
	private sealed class _003CInitialize_003Ed__10 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CreditsSection _003C_003E4__this;

		public CreditsSectionConfig config;

		public int maxLinesPerChunk;

		public Action onFirstChunkInitialized;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CInitialize_003Ed__10(int _003C_003E1__state)
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
			//IL_0328: Expected I4, but got I8
			//IL_033b: Expected I4, but got O
			CreditsSection creditsSection = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				CreditsSectionConfig creditsSectionConfig = config;
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null && config != null)
				{
					if (!string.IsNullOrEmpty(creditsSectionConfig._titleOverride))
					{
						if ((object)creditsSection._titleText != null)
						{
							creditsSection._titleText.text = creditsSectionConfig._titleOverride;
							goto IL_033b;
						}
					}
					else
					{
						StaticLocalisedText titleLocalization = creditsSection._titleLocalization;
						if ((object)creditsSection._titleLocalization != null)
						{
							TextIdentifier titleLangKey = creditsSectionConfig._titleLangKey;
							if (creditsSectionConfig._titleLangKey != null)
							{
								TextIdentifier key = titleLocalization.Key;
								if (titleLocalization.Key != null)
								{
									key.Key = titleLangKey.Key;
									StaticLocalisedText titleLocalization2 = creditsSection._titleLocalization;
									if ((object)creditsSection._titleLocalization != null)
									{
										TextIdentifier titleLangKey2 = creditsSectionConfig._titleLangKey;
										if (creditsSectionConfig._titleLangKey != null)
										{
											TextIdentifier key2 = titleLocalization2.Key;
											if (titleLocalization2.Key != null)
											{
												key2.Raw = titleLangKey2.Raw;
												if ((object)creditsSection._titleLocalization != null)
												{
													creditsSection._titleLocalization.enabled = true;
													if ((object)creditsSection._titleLocalization != null)
													{
														creditsSection._titleLocalization.UpdateText();
														goto IL_033b;
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
			}
			return false;
			IL_033b:
			_003CLoadContent_003Ed__13 obj = new _003CLoadContent_003Ed__13(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = _003C_003E4__this;
			obj.config = config;
			obj.maxLinesPerChunk = maxLinesPerChunk;
			obj.onFirstChunkInitialized = onFirstChunkInitialized;
			Coroutine coroutine = _003C_003E4__this.StartCoroutine(obj);
			_003C_003E2__current = coroutine;
			_003C_003E1__state = 1;
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

	private sealed class _003CLoadContent_003Ed__13 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CreditsSection _003C_003E4__this;

		public CreditsSectionConfig config;

		public int maxLinesPerChunk;

		public Action onFirstChunkInitialized;

		private List<string> _003Cchunks_003E5__2;

		private bool _003ChasInitializedFirstChunk_003E5__3;

		private List<string>.Enumerator _003C_003E7__wrap3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CLoadContent_003Ed__13(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		unsafe void IDisposable.Dispose()
		{
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			if (_003C_003E1__state == -3 || _003C_003E1__state == 2)
			{
				_ = 4294967295L;
				object obj = default(object);
				List<string>.Enumerator enumerator = (List<string>.Enumerator)(obj + 80);
				((List<string>.Enumerator*)enumerator)->Dispose();
			}
		}

		private unsafe bool MoveNext()
		{
			//IL_0855: Expected O, but got I
			//IL_03c7: Expected I4, but got I8
			//IL_0018: Expected O, but got I4
			//IL_007a: Expected I4, but got I8
			//IL_042c: Expected O, but got I4
			//IL_0063: Expected I4, but got I8
			//IL_00e0: Expected I4, but got I8
			//IL_0885: Unknown result type (might be due to invalid IL or missing references)
			//IL_088a: Expected O, but got Unknown
			//IL_0474: Expected O, but got I
			//IL_0493: Expected O, but got F4
			//IL_08ef: Expected I4, but got I8
			//IL_08f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_08fd: Expected O, but got Unknown
			//IL_0104: Unknown result type (might be due to invalid IL or missing references)
			//IL_0109: Expected O, but got Unknown
			//IL_0132: Expected O, but got Ref
			//IL_04c5: Expected O, but got F4
			//IL_0165: Expected O, but got Ref
			//IL_04e9: Expected O, but got I
			//IL_018a: Expected O, but got I
			//IL_0526: Expected O, but got I4
			//IL_052b: Expected I, but got O
			//IL_0533: Expected O, but got F4
			//IL_0540: Expected I, but got O
			//IL_01dd: Expected O, but got I
			//IL_0556: Expected I, but got O
			//IL_0566: Expected O, but got I
			//IL_0694: Expected O, but got F4
			//IL_0214: Expected I, but got O
			//IL_05aa: Expected O, but got F4
			//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c9: Expected O, but got Unknown
			//IL_06e4: Expected I, but got O
			//IL_027d: Expected O, but got I
			//IL_029a: Expected O, but got I
			//IL_06fa: Expected I, but got O
			//IL_070a: Expected O, but got I
			//IL_072e: Expected O, but got F4
			//IL_0733: Expected I, but got O
			//IL_05f5: Expected I, but got O
			//IL_0605: Expected O, but got I
			//IL_0629: Expected O, but got F4
			//IL_02e8: Expected O, but got I
			//IL_0307: Expected F4, but got O
			//IL_0769: Expected O, but got I4
			//IL_037c: Expected O, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (System.String)+20]");
			StringBuilder stringBuilder = (StringBuilder)0;
			string text = default(string);
			bool flag = text._stringLength == 0;
			float num5 = default(float);
			string text2;
			if (!flag)
			{
				object obj = text._stringLength - 1;
				if (!flag)
				{
					if ((nint)obj != 1)
					{
						return false;
					}
					text._stringLength = -3;
				}
				else
				{
					text._stringLength = -1;
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (System.String)+40]");
					bool flag2 = (nint)0 == 0;
					text2 = text;
					nint num2 = default(nint);
					nint num = num2;
					if (flag2)
					{
						throw new NullReferenceException();
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					text._stringLength = -3;
					object obj3 = default(object);
					object obj2 = obj3;
					Vector2 vector2 = default(Vector2);
					Vector2 vector = vector2;
					num2 = 0;
				}
				List<string>.Enumerator enumerator = (List<string>.Enumerator)(text + 80);
				if (((List<string>.Enumerator*)enumerator)->MoveNext())
				{
					object obj4 = text + 80;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (System.String)+20]");
					bool flag3 = (nint)0 == 0;
					string text3 = default(string);
					text2 = (string)(&text3);
					nint num = 0;
					if (!flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v788 @ rbx_v9 (System.Text.StringBuilder)+30]");
						bool flag4 = (nint)0 == 0;
						Transform transform = (Transform)(&text3);
						num = 0;
						if (!flag4)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v788 @ rbx_v9 (System.Text.StringBuilder)+30]");
							Transform transform2 = ((TMP_Text)0).transform;
							bool flag5 = (object)transform2 == null;
							transform = null;
							num = 0;
							if (!flag5)
							{
								Transform parent = transform2.parent;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v788 @ rbx_v9 (System.Text.StringBuilder)+30]");
								TMP_Text tMP_Text = UnityEngine.Object.Instantiate((TMP_Text)0, parent);
								bool flag6 = (object)tMP_Text == null;
								transform = parent;
								num = 0;
								if (!flag6)
								{
									nint num3 = (nint)tMP_Text;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v961 @ r8_v25 (Il2CppClass<TMPro.TMP_Text>)+560]");
									num = 0;
									tMP_Text.text = text3;
									RectTransform rectTransform = tMP_Text.rectTransform;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v788 @ rbx_v9 (System.Text.StringBuilder)+40]");
									bool flag7 = (nint)0 == 0;
									transform = null;
									if (!flag7)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v788 @ rbx_v9 (System.Text.StringBuilder)+40]");
										((List<RectTransform>)0).Add(rectTransform);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v788 @ rbx_v9 (System.Text.StringBuilder)+48]");
										nint num4 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
										object obj2 = num4 ^ 0;
										bool flag8 = (object)rectTransform == null;
										transform = rectTransform;
										num = 0;
										if (!flag8)
										{
											Vector2 vector3 = default(Vector2);
											rectTransform.anchoredPosition = vector3;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v788 @ rbx_v9 (System.Text.StringBuilder)+38]");
											Rect rect = ((RectTransform)0).rect;
											Vector2 preferredValues = tMP_Text.GetPreferredValues(text3, (float)vector3, 0f);
											rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num5);
											float num6 = num5;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v788 @ rbx_v9 (System.Text.StringBuilder)+48]");
											float num7 = num6 + 0f;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (System.String)+48]");
											if ((nint)0 == 0)
											{
												_ = 1;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (System.String)+38]");
												object obj5 = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (System.String)+38]");
												if ((nint)0 != 0)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1180 @ rcx_v77+18] (should have been resolved before IL gen)");
												}
											}
											_ = 0;
											text._stringLength = 2;
											return true;
										}
										throw new NullReferenceException();
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						text2 = (string)(object)transform;
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				text._stringLength = -1;
				List<string>.Enumerator enumerator2 = (List<string>.Enumerator)(text + 80);
				((List<string>.Enumerator*)enumerator2)->Dispose();
				_ = 0;
				_ = 0;
				return false;
			}
			text._stringLength = -1;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (System.String)+20]");
			bool flag9 = (nint)0 == 0;
			text2 = text;
			if (!flag9)
			{
				bool flag10 = stringBuilder.m_MaxCapacity == 0;
				text2 = text;
				if (!flag10)
				{
					RectTransform rectTransform2 = ((TMP_Text)stringBuilder.m_MaxCapacity).rectTransform;
					bool flag11 = (object)rectTransform2 == null;
					text2 = null;
					if (!flag11)
					{
						Vector2 sizeDelta = rectTransform2.sizeDelta;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (System.String)+28]");
						object obj6 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (System.String)+28]");
						bool flag12 = (nint)0 == 0;
						Vector2 vector = (Vector2)num5;
						text2 = null;
						if (!flag12)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v638 @ rcx_v18+20]");
							bool flag13 = (nint)0 == 0;
							vector = (Vector2)num5;
							text2 = null;
							if (!flag13)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v638 @ rcx_v18+20]");
								string text4 = ((TextAsset)0).text;
								List<string> list = new List<string>();
								StringBuilder stringBuilder2 = new StringBuilder();
								stringBuilder2._002Ector();
								StringReader stringReader = new StringReader(text4);
								bool flag14 = stringReader == null;
								object obj7 = 0;
								nint num8 = unchecked((nint)null);
								vector = (Vector2)num5;
								text2 = text4;
								nint num2 = unchecked((nint)null);
								if (!flag14)
								{
									while (true)
									{
										nint num9 = (nint)stringReader;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1056 @ rdx_v20 (Il2CppClass<System.IO.StringReader>)+220]");
										text2 = (string)0;
										string text5 = stringReader.ReadLine();
										if (text5 == null)
										{
											bool flag15 = stringBuilder2 == null;
											vector = (Vector2)num5;
											nint num = num8;
											if (!flag15)
											{
												int length = stringBuilder2.Length;
												if (length > 0)
												{
													nint num10 = (nint)stringBuilder2;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1124 @ rdx_v31 (Il2CppClass<System.Text.StringBuilder>)+170]");
													text2 = (string)0;
													string item = stringBuilder2.ToString();
													bool flag16 = list == null;
													vector = (Vector2)num5;
													num = num8;
													if (flag16)
													{
														throw new NullReferenceException();
													}
													list.Add(item);
												}
												_ = 0;
												text._stringLength = 1;
												return true;
											}
											throw new NullReferenceException();
										}
										bool flag17 = stringBuilder2 == null;
										vector = (Vector2)num5;
										num2 = num8;
										if (flag17)
										{
											break;
										}
										StringBuilder stringBuilder3 = stringBuilder2.AppendLine(text5);
										obj7++;
										object obj8 = obj7;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (System.String)+30]");
										bool flag18 = (nint)obj8 < 0;
										num8 = unchecked((nint)null);
										if (!flag18)
										{
											nint num11 = (nint)stringBuilder2;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1137 @ rdx_v23 (Il2CppClass<System.Text.StringBuilder>)+170]");
											text2 = (string)0;
											string item2 = stringBuilder2.ToString();
											bool flag19 = list == null;
											vector = (Vector2)num5;
											nint num = unchecked((nint)null);
											if (flag19)
											{
												num2 = num;
												throw new NullReferenceException();
											}
											list.Add(item2);
											StringBuilder stringBuilder4 = stringBuilder2.Clear();
											obj7 = 0;
											num8 = 0;
										}
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private unsafe void _003C_003Em__Finally1()
		{
			//IL_0014: Expected I4, but got I8
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Expected O, but got Unknown
			_003C_003E1__state = -1;
			List<string>.Enumerator enumerator = (List<string>.Enumerator)(this + 80);
			((List<string>.Enumerator*)enumerator)->Dispose();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private StaticLocalisedText _titleLocalization;

	private TMP_Text _titleText;

	private TMP_Text _contentText;

	private RectTransform _rectTransform;

	private readonly List<RectTransform> _chunkRects;

	private float _contentHeight;

	public RectTransform RectTransform => _rectTransform;

	public List<RectTransform> ChunkRects => _chunkRects;

	public IEnumerator Initialize(CreditsSectionConfig config, int maxLinesPerChunk, Action onFirstChunkInitialized = null)
	{
		_003CInitialize_003Ed__10 obj = new _003CInitialize_003Ed__10(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.config = config;
		obj.maxLinesPerChunk = maxLinesPerChunk;
		obj.onFirstChunkInitialized = onFirstChunkInitialized;
		return obj;
	}

	public float GetHeight()
	{
		return _contentHeight;
	}

	private void LoadTitle(CreditsSectionConfig config)
	{
		//IL_0037: Expected I, but got O
		//IL_0054: Expected O, but got I
		//IL_0064: Expected O, but got I
		if (!string.IsNullOrEmpty(config._titleOverride))
		{
			TMP_Text titleText = _titleText;
			nint num = (nint)titleText;
			string titleOverride = config._titleOverride;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ r8_v3 (Il2CppClass<TMPro.TMP_Text>)+558]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ r8_v3 (Il2CppClass<TMPro.TMP_Text>)+560]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v166 @ rax_v10 (should have been resolved before IL gen)");
		}
		StaticLocalisedText titleLocalization = _titleLocalization;
		TextIdentifier titleLangKey = config._titleLangKey;
		TextIdentifier key = titleLocalization.Key;
		key.Key = titleLangKey.Key;
		StaticLocalisedText titleLocalization2 = _titleLocalization;
		TextIdentifier titleLangKey2 = config._titleLangKey;
		TextIdentifier key2 = titleLocalization2.Key;
		key2.Raw = titleLangKey2.Raw;
		_titleLocalization.enabled = true;
		_titleLocalization.UpdateText();
	}

	private IEnumerator LoadContent(CreditsSectionConfig config, int maxLinesPerChunk, Action onFirstChunkInitialized = null)
	{
		_003CLoadContent_003Ed__13 obj = new _003CLoadContent_003Ed__13(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.config = config;
		obj.maxLinesPerChunk = maxLinesPerChunk;
		obj.onFirstChunkInitialized = onFirstChunkInitialized;
		return obj;
	}

	private static List<string> SplitIntoChunks(string text, int linesPerChunk)
	{
		//IL_0039: Expected O, but got I4
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_00f1: Expected O, but got I4
		List<string> list = new List<string>();
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder._002Ector();
		StringReader stringReader = new StringReader(text);
		bool flag = stringReader == null;
		object obj = 0;
		if (!flag)
		{
			while (true)
			{
				string text2 = stringReader.ReadLine();
				if (text2 != null)
				{
					if (stringBuilder == null)
					{
						break;
					}
					StringBuilder stringBuilder2 = stringBuilder.AppendLine(text2);
					obj++;
					if ((nint)obj >= linesPerChunk)
					{
						string item = stringBuilder.ToString();
						if (list == null)
						{
							break;
						}
						list.Add(item);
						StringBuilder stringBuilder3 = stringBuilder.Clear();
						obj = 0;
					}
					continue;
				}
				if (stringBuilder == null)
				{
					break;
				}
				int length = stringBuilder.Length;
				if (length > 0)
				{
					string item2 = stringBuilder.ToString();
					if (list == null)
					{
						break;
					}
					list.Add(item2);
				}
				return list;
			}
		}
		return (List<string>)(object)new NullReferenceException();
	}

	public CreditsSection()
	{
		List<RectTransform> chunkRects = new List<RectTransform>();
		_chunkRects = chunkRects;
		base._002Ector();
	}
}
