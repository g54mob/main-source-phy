using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[RequireComponent(typeof(RectTransform))]
	public class ContentSizeFitterForChildren : MonoBehaviour
	{
		[Tooltip("Uncheck to avoid changes to width.")]
		public bool FitWidth;

		[Tooltip("Uncheck to avoid changes to height.")]
		public bool FitHeight;

		[Header("Settings")]
		[Tooltip("Is added to the calculated width (distribution is based on the pivot/anchors).")]
		public float AdditionalWidth;

		[Tooltip("Is added to the calculated height (distribution is based on the pivot/anchors).")]
		public float AdditionalHeight;

		[Tooltip("Uses a very simple logic to check whether or not to refresh (refreshes if the child count changes).")]
		public bool AutoRefresh;

		[Tooltip("Always refresh on each Update() call. Use it only if AutoRefresh is not sufficient, e.g. if the sizes of the children change all the time.")]
		public bool AlwaysRefresh;

		public RectTransform[] IgnoreList;

		public int ForceUpdateFirstNFrames;

		protected int framesInUpdate;

		protected RectTransform rectTransform;

		protected bool isDirty;

		protected int lastChildCount;

		protected Vector3[] corners;

		public RectTransform RectTransform => null;

		private void Awake()
		{
		}

		private void OnEnable()
		{
		}

		public void Update()
		{
		}

		public void Refresh()
		{
		}

		protected void updateSize()
		{
		}

		protected Bounds calculateShallowBounds(Transform root, Transform child)
		{
			return default(Bounds);
		}

		protected bool isIgnored(RectTransform t)
		{
			return false;
		}
	}
}
