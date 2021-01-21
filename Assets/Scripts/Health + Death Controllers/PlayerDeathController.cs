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

    //Triggered Methods------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Handles the death of the player.
    /// </summary>
    protected override void Die()
    {
        //Time.timeScale = 0;
        Debug.Log($"PlayerDeathController.Die() is not implemented yet, but the player should be dead now.");
    }
}
