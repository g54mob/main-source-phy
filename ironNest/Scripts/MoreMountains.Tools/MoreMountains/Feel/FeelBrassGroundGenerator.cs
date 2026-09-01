using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class FeelBrassGroundGenerator : MonoBehaviour
	{
		[Header("Dimensions")]
		public int NumberOfRows;

		public int NumberOfColumns;

		public Vector3 Offset;

		public Vector3 DancerOffset;

		public AnimationCurve Amplitude;

		public float Width;

		public float Depth;

		public float MinRandom;

		public float MaxRandom;

		public float AmplitudeMultiplier;

		public int FloatingCubesAmount;

		[Header("Air Cubes")]
		public int FloatingBlockChance;

		public float MinHeight;

		public float MaxHeight;

		public float MinDistanceToDancer;

		public float MinScale;

		public float MaxScale;

		[Header("Materials")]
		public Material GroundMaterial;

		public Material GroundMaterialAlt1;

		public Material GroundMaterialAlt2;

		[Header("Bindings")]
		public MMRadioReceiver GroundPrefabToInstantiate;

		public Transform ParentContainer;

		public Transform Dancer;

		[Header("Behaviour")]
		public bool GenerateOnAwake;

		[Header("Debug")]
		[MMInspectorButton("GenerateGround")]
		public bool GenerateGroundBtn;

		protected MMRadioReceiver _receiver;

		protected Vector3 _wipPosition;

		protected string _wipName;

		protected int _counter;

		protected virtual void Awake()
		{
		}

		protected virtual void GenerateGround()
		{
		}

		protected virtual MMRadioReceiver InstantiateBlock(Vector3 newPosition, string newName)
		{
			return null;
		}
	}
}
