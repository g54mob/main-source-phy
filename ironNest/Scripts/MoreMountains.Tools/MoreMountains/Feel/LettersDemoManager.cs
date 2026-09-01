using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class LettersDemoManager : MonoBehaviour
	{
		[Header("Feedbacks")]
		public MMFeedbacks FeedbackF;

		public MMFeedbacks FeedbackE1;

		public MMFeedbacks FeedbackE2;

		public MMFeedbacks FeedbackL;

		protected Vector3 _mousePosition;

		protected virtual void Update()
		{
		}

		protected virtual void HandleInput()
		{
		}

		protected virtual void PlayF()
		{
		}

		protected virtual void PlayE1()
		{
		}

		protected virtual void PlayE2()
		{
		}

		protected virtual void PlayL()
		{
		}
	}
}
