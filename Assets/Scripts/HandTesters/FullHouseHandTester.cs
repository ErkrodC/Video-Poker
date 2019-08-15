using System.Collections.Generic;

public class FullHouseHandTester : HandTester {
	private readonly Dictionary<CardValue, int> cardValueCounts;

	public FullHouseHandTester(RewardCurve rewardCurve) : base(rewardCurve) {
		cardValueCounts = new Dictionary<CardValue, int>();
	}

	public override bool TestHand(Card[] hand) {
		cardValueCounts.Clear();

		foreach (Card card in hand) {
			if (cardValueCounts.ContainsKey(card.Value)) { ++cardValueCounts[card.Value]; }
			else { cardValueCounts.Add(card.Value, 1); }
		}

		if (cardValueCounts.Count != 2) { return false; }

		bool containsPair = false;
		bool containsThreeOfAKind = false;

		foreach (CardValue key in cardValueCounts.Keys) {
			if (cardValueCounts[key] == 2) { containsPair = true; }
			if (cardValueCounts[key] == 3) { containsThreeOfAKind = true; }
		}

		return containsPair && containsThreeOfAKind;
	}

	public override string GetHandName() => "Full House";
}