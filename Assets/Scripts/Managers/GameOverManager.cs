using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
	[SerializeField] GameObject gameOverScreenContainer;
	[SerializeField] GameObject winText;
	[SerializeField] GameObject loseText;

	private void Start()
	{
		gameOverScreenContainer.SetActive(false);
	}

	public void SetGameOver(bool victory)
	{
		if (victory)
		{
			loseText.SetActive(false);
			winText.SetActive(true);
			gameOverScreenContainer.SetActive(true);
		}
		else
		{
			loseText.SetActive(true);
			winText.SetActive(false);
			gameOverScreenContainer.SetActive(true);
		}

		Time.timeScale = 0;
	}
}
