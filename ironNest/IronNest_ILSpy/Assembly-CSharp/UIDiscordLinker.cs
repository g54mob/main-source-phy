using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;

public class UIDiscordLinker : MonoBehaviour
{
	[StructLayout((LayoutKind)3)]
	private struct _003CGenerateNewKeyAsync_003Ed__8 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public UIDiscordLinker _003C_003E4__this;

		private MainThreadAwaitable _003C_003Eu__1;

		private TaskAwaiter<string> _003C_003Eu__2;

		private unsafe void MoveNext()
		{
			//IL_025d: Expected O, but got I4
			//IL_026c: Expected I4, but got I8
			//IL_0015: Expected O, but got I4
			//IL_0312: Expected I4, but got I8
			//IL_018d: Expected O, but got I4
			//IL_019c: Expected I4, but got I8
			//IL_028c: Expected O, but got Ref
			//IL_016e: Expected O, but got I4
			//IL_017d: Expected I4, but got I8
			//IL_023f: Expected O, but got Ref
			//IL_0150: Expected O, but got Ref
			//IL_00c5: Expected O, but got Ref
			UIDiscordLinker uIDiscordLinker = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				MainThreadAwaitable awaiter = default(MainThreadAwaitable);
				if (!flag)
				{
					if ((nint)obj == 1)
					{
						_003C_003Eu__1 = (MainThreadAwaitable)0;
						_003C_003E1__state = -1;
						goto IL_01ea;
					}
					RegisterResponse latestRegisterResponse = LeaderboardClient.LatestRegisterResponse;
					if (LeaderboardClient.LatestRegisterResponse != null && latestRegisterResponse._003CDiscordLinked_003Ek__BackingField)
					{
						MainThreadAwaitable mainThreadAwaitable = Awaitable.MainThreadAsync();
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180724280");
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu__1 = awaiter;
							AsyncTaskMethodBuilder asyncTaskMethodBuilder = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
							((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder)->AwaitOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0271;
					}
					uIDiscordLinker.ButtonRoot.SetActive(value: false);
					Task<string> task = LeaderboardClient.GenerateDiscordLinkKey();
					TaskAwaiter<string> awaiter2 = task.GetAwaiter();
					TaskAwaiter<string> awaiter3 = default(TaskAwaiter<string>);
					if (!awaiter3.IsCompleted)
					{
						_003C_003E1__state = 1;
						_003C_003Eu__2 = awaiter3;
						AsyncTaskMethodBuilder asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
						((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder2)->AwaitUnsafeOnCompleted(ref awaiter3, ref this);
						return;
					}
				}
				else
				{
					_003C_003Eu__2 = (TaskAwaiter<string>)0;
					_003C_003E1__state = -1;
					TaskAwaiter<string> awaiter3 = _003C_003Eu__2;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
				string key = default(string);
				Key = key;
				MainThreadAwaitable mainThreadAwaitable2 = Awaitable.MainThreadAsync();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180724280");
				if (awaiter.IsCompleted)
				{
					goto IL_01ea;
				}
				_003C_003E1__state = 2;
				_003C_003Eu__1 = awaiter;
				AsyncTaskMethodBuilder asyncTaskMethodBuilder3 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
				((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder3)->AwaitOnCompleted(ref awaiter, ref this);
				return;
			}
			_003C_003Eu__1 = (MainThreadAwaitable)0;
			_003C_003E1__state = -1;
			goto IL_0271;
			IL_0271:
			_003C_003E4__this.UpdateObjects();
			goto IL_0303;
			IL_0303:
			_003C_003E1__state = -2;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder4 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder4)->SetResult();
			return;
			IL_01ea:
			uIDiscordLinker.Input_DiscordKey.text = Key;
			uIDiscordLinker.ButtonRoot.SetActive(value: false);
			goto IL_0303;
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_0010: Expected O, but got Ref
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	public TMP_InputField Input_DiscordKey;

	public GameObject InputRoot;

	public GameObject ButtonRoot;

	public GameObject LinkedTextRoot;

	private static string Key;

	private void OnEnable()
	{
		UpdateObjects();
	}

	public void UpdateObjects()
	{
		RegisterResponse latestRegisterResponse = LeaderboardClient.LatestRegisterResponse;
		bool flag = LeaderboardClient.LatestRegisterResponse != null && latestRegisterResponse._003CDiscordLinked_003Ek__BackingField;
		bool flag2 = !flag;
		bool active = !flag2;
		LinkedTextRoot.SetActive(active);
		bool active2 = !flag;
		ButtonRoot.SetActive(active2);
		bool active3 = !flag;
		InputRoot.SetActive(active3);
		Input_DiscordKey.text = Key;
		if (!string.IsNullOrEmpty(Key))
		{
			ButtonRoot.SetActive(value: false);
		}
	}

	public void GenerateNewKey()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
		AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
		_003CGenerateNewKeyAsync_003Ed__8 stateMachine = default(_003CGenerateNewKeyAsync_003Ed__8);
		asyncTaskMethodBuilder.Start(ref stateMachine);
		Task task = asyncTaskMethodBuilder.Task;
	}

	public Task GenerateNewKeyAsync()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
		AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
		_003CGenerateNewKeyAsync_003Ed__8 stateMachine = default(_003CGenerateNewKeyAsync_003Ed__8);
		asyncTaskMethodBuilder.Start(ref stateMachine);
		return asyncTaskMethodBuilder.Task;
	}
}
