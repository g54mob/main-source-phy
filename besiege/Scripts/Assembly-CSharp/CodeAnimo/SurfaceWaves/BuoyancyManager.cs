using System.Collections.Generic;
using CodeAnimo.GPGPU;
using CodeAnimo.UnityExtensionMethods;
using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	[AddComponentMenu("Surface Waves/Buoyancy/Buoyancy Manager")]
	public class BuoyancyManager : MonoBehaviour
	{
		[SerializeField]
		[HideInInspector]
		protected GameObject standardSettingsPrefab;

		public Dimensions simPosition;

		public SimulationOutput waveData;

		public SimulationOutput terrainData;

		private List<Buoy> nextFrameBuoys = new List<Buoy>();

		private ComputeKernel1D simKernel;

		public void Reset()
		{
			this.ApplyPrefabSettings(standardSettingsPrefab);
			AddMissingComponents();
		}

		public void Awake()
		{
		}

		protected void AddMissingComponents()
		{
			base.gameObject.AddComponentIfMissingAndCopySettings<ComputeKernel1D>(standardSettingsPrefab);
			BoxCollider boxCollider = base.gameObject.AddComponentIfMissing<Collider, BoxCollider>();
			if (boxCollider != null && standardSettingsPrefab != null)
			{
				boxCollider.ApplyPrefabSettings(standardSettingsPrefab);
				boxCollider.isTrigger = true;
			}
		}

		protected void FixedUpdate()
		{
			calculateBuoyancy();
		}

		protected void OnEnable()
		{
			findKernelReference();
			if (base.enabled)
			{
				ReSubscribe();
			}
		}

		protected void OnTriggerEnter(Collider other)
		{
			Buoy component = other.GetComponent<Buoy>();
			if (!(component == null))
			{
				nextFrameBuoys.Add(component);
				component.addWillBeDestroyedHandler(HandleBuoyDestruction);
			}
		}

		protected void OnTriggerExit(Collider other)
		{
			Buoy component = other.GetComponent<Buoy>();
			if (component != null)
			{
				nextFrameBuoys.Remove(component);
			}
		}

		private void ReSubscribe()
		{
			foreach (Buoy nextFrameBuoy in nextFrameBuoys)
			{
				nextFrameBuoy.addWillBeDestroyedHandler(HandleBuoyDestruction);
			}
		}

		private void findKernelReference()
		{
			try
			{
				simKernel = Kernel.FindCompatibleKernelOnGameObject(base.gameObject) as ComputeKernel1D;
			}
			catch (MissingComponentException)
			{
				Debug.LogWarning("No supported kernel found, disabling buoyancy. Is DirectX 11 mode enabled? (Alternatively, disable or remove BuoyancyManager)", this);
				base.enabled = false;
			}
		}

		private void calculateBuoyancy()
		{
			if (nextFrameBuoys.Count > 0)
			{
				Buoy[] buoys = nextFrameBuoys.ToArray();
				Vector4[] positionData = getPositionData(buoys);
				Vector4[] velocityData = getVelocityData(buoys);
				Vector3[] buoyForces = computeForces(positionData, velocityData);
				applyBuoyForces(buoys, buoyForces);
			}
		}

		private Vector4[] getPositionData(Buoy[] buoys)
		{
			int num = buoys.Length;
			float num2 = (float)simPosition.resolutionX / simPosition.localSize.x;
			float num3 = (float)simPosition.resolutionZ / simPosition.localSize.z;
			Vector4[] array = new Vector4[num];
			for (int i = 0; i < num; i++)
			{
				Buoy buoy = buoys[i];
				Vector3 vector = buoy.position - simPosition.firstCorner;
				array[i] = new Vector4(vector.x * num2, vector.y, vector.z * num3, buoy.radius * 2f);
			}
			return array;
		}

		private Vector4[] getVelocityData(Buoy[] buoys)
		{
			int num = buoys.Length;
			Vector4[] array = new Vector4[num];
			for (int i = 0; i < num; i++)
			{
				Buoy buoy = buoys[i];
				Vector3 vector = buoy.velocityData;
				array[i] = new Vector4(vector.x, vector.y, vector.z, 0f);
			}
			return array;
		}

		private void applyBuoyForces(Buoy[] buoys, Vector3[] buoyForces)
		{
			int num = buoys.Length;
			for (int i = 0; i < num; i++)
			{
				Buoy buoy = buoys[i];
				buoy.applyBuoyancy(buoyForces[i]);
			}
		}

		private void HandleBuoyDestruction(Buoy victim)
		{
			nextFrameBuoys.Remove(victim);
		}

		private Vector3[] computeForces(Vector4[] inputArray, Vector4[] velocityArray)
		{
			int num = inputArray.Length;
			simKernel.elementCount = num;
			int intValue = simKernel.CalculateWarpGroupCount();
			ComputeBuffer computeBuffer = new ComputeBuffer(num, 16);
			ComputeBuffer computeBuffer2 = new ComputeBuffer(num, 16);
			ComputeBuffer computeBuffer3 = new ComputeBuffer(num, 12);
			computeBuffer.SetData(inputArray);
			computeBuffer2.SetData(velocityArray);
			simKernel.SetBuffer("buoyPosition", computeBuffer);
			simKernel.SetBuffer("buoyVelocity", computeBuffer2);
			simKernel.SetBuffer("ForceOut", computeBuffer3);
			simKernel.SetTexture("WaveHeightIn", waveData.outputData);
			simKernel.SetTexture("TerrainHeightIn", terrainData.outputData);
			simKernel.SetFloat("waveHeightScale", 10f);
			simKernel.SetInt("groupCountX", intValue);
			simKernel.Dispatch();
			Vector3[] array = new Vector3[num];
			computeBuffer3.GetData(array);
			computeBuffer3.Release();
			computeBuffer2.Release();
			computeBuffer.Release();
			return array;
		}
	}
}
