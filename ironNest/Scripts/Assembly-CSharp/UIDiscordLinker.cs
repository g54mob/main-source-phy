using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UIDiscordLinker : MonoBehaviour
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CGenerateNewKeyAsync_003Ed__8 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public UIDiscordLinker _003C_003E4__this;

		private MainThreadAwaitable _003C_003Eu__1;

		private TaskAwaiter<string> _003C_003Eu__2;

		private void MoveNext()
		{
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
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
	}

	public void UpdateObjects()
	{
	}

	public void GenerateNewKey()
	{
	}

	[AsyncStateMachine(typeof(_003CGenerateNewKeyAsync_003Ed__8))]
	public Task GenerateNewKeyAsync()
	{
		return null;
	}
}
