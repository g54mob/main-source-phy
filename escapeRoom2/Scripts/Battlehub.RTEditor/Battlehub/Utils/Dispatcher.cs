using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Battlehub.Utils
{
	public class Dispatcher : MonoBehaviour
	{
		private static Dispatcher m_current;

		private int m_lock;

		private bool m_run;

		private Queue<Action> m_wait;

		public static Dispatcher Current
		{
			get
			{
				return m_current;
			}
			private set
			{
				m_current = value;
			}
		}

		private void _BeginInvoke(Action action)
		{
			while (Interlocked.Exchange(ref m_lock, 1) != 0)
			{
			}
			m_wait.Enqueue(action);
			m_run = true;
			Interlocked.Exchange(ref m_lock, 0);
		}

		public static void BeginInvoke(Action action)
		{
			if (m_current != null)
			{
				m_current._BeginInvoke(action);
			}
			else
			{
				action();
			}
		}

		private void Awake()
		{
			if (Current != null)
			{
				UnityEngine.Object.Destroy(Current);
			}
			Current = this;
			m_wait = new Queue<Action>();
		}

		private void Update()
		{
			if (!m_run)
			{
				return;
			}
			Queue<Action> queue = null;
			if (Interlocked.Exchange(ref m_lock, 1) == 0)
			{
				queue = new Queue<Action>(m_wait.Count);
				while (m_wait.Count != 0)
				{
					Action item = m_wait.Dequeue();
					queue.Enqueue(item);
				}
				m_run = false;
				Interlocked.Exchange(ref m_lock, 0);
			}
			if (queue != null)
			{
				while (queue.Count != 0)
				{
					queue.Dequeue()();
				}
			}
		}
	}
}
