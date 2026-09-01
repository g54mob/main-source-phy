using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.TextCore;

namespace TMPro.Examples;

public class TextMeshProFloatingText : MonoBehaviour
{
	private sealed class _003CDisplayTextMeshFloatingText_003Ed__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TextMeshProFloatingText _003C_003E4__this;

		private float _003CCountDuration_003E5__2;

		private float _003Cstarting_Count_003E5__3;

		private float _003Ccurrent_Count_003E5__4;

		private Vector3 _003Cstart_pos_003E5__5;

		private Color32 _003Cstart_color_003E5__6;

		private float _003Calpha_003E5__7;

		private float _003CfadeDuration_003E5__8;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CDisplayTextMeshFloatingText_003Ed__16(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_00e4: Expected I4, but got I8
			//IL_013c: Expected O, but got F4
			//IL_0015: Expected O, but got I4
			//IL_0166: Unknown result type (might be due to invalid IL or missing references)
			//IL_016b: Expected O, but got Unknown
			//IL_00d0: Expected I4, but got I8
			//IL_06d2: Invalid comparison between F4 and I4
			//IL_0052: Expected I4, but got I8
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Expected O, but got Unknown
			//IL_0756: Expected I4, but got O
			//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d9: Expected I4, but got Unknown
			//IL_027b: Invalid comparison between I4 and F4
			//IL_0349: Expected O, but got I
			//IL_0376: Expected O, but got I
			//IL_038c: Expected O, but got I
			//IL_02c6: Expected F4, but got I4
			//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_03de: Expected O, but got Unknown
			//IL_042b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0430: Expected O, but got Unknown
			//IL_047b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0480: Expected O, but got Unknown
			//IL_0489: Unknown result type (might be due to invalid IL or missing references)
			//IL_048e: Expected O, but got Unknown
			//IL_057c: Expected O, but got F4
			//IL_05af: Expected O, but got F4
			//IL_0512: Unknown result type (might be due to invalid IL or missing references)
			//IL_0517: Expected O, but got Unknown
			//IL_0520: Unknown result type (might be due to invalid IL or missing references)
			//IL_0525: Expected O, but got Unknown
			//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_05cc: Expected O, but got Unknown
			//IL_061f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0624: Expected O, but got Unknown
			TextMeshProFloatingText textMeshProFloatingText = _003C_003E4__this;
			_ = 0;
			bool flag = _003C_003E1__state == 0;
			object obj2 = default(object);
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					if ((nint)obj == 1)
					{
						_003C_003E1__state = -1;
						Vector3 position = (Vector3)(obj2 - 96);
						_ = _003Cstart_pos_003E5__5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TextMeshProFloatingText+<DisplayTextMeshFloatingText>d__16)+3C]");
						_ = 0;
						textMeshProFloatingText.m_floatingText_Transform.position = position;
						IEnumerator routine = textMeshProFloatingText.DisplayTextMeshFloatingText();
						Coroutine coroutine = textMeshProFloatingText.StartCoroutine(routine);
					}
					return false;
				}
				_003C_003E1__state = -1;
			}
			else
			{
				_003C_003E1__state = -1;
				_003CCountDuration_003E5__2 = 2f;
				_003Ccurrent_Count_003E5__4 = (_003Cstarting_Count_003E5__3 = UnityEngine.Random.Range(5f, 20f));
				Vector3 position2 = textMeshProFloatingText.m_floatingText_Transform.position;
				_003Cstart_pos_003E5__5 = (Vector3)position2.x;
				_ = position2.z;
				Color color = textMeshProFloatingText.m_textMesh.color;
				Color color2 = (Color)(obj2 - 80);
				_ = color.r;
				Color32 color3 = color2;
				_003Cstart_color_003E5__6 = color3;
				float num = 3f / _003Cstarting_Count_003E5__3;
				_003Calpha_003E5__7 = 255f;
				_ = 0;
				float num2 = num * _003CCountDuration_003E5__2;
				_003CfadeDuration_003E5__8 = num2;
			}
			if (_003Ccurrent_Count_003E5__4 > 0f)
			{
				float deltaTime = Time.deltaTime;
				float num3 = deltaTime / _003CCountDuration_003E5__2;
				float num4 = num3 * _003Cstarting_Count_003E5__3;
				if (!(3f < (_003Ccurrent_Count_003E5__4 -= num4)))
				{
					float deltaTime2 = Time.deltaTime;
					float num5 = deltaTime2 / _003CfadeDuration_003E5__8;
					float num6 = num5 * 255f;
					float num7 = _003Calpha_003E5__7 - num6;
					if (!(0f > num7))
					{
						if (num7 > 255f)
						{
							num7 = 255f;
						}
					}
					else
					{
						num7 = 0f;
					}
					_003Calpha_003E5__7 = num7;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si eax,dword ptr [rdi+30h]\"");
				int num8 = obj2 + 48;
				string text = ((int*)num8)->ToString();
				textMeshProFloatingText.m_textMesh.text = text;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si eax,xmm0\"");
				_ = 0;
				_ = _003Cstart_color_003E5__6;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TextMeshProFloatingText+<DisplayTextMeshFloatingText>d__16)+41]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TextMeshProFloatingText+<DisplayTextMeshFloatingText>d__16)+42]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp+20]");
				object obj3 = (nint)0 >> 8;
				float num9 = (float)_003Cstart_color_003E5__6 / 255f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp+20]");
				object obj4 = (nint)0 >> 16;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp+20]");
				object obj5 = (nint)0 >> 24;
				float num10 = (float)obj3 / 255f;
				float num11 = (float)obj4 / 255f;
				float num12 = (float)obj5 / 255f;
				Color color4 = (Color)(obj2 - 80);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-50]");
				_ = 0;
				textMeshProFloatingText.m_textMesh.color = color4;
				Vector3 position3 = textMeshProFloatingText.m_floatingText_Transform.position;
				float deltaTime3 = Time.deltaTime;
				Vector3 position4 = (Vector3)(obj2 - 96);
				_ = position3.x;
				_ = position3.z;
				textMeshProFloatingText.m_floatingText_Transform.position = position4;
				Vector3 position5 = textMeshProFloatingText.m_cameraTransform.position;
				Vector3 v = (Vector3)(obj2 - 96);
				Vector3 v2 = (Vector3)(obj2 - 80);
				_ = position5.x;
				_ = position5.z;
				_ = textMeshProFloatingText.lastPOS;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rsi_v1 (TMPro.Examples.TextMeshProFloatingText)+60]");
				_ = 0;
				if (TMPro_ExtensionMethods.Compare(v2, v, 1000))
				{
					Quaternion rotation = textMeshProFloatingText.m_cameraTransform.rotation;
					_ = textMeshProFloatingText.lastRotation;
					Quaternion q = (Quaternion)(obj2 - 80);
					Quaternion q2 = (Quaternion)(obj2 - 96);
					_ = rotation.x;
					if (TMPro_ExtensionMethods.Compare(q2, q, 1000))
					{
						goto IL_070c;
					}
				}
				Vector3 position6 = textMeshProFloatingText.m_cameraTransform.position;
				textMeshProFloatingText.lastPOS = (Vector3)position6.x;
				_ = position6.z;
				Quaternion rotation2 = textMeshProFloatingText.m_cameraTransform.rotation;
				textMeshProFloatingText.lastRotation = (Quaternion)rotation2.x;
				_ = rotation2.x;
				Quaternion rotation3 = (Quaternion)(obj2 - 80);
				textMeshProFloatingText.m_floatingText_Transform.rotation = rotation3;
				float num13 = textMeshProFloatingText.m_transform.position.z;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rsi_v1 (TMPro.Examples.TextMeshProFloatingText)+60]");
				float num14 = num13 - 0f;
				Vector3 forward = (Vector3)(obj2 - 80);
				textMeshProFloatingText.m_transform.forward = forward;
				goto IL_070c;
			}
			WaitForSeconds[] k_WaitForSecondsRandom = TextMeshProFloatingText.k_WaitForSecondsRandom;
			int num15 = UnityEngine.Random.Range(0, 20);
			if (num15 < k_WaitForSecondsRandom.Length)
			{
				_003C_003E2__current = k_WaitForSecondsRandom[num15];
				_003C_003E1__state = 2;
				goto IL_0756;
			}
			IndexOutOfRangeException ex = new IndexOutOfRangeException();
			return (byte)(int)ex != 0;
			IL_0756:
			return true;
			IL_070c:
			_003C_003E2__current = k_WaitForEndOfFrame;
			_003C_003E1__state = 1;
			goto IL_0756;
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

	private sealed class _003CDisplayTextMeshProFloatingText_003Ed__15 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TextMeshProFloatingText _003C_003E4__this;

		private float _003CCountDuration_003E5__2;

		private float _003Cstarting_Count_003E5__3;

		private float _003Ccurrent_Count_003E5__4;

		private Vector3 _003Cstart_pos_003E5__5;

		private Color32 _003Cstart_color_003E5__6;

		private float _003Calpha_003E5__7;

		private float _003CfadeDuration_003E5__8;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CDisplayTextMeshProFloatingText_003Ed__15(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_00e4: Expected I4, but got I8
			//IL_013c: Expected O, but got F4
			//IL_0015: Expected O, but got I4
			//IL_0166: Unknown result type (might be due to invalid IL or missing references)
			//IL_016b: Expected O, but got Unknown
			//IL_00d0: Expected I4, but got I8
			//IL_0632: Invalid comparison between F4 and I4
			//IL_0052: Expected I4, but got I8
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Expected O, but got Unknown
			//IL_06b6: Expected I4, but got O
			//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d4: Expected I4, but got Unknown
			//IL_0276: Invalid comparison between I4 and F4
			//IL_02c1: Expected F4, but got I4
			//IL_033c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0341: Expected O, but got Unknown
			//IL_038b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0390: Expected O, but got Unknown
			//IL_03db: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e0: Expected O, but got Unknown
			//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ee: Expected O, but got Unknown
			//IL_04dc: Expected O, but got F4
			//IL_050f: Expected O, but got F4
			//IL_0472: Unknown result type (might be due to invalid IL or missing references)
			//IL_0477: Expected O, but got Unknown
			//IL_0480: Unknown result type (might be due to invalid IL or missing references)
			//IL_0485: Expected O, but got Unknown
			//IL_0527: Unknown result type (might be due to invalid IL or missing references)
			//IL_052c: Expected O, but got Unknown
			//IL_057f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0584: Expected O, but got Unknown
			TextMeshProFloatingText textMeshProFloatingText = _003C_003E4__this;
			_ = 0;
			bool flag = _003C_003E1__state == 0;
			object obj2 = default(object);
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					if ((nint)obj == 1)
					{
						_003C_003E1__state = -1;
						Vector3 position = (Vector3)(obj2 - 96);
						_ = _003Cstart_pos_003E5__5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TextMeshProFloatingText+<DisplayTextMeshProFloatingText>d__15)+3C]");
						_ = 0;
						textMeshProFloatingText.m_floatingText_Transform.position = position;
						IEnumerator routine = textMeshProFloatingText.DisplayTextMeshProFloatingText();
						Coroutine coroutine = textMeshProFloatingText.StartCoroutine(routine);
					}
					return false;
				}
				_003C_003E1__state = -1;
			}
			else
			{
				_003C_003E1__state = -1;
				_003CCountDuration_003E5__2 = 2f;
				_003Ccurrent_Count_003E5__4 = (_003Cstarting_Count_003E5__3 = UnityEngine.Random.Range(5f, 20f));
				Vector3 position2 = textMeshProFloatingText.m_floatingText_Transform.position;
				_003Cstart_pos_003E5__5 = (Vector3)position2.x;
				_ = position2.z;
				Color color = textMeshProFloatingText.m_textMeshPro.color;
				Color color2 = (Color)(obj2 - 80);
				_ = color.r;
				Color32 color3 = color2;
				_003Cstart_color_003E5__6 = color3;
				float num = 3f / _003Cstarting_Count_003E5__3;
				_003Calpha_003E5__7 = 255f;
				_ = 0;
				float num2 = num * _003CCountDuration_003E5__2;
				_003CfadeDuration_003E5__8 = num2;
			}
			if (_003Ccurrent_Count_003E5__4 > 0f)
			{
				float deltaTime = Time.deltaTime;
				float num3 = deltaTime / _003CCountDuration_003E5__2;
				float num4 = num3 * _003Cstarting_Count_003E5__3;
				if (!(3f < (_003Ccurrent_Count_003E5__4 -= num4)))
				{
					float deltaTime2 = Time.deltaTime;
					float num5 = deltaTime2 / _003CfadeDuration_003E5__8;
					float num6 = num5 * 255f;
					float num7 = _003Calpha_003E5__7 - num6;
					if (!(0f > num7))
					{
						if (num7 > 255f)
						{
							num7 = 255f;
						}
					}
					else
					{
						num7 = 0f;
					}
					_003Calpha_003E5__7 = num7;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si eax,dword ptr [rdi+30h]\"");
				int num8 = obj2 + 48;
				string text = ((int*)num8)->ToString();
				textMeshProFloatingText.m_textMeshPro.text = text;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si eax,xmm0\"");
				_ = 0;
				_ = _003Cstart_color_003E5__6;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TextMeshProFloatingText+<DisplayTextMeshProFloatingText>d__15)+41]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TextMeshProFloatingText+<DisplayTextMeshProFloatingText>d__15)+42]");
				_ = 0;
				Color color4 = (Color)(obj2 - 80);
				textMeshProFloatingText.m_textMeshPro.color = color4;
				Vector3 position3 = textMeshProFloatingText.m_floatingText_Transform.position;
				float deltaTime3 = Time.deltaTime;
				Vector3 position4 = (Vector3)(obj2 - 96);
				_ = position3.x;
				_ = position3.z;
				textMeshProFloatingText.m_floatingText_Transform.position = position4;
				Vector3 position5 = textMeshProFloatingText.m_cameraTransform.position;
				Vector3 v = (Vector3)(obj2 - 96);
				Vector3 v2 = (Vector3)(obj2 - 80);
				_ = position5.x;
				_ = position5.z;
				_ = textMeshProFloatingText.lastPOS;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rsi_v1 (TMPro.Examples.TextMeshProFloatingText)+60]");
				_ = 0;
				if (TMPro_ExtensionMethods.Compare(v2, v, 1000))
				{
					Quaternion rotation = textMeshProFloatingText.m_cameraTransform.rotation;
					_ = textMeshProFloatingText.lastRotation;
					Quaternion q = (Quaternion)(obj2 - 80);
					Quaternion q2 = (Quaternion)(obj2 - 96);
					_ = rotation.x;
					if (TMPro_ExtensionMethods.Compare(q2, q, 1000))
					{
						goto IL_066c;
					}
				}
				Vector3 position6 = textMeshProFloatingText.m_cameraTransform.position;
				textMeshProFloatingText.lastPOS = (Vector3)position6.x;
				_ = position6.z;
				Quaternion rotation2 = textMeshProFloatingText.m_cameraTransform.rotation;
				textMeshProFloatingText.lastRotation = (Quaternion)rotation2.x;
				_ = rotation2.x;
				Quaternion rotation3 = (Quaternion)(obj2 - 80);
				textMeshProFloatingText.m_floatingText_Transform.rotation = rotation3;
				float num9 = textMeshProFloatingText.m_transform.position.z;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rsi_v1 (TMPro.Examples.TextMeshProFloatingText)+60]");
				float num10 = num9 - 0f;
				Vector3 forward = (Vector3)(obj2 - 80);
				textMeshProFloatingText.m_transform.forward = forward;
				goto IL_066c;
			}
			WaitForSeconds[] k_WaitForSecondsRandom = TextMeshProFloatingText.k_WaitForSecondsRandom;
			int num11 = UnityEngine.Random.Range(0, 19);
			if (num11 < k_WaitForSecondsRandom.Length)
			{
				_003C_003E2__current = k_WaitForSecondsRandom[num11];
				_003C_003E1__state = 2;
				goto IL_06b6;
			}
			IndexOutOfRangeException ex = new IndexOutOfRangeException();
			return (byte)(int)ex != 0;
			IL_06b6:
			return true;
			IL_066c:
			_003C_003E2__current = k_WaitForEndOfFrame;
			_003C_003E1__state = 1;
			goto IL_06b6;
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

	public Font TheFont;

	private GameObject m_floatingText;

	private TextMeshPro m_textMeshPro;

	private TextMesh m_textMesh;

	private Transform m_transform;

	private Transform m_floatingText_Transform;

	private Transform m_cameraTransform;

	private Vector3 lastPOS;

	private Quaternion lastRotation;

	public int SpawnType;

	public bool IsTextObjectScaleStatic;

	private static WaitForEndOfFrame k_WaitForEndOfFrame;

	private static WaitForSeconds[] k_WaitForSecondsRandom;

	private void Awake()
	{
		Transform transform = base.transform;
		m_transform = transform;
		string text = base.name;
		string text2 = text + " floating text";
		GameObject floatingText = new GameObject(text2);
		m_floatingText = floatingText;
		Camera main = Camera.main;
		Transform cameraTransform = main.transform;
		m_cameraTransform = cameraTransform;
	}

	private unsafe void Start()
	{
		//IL_021a: Expected O, but got Ref
		//IL_0068: Expected O, but got Ref
		//IL_0288: Expected O, but got Ref
		//IL_0358: Expected O, but got I
		//IL_0368: Expected O, but got I
		//IL_0343: Expected O, but got I
		//IL_014c: Expected O, but got Ref
		float num = default(float);
		float num5 = default(float);
		IEnumerator routine;
		if (SpawnType != 0)
		{
			if (SpawnType != 1)
			{
				return;
			}
			Transform floatingText_Transform = m_floatingText.transform;
			m_floatingText_Transform = floatingText_Transform;
			Vector3 position = m_transform.position;
			m_floatingText_Transform.position = (Vector3)(&num);
			TextMesh textMesh = m_floatingText.AddComponent<TextMesh>();
			m_textMesh = textMesh;
			Font font = Resources.Load<Font>("Fonts/ARIAL");
			m_textMesh.font = font;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Font font2 = m_textMesh.font;
			Material material = font2.material;
			Renderer renderer = default(Renderer);
			renderer.SetMaterial(material);
			int num2 = UnityEngine.Random.Range(0, 255);
			int num3 = UnityEngine.Random.Range(0, 255);
			int num4 = UnityEngine.Random.Range(0, 255);
			m_textMesh.color = (Color)(&num5);
			m_textMesh.anchor = TextAnchor.LowerCenter;
			m_textMesh.fontSize = 24;
			IEnumerator enumerator = DisplayTextMeshFloatingText();
			routine = enumerator;
		}
		else
		{
			TextMeshPro textMeshPro = m_floatingText.AddComponent<TextMeshPro>();
			m_textMeshPro = textMeshPro;
			RectTransform rectTransform = m_textMeshPro.rectTransform;
			Vector2 sizeDelta = default(Vector2);
			rectTransform.sizeDelta = sizeDelta;
			Transform floatingText_Transform2 = m_floatingText.transform;
			m_floatingText_Transform = floatingText_Transform2;
			Vector3 position2 = m_transform.position;
			m_floatingText_Transform.position = (Vector3)(&num);
			m_textMeshPro.alignment = TextAlignmentOptions.Center;
			int num6 = UnityEngine.Random.Range(0, 255);
			int num7 = UnityEngine.Random.Range(0, 255);
			int num8 = UnityEngine.Random.Range(0, 255);
			m_textMeshPro.color = (Color)(&num5);
			m_textMeshPro.fontSize = 24f;
			TextMeshPro textMeshPro2 = m_textMeshPro;
			List<OTL_FeatureTag> activeFontFeatures = ((TMP_Text)textMeshPro2).m_ActiveFontFeatures;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ rbx_v6 (System.Collections.Generic.List`1<UnityEngine.TextCore.OTL_FeatureTag>)+1C]");
			_ = (nint)0 + (nint)1;
			if (!RuntimeHelpers.IsReferenceOrContainsReferences<OTL_FeatureTag>())
			{
				_ = 0;
			}
			else
			{
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ rbx_v6 (System.Collections.Generic.List`1<UnityEngine.TextCore.OTL_FeatureTag>)+18]");
				if ((nint)0 > (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ rbx_v6 (System.Collections.Generic.List`1<UnityEngine.TextCore.OTL_FeatureTag>)+10]");
					nint num9 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ rbx_v6 (System.Collections.Generic.List`1<UnityEngine.TextCore.OTL_FeatureTag>)+18]");
					Array.Clear((Array)num9, 0, 0);
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v818 @ rax_v27+B8]");
			object text = 0;
			m_textMeshPro.text = (string)text;
			m_textMeshPro.isTextObjectScaleStatic = IsTextObjectScaleStatic;
			_003CDisplayTextMeshProFloatingText_003Ed__15 obj2 = new _003CDisplayTextMeshProFloatingText_003Ed__15(0);
			obj2._003C_003E1__state = 0;
			obj2._003C_003E4__this = this;
			routine = obj2;
		}
		Coroutine coroutine = StartCoroutine(routine);
	}

	public IEnumerator DisplayTextMeshProFloatingText()
	{
		_003CDisplayTextMeshProFloatingText_003Ed__15 obj = new _003CDisplayTextMeshProFloatingText_003Ed__15(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public IEnumerator DisplayTextMeshFloatingText()
	{
		_003CDisplayTextMeshFloatingText_003Ed__16 obj = new _003CDisplayTextMeshFloatingText_003Ed__16(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public TextMeshProFloatingText()
	{
		//IL_0013: Expected I, but got O
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		lastPOS = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rdx_v1 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		lastRotation = Quaternion.identityQuaternion;
		base._002Ector();
	}

	static TextMeshProFloatingText()
	{
		WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
		k_WaitForEndOfFrame = waitForEndOfFrame;
		WaitForSeconds waitForSeconds = new WaitForSeconds(0.05f);
		WaitForSeconds waitForSeconds2 = new WaitForSeconds(0.1f);
		WaitForSeconds waitForSeconds3 = new WaitForSeconds(0.15f);
		WaitForSeconds waitForSeconds4 = new WaitForSeconds(0.2f);
		WaitForSeconds waitForSeconds5 = new WaitForSeconds(0.25f);
		WaitForSeconds waitForSeconds6 = new WaitForSeconds(0.3f);
		WaitForSeconds waitForSeconds7 = new WaitForSeconds(0.35f);
		WaitForSeconds waitForSeconds8 = new WaitForSeconds(0.4f);
		WaitForSeconds waitForSeconds9 = new WaitForSeconds(0.45f);
		WaitForSeconds waitForSeconds10 = new WaitForSeconds(0.5f);
		WaitForSeconds waitForSeconds11 = new WaitForSeconds(0.55f);
		WaitForSeconds waitForSeconds12 = new WaitForSeconds(0.6f);
		WaitForSeconds waitForSeconds13 = new WaitForSeconds(0.65f);
		WaitForSeconds waitForSeconds14 = new WaitForSeconds(0.7f);
		WaitForSeconds waitForSeconds15 = new WaitForSeconds(0.75f);
		WaitForSeconds waitForSeconds16 = new WaitForSeconds(0.8f);
		WaitForSeconds waitForSeconds17 = new WaitForSeconds(0.85f);
		WaitForSeconds waitForSeconds18 = new WaitForSeconds(0.9f);
		WaitForSeconds waitForSeconds19 = new WaitForSeconds(0.95f);
		WaitForSeconds waitForSeconds20 = new WaitForSeconds(1f);
		k_WaitForSecondsRandom = new WaitForSeconds[20]
		{
			waitForSeconds, waitForSeconds2, waitForSeconds3, waitForSeconds4, waitForSeconds5, waitForSeconds6, waitForSeconds7, waitForSeconds8, waitForSeconds9, waitForSeconds10,
			waitForSeconds11, waitForSeconds12, waitForSeconds13, waitForSeconds14, waitForSeconds15, waitForSeconds16, waitForSeconds17, waitForSeconds18, waitForSeconds19, waitForSeconds20
		};
	}
}
