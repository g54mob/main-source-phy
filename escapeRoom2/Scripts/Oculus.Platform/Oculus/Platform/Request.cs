using UnityEngine;

namespace Oculus.Platform
{
	public sealed class Request<T> : Request
	{
		private Message<T>.Callback callback_;

		public Request(ulong requestID)
			: base(requestID)
		{
		}

		public Request<T> OnComplete(Message<T>.Callback callback)
		{
			if (callback_ != null)
			{
				throw new UnityException("Attempted to attach multiple handlers to a Request.  This is not allowed.");
			}
			callback_ = callback;
			Callback.AddRequest(this);
			return this;
		}

		public override void HandleMessage(Message msg)
		{
			if (!(msg is Message<T>))
			{
				Debug.LogError("Unable to handle message: " + msg.GetType());
				return;
			}
			if (callback_ != null)
			{
				callback_((Message<T>)msg);
				return;
			}
			throw new UnityException("Request with no handler.  This should never happen.");
		}
	}
	public class Request
	{
		private Message.Callback callback_;

		public ulong RequestID { get; set; }

		public Request(ulong requestID)
		{
			RequestID = requestID;
		}

		public Request OnComplete(Message.Callback callback)
		{
			callback_ = callback;
			Callback.AddRequest(this);
			return this;
		}

		public virtual void HandleMessage(Message msg)
		{
			if (callback_ != null)
			{
				callback_(msg);
				return;
			}
			throw new UnityException("Request with no handler.  This should never happen.");
		}

		public static void RunCallbacks(uint limit = 0u)
		{
			if (limit == 0)
			{
				Callback.RunCallbacks();
			}
			else
			{
				Callback.RunLimitedCallbacks(limit);
			}
		}
	}
}
