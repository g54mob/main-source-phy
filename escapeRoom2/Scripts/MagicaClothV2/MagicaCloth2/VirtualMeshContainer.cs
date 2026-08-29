using System;
using UnityEngine;

namespace MagicaCloth2
{
	public class VirtualMeshContainer : IDisposable
	{
		public VirtualMesh shareVirtualMesh;

		public VirtualMesh.UniqueSerializationData uniqueData;

		public bool hasUniqueData => uniqueData != null;

		public VirtualMeshContainer()
		{
		}

		public VirtualMeshContainer(VirtualMesh vmesh)
		{
			shareVirtualMesh = vmesh;
			uniqueData = null;
		}

		public void Dispose()
		{
			shareVirtualMesh?.Dispose();
		}

		public int GetTransformCount()
		{
			if (hasUniqueData)
			{
				return uniqueData.transformData.transformArray.Length;
			}
			return shareVirtualMesh.TransformCount;
		}

		public Transform GetTransformFromIndex(int index)
		{
			if (hasUniqueData)
			{
				return uniqueData.transformData.transformArray[index];
			}
			return shareVirtualMesh.transformData.GetTransformFromIndex(index);
		}

		public Transform GetCenterTransform()
		{
			if (hasUniqueData)
			{
				return uniqueData.transformData.transformArray[shareVirtualMesh.centerTransformIndex];
			}
			return shareVirtualMesh.GetCenterTransform();
		}
	}
}
