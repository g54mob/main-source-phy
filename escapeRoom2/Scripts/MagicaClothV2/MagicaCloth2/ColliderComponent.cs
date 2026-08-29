using System.Collections.Generic;
using UnityEngine;

namespace MagicaCloth2
{
	public abstract class ColliderComponent : ClothBehaviour, IDataValidate
	{
		public Vector3 center;

		[SerializeField]
		protected Vector3 size;

		private HashSet<int> teamIdSet = new HashSet<int>();

		public abstract ColliderManager.ColliderType GetColliderType();

		public abstract void DataValidate();

		public virtual Vector3 GetSize()
		{
			return size;
		}

		public void SetSize(Vector3 size)
		{
			this.size = size;
		}

		public void SetSizeX(float size)
		{
			this.size.x = size;
		}

		public void SetSizeY(float size)
		{
			this.size.y = size;
		}

		public void SetSizeZ(float size)
		{
			this.size.z = size;
		}

		public virtual float GetScale()
		{
			return base.transform.lossyScale.x;
		}

		public virtual bool IsReverseDirection()
		{
			return false;
		}

		internal void Register(int teamId)
		{
			teamIdSet.Add(teamId);
		}

		internal void Exit(int teamId)
		{
			teamIdSet.Remove(teamId);
		}

		public void UpdateParameters()
		{
			DataValidate();
			foreach (int item in teamIdSet)
			{
				MagicaManager.Collider.UpdateParameters(this, item);
			}
		}

		protected virtual void Start()
		{
		}

		protected virtual void OnValidate()
		{
			UpdateParameters();
		}

		protected virtual void OnEnable()
		{
			foreach (int item in teamIdSet)
			{
				MagicaManager.Collider.EnableCollider(this, item, sw: true);
			}
		}

		protected virtual void OnDisable()
		{
			foreach (int item in teamIdSet)
			{
				MagicaManager.Collider.EnableCollider(this, item, sw: false);
			}
		}

		protected virtual void OnDestroy()
		{
			foreach (int item in teamIdSet)
			{
				MagicaManager.Collider.RemoveCollider(this, item);
			}
			teamIdSet.Clear();
		}
	}
}
