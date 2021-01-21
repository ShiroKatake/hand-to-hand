using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A controller class for managing the player's health and death.
/// </summary>
public class PlayerDeathController : Health
{
	//Private Fields---------------------------------------------------------------------------------------------------------------------------------

	//Serialized Fields----------------------------------------------------------------------------

	//Non-Serialized Fields------------------------------------------------------------------------

	private HealthUI healthUI;

	protected override void Awake()
	{
		base.Awake();
		healthUI = FindObjectOfType<HealthUI>();
	}

	//Triggered Methods------------------------------------------------------------------------------------------------------------------------------

	public override void TakeDamage(float amount)
	{
		base.TakeDamage(amount);
		healthUI.SetHeath(CurrentHealth / MaxHealth);
	}

	/// <summary>
	/// Handles the death of the player.
	/// </summary>
	protected override void Die()
    {
		FindObjectOfType<GameOverManager>().SetGameOver(false);
        Debug.Log($"PlayerDeathController.Die() is not implemented yet, but the player should be dead now.");
    }
}
