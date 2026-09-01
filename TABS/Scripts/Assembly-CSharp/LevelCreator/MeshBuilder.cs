using UnityEngine;

namespace LevelCreator
{
	public class MeshBuilder
	{
		private static readonly int[] edgeTable = new int[256]
		{
			0, 265, 515, 778, 1030, 1295, 1541, 1804, 2060, 2309,
			2575, 2822, 3082, 3331, 3593, 3840, 400, 153, 915, 666,
			1430, 1183, 1941, 1692, 2460, 2197, 2975, 2710, 3482, 3219,
			3993, 3728, 560, 825, 51, 314, 1590, 1855, 1077, 1340,
			2620, 2869, 2111, 2358, 3642, 3891, 3129, 3376, 928, 681,
			419, 170, 1958, 1711, 1445, 1196, 2988, 2725, 2479, 2214,
			4010, 3747, 3497, 3232, 1120, 1385, 1635, 1898, 102, 367,
			613, 876, 3180, 3429, 3695, 3942, 2154, 2403, 2665, 2912,
			1520, 1273, 2035, 1786, 502, 255, 1013, 764, 3580, 3317,
			4095, 3830, 2554, 2291, 3065, 2800, 1616, 1881, 1107, 1370,
			598, 863, 85, 348, 3676, 3925, 3167, 3414, 2650, 2899,
			2137, 2384, 1984, 1737, 1475, 1226, 966, 719, 453, 204,
			4044, 3781, 3535, 3270, 3018, 2755, 2505, 2240, 2240, 2505,
			2755, 3018, 3270, 3535, 3781, 4044, 204, 453, 719, 966,
			1226, 1475, 1737, 1984, 2384, 2137, 2899, 2650, 3414, 3167,
			3925, 3676, 348, 85, 863, 598, 1370, 1107, 1881, 1616,
			2800, 3065, 2291, 2554, 3830, 4095, 3317, 3580, 764, 1013,
			255, 502, 1786, 2035, 1273, 1520, 2912, 2665, 2403, 2154,
			3942, 3695, 3429, 3180, 876, 613, 367, 102, 1898, 1635,
			1385, 1120, 3232, 3497, 3747, 4010, 2214, 2479, 2725, 2988,
			1196, 1445, 1711, 1958, 170, 419, 681, 928, 3376, 3129,
			3891, 3642, 2358, 2111, 2869, 2620, 1340, 1077, 1855, 1590,
			314, 51, 825, 560, 3728, 3993, 3219, 3482, 2710, 2975,
			2197, 2460, 1692, 1941, 1183, 1430, 666, 915, 153, 400,
			3840, 3593, 3331, 3082, 2822, 2575, 2309, 2060, 1804, 1541,
			1295, 1030, 778, 515, 265, 0
		};

		private static readonly int[][] triTable = new int[256][]
		{
			new int[0],
			new int[3] { 0, 8, 3 },
			new int[3] { 0, 1, 9 },
			new int[6] { 1, 8, 3, 9, 8, 1 },
			new int[3] { 1, 2, 10 },
			new int[6] { 0, 8, 3, 1, 2, 10 },
			new int[6] { 9, 2, 10, 0, 2, 9 },
			new int[9] { 2, 8, 3, 2, 10, 8, 10, 9, 8 },
			new int[3] { 3, 11, 2 },
			new int[6] { 0, 11, 2, 8, 11, 0 },
			new int[6] { 1, 9, 0, 2, 3, 11 },
			new int[9] { 1, 11, 2, 1, 9, 11, 9, 8, 11 },
			new int[6] { 3, 10, 1, 11, 10, 3 },
			new int[9] { 0, 10, 1, 0, 8, 10, 8, 11, 10 },
			new int[9] { 3, 9, 0, 3, 11, 9, 11, 10, 9 },
			new int[6] { 9, 8, 10, 10, 8, 11 },
			new int[3] { 4, 7, 8 },
			new int[6] { 4, 3, 0, 7, 3, 4 },
			new int[6] { 0, 1, 9, 8, 4, 7 },
			new int[9] { 4, 1, 9, 4, 7, 1, 7, 3, 1 },
			new int[6] { 1, 2, 10, 8, 4, 7 },
			new int[9] { 3, 4, 7, 3, 0, 4, 1, 2, 10 },
			new int[9] { 9, 2, 10, 9, 0, 2, 8, 4, 7 },
			new int[12]
			{
				2, 10, 9, 2, 9, 7, 2, 7, 3, 7,
				9, 4
			},
			new int[6] { 8, 4, 7, 3, 11, 2 },
			new int[9] { 11, 4, 7, 11, 2, 4, 2, 0, 4 },
			new int[9] { 9, 0, 1, 8, 4, 7, 2, 3, 11 },
			new int[12]
			{
				4, 7, 11, 9, 4, 11, 9, 11, 2, 9,
				2, 1
			},
			new int[9] { 3, 10, 1, 3, 11, 10, 7, 8, 4 },
			new int[12]
			{
				1, 11, 10, 1, 4, 11, 1, 0, 4, 7,
				11, 4
			},
			new int[12]
			{
				4, 7, 8, 9, 0, 11, 9, 11, 10, 11,
				0, 3
			},
			new int[9] { 4, 7, 11, 4, 11, 9, 9, 11, 10 },
			new int[3] { 9, 5, 4 },
			new int[6] { 9, 5, 4, 0, 8, 3 },
			new int[6] { 0, 5, 4, 1, 5, 0 },
			new int[9] { 8, 5, 4, 8, 3, 5, 3, 1, 5 },
			new int[6] { 1, 2, 10, 9, 5, 4 },
			new int[9] { 3, 0, 8, 1, 2, 10, 4, 9, 5 },
			new int[9] { 5, 2, 10, 5, 4, 2, 4, 0, 2 },
			new int[12]
			{
				2, 10, 5, 3, 2, 5, 3, 5, 4, 3,
				4, 8
			},
			new int[6] { 9, 5, 4, 2, 3, 11 },
			new int[9] { 0, 11, 2, 0, 8, 11, 4, 9, 5 },
			new int[9] { 0, 5, 4, 0, 1, 5, 2, 3, 11 },
			new int[12]
			{
				2, 1, 5, 2, 5, 8, 2, 8, 11, 4,
				8, 5
			},
			new int[9] { 10, 3, 11, 10, 1, 3, 9, 5, 4 },
			new int[12]
			{
				4, 9, 5, 0, 8, 1, 8, 10, 1, 8,
				11, 10
			},
			new int[12]
			{
				5, 4, 0, 5, 0, 11, 5, 11, 10, 11,
				0, 3
			},
			new int[9] { 5, 4, 8, 5, 8, 10, 10, 8, 11 },
			new int[6] { 9, 7, 8, 5, 7, 9 },
			new int[9] { 9, 3, 0, 9, 5, 3, 5, 7, 3 },
			new int[9] { 0, 7, 8, 0, 1, 7, 1, 5, 7 },
			new int[6] { 1, 5, 3, 3, 5, 7 },
			new int[9] { 9, 7, 8, 9, 5, 7, 10, 1, 2 },
			new int[12]
			{
				10, 1, 2, 9, 5, 0, 5, 3, 0, 5,
				7, 3
			},
			new int[12]
			{
				8, 0, 2, 8, 2, 5, 8, 5, 7, 10,
				5, 2
			},
			new int[9] { 2, 10, 5, 2, 5, 3, 3, 5, 7 },
			new int[9] { 7, 9, 5, 7, 8, 9, 3, 11, 2 },
			new int[12]
			{
				9, 5, 7, 9, 7, 2, 9, 2, 0, 2,
				7, 11
			},
			new int[12]
			{
				2, 3, 11, 0, 1, 8, 1, 7, 8, 1,
				5, 7
			},
			new int[9] { 11, 2, 1, 11, 1, 7, 7, 1, 5 },
			new int[12]
			{
				9, 5, 8, 8, 5, 7, 10, 1, 3, 10,
				3, 11
			},
			new int[15]
			{
				5, 7, 0, 5, 0, 9, 7, 11, 0, 1,
				0, 10, 11, 10, 0
			},
			new int[15]
			{
				11, 10, 0, 11, 0, 3, 10, 5, 0, 8,
				0, 7, 5, 7, 0
			},
			new int[6] { 11, 10, 5, 7, 11, 5 },
			new int[3] { 10, 6, 5 },
			new int[6] { 0, 8, 3, 5, 10, 6 },
			new int[6] { 9, 0, 1, 5, 10, 6 },
			new int[9] { 1, 8, 3, 1, 9, 8, 5, 10, 6 },
			new int[6] { 1, 6, 5, 2, 6, 1 },
			new int[9] { 1, 6, 5, 1, 2, 6, 3, 0, 8 },
			new int[9] { 9, 6, 5, 9, 0, 6, 0, 2, 6 },
			new int[12]
			{
				5, 9, 8, 5, 8, 2, 5, 2, 6, 3,
				2, 8
			},
			new int[6] { 2, 3, 11, 10, 6, 5 },
			new int[9] { 11, 0, 8, 11, 2, 0, 10, 6, 5 },
			new int[9] { 0, 1, 9, 2, 3, 11, 5, 10, 6 },
			new int[12]
			{
				5, 10, 6, 1, 9, 2, 9, 11, 2, 9,
				8, 11
			},
			new int[9] { 6, 3, 11, 6, 5, 3, 5, 1, 3 },
			new int[12]
			{
				0, 8, 11, 0, 11, 5, 0, 5, 1, 5,
				11, 6
			},
			new int[12]
			{
				3, 11, 6, 0, 3, 6, 0, 6, 5, 0,
				5, 9
			},
			new int[9] { 6, 5, 9, 6, 9, 11, 11, 9, 8 },
			new int[6] { 5, 10, 6, 4, 7, 8 },
			new int[9] { 4, 3, 0, 4, 7, 3, 6, 5, 10 },
			new int[9] { 1, 9, 0, 5, 10, 6, 8, 4, 7 },
			new int[12]
			{
				10, 6, 5, 1, 9, 7, 1, 7, 3, 7,
				9, 4
			},
			new int[9] { 6, 1, 2, 6, 5, 1, 4, 7, 8 },
			new int[12]
			{
				1, 2, 5, 5, 2, 6, 3, 0, 4, 3,
				4, 7
			},
			new int[12]
			{
				8, 4, 7, 9, 0, 5, 0, 6, 5, 0,
				2, 6
			},
			new int[15]
			{
				7, 3, 9, 7, 9, 4, 3, 2, 9, 5,
				9, 6, 2, 6, 9
			},
			new int[9] { 3, 11, 2, 7, 8, 4, 10, 6, 5 },
			new int[12]
			{
				5, 10, 6, 4, 7, 2, 4, 2, 0, 2,
				7, 11
			},
			new int[12]
			{
				0, 1, 9, 4, 7, 8, 2, 3, 11, 5,
				10, 6
			},
			new int[15]
			{
				9, 2, 1, 9, 11, 2, 9, 4, 11, 7,
				11, 4, 5, 10, 6
			},
			new int[12]
			{
				8, 4, 7, 3, 11, 5, 3, 5, 1, 5,
				11, 6
			},
			new int[15]
			{
				5, 1, 11, 5, 11, 6, 1, 0, 11, 7,
				11, 4, 0, 4, 11
			},
			new int[15]
			{
				0, 5, 9, 0, 6, 5, 0, 3, 6, 11,
				6, 3, 8, 4, 7
			},
			new int[12]
			{
				6, 5, 9, 6, 9, 11, 4, 7, 9, 7,
				11, 9
			},
			new int[6] { 10, 4, 9, 6, 4, 10 },
			new int[9] { 4, 10, 6, 4, 9, 10, 0, 8, 3 },
			new int[9] { 10, 0, 1, 10, 6, 0, 6, 4, 0 },
			new int[12]
			{
				8, 3, 1, 8, 1, 6, 8, 6, 4, 6,
				1, 10
			},
			new int[9] { 1, 4, 9, 1, 2, 4, 2, 6, 4 },
			new int[12]
			{
				3, 0, 8, 1, 2, 9, 2, 4, 9, 2,
				6, 4
			},
			new int[6] { 0, 2, 4, 4, 2, 6 },
			new int[9] { 8, 3, 2, 8, 2, 4, 4, 2, 6 },
			new int[9] { 10, 4, 9, 10, 6, 4, 11, 2, 3 },
			new int[12]
			{
				0, 8, 2, 2, 8, 11, 4, 9, 10, 4,
				10, 6
			},
			new int[12]
			{
				3, 11, 2, 0, 1, 6, 0, 6, 4, 6,
				1, 10
			},
			new int[15]
			{
				6, 4, 1, 6, 1, 10, 4, 8, 1, 2,
				1, 11, 8, 11, 1
			},
			new int[12]
			{
				9, 6, 4, 9, 3, 6, 9, 1, 3, 11,
				6, 3
			},
			new int[15]
			{
				8, 11, 1, 8, 1, 0, 11, 6, 1, 9,
				1, 4, 6, 4, 1
			},
			new int[9] { 3, 11, 6, 3, 6, 0, 0, 6, 4 },
			new int[6] { 6, 4, 8, 11, 6, 8 },
			new int[9] { 7, 10, 6, 7, 8, 10, 8, 9, 10 },
			new int[12]
			{
				0, 7, 3, 0, 10, 7, 0, 9, 10, 6,
				7, 10
			},
			new int[12]
			{
				10, 6, 7, 1, 10, 7, 1, 7, 8, 1,
				8, 0
			},
			new int[9] { 10, 6, 7, 10, 7, 1, 1, 7, 3 },
			new int[12]
			{
				1, 2, 6, 1, 6, 8, 1, 8, 9, 8,
				6, 7
			},
			new int[15]
			{
				2, 6, 9, 2, 9, 1, 6, 7, 9, 0,
				9, 3, 7, 3, 9
			},
			new int[9] { 7, 8, 0, 7, 0, 6, 6, 0, 2 },
			new int[6] { 7, 3, 2, 6, 7, 2 },
			new int[12]
			{
				2, 3, 11, 10, 6, 8, 10, 8, 9, 8,
				6, 7
			},
			new int[15]
			{
				2, 0, 7, 2, 7, 11, 0, 9, 7, 6,
				7, 10, 9, 10, 7
			},
			new int[15]
			{
				1, 8, 0, 1, 7, 8, 1, 10, 7, 6,
				7, 10, 2, 3, 11
			},
			new int[12]
			{
				11, 2, 1, 11, 1, 7, 10, 6, 1, 6,
				7, 1
			},
			new int[15]
			{
				8, 9, 6, 8, 6, 7, 9, 1, 6, 11,
				6, 3, 1, 3, 6
			},
			new int[6] { 0, 9, 1, 11, 6, 7 },
			new int[12]
			{
				7, 8, 0, 7, 0, 6, 3, 11, 0, 11,
				6, 0
			},
			new int[3] { 7, 11, 6 },
			new int[3] { 7, 6, 11 },
			new int[6] { 3, 0, 8, 11, 7, 6 },
			new int[6] { 0, 1, 9, 11, 7, 6 },
			new int[9] { 8, 1, 9, 8, 3, 1, 11, 7, 6 },
			new int[6] { 10, 1, 2, 6, 11, 7 },
			new int[9] { 1, 2, 10, 3, 0, 8, 6, 11, 7 },
			new int[9] { 2, 9, 0, 2, 10, 9, 6, 11, 7 },
			new int[12]
			{
				6, 11, 7, 2, 10, 3, 10, 8, 3, 10,
				9, 8
			},
			new int[6] { 7, 2, 3, 6, 2, 7 },
			new int[9] { 7, 0, 8, 7, 6, 0, 6, 2, 0 },
			new int[9] { 2, 7, 6, 2, 3, 7, 0, 1, 9 },
			new int[12]
			{
				1, 6, 2, 1, 8, 6, 1, 9, 8, 8,
				7, 6
			},
			new int[9] { 10, 7, 6, 10, 1, 7, 1, 3, 7 },
			new int[12]
			{
				10, 7, 6, 1, 7, 10, 1, 8, 7, 1,
				0, 8
			},
			new int[12]
			{
				0, 3, 7, 0, 7, 10, 0, 10, 9, 6,
				10, 7
			},
			new int[9] { 7, 6, 10, 7, 10, 8, 8, 10, 9 },
			new int[6] { 6, 8, 4, 11, 8, 6 },
			new int[9] { 3, 6, 11, 3, 0, 6, 0, 4, 6 },
			new int[9] { 8, 6, 11, 8, 4, 6, 9, 0, 1 },
			new int[12]
			{
				9, 4, 6, 9, 6, 3, 9, 3, 1, 11,
				3, 6
			},
			new int[9] { 6, 8, 4, 6, 11, 8, 2, 10, 1 },
			new int[12]
			{
				1, 2, 10, 3, 0, 11, 0, 6, 11, 0,
				4, 6
			},
			new int[12]
			{
				4, 11, 8, 4, 6, 11, 0, 2, 9, 2,
				10, 9
			},
			new int[15]
			{
				10, 9, 3, 10, 3, 2, 9, 4, 3, 11,
				3, 6, 4, 6, 3
			},
			new int[9] { 8, 2, 3, 8, 4, 2, 4, 6, 2 },
			new int[6] { 0, 4, 2, 4, 6, 2 },
			new int[12]
			{
				1, 9, 0, 2, 3, 4, 2, 4, 6, 4,
				3, 8
			},
			new int[9] { 1, 9, 4, 1, 4, 2, 2, 4, 6 },
			new int[12]
			{
				8, 1, 3, 8, 6, 1, 8, 4, 6, 6,
				10, 1
			},
			new int[9] { 10, 1, 0, 10, 0, 6, 6, 0, 4 },
			new int[15]
			{
				4, 6, 3, 4, 3, 8, 6, 10, 3, 0,
				3, 9, 10, 9, 3
			},
			new int[6] { 10, 9, 4, 6, 10, 4 },
			new int[6] { 4, 9, 5, 7, 6, 11 },
			new int[9] { 0, 8, 3, 4, 9, 5, 11, 7, 6 },
			new int[9] { 5, 0, 1, 5, 4, 0, 7, 6, 11 },
			new int[12]
			{
				11, 7, 6, 8, 3, 4, 3, 5, 4, 3,
				1, 5
			},
			new int[9] { 9, 5, 4, 10, 1, 2, 7, 6, 11 },
			new int[12]
			{
				6, 11, 7, 1, 2, 10, 0, 8, 3, 4,
				9, 5
			},
			new int[12]
			{
				7, 6, 11, 5, 4, 10, 4, 2, 10, 4,
				0, 2
			},
			new int[15]
			{
				3, 4, 8, 3, 5, 4, 3, 2, 5, 10,
				5, 2, 11, 7, 6
			},
			new int[9] { 7, 2, 3, 7, 6, 2, 5, 4, 9 },
			new int[12]
			{
				9, 5, 4, 0, 8, 6, 0, 6, 2, 6,
				8, 7
			},
			new int[12]
			{
				3, 6, 2, 3, 7, 6, 1, 5, 0, 5,
				4, 0
			},
			new int[15]
			{
				6, 2, 8, 6, 8, 7, 2, 1, 8, 4,
				8, 5, 1, 5, 8
			},
			new int[12]
			{
				9, 5, 4, 10, 1, 6, 1, 7, 6, 1,
				3, 7
			},
			new int[15]
			{
				1, 6, 10, 1, 7, 6, 1, 0, 7, 8,
				7, 0, 9, 5, 4
			},
			new int[15]
			{
				4, 0, 10, 4, 10, 5, 0, 3, 10, 6,
				10, 7, 3, 7, 10
			},
			new int[12]
			{
				7, 6, 10, 7, 10, 8, 5, 4, 10, 4,
				8, 10
			},
			new int[9] { 6, 9, 5, 6, 11, 9, 11, 8, 9 },
			new int[12]
			{
				3, 6, 11, 0, 6, 3, 0, 5, 6, 0,
				9, 5
			},
			new int[12]
			{
				0, 11, 8, 0, 5, 11, 0, 1, 5, 5,
				6, 11
			},
			new int[9] { 6, 11, 3, 6, 3, 5, 5, 3, 1 },
			new int[12]
			{
				1, 2, 10, 9, 5, 11, 9, 11, 8, 11,
				5, 6
			},
			new int[15]
			{
				0, 11, 3, 0, 6, 11, 0, 9, 6, 5,
				6, 9, 1, 2, 10
			},
			new int[15]
			{
				11, 8, 5, 11, 5, 6, 8, 0, 5, 10,
				5, 2, 0, 2, 5
			},
			new int[12]
			{
				6, 11, 3, 6, 3, 5, 2, 10, 3, 10,
				5, 3
			},
			new int[12]
			{
				5, 8, 9, 5, 2, 8, 5, 6, 2, 3,
				8, 2
			},
			new int[9] { 9, 5, 6, 9, 6, 0, 0, 6, 2 },
			new int[15]
			{
				1, 5, 8, 1, 8, 0, 5, 6, 8, 3,
				8, 2, 6, 2, 8
			},
			new int[6] { 1, 5, 6, 2, 1, 6 },
			new int[15]
			{
				1, 3, 6, 1, 6, 10, 3, 8, 6, 5,
				6, 9, 8, 9, 6
			},
			new int[12]
			{
				10, 1, 0, 10, 0, 6, 9, 5, 0, 5,
				6, 0
			},
			new int[6] { 0, 3, 8, 5, 6, 10 },
			new int[3] { 10, 5, 6 },
			new int[6] { 11, 5, 10, 7, 5, 11 },
			new int[9] { 11, 5, 10, 11, 7, 5, 8, 3, 0 },
			new int[9] { 5, 11, 7, 5, 10, 11, 1, 9, 0 },
			new int[12]
			{
				10, 7, 5, 10, 11, 7, 9, 8, 1, 8,
				3, 1
			},
			new int[9] { 11, 1, 2, 11, 7, 1, 7, 5, 1 },
			new int[12]
			{
				0, 8, 3, 1, 2, 7, 1, 7, 5, 7,
				2, 11
			},
			new int[12]
			{
				9, 7, 5, 9, 2, 7, 9, 0, 2, 2,
				11, 7
			},
			new int[15]
			{
				7, 5, 2, 7, 2, 11, 5, 9, 2, 3,
				2, 8, 9, 8, 2
			},
			new int[9] { 2, 5, 10, 2, 3, 5, 3, 7, 5 },
			new int[12]
			{
				8, 2, 0, 8, 5, 2, 8, 7, 5, 10,
				2, 5
			},
			new int[12]
			{
				9, 0, 1, 5, 10, 3, 5, 3, 7, 3,
				10, 2
			},
			new int[15]
			{
				9, 8, 2, 9, 2, 1, 8, 7, 2, 10,
				2, 5, 7, 5, 2
			},
			new int[6] { 1, 3, 5, 3, 7, 5 },
			new int[9] { 0, 8, 7, 0, 7, 1, 1, 7, 5 },
			new int[9] { 9, 0, 3, 9, 3, 5, 5, 3, 7 },
			new int[6] { 9, 8, 7, 5, 9, 7 },
			new int[9] { 5, 8, 4, 5, 10, 8, 10, 11, 8 },
			new int[12]
			{
				5, 0, 4, 5, 11, 0, 5, 10, 11, 11,
				3, 0
			},
			new int[12]
			{
				0, 1, 9, 8, 4, 10, 8, 10, 11, 10,
				4, 5
			},
			new int[15]
			{
				10, 11, 4, 10, 4, 5, 11, 3, 4, 9,
				4, 1, 3, 1, 4
			},
			new int[12]
			{
				2, 5, 1, 2, 8, 5, 2, 11, 8, 4,
				5, 8
			},
			new int[15]
			{
				0, 4, 11, 0, 11, 3, 4, 5, 11, 2,
				11, 1, 5, 1, 11
			},
			new int[15]
			{
				0, 2, 5, 0, 5, 9, 2, 11, 5, 4,
				5, 8, 11, 8, 5
			},
			new int[6] { 9, 4, 5, 2, 11, 3 },
			new int[12]
			{
				2, 5, 10, 3, 5, 2, 3, 4, 5, 3,
				8, 4
			},
			new int[9] { 5, 10, 2, 5, 2, 4, 4, 2, 0 },
			new int[15]
			{
				3, 10, 2, 3, 5, 10, 3, 8, 5, 4,
				5, 8, 0, 1, 9
			},
			new int[12]
			{
				5, 10, 2, 5, 2, 4, 1, 9, 2, 9,
				4, 2
			},
			new int[9] { 8, 4, 5, 8, 5, 3, 3, 5, 1 },
			new int[6] { 0, 4, 5, 1, 0, 5 },
			new int[12]
			{
				8, 4, 5, 8, 5, 3, 9, 0, 5, 0,
				3, 5
			},
			new int[3] { 9, 4, 5 },
			new int[9] { 4, 11, 7, 4, 9, 11, 9, 10, 11 },
			new int[12]
			{
				0, 8, 3, 4, 9, 7, 9, 11, 7, 9,
				10, 11
			},
			new int[12]
			{
				1, 10, 11, 1, 11, 4, 1, 4, 0, 7,
				4, 11
			},
			new int[15]
			{
				3, 1, 4, 3, 4, 8, 1, 10, 4, 7,
				4, 11, 10, 11, 4
			},
			new int[12]
			{
				4, 11, 7, 9, 11, 4, 9, 2, 11, 9,
				1, 2
			},
			new int[15]
			{
				9, 7, 4, 9, 11, 7, 9, 1, 11, 2,
				11, 1, 0, 8, 3
			},
			new int[9] { 11, 7, 4, 11, 4, 2, 2, 4, 0 },
			new int[12]
			{
				11, 7, 4, 11, 4, 2, 8, 3, 4, 3,
				2, 4
			},
			new int[12]
			{
				2, 9, 10, 2, 7, 9, 2, 3, 7, 7,
				4, 9
			},
			new int[15]
			{
				9, 10, 7, 9, 7, 4, 10, 2, 7, 8,
				7, 0, 2, 0, 7
			},
			new int[15]
			{
				3, 7, 10, 3, 10, 2, 7, 4, 10, 1,
				10, 0, 4, 0, 10
			},
			new int[6] { 1, 10, 2, 8, 7, 4 },
			new int[9] { 4, 9, 1, 4, 1, 7, 7, 1, 3 },
			new int[12]
			{
				4, 9, 1, 4, 1, 7, 0, 8, 1, 8,
				7, 1
			},
			new int[6] { 4, 0, 3, 7, 4, 3 },
			new int[3] { 4, 8, 7 },
			new int[6] { 9, 10, 8, 10, 11, 8 },
			new int[9] { 3, 0, 9, 3, 9, 11, 11, 9, 10 },
			new int[9] { 0, 1, 10, 0, 10, 8, 8, 10, 11 },
			new int[6] { 3, 1, 10, 11, 3, 10 },
			new int[9] { 1, 2, 11, 1, 11, 9, 9, 11, 8 },
			new int[12]
			{
				3, 0, 9, 3, 9, 11, 1, 2, 9, 2,
				11, 9
			},
			new int[6] { 0, 2, 11, 8, 0, 11 },
			new int[3] { 3, 2, 11 },
			new int[9] { 2, 3, 8, 2, 8, 10, 10, 8, 9 },
			new int[6] { 9, 10, 2, 0, 9, 2 },
			new int[12]
			{
				2, 3, 8, 2, 8, 10, 0, 1, 8, 1,
				10, 8
			},
			new int[3] { 1, 10, 2 },
			new int[6] { 1, 3, 8, 9, 1, 8 },
			new int[3] { 0, 9, 1 },
			new int[3] { 0, 3, 8 },
			new int[0]
		};

		private static float GetMaterial(float[,,] materials, Vector3 midPoint, Vector3Int limitedChunkSize)
		{
			if (materials == null)
			{
				return 0f;
			}
			Vector3 vector = new Vector3(midPoint.x * (float)Level.MaterialChunk.noOfCells.x / (float)Level.VoxelChunk.noOfCells.x, midPoint.y * (float)Level.MaterialChunk.noOfCells.y / (float)Level.VoxelChunk.noOfCells.y, midPoint.z * (float)Level.MaterialChunk.noOfCells.z / (float)Level.VoxelChunk.noOfCells.z);
			int num = Mathf.Clamp(Mathf.FloorToInt(vector.x), 0, limitedChunkSize.x - 1);
			int num2 = Mathf.Min(num + 1, limitedChunkSize.x - 1);
			float t = vector.x - (float)num;
			int num3 = Mathf.Clamp(Mathf.FloorToInt(vector.y), 0, limitedChunkSize.y - 1);
			int num4 = Mathf.Min(num3 + 1, limitedChunkSize.y - 1);
			float t2 = vector.y - (float)num3;
			int num5 = Mathf.Clamp(Mathf.FloorToInt(vector.z), 0, limitedChunkSize.z - 1);
			int num6 = Mathf.Min(num5 + 1, limitedChunkSize.z - 1);
			float t3 = vector.z - (float)num5;
			return Utility.LerpCyclic(Utility.LerpCyclic(Utility.LerpCyclic(materials[num5, num3, num], materials[num5, num3, num2], t), Utility.LerpCyclic(materials[num5, num4, num], materials[num5, num4, num2], t), t2), Utility.LerpCyclic(Utility.LerpCyclic(materials[num6, num3, num], materials[num6, num3, num2], t), Utility.LerpCyclic(materials[num6, num4, num], materials[num6, num4, num2], t), t2), t3);
		}

		private static void AddTriangle(MeshData meshData, Vector3 pos1, Vector3 pos2, Vector3 pos3, Vector2 material)
		{
			Vector3 normalized = Vector3.Cross(pos2 - pos1, pos3 - pos1).normalized;
			meshData.vertices.Add(new MeshData.Vertex
			{
				position = pos1,
				normal = normalized,
				material = material
			});
			meshData.indices.Add(meshData.vertices.Count - 1);
			meshData.vertices.Add(new MeshData.Vertex
			{
				position = pos2,
				normal = normalized,
				material = material
			});
			meshData.indices.Add(meshData.vertices.Count - 1);
			meshData.vertices.Add(new MeshData.Vertex
			{
				position = pos3,
				normal = normalized,
				material = material
			});
			meshData.indices.Add(meshData.vertices.Count - 1);
		}

		private static Vector3 LinearInterp(float isolevel, ref Vector3 p1, ref Vector3 p2, float density1, float density2)
		{
			if ((double)Mathf.Abs(isolevel - density1) < 1E-05)
			{
				return p1;
			}
			if ((double)Mathf.Abs(isolevel - density2) < 1E-05)
			{
				return p2;
			}
			float num = (isolevel - density1) / (density2 - density1);
			return p1 + num * (p2 - p1);
		}

		private static void AddISOSurfaceMeshUsingMarchinCubes(MeshData meshData, float[,,] voxels, float[,,] materials, Vector3Int chunkPosition)
		{
			Vector3Int limitedChunkSize = ((materials == null) ? Vector3Int.zero : Vector3Int.Min(new Vector3Int(materials.GetLength(2) - 1, materials.GetLength(1) - 1, materials.GetLength(0) - 1), new Vector3Int(Level.MaterialChunk.materialBounds.max.x - 1 - Level.MaterialChunk.noOfCells.x * (chunkPosition.x / Level.VoxelChunk.noOfCells.x), Level.MaterialChunk.materialBounds.max.y - 1 - Level.MaterialChunk.noOfCells.y * (chunkPosition.y / Level.VoxelChunk.noOfCells.y), Level.MaterialChunk.materialBounds.max.z - 1 - Level.MaterialChunk.noOfCells.z * (chunkPosition.z / Level.VoxelChunk.noOfCells.z))));
			int num = voxels.GetLength(0) - 1;
			int num2 = voxels.GetLength(1) - 1;
			int num3 = voxels.GetLength(2) - 1;
			Vector3[] array = new Vector3[12];
			Vector3 p = Vector3.zero;
			Vector3 p2 = Vector3.zero;
			Vector3 p3 = Vector3.zero;
			Vector3 p4 = Vector3.zero;
			Vector3 p5 = Vector3.zero;
			Vector3 p6 = Vector3.zero;
			Vector3 p7 = Vector3.zero;
			Vector3 p8 = Vector3.zero;
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					for (int k = 0; k < num3; k++)
					{
						p.Set(k, j, i + 1);
						p2.Set(k + 1, j, i + 1);
						p3.Set(k + 1, j, i);
						p4.Set(k, j, i);
						p5.Set(k, j + 1, i + 1);
						p6.Set(k + 1, j + 1, i + 1);
						p7.Set(k + 1, j + 1, i);
						p8.Set(k, j + 1, i);
						float num4 = voxels[i + 1, j, k];
						float num5 = voxels[i + 1, j, k + 1];
						float num6 = voxels[i, j, k + 1];
						float num7 = voxels[i, j, k];
						float num8 = voxels[i + 1, j + 1, k];
						float num9 = voxels[i + 1, j + 1, k + 1];
						float num10 = voxels[i, j + 1, k + 1];
						float num11 = voxels[i, j + 1, k];
						int num12 = 0;
						if (num4 <= 0.5f)
						{
							num12 |= 1;
						}
						if (num5 <= 0.5f)
						{
							num12 |= 2;
						}
						if (num6 <= 0.5f)
						{
							num12 |= 4;
						}
						if (num7 <= 0.5f)
						{
							num12 |= 8;
						}
						if (num8 <= 0.5f)
						{
							num12 |= 0x10;
						}
						if (num9 <= 0.5f)
						{
							num12 |= 0x20;
						}
						if (num10 <= 0.5f)
						{
							num12 |= 0x40;
						}
						if (num11 <= 0.5f)
						{
							num12 |= 0x80;
						}
						if (num12 != 0 && num12 != 255)
						{
							if ((edgeTable[num12] & 1) != 0)
							{
								array[0] = LinearInterp(0.5f, ref p, ref p2, num4, num5);
							}
							if ((edgeTable[num12] & 2) != 0)
							{
								array[1] = LinearInterp(0.5f, ref p2, ref p3, num5, num6);
							}
							if ((edgeTable[num12] & 4) != 0)
							{
								array[2] = LinearInterp(0.5f, ref p3, ref p4, num6, num7);
							}
							if ((edgeTable[num12] & 8) != 0)
							{
								array[3] = LinearInterp(0.5f, ref p4, ref p, num7, num4);
							}
							if ((edgeTable[num12] & 0x10) != 0)
							{
								array[4] = LinearInterp(0.5f, ref p5, ref p6, num8, num9);
							}
							if ((edgeTable[num12] & 0x20) != 0)
							{
								array[5] = LinearInterp(0.5f, ref p6, ref p7, num9, num10);
							}
							if ((edgeTable[num12] & 0x40) != 0)
							{
								array[6] = LinearInterp(0.5f, ref p7, ref p8, num10, num11);
							}
							if ((edgeTable[num12] & 0x80) != 0)
							{
								array[7] = LinearInterp(0.5f, ref p8, ref p5, num11, num8);
							}
							if ((edgeTable[num12] & 0x100) != 0)
							{
								array[8] = LinearInterp(0.5f, ref p, ref p5, num4, num8);
							}
							if ((edgeTable[num12] & 0x200) != 0)
							{
								array[9] = LinearInterp(0.5f, ref p2, ref p6, num5, num9);
							}
							if ((edgeTable[num12] & 0x400) != 0)
							{
								array[10] = LinearInterp(0.5f, ref p3, ref p7, num6, num10);
							}
							if ((edgeTable[num12] & 0x800) != 0)
							{
								array[11] = LinearInterp(0.5f, ref p4, ref p8, num7, num11);
							}
							for (int l = 0; l < triTable[num12].Length; l += 3)
							{
								Vector3 vector = array[triTable[num12][l]];
								Vector3 vector2 = array[triTable[num12][l + 1]];
								Vector3 vector3 = array[triTable[num12][l + 2]];
								Vector3 midPoint = (vector + vector2 + vector3) / 3f;
								AddTriangle(meshData, vector, vector2, vector3, new Vector2(GetMaterial(materials, midPoint, limitedChunkSize), 0f));
							}
						}
					}
				}
			}
		}

		public static bool IsHomogenousChunk(float[,,] voxels)
		{
			bool flag = voxels[0, 0, 0] > 0.5f;
			foreach (float num in voxels)
			{
				if (flag != num > 0.5f)
				{
					return false;
				}
			}
			return true;
		}

		public static void BuildMeshData(MeshData meshData, float[,,] voxels, float[,,] materials, Vector3Int chunkPosition)
		{
			meshData.vertices.Clear();
			meshData.indices.Clear();
			if (!IsHomogenousChunk(voxels))
			{
				AddISOSurfaceMeshUsingMarchinCubes(meshData, voxels, materials, chunkPosition);
			}
		}
	}
}
