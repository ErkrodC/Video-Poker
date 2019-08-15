using System;
using System.Collections.Generic;

public class Deck {
	private Random random;
	private List<Card> undealtCards;
	private List<Card> discardedCards;

	private CardSuit[] allCardSuits = (CardSuit[]) Enum.GetValues(typeof(CardSuit));
	private CardValue[] allCardValues = (CardValue[]) Enum.GetValues(typeof(CardValue));

	public Deck() {
		random = new Random();
		undealtCards = new List<Card>();
		discardedCards = new List<Card>();

		Reset();
	}

	public void Shuffle() {
		for (int i = undealtCards.Count - 1; i > 0; i--) {
			int swapWithPos = random.Next(i + 1);

			Card temp = undealtCards[i];
			undealtCards[i] = undealtCards[swapWithPos];
			undealtCards[swapWithPos] = temp;
		}
	}

	public Card GetTopCard() {
		Card topCard = undealtCards[undealtCards.Count - 1];
		undealtCards.RemoveAt(undealtCards.Count - 1);
		return topCard;
	}

	public void DiscardCard(Card discardedCard) {
		if (!discardedCards.Contains(discardedCard)) { discardedCards.Add(discardedCard); }
	}

	public void Reset() {
		undealtCards.Clear();
		discardedCards.Clear();
		
		foreach (CardSuit cardSuit in allCardSuits) {
			foreach (CardValue cardValue in allCardValues) {
				undealtCards.Add(new Card(cardSuit, cardValue));
			}
		}

		Shuffle();
	}
}