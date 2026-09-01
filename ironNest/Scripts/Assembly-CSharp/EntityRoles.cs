using System;

[Flags]
public enum EntityRoles
{
	None = 0,
	Enemy = 1,
	Ally = 2,
	Spotter = 6,
	AllyGroup1 = 0xA,
	AllyGroup2 = 0x12,
	Target = 0x20,
	OptionalTarget = 0x40,
	EnemyGroup1 = 0x81,
	EnemyGroup2 = 0x101,
	EnemyGroup3 = 0x201,
	Artillery = 0x8000,
	Fortification = 0x10000,
	Infantry = 0x20000,
	Tank = 0x40000,
	ListeningPost = 0x80000,
	AABattery = 0x100000,
	Reference = 0x2000000
}
