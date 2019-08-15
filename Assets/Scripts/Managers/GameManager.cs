using System;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager> {
	public event Action<PlayerState> InitialHandDealt;
	public event Action<Card[]> CardsRedealt;
	public event Action<int> RedealFlagToggled;
	public event Action<string, int, PlayerState> PlayerWon;
	public event Action<int> BetChanged;
	public event Action<PlayerState> PlayerJoined;

	private const int kHandSize = 5;
	private const int kStartingCredits = 5;
	private const int kDefaultBet = 1;

	private Deck currentDeck;
	private PlayerState currentPlayer;
	private int currentBet = kDefaultBet;

	private void Start() {
		Initialize();
	}

	public void StartNewGame() {
		currentDeck.Reset();

		if (currentPlayer.Credits > 0) {
			currentPlayer.Credits -= currentBet;
			Deal();
		}
	}

	public void IncreaseBet(int increaseAmount) {
		currentBet = Mathf.Min(currentBet + increaseAmount, currentPlayer.Credits);
		BetChanged?.Invoke(currentBet);
	}

	public void Redeal() {
		for (int i = 0; i < currentPlayer.RedealFlags.Length; i++) {
			if (currentPlayer.RedealFlags[i]) {
				currentDeck.DiscardCard(currentPlayer.Hand[i]);
				currentPlayer.Hand[i] = currentDeck.GetTopCard();
			}

			currentPlayer.RedealFlags[i] = false;
		}

		CardsRedealt?.Invoke(currentPlayer.Hand);

		if (Rules.TestHand(currentPlayer.Hand, currentBet, out string winningHandName, out int reward)) {
			currentPlayer.Credits += reward;
			PlayerWon?.Invoke(winningHandName, reward, currentPlayer);
		}

		currentBet = kDefaultBet;
		BetChanged?.Invoke(currentBet);
	}

	public void OnVisualCardButton(int handIndex) {
		currentPlayer.RedealFlags[handIndex] = !currentPlayer.RedealFlags[handIndex];

		int redealFlagsCount = 0;
		foreach (bool redealFlag in currentPlayer.RedealFlags) {
			if (redealFlag) { ++redealFlagsCount; }
		}
		
		RedealFlagToggled?.Invoke(redealFlagsCount);
	}

	public bool GetRedealFlag(int handIndex) {
		return currentPlayer.RedealFlags[handIndex];
	}

	private void Initialize() {
		currentDeck = new Deck();
		currentPlayer = new PlayerState(kHandSize, kStartingCredits);
		PlayerJoined?.Invoke(currentPlayer);
	}

	private void Deal() {
		for (int i = 0; i < kHandSize; i++) {
			currentPlayer.Hand[i] = currentDeck.GetTopCard();
		}
		
		InitialHandDealt?.Invoke(currentPlayer);
	}

	public int GetCurrentBet() {
		return currentBet;
	}
}