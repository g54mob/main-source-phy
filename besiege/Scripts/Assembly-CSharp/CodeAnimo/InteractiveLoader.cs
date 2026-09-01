using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace CodeAnimo
{
	[ExecuteInEditMode]
	public class InteractiveLoader : MonoBehaviour
	{
		public delegate void loadMethod();

		[SerializeField]
		private Queue<loadMethod> loadMethodQueue = new Queue<loadMethod>();

		[HideInInspector]
		[SerializeField]
		private bool m_loading;

		[HideInInspector]
		[SerializeField]
		private bool m_newMethodAdded;

		private int m_completedCount;

		private Stopwatch updateTimer = new Stopwatch();

		public int endUpdateTime = 8;

		public bool loadOnNewData;

		public int ElementCount
		{
			get
			{
				return loadMethodQueue.Count;
			}
		}

		public bool Loading
		{
			get
			{
				return m_loading;
			}
		}

		public float CompletionFraction
		{
			get
			{
				int count = loadMethodQueue.Count;
				int num = m_completedCount + count;
				if (count == 0)
				{
					return 1f;
				}
				return (float)m_completedCount / (float)num;
			}
		}

		public event EventHandler loadingComplete;

		protected void Update()
		{
			if (loadOnNewData && m_newMethodAdded)
			{
				StartLoading();
			}
			if (m_loading)
			{
				RunLoadingFrame();
			}
		}

		public void EditorUpdate()
		{
			Update();
		}

		public void AddMethod(loadMethod method)
		{
			if (method != null)
			{
				loadMethodQueue.Enqueue(method);
				m_newMethodAdded = true;
			}
		}

		public void ClearMethods()
		{
			loadMethodQueue.Clear();
		}

		public void StartLoading()
		{
			m_newMethodAdded = false;
			if (!m_loading)
			{
				m_completedCount = 0;
				m_loading = true;
			}
		}

		public void StopLoading()
		{
			m_loading = false;
		}

		private void RunLoadingFrame()
		{
			updateTimer.Reset();
			updateTimer.Start();
			try
			{
				while (updateTimer.ElapsedMilliseconds < endUpdateTime && m_loading)
				{
					LoadElement();
				}
			}
			catch
			{
				StopLoading();
				throw;
			}
			finally
			{
				updateTimer.Stop();
			}
		}

		private void LoadElement()
		{
			if (loadMethodQueue.Count <= 0)
			{
				OnLoadingComplete();
				return;
			}
			loadMethod loadMethod2 = loadMethodQueue.Dequeue();
			if (loadMethod2 != null)
			{
				loadMethod2();
				m_completedCount++;
				return;
			}
			StopLoading();
			throw new NullReferenceException("One of the loading tasks is null");
		}

		private void OnLoadingComplete()
		{
			StopLoading();
			if (this.loadingComplete != null)
			{
				this.loadingComplete(this, EventArgs.Empty);
			}
		}
	}
}
