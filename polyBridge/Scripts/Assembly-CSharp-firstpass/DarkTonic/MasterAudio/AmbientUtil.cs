using System.Collections.Generic;
using UnityEngine;

namespace DarkTonic.MasterAudio
{
	public static class AmbientUtil
	{
		public const string FollowerHolderName = "_Followers";

		public const string ListenerFollowerName = "~ListenerFollower~";

		public const float ListenerFollowerTrigRadius = 0.01f;

		public const int IgnoreRaycastLayerNumber = 2;

		private static Transform _followerHolder;

		private static ListenerFollower _listenerFollower;

		private static List<TransformFollower> _transformFollowers = new List<TransformFollower>();

		public static ListenerFollower ListenerFollower
		{
			get
			{
				if (_listenerFollower != null)
				{
					return _listenerFollower;
				}
				if (FollowerHolder == null)
				{
					return null;
				}
				Transform transform = FollowerHolder.GetChildTransform("~ListenerFollower~");
				if (transform == null)
				{
					transform = new GameObject("~ListenerFollower~").transform;
					transform.parent = FollowerHolder;
					transform.gameObject.layer = FollowerHolder.gameObject.layer;
				}
				_listenerFollower = transform.GetComponent<ListenerFollower>();
				if (_listenerFollower == null)
				{
					_listenerFollower = transform.gameObject.AddComponent<ListenerFollower>();
				}
				return _listenerFollower;
			}
		}

		public static Transform FollowerHolder
		{
			get
			{
				if (!Application.isPlaying || MasterAudio.SafeInstance == null)
				{
					return null;
				}
				if (_followerHolder != null)
				{
					return _followerHolder;
				}
				Transform trans = MasterAudio.SafeInstance.Trans;
				_followerHolder = trans.GetChildTransform("_Followers");
				if (_followerHolder != null)
				{
					return _followerHolder;
				}
				_followerHolder = new GameObject("_Followers").transform;
				_followerHolder.parent = trans;
				_followerHolder.gameObject.layer = trans.gameObject.layer;
				return _followerHolder;
			}
		}

		public static bool HasListenerFollower => _listenerFollower != null;

		public static int AmbientCount => _transformFollowers.Count;

		public static bool HasListenerFolowerRigidBody => false;

		public static void InitFollowerHolder()
		{
			Transform followerHolder = FollowerHolder;
			if (followerHolder != null)
			{
				followerHolder.DestroyAllChildren();
			}
		}

		public static bool InitListenerFollower()
		{
			_ = MasterAudio.ListenerTrans == null;
			return false;
		}

		public static void RemoveTransformFollower(TransformFollower follower)
		{
			_transformFollowers.Remove(follower);
		}

		public static Transform InitAudioSourceFollower(Transform transToFollow, string followerName, string soundGroupName, string variationName, float volume, bool willFollowSource, bool willPositionOnClosestColliderPoint, bool useTopCollider, bool useChildColliders, MasterAudio.AmbientSoundExitMode exitMode, float exitFadeTime, MasterAudio.AmbientSoundReEnterMode reEnterMode, float reEnterFadeTime)
		{
			return null;
		}

		public static void ManualUpdate()
		{
			UpdateListenerFollower();
			for (int i = 0; i < _transformFollowers.Count; i++)
			{
				_transformFollowers[i].ManualUpdate();
			}
		}

		private static void UpdateListenerFollower()
		{
			if (_listenerFollower != null)
			{
				_listenerFollower.ManualUpdate();
			}
		}
	}
}
