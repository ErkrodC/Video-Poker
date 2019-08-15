using System.Collections.Generic;

public class TwoPairHandTester : HandTester {
	private readonly Dictionary<CardValue, int> cardValueCounts; // NOTE realized late that most testers could base their logic on this card value count dict, could save some calculation costs

	public TwoPairHandTester(RewardCurve rewardCurve) : base(rewardCurve) {
		cardValueCounts = new Dictionary<CardValue, int>();
	}
	
	public override bool TestHand(Card[] hand) {
		cardValueCounts.Clear();

		foreach (Card card in hand) {
			if (cardValueCounts.ContainsKey(card.Value)) { ++cardValueCounts[card.Value]; }
			else { cardValueCounts.Add(card.Value, 1); }
		}

		if (cardValueCounts.Count < 2) { return false; }

		int pairsCount = 0;
		foreach (CardValue key in cardValueCounts.Keys) {
			if (cardValueCounts[key] == 2) { ++pairsCount; }
		}

		return pairsCount == 2;
	}

	public override string GetHandName() => "Two Pair";
}