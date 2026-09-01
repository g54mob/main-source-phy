using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMDebugTouchDisplay : MonoBehaviour
	{
		[Header("Bindings")]
		public Canvas TargetCanvas;

		[Header("Touches")]
		public RectTransform TouchPrefab;

		public int TouchProvision;

		protected List<RectTransform> _touchDisplays;

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void DetectTouches()
		{
		}

		protected virtual void DisableAllDisplays()
		{
		}

		protected virtual void OnDisable()
		{
		}
	}
}
