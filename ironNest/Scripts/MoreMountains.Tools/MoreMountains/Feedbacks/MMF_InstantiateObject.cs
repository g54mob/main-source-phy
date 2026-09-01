using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback allows you to instantiate the object specified in its inspector, at the feedback's position (plus an optional offset). You can also optionally (and automatically) create an object pool at initialization to save on performance. In that case you'll need to specify a pool size (usually the maximum amount of these instantiated objects you plan on having in your scene at each given time).")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("GameObject/Instantiate Object")]
	public class MMF_InstantiateObject : MMF_Feedback
	{
		public enum PositionModes
		{
			FeedbackPosition = 0,
			Transform = 1,
			WorldPosition = 2,
			Script = 3
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Instantiate Object", true, 37, true, false)]
		[Tooltip("the object to instantiate")]
		[FormerlySerializedAs("VfxToInstantiate")]
		public GameObject GameObjectToInstantiate;

		[MMFInspectorGroup("Position", true, 39, false, false)]
		[Tooltip("the chosen way to position the object")]
		public PositionModes PositionMode;

		[Tooltip("the chosen way to position the object")]
		public bool AlsoApplyRotation;

		[Tooltip("the chosen way to position the object")]
		public bool AlsoApplyScale;

		[Tooltip("the transform at which to instantiate the object")]
		[MMFEnumCondition("PositionMode", new int[] { 1 })]
		public Transform TargetTransform;

		[Tooltip("the transform at which to instantiate the object")]
		[MMFEnumCondition("PositionMode", new int[] { 2 })]
		public Vector3 TargetPosition;

		[Tooltip("the position offset at which to instantiate the object")]
		[FormerlySerializedAs("VfxPositionOffset")]
		public Vector3 PositionOffset;

		[Tooltip("if this is true, instantiation position will be randomized between RandomizeMin and RandomizeMax")]
		public bool RandomizePosition;

		[Tooltip("the minimum value we'll randomize our position with")]
		[MMFCondition("RandomizePosition", true)]
		public Vector3 RandomizedPositionMin;

		[Tooltip("the maximum value we'll randomize our position with")]
		[MMFCondition("RandomizePosition", true)]
		public Vector3 RandomizedPositionMax;

		[MMFInspectorGroup("Parent", true, 47, false, false)]
		[Tooltip("if specified, the instantiated object will be parented to this transform ")]
		public Transform ParentTransform;

		[MMFInspectorGroup("Object Pool", true, 40, false, false)]
		[Tooltip("whether or not we should create automatically an object pool for this object")]
		[FormerlySerializedAs("VfxCreateObjectPool")]
		public bool CreateObjectPool;

		[Tooltip("the initial and planned size of this object pool")]
		[MMFCondition("CreateObjectPool", true)]
		[FormerlySerializedAs("VfxObjectPoolSize")]
		public int ObjectPoolSize;

		[Tooltip("whether or not to create a new pool even if one already exists for that same prefab")]
		[MMFCondition("CreateObjectPool", true)]
		public bool MutualizePools;

		[Tooltip("the transform the pool of objects will be parented to")]
		[MMFCondition("CreateObjectPool", true)]
		public Transform PoolParentTransform;

		protected MMMiniObjectPooler _objectPooler;

		protected GameObject _newGameObject;

		protected bool _poolCreatedOrFound;

		protected Vector3 _randomizedPosition;

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void PositionObject(Vector3 position)
		{
		}

		protected virtual Vector3 GetPosition(Vector3 position)
		{
			return default(Vector3);
		}

		protected virtual Quaternion GetRotation()
		{
			return default(Quaternion);
		}

		protected virtual Vector3 GetScale()
		{
			return default(Vector3);
		}
	}
}
