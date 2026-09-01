using System.Diagnostics;
using UnityEngine;

namespace SRF
{
	public abstract class SRMonoBehaviour : MonoBehaviour
	{
		private Collider _collider;

		private Transform _transform;

		private Rigidbody _rigidBody;

		private GameObject _gameObject;

		private Rigidbody2D _rigidbody2D;

		private Collider2D _collider2D;

		public Transform CachedTransform
		{
			[DebuggerNonUserCode]
			[DebuggerStepThrough]
			get
			{
				if (_transform == null)
				{
					_transform = base.transform;
				}
				return _transform;
			}
		}

		public Collider CachedCollider
		{
			[DebuggerStepThrough]
			[DebuggerNonUserCode]
			get
			{
				if (_collider == null)
				{
					_collider = GetComponent<Collider>();
				}
				return _collider;
			}
		}

		public Collider2D CachedCollider2D
		{
			[DebuggerNonUserCode]
			[DebuggerStepThrough]
			get
			{
				if (_collider2D == null)
				{
					_collider2D = GetComponent<Collider2D>();
				}
				return _collider2D;
			}
		}

		public Rigidbody CachedRigidBody
		{
			[DebuggerNonUserCode]
			[DebuggerStepThrough]
			get
			{
				if (_rigidBody == null)
				{
					_rigidBody = GetComponent<Rigidbody>();
				}
				return _rigidBody;
			}
		}

		public Rigidbody2D CachedRigidBody2D
		{
			[DebuggerNonUserCode]
			[DebuggerStepThrough]
			get
			{
				if (_rigidbody2D == null)
				{
					_rigidbody2D = GetComponent<Rigidbody2D>();
				}
				return _rigidbody2D;
			}
		}

		public GameObject CachedGameObject
		{
			[DebuggerNonUserCode]
			[DebuggerStepThrough]
			get
			{
				if (_gameObject == null)
				{
					_gameObject = base.gameObject;
				}
				return _gameObject;
			}
		}

		public new Transform transform
		{
			get
			{
				return CachedTransform;
			}
		}

		[DebuggerNonUserCode]
		[DebuggerStepThrough]
		protected void AssertNotNull(object value, string fieldName = null)
		{
			SRDebugUtil.AssertNotNull(value, fieldName, this);
		}

		[DebuggerStepThrough]
		[DebuggerNonUserCode]
		protected void Assert(bool condition, string message = null)
		{
			SRDebugUtil.Assert(condition, message, this);
		}

		[DebuggerNonUserCode]
		[Conditional("UNITY_EDITOR")]
		[DebuggerStepThrough]
		protected void EditorAssertNotNull(object value, string fieldName = null)
		{
			AssertNotNull(value, fieldName);
		}

		[DebuggerNonUserCode]
		[Conditional("UNITY_EDITOR")]
		[DebuggerStepThrough]
		protected void EditorAssert(bool condition, string message = null)
		{
			Assert(condition, message);
		}
	}
}
