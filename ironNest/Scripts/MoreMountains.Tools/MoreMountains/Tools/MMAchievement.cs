using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	[Serializable]
	public class MMAchievement
	{
		[Header("Identification")]
		public string AchievementID;

		public AchievementTypes AchievementType;

		public bool HiddenAchievement;

		public bool UnlockedStatus;

		[Header("Description")]
		public string Title;

		public string Description;

		public int Points;

		[Header("Image and Sounds")]
		public Sprite LockedImage;

		public Sprite UnlockedImage;

		public AudioClip UnlockedSound;

		[Header("Progress")]
		public int ProgressTarget;

		public int ProgressCurrent;

		public virtual void UnlockAchievement()
		{
		}

		public virtual void LockAchievement()
		{
		}

		public virtual void AddProgress(int newProgress)
		{
		}

		public virtual void SetProgress(int newProgress)
		{
		}

		protected virtual void EvaluateProgress()
		{
		}

		public virtual MMAchievement Copy()
		{
			return null;
		}
	}
}
