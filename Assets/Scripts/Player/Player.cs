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
    private PlayerHandController grenadeController;
    private PlayerGrenadeThrowController grenadeThrowController;
    private PlayerWeaponController weaponController;
    private PlayerShootingController shootingController;

    //Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------

    /// <summary>
    /// The controller class for the player's movement.
    /// </summary>
    public PlayerMovementController MovementController { get => movementController; }

    /// <summary>
    /// The controller class for the player's hand grenades.
    /// </summary>
    public PlayerHandController GrenadeController { get => grenadeController; }

    /// <summary>
    /// The controller class for the player throwing their hand grenades.
    /// </summary>
    public PlayerGrenadeThrowController GrenadeThrowController { get => grenadeThrowController; }

    /// <summary>
    /// The controller class for the player's weapons.
    /// </summary>
    public PlayerWeaponController WeaponController { get => weaponController; }

    /// <summary>
    /// The controller class for the player shooting with their current weapon.
    /// </summary>
    public PlayerShootingController ShootingController { get => shootingController; }

    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
    /// Awake() runs before Start().
    /// </summary>
    protected override void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
        grenadeController = GetComponent<PlayerHandController>();
        grenadeThrowController = GetComponent<PlayerGrenadeThrowController>();
        weaponController = GetComponent<PlayerWeaponController>();
        shootingController = GetComponent<PlayerShootingController>();
    }
}
