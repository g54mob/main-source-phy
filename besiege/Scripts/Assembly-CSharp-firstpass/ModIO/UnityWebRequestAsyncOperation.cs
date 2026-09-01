using System;
using UnityEngine;
using UnityEngine.Networking;

namespace ModIO
{
	public class UnityWebRequestAsyncOperation
	{
		private Action<UnityWebRequestAsyncOperation> m_completeCallback;

		private bool isDisposed;

		public UnityWebRequest webRequest { get; private set; }

		public AsyncOperation operation { get; private set; }

		public bool isDone
		{
			get
			{
				return operation.isDone;
			}
		}

		public float progress
		{
			get
			{
				return operation.progress;
			}
		}

		public int priority
		{
			get
			{
				return operation.priority;
			}
			set
			{
				operation.priority = value;
			}
		}

		public bool allowSceneActivation
		{
			get
			{
				return operation.allowSceneActivation;
			}
			set
			{
				operation.allowSceneActivation = value;
			}
		}

		public event Action<UnityWebRequestAsyncOperation> completed
		{
			add
			{
				if (operation.isDone)
				{
					value(this);
				}
				else
				{
					m_completeCallback = (Action<UnityWebRequestAsyncOperation>)Delegate.Combine(m_completeCallback, value);
				}
			}
			remove
			{
				m_completeCallback = (Action<UnityWebRequestAsyncOperation>)Delegate.Remove(m_completeCallback, value);
			}
		}

		public UnityWebRequestAsyncOperation(UnityWebRequest request, AsyncOperation operation)
		{
			webRequest = request;
			this.operation = operation;
		}

		internal void InvokeCompletionEvent()
		{
			if (m_completeCallback != null)
			{
				m_completeCallback(this);
				m_completeCallback = null;
			}
		}
	}
}
