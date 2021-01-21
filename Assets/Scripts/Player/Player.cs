using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class collecting all of the components of the player.
/// </summary>
public class Player : PublicInstanceSerializableSingleton<Player>
{
	//Private Fields---------------------------------------------------------------------------------------------------------------------------------

	//Serialized Fields------------------------------------------------------------------------

	[SerializeField] private Animator leftHandAnimator;
	[SerializeField] private Animator rightHandAnimator;

	//Non-Serialized Fields------------------------------------------------------------------------

	private PlayerMovementController movementController;
    private PlayerHandController handController;
    private PlayerGrenadeThrowController grenadeThrowController;
    private PlayerShootingController shootingController;
    private Camera povCamera;

    //Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------

    /// <summary>
    /// The player's POV camera.
    /// </summary>
    public Camera Camera { get => povCamera; }

    /// <summary>
    /// The controller class for the player throwing their hand grenades.
    /// </summary>
    public PlayerGrenadeThrowController GrenadeThrowController { get => grenadeThrowController; }

    /// <summary>
    /// The controller class for the player's hand grenades.
    /// </summary>
    public PlayerHandController HandController { get => handController; }

    /// <summary>
    /// The animator controlling the player's left hand.
    /// </summary>
    public Animator LeftHandAnimator { get => leftHandAnimator; }

    /// <summary>
    /// The controller class for the player's movement.
    /// </summary>
    public PlayerMovementController MovementController { get => movementController; }

    /// <summary>
	/// The animator controlling the player's right hand.
	/// </summary>
	public Animator RightHandAnimator { get => rightHandAnimator; }

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
        base.Awake();
        movementController = GetComponent<PlayerMovementController>();
        handController = GetComponent<PlayerHandController>();
        grenadeThrowController = GetComponent<PlayerGrenadeThrowController>();
        shootingController = GetComponent<PlayerShootingController>();
        povCamera = GetComponentInChildren<Camera>();
    }
}
