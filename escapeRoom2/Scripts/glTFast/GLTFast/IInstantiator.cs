using Unity.Collections;
using UnityEngine;

namespace GLTFast
{
	public interface IInstantiator
	{
		void BeginScene(string name, uint[] rootNodeIndices);

		void AddAnimation(AnimationClip[] animationClips);

		void CreateNode(uint nodeIndex, uint? parentIndex, Vector3 position, Quaternion rotation, Vector3 scale);

		void SetNodeName(uint nodeIndex, string name);

		void AddPrimitive(uint nodeIndex, string meshName, MeshResult meshResult, uint[] joints = null, uint? rootJoint = null, float[] morphTargetWeights = null, int meshNumeration = 0);

		void AddPrimitiveInstanced(uint nodeIndex, string meshName, MeshResult meshResult, uint instanceCount, NativeArray<Vector3>? positions, NativeArray<Quaternion>? rotations, NativeArray<Vector3>? scales, int meshNumeration = 0);

		void AddCamera(uint nodeIndex, uint cameraIndex);

		void AddLightPunctual(uint nodeIndex, uint lightIndex);

		void EndScene(uint[] rootNodeIndices);
	}
}
