using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	//Private Fields---------------------------------------------------------------------------------------------------------------------------------

	//Serialized Fields----------------------------------------------------------------------------

	[SerializeField] private GameObject pauseMenu;
	
	//Non-Serialized Fields--------------------------------------------------------------------

	private bool pause;
	private GameOverManager gameOverManager;

	//Public Properties------------------------------------------------------------------------------------------------------------------------------

	public static bool GameIsPaused;

	//Initialization Methods-------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Start() is run on the frame when a script is enabled just before any of the Update methods are called for the first time. 
	/// Start() runs after Awake().
	/// </summary>
	private void Start()
	{
		gameOverManager = FindObjectOfType<GameOverManager>();
		Cursor.lockState = CursorLockMode.Locked;
		Resume();
	}

	//Core Recurring Methods-------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Update() is run every frame.
	/// </summary>
	void Update()
	{
		GetInput();
	}

	/// <summary>
	/// Gets the player's pausing input.
	/// </summary>
	private void GetInput()
	{
		if (!gameOverManager.gameOver && Input.GetButtonDown("Pause"))
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	/// <summary>
	/// Resumes the game.
	/// </summary>
	private void Resume()
	{
		Cursor.lockState = CursorLockMode.Locked;
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	/// <summary>
	/// Pausess the game.
	/// </summary>
	private void Pause()
	{
		Cursor.lockState = CursorLockMode.None;
		pauseMenu.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}
}
