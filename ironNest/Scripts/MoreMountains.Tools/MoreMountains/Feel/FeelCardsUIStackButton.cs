using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class FeelCardsUIStackButton : MonoBehaviour
	{
		public MMFeedbacks StackFeedback;

		public List<MMFeedbacks> BlockerFeedbacks;

		public virtual void Stack()
		{
		}
	}
}
