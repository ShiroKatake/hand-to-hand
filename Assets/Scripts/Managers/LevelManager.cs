using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ELevels
{
	MainMenu,
	Gameplay
}

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance { get; set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void LoadScene(ELevels level)
	{
		SceneManager.LoadScene((int)level);
	}
}
