using NaughtyAttributes;
using UnityEngine;

namespace LevelCreator
{
	public class ScaleByDistance : MonoBehaviour
	{
		public bool dontTargetPlayer = true;

		[ShowIf("dontTargetPlayer")]
		public Transform targetInitial;

		private Transform target;

		public Vector3 farScale;

		public Vector3 nearScale;

		public float maxDistance;

		private DMEditor dmEditor;

		private void Start()
		{
			target = targetInitial;
			dmEditor = DMEditor.Instance;
			if (!dontTargetPlayer)
			{
				target = dmEditor.playerCamera.transform;
			}
		}

		private void Update()
		{
			base.transform.localScale = Vector3.Lerp(nearScale, farScale, Vector3.Distance(base.transform.position, target.position) / maxDistance);
		}
	}
}
