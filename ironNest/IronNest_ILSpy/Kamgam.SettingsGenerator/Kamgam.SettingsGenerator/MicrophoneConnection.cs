using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class MicrophoneConnection : ConnectionWithOptions<string>, IConnectionWithSettingsAccess
{
	[StructLayout((LayoutKind)3)]
	private struct _003CstartStopPolling_003Ed__4 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public MicrophoneConnection _003C_003E4__this;

		private TaskAwaiter _003C_003Eu__1;

		private unsafe void MoveNext()
		{
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_01f8: Expected I4, but got I8
			//IL_0203: Expected O, but got Ref
			//IL_0065: Expected I4, but got O
			//IL_01d1: Expected O, but got Ref
			//IL_010d: Expected O, but got I4
			//IL_0137: Expected O, but got I4
			//IL_017d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0182: Expected O, but got Unknown
			MicrophoneConnection microphoneConnection = _003C_003E4__this;
			float num;
			TaskAwaiter awaiter = default(TaskAwaiter);
			if (_003C_003E1__state == 0)
			{
				_003C_003Eu__1 = (TaskAwaiter)0;
				_003C_003E1__state = -1;
				num = 0.01f;
				awaiter = _003C_003Eu__1;
				goto IL_00a7;
			}
			num = 0.01f;
			goto IL_023e;
			IL_023e:
			if (microphoneConnection._pollIntervalInSec > num)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si ecx,xmm6\"");
				Task task = Task.Delay((int)typeof(Task));
				TaskAwaiter awaiter2 = task.GetAwaiter();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D2870");
				object obj = default(object);
				if (obj != null)
				{
					goto IL_00a7;
				}
				_003C_003E1__state = 0;
				_003C_003Eu__1 = _003C_003Eu__1;
				AsyncVoidMethodBuilder asyncVoidMethodBuilder = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
				((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder)->AwaitUnsafeOnCompleted(ref awaiter, ref this);
				return;
			}
			goto IL_01e9;
			IL_01e9:
			_003C_003E1__state = -2;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder2)->SetResult();
			return;
			IL_00a7:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180CF3BB0");
			if (!(num < microphoneConnection._pollIntervalInSec))
			{
				goto IL_01e9;
			}
			string[] devices = Microphone.devices;
			List<string> values = microphoneConnection._values;
			object obj2 = values._size - 1;
			if (devices.Length == (nint)obj2)
			{
				object obj3 = 0;
				while ((nint)obj3 < devices.Length)
				{
					if (microphoneConnection._values.Contains(devices[obj3]))
					{
						obj3++;
						continue;
					}
					microphoneConnection.onDeviceListChanged();
					break;
				}
			}
			else
			{
				List<string> optionLabels = microphoneConnection.GetOptionLabels();
				microphoneConnection._settings.RefreshRegisteredResolversWithConnection<MicrophoneConnection>();
			}
			goto IL_023e;
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_000b: Expected O, but got Ref
			AsyncVoidMethodBuilder asyncVoidMethodBuilder = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	protected float _pollIntervalInSec = -1f;

	protected List<string> _values;

	protected List<string> _labels;

	protected Settings _settings;

	protected int _selectedDeviceIndex;

	public float PollIntervalInSec
	{
		get
		{
			return _pollIntervalInSec;
		}
		set
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj = default(object);
			if (obj == null)
			{
				_pollIntervalInSec = value;
				AsyncVoidMethodBuilder asyncVoidMethodBuilder = AsyncVoidMethodBuilder.Create();
				AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = default(AsyncVoidMethodBuilder);
				_003CstartStopPolling_003Ed__4 stateMachine = default(_003CstartStopPolling_003Ed__4);
				asyncVoidMethodBuilder2.Start(ref stateMachine);
			}
		}
	}

	private void startStopPolling()
	{
		AsyncVoidMethodBuilder asyncVoidMethodBuilder = AsyncVoidMethodBuilder.Create();
		AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = default(AsyncVoidMethodBuilder);
		_003CstartStopPolling_003Ed__4 stateMachine = default(_003CstartStopPolling_003Ed__4);
		asyncVoidMethodBuilder2.Start(ref stateMachine);
	}

	private void onDeviceListChanged()
	{
		List<string> optionLabels = GetOptionLabels();
		_settings.RefreshRegisteredResolversWithConnection<MicrophoneConnection>();
	}

	public MicrophoneConnection(float pollIntervalInSec = -1f)
	{
		base._002Ector();
		ConnectionWithOptions<string> connectionWithOptions = default(ConnectionWithOptions<string>);
		if (connectionWithOptions == null)
		{
			_pollIntervalInSec = pollIntervalInSec;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder = AsyncVoidMethodBuilder.Create();
			AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = default(AsyncVoidMethodBuilder);
			_003CstartStopPolling_003Ed__4 stateMachine = default(_003CstartStopPolling_003Ed__4);
			asyncVoidMethodBuilder2.Start(ref stateMachine);
		}
	}

	public override List<string> GetOptionLabels()
	{
		List<string> deviceNames = getDeviceNames();
		if (_labels != null)
		{
			List<string> labels = _labels;
			int version = labels._version + 1;
			labels._version = version;
			if (!RuntimeHelpers.IsReferenceOrContainsReferences<string>())
			{
				labels._size = 0;
			}
			else
			{
				labels._size = 0;
				if (labels._size > 0)
				{
					Array.Clear(labels._items, 0, labels._size);
				}
			}
		}
		else
		{
			List<string> labels2 = new List<string>();
			_labels = labels2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<string>.Enumerator enumerator = default(List<string>.Enumerator);
		string text = default(string);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (text != null)
				{
					if (_labels == null)
					{
						throw new NullReferenceException();
					}
					_labels.Add(text);
				}
				else
				{
					if (_labels == null)
					{
						break;
					}
					_labels.Add("Default");
				}
				continue;
			}
			enumerator.Dispose();
			return _labels;
		}
		throw new NullReferenceException();
	}

	public override void SetOptionLabels(List<string> optionLabels)
	{
		List<string> deviceNames = getDeviceNames();
		if (optionLabels != null && optionLabels._size == deviceNames._size)
		{
			List<string> labels = new List<string>(optionLabels);
			_labels = labels;
		}
		else
		{
			int num = default(int);
			string text = num.ToString();
			string message = "Invalid new labels. Need to be " + text + ".";
			Debug.LogError(message);
		}
	}

	public override void RefreshOptionLabels()
	{
		//IL_000c: Expected I, but got O
		//IL_001c: Expected O, but got I
		//IL_002c: Expected O, but got I
		_labels = null;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.MicrophoneConnection>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.MicrophoneConnection>)+2D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected List<string> getDeviceNames()
	{
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Expected O, but got Unknown
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Expected O, but got Unknown
		if (_values != null)
		{
			List<string> values = _values;
			int version = values._version + 1;
			values._version = version;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj = default(object);
			if (obj == null)
			{
				values._size = 0;
			}
			else
			{
				values._size = 0;
				if (values._size > 0)
				{
					Array.Clear(values._items, 0, values._size);
				}
			}
		}
		else
		{
			List<string> values2 = new List<string>();
			_values = values2;
		}
		_values.Add(null);
		string[] devices = Microphone.devices;
		object obj2 = devices + 32;
		int num = 0;
		int num2 = 0;
		while (true)
		{
			if (num2 < devices.Length)
			{
				if (num >= devices.Length)
				{
					break;
				}
				_values.Add((string)obj2);
				num++;
				obj2 += 8;
				num2 = num;
				continue;
			}
			return _values;
		}
		return (List<string>)(object)new IndexOutOfRangeException();
	}

	public override int Get()
	{
		return _selectedDeviceIndex;
	}

	public override void Set(int index)
	{
		//IL_0005: Expected I, but got O
		//IL_001f: Expected O, but got I
		//IL_002f: Expected O, but got I
		nint num = (nint)this;
		_selectedDeviceIndex = index;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.MicrophoneConnection>)+258]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.MicrophoneConnection>)+260]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v3 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public string GetSelectedDeviceName()
	{
		if (_values != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			string result = default(string);
			return result;
		}
		return (string)(object)new NullReferenceException();
	}

	public AudioClip StartRecording(bool loop, int lengthSec, int frequency)
	{
		if (_values != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			string deviceName = default(string);
			return Microphone.Start(deviceName, loop, lengthSec, frequency);
		}
		return (AudioClip)(object)new NullReferenceException();
	}

	public void EndRecording()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		string deviceName = default(string);
		Microphone.End(deviceName);
	}

	public bool IsRecording()
	{
		//IL_0034: Expected I4, but got O
		if (_values != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			string deviceName = default(string);
			return Microphone.IsRecording(deviceName);
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public int GetPosition()
	{
		//IL_0034: Expected I4, but got O
		if (_values != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			string deviceName = default(string);
			return Microphone.GetPosition(deviceName);
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public void GetDeviceCaps(out int minFreq, out int maxFreq)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		string deviceName = default(string);
		Microphone.GetDeviceCaps(deviceName, out minFreq, out maxFreq);
	}

	public void SetSettings(Settings settings)
	{
		_settings = settings;
	}

	public Settings GetSettings()
	{
		return _settings;
	}
}
