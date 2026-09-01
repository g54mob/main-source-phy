using Cpp2ILInjected;
using UnityEngine;

public sealed class MinimalTransformRandomizer : MonoBehaviour
{
	public enum PositionSpace
	{
		ParentLocal,
		World,
		Self
	}

	private bool resetOnDisable;

	private bool recaptureOriginalOnEveryEnable;

	private bool randomizePosition = true;

	private PositionSpace positionSpace;

	private float positionRadius = 100f;

	private bool positionX = true;

	private bool positionY;

	private bool positionZ;

	private bool randomizeRotation;

	private bool rotationX;

	private bool rotationY = true;

	private bool rotationZ;

	private float rotationMaxX;

	private float rotationMaxY = 360f;

	private float rotationMaxZ;

	private bool _hasOriginal;

	private Vector3 _originalLocalPosition;

	private Quaternion _originalLocalRotation;

	private Vector3 _originalWorldPosition;

	private void OnEnable()
	{
		//IL_0069: Expected O, but got F4
		//IL_009e: Expected O, but got F4
		//IL_00c9: Expected O, but got F4
		//IL_0132: Expected F4, but got I4
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Expected O, but got Unknown
		//IL_0147: Expected O, but got I4
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Expected O, but got Unknown
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Expected O, but got Unknown
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Expected O, but got Unknown
		if (!_hasOriginal || recaptureOriginalOnEveryEnable)
		{
			Transform transform = base.transform;
			Vector3 localPosition = transform.localPosition;
			_originalLocalPosition = (Vector3)localPosition.x;
			_ = localPosition.z;
			Transform transform2 = base.transform;
			_originalLocalRotation = (Quaternion)transform2.localRotation.x;
			Transform transform3 = base.transform;
			Vector3 position = transform3.position;
			_originalWorldPosition = (Vector3)position.x;
			_ = position.z;
			_hasOriginal = true;
		}
		if (randomizePosition)
		{
			Vector3 insideUnitSphere = Random.insideUnitSphere;
			float num = positionRadius * insideUnitSphere.z;
			_ = insideUnitSphere.x;
			if (!positionX || (positionY && !positionZ))
			{
				num = 0f;
			}
			bool flag = positionSpace == PositionSpace.ParentLocal;
			object obj2 = default(object);
			if (!flag)
			{
				object obj = positionSpace - 1;
				if (!flag)
				{
					if ((nint)obj == 1)
					{
						Vector3 vector = (Vector3)(obj2 - 96);
						Quaternion quaternion = (Quaternion)(obj2 - 64);
						_ = _originalLocalRotation;
						Vector3 vector2 = quaternion * vector;
						Transform transform4 = base.transform;
						float num2 = vector2.z;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MinimalTransformRandomizer)+68]");
						float num3 = num2 + 0f;
						Vector3 position2 = (Vector3)(obj2 - 96);
						transform4.position = position2;
					}
				}
				else
				{
					Transform transform5 = base.transform;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MinimalTransformRandomizer)+68]");
					float num4 = 0f + num;
					Vector3 position3 = (Vector3)(obj2 - 96);
					transform5.position = position3;
				}
			}
			else
			{
				Transform transform6 = base.transform;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MinimalTransformRandomizer)+4C]");
				float num5 = 0f + num;
				Vector3 localPosition2 = (Vector3)(obj2 - 96);
				transform6.localPosition = localPosition2;
			}
		}
		if (randomizeRotation)
		{
			ApplyRandomLocalRotation();
		}
	}

	private unsafe void OnDisable()
	{
		//IL_009b: Expected O, but got I4
		//IL_00ff: Expected O, but got Ref
		//IL_007c: Expected O, but got Ref
		//IL_00d4: Expected O, but got Ref
		if (!resetOnDisable || !_hasOriginal)
		{
			return;
		}
		Vector3 vector = default(Vector3);
		if (positionSpace == PositionSpace.ParentLocal)
		{
			Transform transform = base.transform;
			transform.localPosition = (Vector3)(&vector);
			vector = _originalLocalPosition;
		}
		else
		{
			object obj = positionSpace - 1;
			if ((nint)obj <= 1)
			{
				Transform transform2 = base.transform;
				transform2.position = (Vector3)(&vector);
				vector = _originalWorldPosition;
			}
		}
		Transform transform3 = base.transform;
		transform3.localRotation = (Quaternion)(&vector);
	}

	private void CaptureOriginalIfNeeded()
	{
		//IL_0069: Expected O, but got F4
		//IL_009e: Expected O, but got F4
		//IL_00c9: Expected O, but got F4
		if (!_hasOriginal || recaptureOriginalOnEveryEnable)
		{
			Transform transform = base.transform;
			Vector3 localPosition = transform.localPosition;
			_originalLocalPosition = (Vector3)localPosition.x;
			_ = localPosition.z;
			Transform transform2 = base.transform;
			_originalLocalRotation = (Quaternion)transform2.localRotation.x;
			Transform transform3 = base.transform;
			Vector3 position = transform3.position;
			_originalWorldPosition = (Vector3)position.x;
			_ = position.z;
			_hasOriginal = true;
		}
	}

	private unsafe void ApplyRandomPosition()
	{
		//IL_0036: Expected O, but got I4
		//IL_00ca: Expected O, but got Ref
		//IL_0118: Expected O, but got Ref
		//IL_0071: Expected O, but got Ref
		//IL_0071: Expected O, but got Ref
		Vector3 insideUnitSphere = Random.insideUnitSphere;
		bool flag = default(bool);
		object obj = default(object);
		if (positionX)
		{
			if (positionY && !positionZ)
			{
				/*Error: End of method reached without returning.*/;
			}
			flag = positionSpace == PositionSpace.ParentLocal;
			if (flag)
			{
				Transform transform = base.transform;
				transform.localPosition = (Vector3)(&obj);
				return;
			}
		}
		object obj2 = positionSpace - 1;
		Transform transform2;
		object obj4 = default(object);
		if (!flag)
		{
			if ((nint)obj2 != 1)
			{
				return;
			}
			object obj3 = default(object);
			Vector3 vector = (Quaternion)(&obj3) * (Vector3)(&obj);
			transform2 = base.transform;
			obj = obj4;
		}
		else
		{
			transform2 = base.transform;
			obj = obj4;
		}
		transform2.position = (Vector3)(&obj);
	}

	private unsafe void ApplyRandomLocalRotation()
	{
		//IL_00c2: Expected O, but got Ref
		if (rotationX)
		{
			float minInclusive = rotationMaxX ^ -0f;
			float num = Random.Range(minInclusive, rotationMaxX);
		}
		if (rotationY)
		{
			float minInclusive2 = rotationMaxY ^ -0f;
			float num2 = Random.Range(minInclusive2, rotationMaxY);
		}
		if (rotationZ)
		{
			float minInclusive3 = rotationMaxZ ^ -0f;
			float num3 = Random.Range(minInclusive3, rotationMaxZ);
		}
		Vector3 euler = default(Vector3);
		Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
		Transform transform = base.transform;
		float num4 = default(float);
		transform.localRotation = (Quaternion)(&num4);
	}
}
