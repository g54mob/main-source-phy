using System;
using System.Collections.Generic;
using Modding;
using UnityEngine;

namespace InternalModding.Resources
{
	public class GrabModResource : MonoBehaviour
	{
		[SerializeField]
		private int resourceId = -1;

		public void Set(int id, ModResource resource, Action<GameObject> postSetAction, Action prefabPostSetAction)
		{
			resourceId = id;
			resource.OnLoad += delegate
			{
				if (!(this == null))
				{
					if (!resource.HasError)
					{
						resource.ApplyToObject(base.gameObject);
						if (postSetAction != null)
						{
							postSetAction(base.gameObject);
						}
						if (prefabPostSetAction != null)
						{
							prefabPostSetAction();
						}
					}
					UnityEngine.Object.Destroy(this);
				}
			};
		}

		public void Awake()
		{
			if (resourceId == -1)
			{
				return;
			}
			KeyValuePair<ModResource, Action<GameObject>> resourceByGrabId = ModResource.GetResourceByGrabId(resourceId);
			ModResource resource = resourceByGrabId.Key;
			Action<GameObject> postSetAction = resourceByGrabId.Value;
			resource.OnLoad += delegate
			{
				if (!(this == null))
				{
					if (!resource.HasError)
					{
						resource.ApplyToObject(base.gameObject);
						if (postSetAction != null)
						{
							postSetAction(base.gameObject);
						}
					}
					UnityEngine.Object.Destroy(this);
				}
			};
		}
	}
}
