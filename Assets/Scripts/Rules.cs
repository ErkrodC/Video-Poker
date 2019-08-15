using System;

public static class Rules {
	public static readonly HandTester[] HandTestersDescWinningPriority = {
		new RoyalFlushHandTester(RewardCurveLib.RoyalFlush),
		new StraightFlushHandTester(RewardCurveLib.StraightFlush),
		new OfAKindHandTester(4, RewardCurveLib.FourAces, CardValue.A),
		new OfAKindHandTester(4, RewardCurveLib.Four234, CardValue.Two, CardValue.Three, CardValue.Four),
		new OfAKindHandTester(4, RewardCurveLib.Four5ThruK,
			CardValue.Five, CardValue.Six, CardValue.Seven, CardValue.Eight, CardValue.Nine, CardValue.Ten, CardValue.J, CardValue.Q, CardValue.K
		),
		new FullHouseHandTester(RewardCurveLib.FullHouse),
		new FlushHandTester(RewardCurveLib.Flush),
		new StraightHandTester(RewardCurveLib.Straight),
		new OfAKindHandTester(3, RewardCurveLib.ThreeOfAKind, (CardValue[]) Enum.GetValues(typeof(CardValue))),
		new TwoPairHandTester(RewardCurveLib.TwoPair),
		new OfAKindHandTester(2, RewardCurveLib.JacksOrBetter, CardValue.J, CardValue.Q, CardValue.K, CardValue.A)
	};

	public static bool TestHand(Card[] hand, int initialBet, out string winningHandName, out int reward) {
		foreach (HandTester handTester in HandTestersDescWinningPriority) {
			if (handTester.TestHand(hand)) {
				winningHandName = handTester.GetHandName();
				reward = handTester.RewardCurve.GetReward(initialBet);
				return true;
			}
		}

		winningHandName = string.Empty;
		reward = -1;
		return false;
	}
}