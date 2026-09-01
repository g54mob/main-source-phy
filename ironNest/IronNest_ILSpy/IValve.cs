using System;

public interface IValve
{
	event Action<float> DamageChanged01;

	float GetDamage01();

	void Damage();

	void ForceFix();

	void SetDamage01(float damage01);
}
