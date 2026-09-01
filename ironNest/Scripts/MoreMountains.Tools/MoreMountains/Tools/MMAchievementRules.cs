using UnityEngine;

namespace MoreMountains.Tools
{
	public abstract class MMAchievementRules : MonoBehaviour, MMEventListener<MMGameEvent>, MMEventListenerBase
	{
		public MMAchievementList AchievementList;

		[MMInspectorButton("PrintCurrentStatus")]
		public bool PrintCurrentStatusBtn;

		public virtual void PrintCurrentStatus()
		{
		}

		protected virtual void Awake()
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		public virtual void OnMMEvent(MMGameEvent gameEvent)
		{
		}
	}
}
