using System;
using System.Collections;
using UnityEngine;

namespace Dreamteck.Splines
{
	[AddComponentMenu("Dreamteck/Splines/Object Controller")]
	public class ObjectController : SplineUser
	{
		[Serializable]
		internal class ObjectControl
		{
			public GameObject gameObject;

			public Vector3 position = Vector3.zero;

			public Quaternion rotation = Quaternion.identity;

			public Vector3 scale = Vector3.one;

			public bool active = true;

			public Vector3 baseScale = Vector3.one;

			public bool isNull => gameObject == null;

			public Transform transform
			{
				get
				{
					if (gameObject == null)
					{
						return null;
					}
					return gameObject.transform;
				}
			}

			public ObjectControl(GameObject input)
			{
				gameObject = input;
				baseScale = gameObject.transform.localScale;
			}

			public void Destroy()
			{
				if (!(gameObject == null))
				{
					UnityEngine.Object.Destroy(gameObject);
				}
			}

			public void DestroyImmediate()
			{
				if (!(gameObject == null))
				{
					UnityEngine.Object.DestroyImmediate(gameObject);
				}
			}

			public void Apply()
			{
				if (!(gameObject == null))
				{
					transform.position = position;
					transform.rotation = rotation;
					transform.localScale = scale;
					gameObject.SetActive(active);
				}
			}
		}

		public enum ObjectMethod
		{
			Instantiate = 0,
			GetChildren = 1
		}

		public enum Positioning
		{
			Stretch = 0,
			Clip = 1
		}

		public enum Iteration
		{
			Ordered = 0,
			Random = 1
		}

		[SerializeField]
		[HideInInspector]
		public GameObject[] objects = new GameObject[0];

		[SerializeField]
		[HideInInspector]
		private float _positionOffset;

		[SerializeField]
		[HideInInspector]
		private int _spawnCount;

		[SerializeField]
		[HideInInspector]
		private Positioning _objectPositioning;

		[SerializeField]
		[HideInInspector]
		private Iteration _iteration;

		[SerializeField]
		[HideInInspector]
		private int _randomSeed = 1;

		[SerializeField]
		[HideInInspector]
		private Vector2 _randomSize = Vector2.one;

		[SerializeField]
		[HideInInspector]
		private Vector2 _offset = Vector2.zero;

		[SerializeField]
		[HideInInspector]
		private Vector3 _minRotationOffset = Vector3.zero;

		[SerializeField]
		[HideInInspector]
		private Vector3 _maxRotationOffset = Vector3.zero;

		[SerializeField]
		[HideInInspector]
		private Vector3 _minScaleMultiplier = Vector3.one;

		[SerializeField]
		[HideInInspector]
		private Vector3 _maxScaleMultiplier = Vector3.one;

		[SerializeField]
		[HideInInspector]
		private bool _randomizeOffset;

		[SerializeField]
		[HideInInspector]
		private bool _useRandomOffsetRotation;

		[SerializeField]
		[HideInInspector]
		private bool _shellOffset = true;

		[SerializeField]
		[HideInInspector]
		private bool _randomOffset;

		[SerializeField]
		[HideInInspector]
		private bool _applyRotation = true;

		[SerializeField]
		[HideInInspector]
		private bool _applyScale;

		[SerializeField]
		[HideInInspector]
		private ObjectMethod _objectMethod;

		[HideInInspector]
		public bool delayedSpawn;

		[HideInInspector]
		public float spawnDelay = 0.1f;

		[SerializeField]
		[HideInInspector]
		private int lastChildCount;

		[SerializeField]
		[HideInInspector]
		private ObjectControl[] spawned = new ObjectControl[0];

		private SplineResult evaluateResult = new SplineResult();

		private System.Random randomizer;

		private System.Random randomizer2;

		private System.Random rotationRandomizer;

		private System.Random scaleRandomizer;

		public ObjectMethod objectMethod
		{
			get
			{
				return _objectMethod;
			}
			set
			{
				if (value != _objectMethod)
				{
					if (value == ObjectMethod.GetChildren)
					{
						_objectMethod = value;
						Spawn();
					}
					else
					{
						_objectMethod = value;
					}
				}
			}
		}

		public int spawnCount
		{
			get
			{
				return _spawnCount;
			}
			set
			{
				if (value == _spawnCount)
				{
					return;
				}
				if (value < 0)
				{
					value = 0;
				}
				if (_objectMethod == ObjectMethod.Instantiate)
				{
					if (value < _spawnCount)
					{
						_spawnCount = value;
						Remove();
					}
					else
					{
						_spawnCount = value;
						Spawn();
					}
				}
				else
				{
					_spawnCount = value;
				}
			}
		}

		public Positioning objectPositioning
		{
			get
			{
				return _objectPositioning;
			}
			set
			{
				if (value != _objectPositioning)
				{
					_objectPositioning = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public Iteration iteration
		{
			get
			{
				return _iteration;
			}
			set
			{
				if (value != _iteration)
				{
					_iteration = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public int randomSeed
		{
			get
			{
				return _randomSeed;
			}
			set
			{
				if (value != _randomSeed)
				{
					_randomSeed = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public Vector2 offset
		{
			get
			{
				return _offset;
			}
			set
			{
				if (value != _offset)
				{
					_offset = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public Vector3 minRotationOffset
		{
			get
			{
				return _minRotationOffset;
			}
			set
			{
				if (value != _minRotationOffset)
				{
					_minRotationOffset = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public Vector3 maxRotationOffset
		{
			get
			{
				return _maxRotationOffset;
			}
			set
			{
				if (value != _maxRotationOffset)
				{
					_maxRotationOffset = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public Vector3 rotationOffset
		{
			get
			{
				return (_maxRotationOffset + _minRotationOffset) / 2f;
			}
			set
			{
				if (value != _minRotationOffset || value != _maxRotationOffset)
				{
					_minRotationOffset = (_maxRotationOffset = value);
					Rebuild(sampleComputer: false);
				}
			}
		}

		public Vector3 minScaleMultiplier
		{
			get
			{
				return _minScaleMultiplier;
			}
			set
			{
				if (value != _minScaleMultiplier)
				{
					_minScaleMultiplier = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public Vector3 maxScaleMultiplier
		{
			get
			{
				return _maxScaleMultiplier;
			}
			set
			{
				if (value != _maxScaleMultiplier)
				{
					_maxScaleMultiplier = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public Vector3 scaleMultiplier
		{
			get
			{
				return (_minScaleMultiplier + _maxScaleMultiplier) / 2f;
			}
			set
			{
				if (value != _minScaleMultiplier || value != _maxScaleMultiplier)
				{
					_minScaleMultiplier = (_maxScaleMultiplier = value);
					Rebuild(sampleComputer: false);
				}
			}
		}

		public bool randomizeOffset
		{
			get
			{
				return _randomizeOffset;
			}
			set
			{
				if (value != _randomizeOffset)
				{
					_randomizeOffset = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public bool useRandomOffsetRotation
		{
			get
			{
				return _useRandomOffsetRotation;
			}
			set
			{
				if (value != _useRandomOffsetRotation)
				{
					_useRandomOffsetRotation = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public bool shellOffset
		{
			get
			{
				return _shellOffset;
			}
			set
			{
				if (value != _shellOffset)
				{
					_shellOffset = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public bool randomOffset
		{
			get
			{
				return _randomOffset;
			}
			set
			{
				if (value != _randomOffset)
				{
					_randomOffset = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public bool applyRotation
		{
			get
			{
				return _applyRotation;
			}
			set
			{
				if (value != _applyRotation)
				{
					_applyRotation = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public bool applyScale
		{
			get
			{
				return _applyScale;
			}
			set
			{
				if (value != _applyScale)
				{
					_applyScale = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public Vector2 randomSize
		{
			get
			{
				return _randomSize;
			}
			set
			{
				if (value != _randomSize)
				{
					_randomSize = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public float positionOffset
		{
			get
			{
				return _positionOffset;
			}
			set
			{
				if (value != _positionOffset)
				{
					_positionOffset = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public void Clear()
		{
			for (int i = 0; i < spawned.Length; i++)
			{
				if (spawned[i] != null)
				{
					spawned[i].transform.localScale = spawned[i].baseScale;
					if (_objectMethod == ObjectMethod.GetChildren)
					{
						spawned[i].gameObject.SetActive(value: false);
					}
					else
					{
						spawned[i].Destroy();
					}
				}
			}
			spawned = new ObjectControl[0];
		}

		private void Remove()
		{
			if (_spawnCount >= spawned.Length)
			{
				return;
			}
			int num = spawned.Length - 1;
			while (num >= _spawnCount && num < spawned.Length)
			{
				if (spawned[num] != null)
				{
					spawned[num].transform.localScale = spawned[num].baseScale;
					if (_objectMethod == ObjectMethod.GetChildren)
					{
						spawned[num].gameObject.SetActive(value: false);
					}
					else if (Application.isEditor)
					{
						spawned[num].DestroyImmediate();
					}
					else
					{
						spawned[num].Destroy();
					}
				}
				num--;
			}
			ObjectControl[] array = new ObjectControl[_spawnCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = spawned[i];
			}
			spawned = array;
			Rebuild(sampleComputer: false);
		}

		public void GetAll()
		{
			ObjectControl[] array = new ObjectControl[base.transform.childCount];
			int num = 0;
			foreach (Transform item in base.transform)
			{
				if (array[num] == null)
				{
					array[num++] = new ObjectControl(item.gameObject);
					continue;
				}
				bool flag = false;
				for (int i = 0; i < spawned.Length; i++)
				{
					if (spawned[i].gameObject == item.gameObject)
					{
						array[num++] = spawned[i];
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					array[num++] = new ObjectControl(item.gameObject);
				}
			}
			spawned = array;
		}

		public void Spawn()
		{
			if (_objectMethod == ObjectMethod.Instantiate)
			{
				if (delayedSpawn && Application.isPlaying)
				{
					StopCoroutine("InstantiateAllWithDelay");
					StartCoroutine(InstantiateAllWithDelay());
				}
				else
				{
					InstantiateAll();
				}
			}
			else
			{
				GetAll();
			}
			Rebuild(sampleComputer: false);
		}

		protected override void LateRun()
		{
			base.LateRun();
			if (_objectMethod == ObjectMethod.GetChildren && lastChildCount != base.transform.childCount)
			{
				Spawn();
				lastChildCount = base.transform.childCount;
			}
		}

		private IEnumerator InstantiateAllWithDelay()
		{
			if (!(base.computer == null) && objects.Length != 0)
			{
				for (int i = spawned.Length; i <= spawnCount; i++)
				{
					InstantiateSingle();
					yield return new WaitForSeconds(spawnDelay);
				}
			}
		}

		private void InstantiateAll()
		{
			if (!(base.computer == null) && objects.Length != 0)
			{
				for (int i = spawned.Length; i < spawnCount; i++)
				{
					InstantiateSingle();
				}
			}
		}

		private void InstantiateSingle()
		{
			if (objects.Length != 0)
			{
				int num = 0;
				num = ((_iteration != Iteration.Ordered) ? UnityEngine.Random.Range(0, objects.Length) : (spawned.Length - Mathf.FloorToInt(spawned.Length / objects.Length) * objects.Length));
				if (!(objects[num] == null))
				{
					ObjectControl[] array = new ObjectControl[spawned.Length + 1];
					spawned.CopyTo(array, 0);
					array[^1] = new ObjectControl(UnityEngine.Object.Instantiate(objects[num], base.transform.position, base.transform.rotation));
					array[^1].transform.parent = base.transform;
					spawned = array;
				}
			}
		}

		protected override void Build()
		{
			base.Build();
			randomizer = new System.Random(_randomSeed);
			randomizer2 = new System.Random(_randomSeed + 1);
			rotationRandomizer = new System.Random(_randomSeed + 2);
			scaleRandomizer = new System.Random(_randomSeed + 3);
			Quaternion quaternion = Quaternion.Euler(_minRotationOffset);
			bool flag = _minRotationOffset != _maxRotationOffset;
			bool flag2 = _minScaleMultiplier != _maxScaleMultiplier;
			for (int i = 0; i < spawned.Length; i++)
			{
				if (spawned[i] == null)
				{
					Clear();
					Spawn();
					break;
				}
				float num = 0f;
				if (spawned.Length > 1)
				{
					num = (float)i / (float)(spawned.Length - 1);
				}
				num += positionOffset;
				if (num > 1f)
				{
					num -= 1f;
				}
				else if (num < 0f)
				{
					num += 1f;
				}
				if (objectPositioning == Positioning.Clip)
				{
					Evaluate(evaluateResult, num);
				}
				else
				{
					Evaluate(evaluateResult, DMath.Lerp(base.clipFrom, base.clipTo, num));
				}
				spawned[i].position = evaluateResult.position;
				if (_applyScale)
				{
					Vector3 scale = spawned[i].baseScale * evaluateResult.size;
					if (flag2)
					{
						scale.x *= Mathf.Lerp(_minScaleMultiplier.x, _maxScaleMultiplier.x, (float)scaleRandomizer.NextDouble());
						scale.y *= Mathf.Lerp(_minScaleMultiplier.y, _maxScaleMultiplier.y, (float)scaleRandomizer.NextDouble());
						scale.z *= Mathf.Lerp(_minScaleMultiplier.z, _maxScaleMultiplier.z, (float)scaleRandomizer.NextDouble());
					}
					else
					{
						scale.x *= scaleMultiplier.x;
						scale.y *= scaleMultiplier.y;
						scale.z *= scaleMultiplier.z;
					}
					spawned[i].scale = scale;
				}
				else
				{
					spawned[i].scale = spawned[i].baseScale;
				}
				Vector3 normalized = Vector3.Cross(evaluateResult.direction, evaluateResult.normal).normalized;
				spawned[i].position += -normalized * _offset.x + evaluateResult.normal * _offset.y;
				if (_applyRotation)
				{
					if (flag)
					{
						quaternion = Quaternion.Euler(Mathf.Lerp(_minRotationOffset.x, _maxRotationOffset.x, (float)rotationRandomizer.NextDouble()), Mathf.Lerp(_minRotationOffset.y, _maxRotationOffset.y, (float)rotationRandomizer.NextDouble()), Mathf.Lerp(_minRotationOffset.z, _maxRotationOffset.z, (float)rotationRandomizer.NextDouble()));
					}
					if (!_randomizeOffset || !_useRandomOffsetRotation)
					{
						spawned[i].rotation = evaluateResult.rotation * quaternion;
					}
				}
				if (_randomizeOffset)
				{
					float num2 = (float)randomizer.NextDouble();
					float f = (float)randomizer2.NextDouble() * 360f * (MathF.PI / 180f);
					Vector2 vector = new Vector2(num2 * Mathf.Cos(f), num2 * Mathf.Sin(f));
					if (_shellOffset)
					{
						vector.Normalize();
					}
					else
					{
						vector = Vector2.ClampMagnitude(vector, 1f);
					}
					Vector3 position = spawned[i].position;
					spawned[i].position += vector.x * normalized * _randomSize.x * evaluateResult.size * 0.5f + vector.y * evaluateResult.normal * _randomSize.y * evaluateResult.size * 0.5f;
					if (_useRandomOffsetRotation)
					{
						spawned[i].rotation = Quaternion.LookRotation(evaluateResult.direction, spawned[i].position - position) * quaternion;
					}
				}
				if (_objectPositioning == Positioning.Clip)
				{
					if ((double)num < base.clipFrom || (double)num > base.clipTo)
					{
						spawned[i].active = false;
					}
					else
					{
						spawned[i].active = true;
					}
				}
			}
		}

		protected override void PostBuild()
		{
			base.PostBuild();
			for (int i = 0; i < spawned.Length; i++)
			{
				spawned[i].Apply();
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (objectMethod == ObjectMethod.Instantiate)
			{
				Clear();
			}
		}
	}
}
