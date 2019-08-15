using System;
using System.Collections.Generic;
using UnityEngine;

public class RewardCurve {
	private readonly List<Point> curve;

	public RewardCurve(params (int Credits, int Reward)[] points) {
		curve = new List<Point>();
		
		foreach ((int credits, int reward) in points) {
			curve.Add(new Point(credits, reward));
		}
		curve.Sort();
	}

	public int GetReward(int creditsSpent) {
		int highIndex = 0;
		for (int i = 0; i < curve.Count; i++) {
			if (curve[i].Credits > creditsSpent) {
				highIndex = i;
				break;
			}

			if (i == curve.Count - 1) {
				highIndex = i;
			}
		}

		if (highIndex == 0) { return curve[highIndex].Reward; }

		Point lowPoint = curve[highIndex - 1];
		Point highPoint = curve[highIndex];
			
		float t = (float) (creditsSpent - lowPoint.Credits) / (highPoint.Credits - lowPoint.Credits);
		return Mathf.RoundToInt(Mathf.Lerp(lowPoint.Reward, highPoint.Reward, t));
	}

	public List<Point> GetPoints() {
		return curve;
	}

	public struct Point : IComparable<Point> {
		public int Credits;
		public int Reward;

		public Point(int credits, int reward) {
			Credits = credits;
			Reward = reward;
		}

		public int CompareTo(Point other) => Credits.CompareTo(other.Credits);
	}
}

public static class RewardCurveLib {
	public static readonly RewardCurve RoyalFlush = new RewardCurve(
		(1, 250),
		(2, 500),
		(3, 750),
		(4, 1000),
		(5, 4000)
	);
	
	public static readonly RewardCurve StraightFlush = new RewardCurve(
		(1, 50),
		(2, 100),
		(3, 150),
		(4, 200),
		(5, 250)
	);
	
	public static readonly RewardCurve FourAces = new RewardCurve(
		(1, 160),
		(2, 320),
		(3, 480),
		(4, 640),
		(5, 800)
	);
	
	public static readonly RewardCurve Four234 = new RewardCurve(
		(1, 80),
		(2, 160),
		(3, 240),
		(4, 320),
		(5, 400)
	);
	
	public static readonly RewardCurve Four5ThruK = new RewardCurve(
		(1, 50),
		(2, 100),
		(3, 150),
		(4, 200),
		(5, 250)
	);
	
	public static readonly RewardCurve FullHouse = new RewardCurve(
		(1, 10),
		(2, 20),
		(3, 30),
		(4, 40),
		(5, 50)
	);
	
	public static readonly RewardCurve Flush = new RewardCurve(
		(1, 7),
		(2, 14),
		(3, 21),
		(4, 28),
		(5, 35)
	);
	
	public static readonly RewardCurve Straight = new RewardCurve(
		(1, 5),
		(2, 10),
		(3, 15),
		(4, 20),
		(5, 25)
	);
	
	public static readonly RewardCurve ThreeOfAKind = new RewardCurve(
		(1, 3),
		(2, 6),
		(3, 9),
		(4, 12),
		(5, 15)
	);
	
	public static readonly RewardCurve TwoPair = new RewardCurve(
		(1, 1),
		(2, 2),
		(3, 3),
		(4, 4),
		(5, 5)
	);
	
	public static readonly RewardCurve JacksOrBetter = new RewardCurve(
		(1, 1),
		(2, 2),
		(3, 3),
		(4, 4),
		(5, 5)
	);
}
