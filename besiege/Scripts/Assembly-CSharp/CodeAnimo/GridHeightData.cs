using System;
using UnityEngine;

namespace CodeAnimo
{
	public class GridHeightData : MonoBehaviour
	{
		public virtual bool hasData
		{
			get
			{
				return false;
			}
		}

		public virtual int maximumU
		{
			get
			{
				return int.MaxValue;
			}
		}

		public virtual int maximumV
		{
			get
			{
				return int.MaxValue;
			}
		}

		public event EventHandler HeightDataUpdated;

		public virtual float getGridHeight(int u, int v)
		{
			return 0f;
		}

		public void subscribeToHeightDataUpdated(EventHandler listener)
		{
			this.HeightDataUpdated = (EventHandler)Delegate.Remove(this.HeightDataUpdated, listener);
			this.HeightDataUpdated = (EventHandler)Delegate.Combine(this.HeightDataUpdated, listener);
		}

		public void unsubscribeFromHeightDataUpdated(EventHandler listener)
		{
			this.HeightDataUpdated = (EventHandler)Delegate.Remove(this.HeightDataUpdated, listener);
		}

		protected void onHeightDataUpdated(EventArgs e)
		{
			if (this.HeightDataUpdated != null)
			{
				this.HeightDataUpdated(this, e);
			}
		}
	}
}
