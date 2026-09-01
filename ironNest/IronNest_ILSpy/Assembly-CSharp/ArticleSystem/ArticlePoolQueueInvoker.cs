using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace ArticleSystem;

public class ArticlePoolQueueInvoker : MonoBehaviour
{
	public ArticlePoolDefinition defaultPool;

	public GameObject defaultPrefab;

	public int defaultUses;

	public string defaultNote;

	public bool autoEnqueueOnStart;

	private void Start()
	{
		if (!autoEnqueueOnStart)
		{
			return;
		}
		if (defaultPrefab != null && defaultPrefab != null)
		{
			ArticlePoolQueueManager instance = ArticlePoolQueueManager.Instance;
			int count = defaultUses;
			if (defaultUses < 1)
			{
				count = 1;
			}
			instance.EnqueueSpecificArticle(defaultPrefab, count, defaultNote);
		}
		if (defaultPool != null && defaultPool != null)
		{
			ArticlePoolQueueManager instance2 = ArticlePoolQueueManager.Instance;
			int count2 = defaultUses;
			if (defaultUses < 1)
			{
				count2 = 1;
			}
			instance2.EnqueuePool(defaultPool, count2, defaultNote);
		}
	}

	public void EnqueueConfigured()
	{
		if (defaultPool != null)
		{
			ArticlePoolQueueManager instance = ArticlePoolQueueManager.Instance;
			int count = defaultUses;
			if (defaultUses < 1)
			{
				count = 1;
			}
			instance.EnqueuePool(defaultPool, count, defaultNote);
		}
	}

	public void EnqueuePool(ArticlePoolDefinition pool, int uses = 1)
	{
		if (pool != null)
		{
			ArticlePoolQueueManager instance = ArticlePoolQueueManager.Instance;
			bool flag = uses < 1;
			int count = 1;
			if (!flag)
			{
				count = uses;
			}
			instance.EnqueuePool(pool, count, defaultNote);
		}
	}

	public void EnqueueConfiguredSpecific()
	{
		if (defaultPrefab != null)
		{
			ArticlePoolQueueManager instance = ArticlePoolQueueManager.Instance;
			int count = defaultUses;
			if (defaultUses < 1)
			{
				count = 1;
			}
			instance.EnqueueSpecificArticle(defaultPrefab, count, defaultNote);
		}
	}

	public void EnqueueSpecific(GameObject prefab, int uses = 1)
	{
		if (prefab != null)
		{
			ArticlePoolQueueManager instance = ArticlePoolQueueManager.Instance;
			bool flag = uses < 1;
			int count = 1;
			if (!flag)
			{
				count = uses;
			}
			instance.EnqueueSpecificArticle(prefab, count, defaultNote);
		}
	}

	public void ClearQueue()
	{
		ArticlePoolQueueManager instance = ArticlePoolQueueManager.Instance;
		List<ArticlePoolQueueManager.QueueEntry> queue = instance.queue;
		int version = queue._version + 1;
		queue._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			queue._size = 0;
		}
		else
		{
			queue._size = 0;
			if (queue._size > 0)
			{
				Array.Clear(queue._items, 0, queue._size);
			}
		}
		if (instance.logDebug)
		{
			Debug.Log("[ArticlePoolQueueManager] Queue cleared.");
		}
	}

	public void ResetSequential()
	{
		ArticlePoolQueueManager instance = ArticlePoolQueueManager.Instance;
		instance._sequentialNextIndex.Clear();
		if (instance.logDebug)
		{
			Debug.Log("[ArticlePoolQueueManager] All per-pool sequential indices reset.");
		}
	}

	public ArticlePoolQueueInvoker()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A60E]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		defaultUses = 1;
		defaultNote = "";
		base._002Ector();
	}
}
