using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMGyroParallax : MMGyroscope
	{
		[Header("Cameras")]
		public List<MMGyroCam> Cams;

		protected Vector3 _newAngles;

		protected override void Start()
		{
		}

		public virtual void Initialization()
		{
		}

		protected override void Update()
		{
		}

		protected virtual void MoveCameras()
		{
		}
	}
}
