using UnityEngine;

public interface ISelectable
{
	long identifier { get; }

	bool IsSelected { get; set; }

	bool IsSelectedExtra { get; set; }

	int SymmetryIndex { get; set; }

	bool IsDestroyed { get; set; }

	float TransformMultiplier { get; set; }

	Transform GetTransform();

	Vector3 GetCenter();

	void Select(bool selected);

	void SetPosition(Vector3 pos);

	void SetRotation(Quaternion rot);
}
