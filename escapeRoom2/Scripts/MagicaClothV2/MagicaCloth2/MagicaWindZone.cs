using UnityEngine;

namespace MagicaCloth2
{
	[AddComponentMenu("MagicaCloth2/MagicaWindZone")]
	[HelpURL("https://magicasoft.jp/en/mc2_windzone_component/")]
	public class MagicaWindZone : ClothBehaviour
	{
		public enum Mode
		{
			GlobalDirection = 0,
			SphereDirection = 1,
			BoxDirection = 2,
			SphereRadial = 10
		}

		public Mode mode;

		public Vector3 size = new Vector3(10f, 10f, 10f);

		public float radius = 10f;

		[Range(0f, 30f)]
		public float main = 5f;

		[Range(0f, 1f)]
		public float turbulence = 1f;

		[Range(-180f, 180f)]
		public float directionAngleX;

		[Range(-180f, 180f)]
		public float directionAngleY;

		public AnimationCurve attenuation = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);

		public bool isAddition;

		public int WindId { get; private set; } = -1;

		public void Awake()
		{
			WindId = MagicaManager.Wind.AddWind(this);
		}

		public void Start()
		{
		}

		public void OnEnable()
		{
			MagicaManager.Wind.SetEnable(WindId, sw: true);
		}

		public void OnDisable()
		{
			MagicaManager.Wind.SetEnable(WindId, sw: false);
		}

		public void OnDestroy()
		{
			MagicaManager.Wind.RemoveWind(WindId);
			WindId = -1;
		}

		public bool IsDirection()
		{
			Mode mode = this.mode;
			if ((uint)mode <= 2u)
			{
				return true;
			}
			return false;
		}

		public bool IsRadial()
		{
			if (mode == Mode.SphereRadial)
			{
				return true;
			}
			return false;
		}

		public bool IsAddition()
		{
			return isAddition;
		}

		public Vector3 GetWindDirection(bool localSpace = false)
		{
			Vector3 vector = Quaternion.Euler(directionAngleX, directionAngleY, 0f) * Vector3.forward;
			if (!localSpace)
			{
				return base.transform.TransformDirection(vector);
			}
			return vector;
		}

		public void SetWindDirection(Vector3 dir, bool localSpace = false)
		{
			Vector3 toDirection = (localSpace ? dir : base.transform.InverseTransformDirection(dir));
			Vector3 eulerAngles = Quaternion.FromToRotation(Vector3.forward, toDirection).eulerAngles;
			directionAngleX = ((eulerAngles.x > 180f) ? (eulerAngles.x - 360f) : eulerAngles.x);
			directionAngleY = ((eulerAngles.y > 180f) ? (eulerAngles.y - 360f) : eulerAngles.y);
		}
	}
}
