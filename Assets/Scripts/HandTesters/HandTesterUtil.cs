using System;

public static class HandTesterUtil {
	public static bool TestHandForStraight(Card[] hand) {
		CardValue lowValue = hand[0].Value;
		for (int i = 1; i < hand.Length; i++) {
			Card card = hand[i];
			if (card.Value < lowValue) {
				lowValue = card.Value;
			} else if (card.Value == lowValue) {
				return false;
			}
		}

		for (int i = 1; i < hand.Length; i++) { // assuming straight is full size of hand
			if (!Array.Exists(hand, card => (int) card.Value == (int) (lowValue + 1))) { return false; }
			lowValue = lowValue + 1;
		}

		return true;
	}

	public static bool TestHandForFlush(Card[] hand) {
		CardSuit cardSuitToMatch = hand[0].Suit;
        
        for (int i = 1; i < hand.Length; i++) {
        	if (hand[i].Suit != cardSuitToMatch) { return false; } // assuming flush is full size of hand
        }

        return true;
	}
}