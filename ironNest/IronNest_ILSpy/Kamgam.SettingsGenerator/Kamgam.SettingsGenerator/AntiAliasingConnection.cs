using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class AntiAliasingConnection : ConnectionWithOptions<string>
{
	public bool LimitToMainCamera;

	public bool IncludeMSAA;

	protected MSAAConnection _msaaConnection;

	protected List<string> _labels;

	public MSAAConnection MsaaConnection
	{
		get
		{
			if (_msaaConnection == null)
			{
				MSAAConnection msaaConnection = new MSAAConnection();
				_msaaConnection = msaaConnection;
			}
			return _msaaConnection;
		}
	}

	public unsafe AntiAliasingConnection()
	{
		//IL_0085: Expected I, but got O
		//IL_011b: Expected I, but got I8
		//IL_014c: Expected I, but got O
		//IL_0226: Expected I, but got I8
		//IL_00e1: Expected I, but got I8
		//IL_029b: Expected I, but got O
		//IL_02c6: Expected I, but got O
		//IL_02d4: Expected I, but got O
		base._002Ector();
		CameraDetector instance = CameraDetector.Instance;
		if (!(instance != null))
		{
			return;
		}
		CameraDetector instance2 = CameraDetector.Instance;
		CameraDetector.OnNewCameraFoundDelegate onNewCameraFoundDelegate = null;
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v99 @ rbx_v7 (Il2CppMethodInfo)+8]");
		((Delegate)onNewCameraFoundDelegate).method_ptr = (IntPtr)0;
		((Delegate)onNewCameraFoundDelegate).method = (nint)__ldftn(AntiAliasingConnection.onNewCameraFound);
		((Delegate)onNewCameraFoundDelegate).m_target = this;
		((Delegate)onNewCameraFoundDelegate).method_code = (IntPtr)onNewCameraFoundDelegate;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66E0");
		object obj = default(object);
		nint invoke_impl;
		if (obj != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v99 @ rbx_v7 (Il2CppMethodInfo)+52]");
			if ((nint)0 != 1)
			{
				goto IL_013a;
			}
			invoke_impl = unchecked((nint)6442459232L);
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v99 @ rbx_v7 (Il2CppMethodInfo)+52]");
			if ((nint)0 != 0)
			{
				if (this != null)
				{
					goto IL_013a;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6F80");
				IntPtr intPtr = default(IntPtr);
				throw intPtr;
			}
			invoke_impl = unchecked((nint)6442459120L);
		}
		goto IL_0207;
		IL_0207:
		((Delegate)onNewCameraFoundDelegate).invoke_impl = invoke_impl;
		((Delegate)onNewCameraFoundDelegate).extra_arg = unchecked((nint)6442458752L);
		Delegate obj2 = Delegate.Combine(instance2.OnNewCameraFound, onNewCameraFoundDelegate);
		if ((object)obj2 == null)
		{
			instance2.OnNewCameraFound = null;
			return;
		}
		bool flag = (object)obj2.GetType() != typeof(CameraDetector.OnNewCameraFoundDelegate);
		Delegate obj3 = null;
		if (!flag)
		{
			obj3 = obj2;
		}
		bool flag2 = (object)obj3 == null;
		nint num2 = (nint)typeof(CameraDetector.OnNewCameraFoundDelegate);
		if (!flag2)
		{
			instance2.OnNewCameraFound = (CameraDetector.OnNewCameraFoundDelegate)obj3;
			bool flag3 = (object)obj2.GetType() != typeof(CameraDetector.OnNewCameraFoundDelegate);
			Delegate obj4 = null;
			if (!flag3)
			{
				obj4 = obj2;
			}
			bool flag4 = (object)obj4 == null;
			num2 = (nint)typeof(CameraDetector.OnNewCameraFoundDelegate);
			nint num3 = (nint)typeof(CameraDetector.OnNewCameraFoundDelegate);
			if (!flag4)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		return;
		IL_013a:
		((Delegate)onNewCameraFoundDelegate).method_code = (IntPtr)((Delegate)onNewCameraFoundDelegate).m_target;
		invoke_impl = ((Delegate)onNewCameraFoundDelegate).method_ptr;
		goto IL_0207;
	}

	protected void onNewCameraFound(Camera cam)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 2 Invalid \"Jump target not found in method: 0x180A06C60\"");
	}

	public override List<string> GetOptionLabels()
	{
		if (_labels == null)
		{
			List<string> labels = new List<string>();
			_labels = labels;
			if (_labels != null)
			{
				_labels.Add("Disabled");
				if (_labels != null)
				{
					_labels.Add("FAA");
					if (_labels != null)
					{
						_labels.Add("SMAA");
						if (_labels != null)
						{
							_labels.Add("TAA");
							if (!IncludeMSAA)
							{
								goto IL_018b;
							}
							if (_labels != null)
							{
								_labels.Add("MSAA 2x");
								if (_labels != null)
								{
									_labels.Add("MSAA 4x");
									if (_labels != null)
									{
										_labels.Add("MSAA 8x");
										goto IL_018b;
									}
								}
							}
						}
					}
				}
			}
			return (List<string>)(object)new NullReferenceException();
		}
		goto IL_018b;
		IL_018b:
		return _labels;
	}

	public override void SetOptionLabels(List<string> optionLabels)
	{
		if (optionLabels != null && optionLabels._size == 3)
		{
			_labels = optionLabels;
		}
		else
		{
			Debug.LogError("Invalid new labels. Need to be three.");
		}
	}

	public override void RefreshOptionLabels()
	{
		//IL_000c: Expected I, but got O
		//IL_001c: Expected O, but got I
		//IL_002c: Expected O, but got I
		_labels = null;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.AntiAliasingConnection>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.AntiAliasingConnection>)+2D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override int Get()
	{
		//IL_01bf: Expected I4, but got O
		//IL_014f: Expected O, but got I
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Expected O, but got Unknown
		Camera main = Camera.main;
		if (main != null)
		{
			Camera main2 = Camera.main;
			if ((object)main2 == null)
			{
				goto IL_01b1;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			UnityEngine.Object obj = default(UnityEngine.Object);
			bool flag = obj == null;
			if (!flag)
			{
				if (IncludeMSAA != flag)
				{
					MSAAConnection msaaConnection = MsaaConnection;
					if (msaaConnection == null)
					{
						goto IL_01b1;
					}
					int num = msaaConnection.Get();
					if (num > 0)
					{
						return num + 3;
					}
				}
				if ((object)obj == null)
				{
					goto IL_01b1;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ stack_18_v3 (UnityEngine.Object)+50]");
				bool flag2 = (nint)0 == 0;
				if (!flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ stack_18_v3 (UnityEngine.Object)+50]");
					object obj2 = -1;
					if (flag2)
					{
						return 1;
					}
					object obj3 = obj2 - 1;
					if (flag2)
					{
						return 2;
					}
					if ((nint)obj3 == 1)
					{
						return 3;
					}
				}
			}
		}
		return 0;
		IL_01b1:
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public override void Set(int index)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_0025: Expected O, but got I4
		//IL_002e: Expected O, but got I4
		//IL_00db: Expected I, but got O
		//IL_00eb: Expected O, but got I
		//IL_00fb: Expected O, but got I
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		//IL_00a8: Expected O, but got I4
		Camera[] allCameras = Camera.allCameras;
		object obj = allCameras + 32;
		object obj2 = 0;
		object obj3 = 0;
		while (true)
		{
			if ((nint)obj2 < allCameras.Length)
			{
				GameObject gameObject = ((Component)obj).gameObject;
				if (gameObject.activeInHierarchy && ((Behaviour)obj).isActiveAndEnabled)
				{
					setOnCamera((Camera)obj, index);
					object obj4 = 0;
				}
				obj3++;
				obj += 8;
				obj2 = obj3;
			}
			else
			{
				nint num = (nint)this;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v140 @ r8_v5 (Il2CppClass<Kamgam.SettingsGenerator.AntiAliasingConnection>)+258]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v140 @ r8_v5 (Il2CppClass<Kamgam.SettingsGenerator.AntiAliasingConnection>)+260]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v143 @ rax_v7 (should have been resolved before IL gen)");
			}
		}
	}

	private void setOnCamera(Camera cam, int index)
	{
		if (LimitToMainCamera)
		{
			Camera main = Camera.main;
			if (!(cam == main))
			{
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		UnityEngine.Object obj = default(UnityEngine.Object);
		if (!(obj != null))
		{
			return;
		}
		switch (index)
		{
		case 2:
			_ = 2;
			break;
		case 1:
			_ = 1;
			break;
		case 0:
			_ = 0;
			break;
		}
		if (IncludeMSAA)
		{
			int num = index + -3;
			if (num <= 0)
			{
				num = 0;
			}
			if (num > 0)
			{
				_ = 0;
			}
			MSAAConnection msaaConnection = MsaaConnection;
			msaaConnection.Set(num);
		}
	}
}
