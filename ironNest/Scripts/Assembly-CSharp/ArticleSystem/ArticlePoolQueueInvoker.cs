using UnityEngine;

namespace ArticleSystem
{
	[DisallowMultipleComponent]
	public class ArticlePoolQueueInvoker : MonoBehaviour
	{
		[Header("Defaults (Optional)")]
		[Tooltip("Default pool to enqueue when calling EnqueueConfigured(). Leave empty if you plan to pass a pool via UnityEvent parameters.")]
		public ArticlePoolDefinition defaultPool;

		[Tooltip("Default prefab to enqueue when calling EnqueueConfiguredSpecific(). Leave empty if you plan to pass a prefab via UnityEvent parameters.")]
		public GameObject defaultPrefab;

		[Tooltip("Default use-count when enqueuing via the configured methods (how many population passes should this entry contribute). Minimum 1.")]
		[Min(1f)]
		public int defaultUses;

		[Tooltip("Optional note to attach to queued entries (for debugging).")]
		public string defaultNote;

		[Header("Auto Enqueue on Start")]
		[Tooltip("When enabled, automatically enqueues on Start without needing a UnityEvent trigger.\n\nWhat gets enqueued depends on which defaults are set:\n- If 'Default Prefab' is assigned  → calls EnqueueConfiguredSpecific()\n- If 'Default Pool' is assigned    → calls EnqueueConfigured()\n- If both are assigned             → enqueues both\n- If neither is assigned           → does nothing\n\nDefault: OFF.")]
		public bool autoEnqueueOnStart;

		private void Start()
		{
		}

		[Tooltip("Enqueues the 'defaultPool' once per pass for 'defaultUses' passes. Use this from UnityEvents without parameters.")]
		public void EnqueueConfigured()
		{
		}

		[Tooltip("Enqueues the given pool once per pass for 'uses' passes. Use this from UnityEvents with a pool parameter, or wire a constant in the Inspector.")]
		public void EnqueuePool(ArticlePoolDefinition pool, int uses = 1)
		{
		}

		[Tooltip("Enqueues the 'defaultPrefab' once per pass for 'defaultUses' passes. Use this from UnityEvents without parameters.")]
		public void EnqueueConfiguredSpecific()
		{
		}

		[Tooltip("Enqueues the given prefab once per pass for 'uses' passes. Use this from UnityEvents with a prefab parameter.")]
		public void EnqueueSpecific(GameObject prefab, int uses = 1)
		{
		}

		[Tooltip("Clears all queued entries. Use with caution; safe for UnityEvents.")]
		public void ClearQueue()
		{
		}

		[Tooltip("Resets all per-pool sequential indices back to 0. Safe for UnityEvents.")]
		public void ResetSequential()
		{
		}
	}
}
