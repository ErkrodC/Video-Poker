using System;

public class RoyalFlushHandTester : HandTester {
	private readonly CardValue[] cardValuesToMatch = {
		CardValue.A,
		CardValue.K,
		CardValue.Q,
		CardValue.J,
		CardValue.Ten
	};

	public RoyalFlushHandTester(RewardCurve rewardCurve) : base(rewardCurve) { }

	public override bool TestHand(Card[] hand) {
		if (!HandTesterUtil.TestHandForFlush(hand)) { return false; }

		foreach (CardValue cardValueToMatch in cardValuesToMatch) {
			if (!Array.Exists(hand, card => card.Value == cardValueToMatch)) { return false; }
		}

		return true;
	}

	public override string GetHandName() => "Royal Flush";
}
