using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using Kamgam.UGUIComponentsForSettings;
using UnityEngine;
using UnityEngine.UI;

public class UIImageByteCycler : MonoBehaviour
{
	private sealed class _003CCycleRoutine_003Ed__25 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UIImageByteCycler _003C_003E4__this;

		private WaitForSecondsRealtime _003Cwait_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCycleRoutine_003Ed__25(int _003C_003E1__state)
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
			//IL_00a9: Expected I4, but got I8
			//IL_0221: Expected I4, but got O
			UIImageByteCycler uIImageByteCycler = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					bool flag = !(0.01f < uIImageByteCycler.cycleInterval);
					float time = 0.01f;
					if (!flag)
					{
						time = uIImageByteCycler.cycleInterval;
					}
					WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(time);
					_003Cwait_003E5__2 = waitForSecondsRealtime;
					goto IL_016d;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_020d;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					List<Sprite> sprites = uIImageByteCycler.sprites;
					int num = ++uIImageByteCycler.currentIndex;
					if (uIImageByteCycler.sprites != null)
					{
						if (num >= sprites._size)
						{
							if (!uIImageByteCycler.loop)
							{
								if (uIImageByteCycler.sprites != null)
								{
									int currentIndex = sprites._size - 1;
									uIImageByteCycler.currentIndex = currentIndex;
									_003C_003E4__this.ShowCurrentFrame();
									int sliderFrameSilently = uIImageByteCycler.currentIndex + 1;
									_003C_003E4__this.SetSliderFrameSilently(sliderFrameSilently);
									uIImageByteCycler.cycleRoutine = null;
									goto IL_020d;
								}
								goto IL_0213;
							}
							uIImageByteCycler.currentIndex = 0;
						}
						_003C_003E4__this.ShowCurrentFrame();
						int sliderFrameSilently2 = uIImageByteCycler.currentIndex + 1;
						_003C_003E4__this.SetSliderFrameSilently(sliderFrameSilently2);
						goto IL_016d;
					}
				}
			}
			goto IL_0213;
			IL_016d:
			_003C_003E2__current = _003Cwait_003E5__2;
			_003C_003E1__state = 1;
			return true;
			IL_020d:
			return false;
			IL_0213:
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

	private sealed class _003CLoadAsyncRoutine_003Ed__26 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UIImageByteCycler _003C_003E4__this;

		public IReadOnlyList<byte[]> imageBytes;

		private int _003CloadedThisFrame_003E5__2;

		private int _003CframesPerFrame_003E5__3;

		private int _003Ci_003E5__4;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CLoadAsyncRoutine_003Ed__26(int _003C_003E1__state)
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
			//IL_00af: Expected I4, but got I8
			//IL_0500: Expected I4, but got O
			//IL_0110: Expected I, but got O
			//IL_0148: Expected O, but got I
			//IL_0553: Expected O, but got I4
			//IL_02f2: Expected O, but got I4
			//IL_0308: Expected O, but got I
			//IL_0311: Unknown result type (might be due to invalid IL or missing references)
			//IL_0316: Expected O, but got Unknown
			//IL_031e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0323: Expected O, but got Unknown
			UIImageByteCycler uIImageByteCycler = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_003CloadedThisFrame_003E5__2 = 0;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_04f2;
				}
				bool flag = uIImageByteCycler.FramesLoadedPerFrame >= 1;
				bool flag2 = (byte)uIImageByteCycler.FramesLoadedPerFrame != 0;
				if (!flag)
				{
					flag2 = true;
				}
				_003CframesPerFrame_003E5__3 = (flag2 ? 1 : 0);
				_003Ci_003E5__4 = 0;
				goto IL_05fd;
			}
			if (_003C_003E1__state != 1)
			{
				goto IL_04e4;
			}
			_003C_003E1__state = -1;
			goto IL_057d;
			IL_04f2:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_04e4:
			return false;
			IL_05fd:
			if (imageBytes != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj = default(object);
				if (_003Ci_003E5__4 < (nint)obj)
				{
					IReadOnlyList<byte[]> readOnlyList = imageBytes;
					if (imageBytes != null)
					{
						nint num = (nint)readOnlyList;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v100 @ r10_v2 (Il2CppClass<System.Collections.Generic.IReadOnlyList`1<System.Byte[]>>)+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_0188;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v100 @ r10_v2 (Il2CppClass<System.Collections.Generic.IReadOnlyList`1<System.Byte[]>>)+B0]");
						object obj2 = 0;
						int num2 = 0;
						while (true)
						{
							object obj3 = num2 + num2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v418 @ r8_v11+v421 @ rax_v33*8]");
							if (0 == (nint)typeof(IReadOnlyList<byte[]>))
							{
								break;
							}
							num2++;
							int num3 = num2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v100 @ r10_v2 (Il2CppClass<System.Collections.Generic.IReadOnlyList`1<System.Byte[]>>)+12E]");
							if ((nint)num3 < (nint)0)
							{
								continue;
							}
							goto IL_0188;
						}
						object obj4 = num2 + num2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v418 @ r8_v11+8+v479 @ rcx_v27*8]");
						object obj5 = (nint)0 << 4;
						object obj6 = obj5 + 312;
						object obj7 = obj6 + num;
						goto IL_0197;
					}
				}
				else if ((object)_003C_003E4__this != null)
				{
					uIImageByteCycler.loadRoutine = null;
					List<Sprite> sprites = uIImageByteCycler.sprites;
					if (uIImageByteCycler.sprites != null)
					{
						if (sprites._size != 0)
						{
							_003C_003E4__this.ConfigureSlider();
							List<Sprite> sprites2 = uIImageByteCycler.sprites;
							if (uIImageByteCycler.sprites == null)
							{
								goto IL_04f2;
							}
							int num4 = uIImageByteCycler.currentIndex;
							if (uIImageByteCycler.currentIndex >= 0)
							{
								int num5 = sprites2._size - 1;
								if (num4 > num5)
								{
									num4 = num5;
								}
							}
							else
							{
								num4 = 0;
							}
							uIImageByteCycler.currentIndex = num4;
							_003C_003E4__this.ShowCurrentFrame();
							int sliderFrameSilently = uIImageByteCycler.currentIndex + 1;
							_003C_003E4__this.SetSliderFrameSilently(sliderFrameSilently);
							if (uIImageByteCycler.playOnLoad)
							{
								List<Sprite> sprites3 = uIImageByteCycler.sprites;
								if (uIImageByteCycler.sprites == null)
								{
									goto IL_04f2;
								}
								if (sprites3._size > 1 && _003C_003E4__this.isActiveAndEnabled)
								{
									_003C_003E4__this.Play();
								}
							}
						}
						goto IL_04e4;
					}
				}
			}
			goto IL_04f2;
			IL_0197:
			byte[] bytes = imageBytes.get_Item(_003Ci_003E5__4);
			if ((object)_003C_003E4__this != null)
			{
				bool flag3 = _003C_003E4__this.TryLoadFrame(bytes);
				List<Sprite> sprites4 = uIImageByteCycler.sprites;
				if (uIImageByteCycler.sprites != null)
				{
					if (sprites4._size == 1)
					{
						uIImageByteCycler.currentIndex = 0;
						_003C_003E4__this.ConfigureSlider();
						_003C_003E4__this.ShowCurrentFrame();
						int sliderFrameSilently2 = uIImageByteCycler.currentIndex + 1;
						_003C_003E4__this.SetSliderFrameSilently(sliderFrameSilently2);
					}
					if (++_003CloadedThisFrame_003E5__2 >= _003CframesPerFrame_003E5__3)
					{
						_003CloadedThisFrame_003E5__2 = 0;
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
					goto IL_057d;
				}
			}
			goto IL_04f2;
			IL_057d:
			int num6 = _003Ci_003E5__4 + 1;
			_003Ci_003E5__4 = num6;
			goto IL_05fd;
			IL_0188:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0197;
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

	public Image targetImage;

	public SliderUGUI Slider_FrameCounter;

	public float cycleInterval = 0.333f;

	public bool playOnLoad = true;

	public bool loop;

	public int FramesLoadedPerFrame = 1;

	private readonly List<Texture2D> textures;

	private readonly List<Sprite> sprites;

	private Coroutine loadRoutine;

	private Coroutine cycleRoutine;

	private int currentIndex;

	private void OnEnable()
	{
		if (playOnLoad)
		{
			List<Sprite> list = sprites;
			if (list._size > 1)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 61 Invalid \"Jump target not found in method: 0x1804AB7E0\"");
			}
		}
	}

	private void OnDisable()
	{
		if (cycleRoutine != null)
		{
			StopCoroutine(cycleRoutine);
			cycleRoutine = null;
		}
		if (loadRoutine != null)
		{
			StopCoroutine(loadRoutine);
			loadRoutine = null;
		}
	}

	private void OnDestroy()
	{
		if (cycleRoutine != null)
		{
			StopCoroutine(cycleRoutine);
			cycleRoutine = null;
		}
		if (loadRoutine != null)
		{
			StopCoroutine(loadRoutine);
			loadRoutine = null;
		}
		ClearLoadedFrames();
	}

	public void Load(byte[][] imageBytes)
	{
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		//IL_00ad: Expected O, but got I4
		//IL_00b6: Expected O, but got I4
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Expected O, but got Unknown
		if (cycleRoutine != null)
		{
			StopCoroutine(cycleRoutine);
			cycleRoutine = null;
		}
		if (loadRoutine != null)
		{
			StopCoroutine(loadRoutine);
			loadRoutine = null;
		}
		ClearLoadedFrames();
		if (imageBytes == null || imageBytes.Length == 0)
		{
			return;
		}
		object obj = imageBytes + 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj3 < imageBytes.Length)
		{
			bool flag = TryLoadFrame((byte[])obj);
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
		List<Sprite> list = sprites;
		if (list._size == 0)
		{
			return;
		}
		ConfigureSlider();
		currentIndex = 0;
		if (targetImage != null)
		{
			List<Sprite> list2 = sprites;
			if (list2._size != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Sprite sprite = default(Sprite);
				targetImage.sprite = sprite;
				targetImage.preserveAspect = true;
			}
		}
		int sliderFrameSilently = currentIndex + 1;
		SetSliderFrameSilently(sliderFrameSilently);
		if (playOnLoad)
		{
			List<Sprite> list3 = sprites;
			if (list3._size > 1)
			{
				Play();
			}
		}
	}

	public void LoadAsync(IReadOnlyList<byte[]> imageBytes)
	{
		if (cycleRoutine != null)
		{
			StopCoroutine(cycleRoutine);
			cycleRoutine = null;
		}
		if (loadRoutine != null)
		{
			StopCoroutine(loadRoutine);
			loadRoutine = null;
		}
		ClearLoadedFrames();
		if (imageBytes != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj = default(object);
			if (obj != null)
			{
				List<byte[]> imageBytes2 = new List<byte[]>(imageBytes);
				_003CLoadAsyncRoutine_003Ed__26 obj2 = new _003CLoadAsyncRoutine_003Ed__26(0);
				obj2._003C_003E1__state = 0;
				obj2._003C_003E4__this = this;
				obj2.imageBytes = imageBytes2;
				Coroutine coroutine = StartCoroutine(obj2);
				loadRoutine = coroutine;
			}
		}
	}

	public void Load(IEnumerable<byte[]> imageBytes)
	{
		if (imageBytes != null)
		{
			List<byte[]> list = new List<byte[]>(imageBytes);
			byte[][] imageBytes2 = list.ToArray();
			Load(imageBytes2);
			return;
		}
		if ((object)cycleRoutine != imageBytes)
		{
			StopCoroutine(cycleRoutine);
			cycleRoutine = (Coroutine)(object)imageBytes;
		}
		if ((object)loadRoutine != imageBytes)
		{
			StopCoroutine(loadRoutine);
			loadRoutine = (Coroutine)(object)imageBytes;
		}
		ClearLoadedFrames();
	}

	public void LoadAsync(IEnumerable<byte[]> imageBytes)
	{
		if (imageBytes != null)
		{
			List<byte[]> list = new List<byte[]>(imageBytes);
			if (cycleRoutine != null)
			{
				StopCoroutine(cycleRoutine);
				cycleRoutine = null;
			}
			if (loadRoutine != null)
			{
				StopCoroutine(loadRoutine);
				loadRoutine = null;
			}
			ClearLoadedFrames();
			if (list != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj = default(object);
				if (obj != null)
				{
					List<byte[]> imageBytes2 = new List<byte[]>(list);
					_003CLoadAsyncRoutine_003Ed__26 obj2 = new _003CLoadAsyncRoutine_003Ed__26(0);
					obj2._003C_003E1__state = 0;
					obj2._003C_003E4__this = this;
					obj2.imageBytes = imageBytes2;
					Coroutine coroutine = StartCoroutine(obj2);
					loadRoutine = coroutine;
				}
			}
		}
		else
		{
			if ((object)cycleRoutine != imageBytes)
			{
				StopCoroutine(cycleRoutine);
				cycleRoutine = (Coroutine)(object)imageBytes;
			}
			if ((object)loadRoutine != imageBytes)
			{
				StopCoroutine(loadRoutine);
				loadRoutine = (Coroutine)(object)imageBytes;
			}
			ClearLoadedFrames();
		}
	}

	public void Play()
	{
		if (cycleRoutine != null)
		{
			StopCoroutine(cycleRoutine);
			cycleRoutine = null;
		}
		if (base.isActiveAndEnabled)
		{
			List<Sprite> list = sprites;
			if (list._size > 1)
			{
				_003CCycleRoutine_003Ed__25 obj = new _003CCycleRoutine_003Ed__25(0);
				obj._003C_003E1__state = 0;
				obj._003C_003E4__this = this;
				Coroutine coroutine = StartCoroutine(obj);
				cycleRoutine = coroutine;
			}
		}
	}

	public void Stop()
	{
		if (cycleRoutine != null)
		{
			StopCoroutine(cycleRoutine);
			cycleRoutine = null;
		}
	}

	public void Clear()
	{
		if (cycleRoutine != null)
		{
			StopCoroutine(cycleRoutine);
			cycleRoutine = null;
		}
		if (loadRoutine != null)
		{
			StopCoroutine(loadRoutine);
			loadRoutine = null;
		}
		ClearLoadedFrames();
	}

	private unsafe void ClearLoadedFrames()
	{
		//IL_0384: Expected O, but got Ref
		//IL_01d8: Expected O, but got I
		//IL_01a8: Expected O, but got I
		Image image;
		if (targetImage != null)
		{
			image = targetImage;
			if ((object)targetImage == null)
			{
				goto IL_02da;
			}
			targetImage.sprite = null;
		}
		image = (Image)(object)sprites;
		if (sprites != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<Sprite>.Enumerator enumerator = default(List<Sprite>.Enumerator);
			UnityEngine.Object obj = default(UnityEngine.Object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj != null)
				{
					UnityEngine.Object.Destroy(obj);
				}
			}
			enumerator.Dispose();
			if (textures != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<Texture2D>.Enumerator enumerator2 = default(List<Texture2D>.Enumerator);
				while (enumerator2.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if (obj != null)
					{
						UnityEngine.Object.Destroy(obj);
					}
				}
				enumerator2.Dispose();
				List<Sprite> list = sprites;
				bool flag = sprites == null;
				image = (Image)(&enumerator2);
				if (!flag)
				{
					int version = list._version + 1;
					list._version = version;
					((List<Texture2D>.Enumerator*)null)->Dispose();
					object obj2 = default(object);
					if (obj2 == null)
					{
						list._size = 0;
						image = (Image)0;
					}
					else
					{
						list._size = 0;
						bool flag2 = list._size <= 0;
						image = (Image)0;
						if (!flag2)
						{
							Array.Clear(list._items, 0, list._size);
						}
					}
					List<Texture2D> list2 = textures;
					if (textures != null)
					{
						int version2 = list2._version + 1;
						list2._version = version2;
						if (!RuntimeHelpers.IsReferenceOrContainsReferences<Texture2D>())
						{
							list2._size = 0;
						}
						else
						{
							list2._size = 0;
							if (list2._size > 0)
							{
								Array.Clear(list2._items, 0, list2._size);
							}
						}
						currentIndex = 0;
						return;
					}
				}
			}
		}
		goto IL_02da;
		IL_02da:
		throw new NullReferenceException();
	}

	public void SetFrame(int index)
	{
		List<Sprite> list = sprites;
		if (list._size == 0)
		{
			return;
		}
		int num2;
		if (index >= 0)
		{
			int num = list._size - 1;
			bool flag = index <= num;
			num2 = index;
			if (!flag)
			{
				num2 = num;
			}
		}
		else
		{
			num2 = 0;
		}
		currentIndex = num2;
		ShowCurrentFrame();
		int sliderFrameSilently = currentIndex + 1;
		SetSliderFrameSilently(sliderFrameSilently);
	}

	public void JumpToFrameAndPause(float frameNumber)
	{
		//IL_0298: Expected I, but got O
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		//IL_0284: Expected F8, but got I4
		//IL_02ac: Expected I4, but got F8
		nint num = (nint)typeof(Math);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm6,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm6,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rcx_v2 (Il2CppClass<System.Math>)+E4]");
		double num2;
		double num3 = default(double);
		double num4;
		if ((nint)0 >= (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EEB0");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804AAEC5h\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rcx_v2 (Il2CppClass<System.Math>)+E4]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm1\"");
				num2 = Math.Floor(frameNumber);
				goto IL_0196;
			}
			object obj = num3 & 1;
			bool flag = obj == null;
			num4 = num3;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,qword ptr [182206E88h]\"");
				num4 = num3;
			}
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EEB0");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,qword ptr [182206D70h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804AAEFDh\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rcx_v2 (Il2CppClass<System.Math>)+E4]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,qword ptr [182206D18h]\"");
				num2 = Math.Ceiling(frameNumber);
				goto IL_0196;
			}
			object obj2 = num3 & 1;
			bool flag2 = obj2 == null;
			num4 = num3;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,qword ptr [182206E88h]\"");
				num4 = num3;
			}
		}
		goto IL_02d1;
		IL_02d1:
		List<Sprite> list = sprites;
		if (list._size == 0)
		{
			return;
		}
		if (cycleRoutine != null)
		{
			StopCoroutine(cycleRoutine);
			cycleRoutine = null;
		}
		List<Sprite> list2 = sprites;
		bool flag3 = (nint)sprites < 0;
		double num5 = num4 - 1.0;
		if (!flag3)
		{
			double num6 = (double)list2._size - 1.0;
			if (num5 > num6)
			{
				num5 = num6;
			}
		}
		else
		{
			num5 = 0.0;
		}
		currentIndex = (int)num5;
		ShowCurrentFrame();
		int sliderFrameSilently = currentIndex + 1;
		SetSliderFrameSilently(sliderFrameSilently);
		return;
		IL_0196:
		num4 = num2;
		goto IL_02d1;
	}

	public void JumpToFrameAndPause(int frameNumber)
	{
		List<Sprite> list = sprites;
		if (list._size == 0)
		{
			return;
		}
		if (cycleRoutine != null)
		{
			StopCoroutine(cycleRoutine);
			cycleRoutine = null;
		}
		List<Sprite> list2 = sprites;
		int num = frameNumber - 1;
		if (num >= 0)
		{
			int num2 = list2._size - 1;
			if (num > num2)
			{
				num = num2;
			}
		}
		else
		{
			num = 0;
		}
		currentIndex = num;
		ShowCurrentFrame();
		int sliderFrameSilently = currentIndex + 1;
		SetSliderFrameSilently(sliderFrameSilently);
	}

	private IEnumerator CycleRoutine()
	{
		_003CCycleRoutine_003Ed__25 obj = new _003CCycleRoutine_003Ed__25(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator LoadAsyncRoutine(IReadOnlyList<byte[]> imageBytes)
	{
		_003CLoadAsyncRoutine_003Ed__26 obj = new _003CLoadAsyncRoutine_003Ed__26(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.imageBytes = imageBytes;
		return obj;
	}

	private unsafe bool TryLoadFrame(byte[] bytes)
	{
		//IL_020c: Expected I4, but got O
		//IL_010f: Expected O, but got Ref
		if (bytes != null && bytes.Length != 0)
		{
			bool mipChain = default(bool);
			Texture2D texture2D = new Texture2D(2, 2, TextureFormat.RGBA32, mipChain);
			if (ImageConversion.LoadImage(texture2D, bytes, markNonReadable: false))
			{
				if (textures != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					string text = $"UIImageFrame_{arg}";
					if ((object)texture2D != null)
					{
						texture2D.name = text;
						int width = texture2D.width;
						int height = texture2D.height;
						object obj = default(object);
						Vector2 pivot = default(Vector2);
						Sprite sprite = Sprite.Create(texture2D, (Rect)(&obj), pivot, 100f);
						if (sprites != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							object arg2 = default(object);
							string text2 = $"UIImageFrame_{arg2}";
							if ((object)sprite != null)
							{
								sprite.name = text2;
								if (textures != null)
								{
									textures.Add(texture2D);
									if (sprites != null)
									{
										sprites.Add(sprite);
										return true;
									}
								}
							}
						}
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			UnityEngine.Object.Destroy(texture2D);
			return false;
		}
		return false;
	}

	private void StopLoading()
	{
		if (loadRoutine != null)
		{
			StopCoroutine(loadRoutine);
			loadRoutine = null;
		}
	}

	private void ConfigureSlider()
	{
		//IL_0066: Expected F4, but got I4
		//IL_007d: Expected F4, but got I4
		if (Slider_FrameCounter != null)
		{
			Slider_FrameCounter.MinValue = 1f;
			List<Sprite> list = sprites;
			bool flag = list._size >= 1;
			float maxValue = list._size;
			if (!flag)
			{
				maxValue = 1f;
			}
			Slider_FrameCounter.MaxValue = maxValue;
			SliderUGUI slider_FrameCounter = Slider_FrameCounter;
			slider_FrameCounter.StepSize = 1f;
			slider_FrameCounter.WholeNumbers = true;
		}
	}

	private void ShowCurrentFrame()
	{
		if (targetImage != null)
		{
			List<Sprite> list = sprites;
			if (list._size != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Sprite sprite = default(Sprite);
				targetImage.sprite = sprite;
				targetImage.preserveAspect = true;
			}
		}
	}

	private void SetSliderFrameSilently(int frameNumber)
	{
		//IL_004e: Invalid comparison between F4 and I4
		//IL_006a: Invalid comparison between I4 and F4
		//IL_007b: Expected F4, but got I4
		if (!(Slider_FrameCounter != null))
		{
			return;
		}
		float minValue = Slider_FrameCounter.MinValue;
		float maxValue = Slider_FrameCounter.MaxValue;
		float num;
		if (!(minValue > (float)frameNumber))
		{
			bool flag = !((float)frameNumber > maxValue);
			num = frameNumber;
			if (!flag)
			{
				num = maxValue;
			}
		}
		else
		{
			num = minValue;
		}
		SliderUGUI slider_FrameCounter = Slider_FrameCounter;
		bool flag2 = slider_FrameCounter.Slider != null;
		SliderUGUI slider_FrameCounter2 = Slider_FrameCounter;
		if (!flag2)
		{
			slider_FrameCounter2.Value = num;
			return;
		}
		slider_FrameCounter2.Slider.SetValueWithoutNotify(num);
		Slider_FrameCounter.UpdateText();
	}

	public UIImageByteCycler()
	{
		List<Texture2D> list = new List<Texture2D>();
		textures = list;
		sprites = new List<Sprite>();
		base._002Ector();
	}
}
