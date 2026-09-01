using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLobbyData), "Chat", null)]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLobbyData))]
	public class SteamLobbyChatUI : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CForceScrollDown_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public SteamLobbyChatUI _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CForceScrollDown_003Ed__17(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CSelectInputField_003Ed__16 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public SteamLobbyChatUI _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CSelectInputField_003Ed__16(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[SettingsField(0, false, "Chat UI")]
		[SerializeField]
		private int maxMessages;

		[ElementField("Chat UI", 0)]
		[SerializeField]
		private GameObject chatPanel;

		[ElementField("Chat UI", 0)]
		[SerializeField]
		private TMP_InputField inputField;

		[ElementField("Chat UI", 0)]
		[SerializeField]
		private ScrollRect scrollView;

		[ElementField("Chat UI", 0)]
		[SerializeField]
		private Transform messageRoot;

		[TemplateField("Chat UI", 0)]
		[SerializeField]
		private GameObject myChatTemplate;

		[TemplateField("Chat UI", 0)]
		[SerializeField]
		private GameObject theirChatTemplate;

		private SteamLobbyData _mInspector;

		private readonly List<SteamLobbyMemberChatMessage> _chatMessages;

		private void Start()
		{
		}

		private void HandleOnChanged(LobbyData arg0)
		{
		}

		private void OnDestroy()
		{
		}

		private void Update()
		{
		}

		public void Clear()
		{
		}

		private void HandleChatMessage(LobbyChatMsg message)
		{
		}

		public void SendMessage()
		{
		}

		[IteratorStateMachine(typeof(_003CSelectInputField_003Ed__16))]
		private IEnumerator SelectInputField()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CForceScrollDown_003Ed__17))]
		private IEnumerator ForceScrollDown()
		{
			return null;
		}
	}
}
