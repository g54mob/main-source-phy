using System;

[Flags]
public enum GameModeState : uint
{
	None = 0u,
	Menu = 1u,
	Sandbox = 2u,
	Campaign = 4u,
	Test = 8u,
	LocalMultiplayer = 0x10u,
	OnlineMultiplayer = 0x20u,
	All = 0x3Fu
}
