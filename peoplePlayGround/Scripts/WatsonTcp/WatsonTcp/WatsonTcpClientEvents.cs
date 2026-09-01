using System;

namespace WatsonTcp
{
	public class WatsonTcpClientEvents
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

		public event EventHandler AuthenticationSucceeded;

		public event EventHandler AuthenticationFailure;

		public event EventHandler<MessageReceivedEventArgs> MessageReceived;

		public event EventHandler<StreamReceivedEventArgs> StreamReceived;

		public event EventHandler<ConnectionEventArgs> ServerConnected;

		public event EventHandler<DisconnectionEventArgs> ServerDisconnected;

		public event EventHandler<ExceptionEventArgs> ExceptionEncountered;

		internal void HandleAuthenticationSucceeded(object sender, EventArgs args)
		{
			WrappedEventHandler(delegate
			{
				this.AuthenticationSucceeded?.Invoke(sender, args);
			}, "ServerConnected", sender);
		}

		internal void HandleAuthenticationFailure(object sender, EventArgs args)
		{
			WrappedEventHandler(delegate
			{
				this.AuthenticationFailure?.Invoke(sender, args);
			}, "AuthenticationFailure", sender);
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

		internal void HandleServerConnected(object sender, ConnectionEventArgs args)
		{
			WrappedEventHandler(delegate
			{
				this.ServerConnected?.Invoke(sender, args);
			}, "ServerConnected", sender);
		}

		internal void HandleServerDisconnected(object sender, DisconnectionEventArgs args)
		{
			WrappedEventHandler(delegate
			{
				this.ServerDisconnected?.Invoke(sender, args);
			}, "ServerDisconnected", sender);
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
			try
			{
				action();
			}
			catch (Exception obj)
			{
				(((WatsonTcpClient)sender).Settings?.Logger)?.Invoke(Severity.Error, "Event handler exception in " + handler + ": " + Environment.NewLine + SerializationHelper.SerializeJson(obj, pretty: true));
			}
		}
	}
}
