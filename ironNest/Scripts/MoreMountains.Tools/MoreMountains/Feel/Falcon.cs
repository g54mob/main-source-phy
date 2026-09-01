using System.Collections.Generic;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class Falcon : MonoBehaviour
	{
		[Header("Input")]
		[Tooltip("a key to use to jump")]
		public KeyCode ActionKey;

		[Tooltip("a secondary key to use to jump")]
		public KeyCode ActionKeyAlt;

		[Header("Bindings")]
		[Tooltip("the various wigglers that make the car move")]
		public List<MMWiggle> Wigglers;

		[Tooltip("the wiggler associated to the camera")]
		public MMWiggle CameraWiggler;

		[Tooltip("the ground's panning texture")]
		public MMPanningTexture Offsetter;

		[Tooltip("the particles that are supposed to loop (rocks etc)")]
		public List<ParticleSystem> ParticleLoops;

		[Tooltip("the on/off emitters (wind, smoke)")]
		public List<ParticleSystem> ParticleEmitters;

		[Tooltip("the wheels' auto rotators")]
		public List<MMAutoRotate> AutoRotaters;

		[Header("Settings")]
		[Tooltip("the speed at which the wheel should rotate")]
		public float RotationSpeed;

		[Header("Feedbacks")]
		[Tooltip("a feedback to call when the car starts driving")]
		public MMFeedbacks DriveFeedback;

		[Tooltip("a feedback to call when the car stops")]
		public MMFeedbacks StopFeedback;

		protected bool _turning;

		protected virtual void Start()
		{
		}

		protected virtual void SetCar(bool status)
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void HandleInput()
		{
		}

		protected virtual void HandleCar()
		{
		}

		protected virtual void Drive()
		{
		}

		protected virtual void TurnStop()
		{
		}
	}
}
