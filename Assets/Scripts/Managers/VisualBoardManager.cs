using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualBoardManager : MonoBehaviour {
	[SerializeField] private VisualCard visualCardPrefab;
	[SerializeField] private Transform playerHandParent;
	[SerializeField] private Button NewGameButton;
	[SerializeField] private Button RedealButton;
	[SerializeField] private Text RedealButtonText;
	[SerializeField] private Image RedealButtonImage;
	[SerializeField] private Text WinningHandText;
	[SerializeField] private Text CreditsText;
	[SerializeField] private Text BetText;
	[SerializeField] private Button IncreaseBetButton;

	private List<VisualCard> visualPlayerHand;

	private void Start() {
		visualPlayerHand = new List<VisualCard>();
		
		GameManager.Instance.InitialHandDealt += OnInitialHandDealt;
		GameManager.Instance.CardsRedealt += OnCardsRedealt;
		GameManager.Instance.RedealFlagToggled += UpdateRedealButton;
		GameManager.Instance.PlayerWon += OnPlayerWon;
		GameManager.Instance.BetChanged += UpdateBetText;
		GameManager.Instance.PlayerJoined += OnPlayerJoined;

		NewGameButton.onClick.AddListener(GameManager.Instance.StartNewGame);
		RedealButton.onClick.AddListener(GameManager.Instance.Redeal);
		IncreaseBetButton.onClick.AddListener(() => GameManager.Instance.IncreaseBet(1)); // just hard coding bet increase for now.
		
		NewGameButton.gameObject.SetActive(true);
		RedealButton.gameObject.SetActive(false);
		IncreaseBetButton.gameObject.SetActive(true);
	}

	public void OnPlayerWon(string winningHandName, int reward, PlayerState player) {
		WinningHandText.text = $"{winningHandName}!\nReward: {reward}";
		WinningHandText.gameObject.SetActive(true);
		UpdateCreditsText(player.Credits);
	}

	private void UpdateHand(Card[] hand) {
		foreach (VisualCard visualCard in visualPlayerHand) {
			Destroy(visualCard.gameObject);
		}

		visualPlayerHand.Clear();
		
		for (int i = 0; i < hand.Length; i++) {
			Card card = hand[i];
			VisualCard newVisualCard = Instantiate(visualCardPrefab, playerHandParent);
			newVisualCard.InitializeCard(card, i);
			visualPlayerHand.Add(newVisualCard);
		}
	}

	private void OnInitialHandDealt(PlayerState player) {
		NewGameButton.gameObject.SetActive(false);
		RedealButton.gameObject.SetActive(true);
		IncreaseBetButton.gameObject.SetActive(false);
		WinningHandText.gameObject.SetActive(false);
		UpdateRedealButton(0);
		UpdateHand(player.Hand);
		SetAllVisualCardsInteractable(true);
		UpdateCreditsText(player.Credits);
	}

	private void OnCardsRedealt(Card[] hand) {
		NewGameButton.gameObject.SetActive(true);
		RedealButton.gameObject.SetActive(false);
		IncreaseBetButton.gameObject.SetActive(true);
		UpdateHand(hand);
		SetAllVisualCardsInteractable(false);
	}

	private void UpdateRedealButton(int numRedealFlags) {
		if (numRedealFlags == 0) {
			RedealButtonText.text = "Hold";
			RedealButtonImage.color = Color.magenta;
		} else {
			RedealButtonText.text = "Redeal";
			RedealButtonImage.color = Color.yellow;
		}
	}

	private void SetAllVisualCardsInteractable(bool interactable) {
		foreach (VisualCard visualCard in visualPlayerHand) {
			visualCard.SetInteractable(interactable);
		}
	}

	private void OnPlayerJoined(PlayerState player) {
		UpdateCreditsText(player.Credits);
		UpdateBetText(GameManager.Instance.GetCurrentBet());
	}

	private void UpdateCreditsText(int credits) {
		CreditsText.text = $"Credits: {credits}";
	}

	private void UpdateBetText(int updatedBet) {
		BetText.text = $"Bet: {updatedBet}";
	}
}