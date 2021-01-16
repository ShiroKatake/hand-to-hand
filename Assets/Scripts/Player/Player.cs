using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class collecting all of the components of the player.
/// </summary>
public class Player : PublicInstanceSerializableSingleton<Player>
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------

    //Non-Serialized Fields------------------------------------------------------------------------

    private PlayerMovementController movementController;

    //Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------

    /// <summary>
    /// The controller class for the player's movement.
    /// </summary>
    public PlayerMovementController MovementController { get => movementController; }

    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
    /// Awake() runs before Start().
    /// </summary>
    protected override void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
    }
}
