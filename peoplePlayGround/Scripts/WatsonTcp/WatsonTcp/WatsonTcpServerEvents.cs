using System;

namespace WatsonTcp
{
	public class WatsonTcpServerEvents
	{
		internal bool IsUsingMessages
		{
			get
			{
				if (this.MessageReceived != null && this.MessageReceived.GetInvocationList().Length != 0)
				{
					return true;
				}
				return false;
			}
		}

		internal bool IsUsingStreams
		{
			get
			{
				if (this.StreamReceived != null && this.StreamReceived.GetInvocationList().Length != 0)
				{
					return true;
				}
				return false;
			}
		}

		public event EventHandler<AuthenticationRequestedEventArgs> AuthenticationRequested;

		public event EventHandler<AuthenticationSucceededEventArgs> AuthenticationSucceeded;

		public event EventHandler<AuthenticationFailedEventArgs> AuthenticationFailed;

		public event EventHandler<ConnectionEventArgs> ClientConnected;

		public event EventHandler<DisconnectionEventArgs> ClientDisconnected;

		public event EventHandler<MessageReceivedEventArgs> MessageReceived;

		public event EventHandler<StreamReceivedEventArgs> StreamReceived;

		public event EventHandler ServerStarted;

		public event EventHandler ServerStopped;

		public event EventHandler<ExceptionEventArgs> ExceptionEncountered;

		internal void HandleAuthenticationRequested(object sender, AuthenticationRequestedEventArgs args)
		{
			WrappedEventHandler(delegate
			{
				this.AuthenticationRequested?.Invoke(sender, args);
			}, "AuthenticationRequested", sender);
		}

		internal void HandleAuthenticationSucceeded(object sender, AuthenticationSucceededEventArgs args)
		{
			WrappedEventHandler(delegate
			{
				this.AuthenticationSucceeded?.Invoke(sender, args);
			}, "AuthenticationSucceeded", sender);
		}

		internal void HandleAuthenticationFailed(object sender, AuthenticationFailedEventArgs args)
		{
			WrappedEventHandler(delegate
			{
				this.AuthenticationFailed?.Invoke(sender, args);
			}, "AuthenticationFailed", sender);
		}

		internal void HandleClientConnected(object sender, ConnectionEventArgs args)
		{
			WrappedEventHandler(delegate
			{
				this.ClientConnected?.Invoke(sender, args);
			}, "ClientConnected", sender);
		}

		internal void HandleClientDisconnected(object sender, DisconnectionEventArgs args)
		{
			WrappedEventHandler(delegate
			{
				this.ClientDisconnected?.Invoke(sender, args);
			}, "ClientDisconnected", sender);
		}

		internal void HandleMessageReceived(object sender, MessageReceivedEventArgs args)
		{
			WrappedEventHandler(delegate
			{
				this.MessageReceived?.Invoke(sender, args);
			}, "MessageReceived", sender);
		}

		internal void HandleStreamReceived(object sender, StreamReceivedEventArgs args)
		{
			WrappedEventHandler(delegate
			{
				this.StreamReceived?.Invoke(sender, args);
			}, "StreamReceived", sender);
		}

		internal void HandleServerStarted(object sender, EventArgs args)
		{
			WrappedEventHandler(delegate
			{
				this.ServerStarted?.Invoke(sender, args);
			}, "ServerStarted", sender);
		}

		internal void HandleServerStopped(object sender, EventArgs args)
		{
			WrappedEventHandler(delegate
			{
				this.ServerStopped?.Invoke(sender, args);
			}, "ServerStopped", sender);
		}

		internal void HandleExceptionEncountered(object sender, ExceptionEventArgs args)
		{
			WrappedEventHandler(delegate
			{
				this.ExceptionEncountered?.Invoke(sender, args);
			}, "ExceptionEncountered", sender);
		}

		internal void WrappedEventHandler(Action action, string handler, object sender)
		{
			if (action == null)
			{
				return;
			}
			Action<Severity, string> logger = ((WatsonTcpServer)sender).Settings.Logger;
			try
			{
				action();
			}
			catch (Exception obj)
			{
				logger?.Invoke(Severity.Error, "Event handler exception in " + handler + ": " + Environment.NewLine + SerializationHelper.SerializeJson(obj, pretty: true));
			}
		}
	}
}
