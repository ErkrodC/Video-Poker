using System;

public class OfAKindHandTester : HandTester {
	private readonly CardValue[] testerCardValuesToMatch;
	private readonly int numOfAKind;
	
	public OfAKindHandTester(int numOfAKind, RewardCurve rewardCurve, params CardValue[] testerCardValuesToMatch) : base (rewardCurve) {
		this.testerCardValuesToMatch = testerCardValuesToMatch;
		this.numOfAKind = numOfAKind;
	}
	
	public override bool TestHand(Card[] hand) {
		foreach (CardValue cardValueToMatch in testerCardValuesToMatch) {
			int cardValueCount = 0;
			
			foreach (Card card in hand) {
				if (card.Value == cardValueToMatch) { ++cardValueCount; }
			}

			if (cardValueCount == numOfAKind) { return true; }
		}

		return false;
	}

	public override string GetHandName() {
		if (numOfAKind == 2) { return "Pair"; }

		string name = $"{numOfAKind} of a Kind";

		if (testerCardValuesToMatch.Length != Enum.GetValues(typeof(CardValue)).Length) {
			if (testerCardValuesToMatch.Length == 1) { name = $"{numOfAKind} {testerCardValuesToMatch[0]}'s"; }
			else {
				CardValue lowValue = testerCardValuesToMatch[0];
				CardValue highValue = testerCardValuesToMatch[testerCardValuesToMatch.Length - 1];
				name += $" {lowValue}-{highValue}";
			}
		}

		return name;
	}
}
