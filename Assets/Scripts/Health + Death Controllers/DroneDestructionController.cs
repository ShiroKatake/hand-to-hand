using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the health and destruction of drones.
/// </summary>
public class DroneDestructionController : Health
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------

    //Serialized Fields----------------------------------------------------------------------------

    [Header("Explosion")]
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private AudioClip explosionSFX;

    //Non-Serialized Fields------------------------------------------------------------------------                                                    

    //Components
    private AudioSource audioSource;
    private CharacterController characterController;
    private DroneMovementController movementController;
    private EnemyShootingController shootingController;
    private Weapon weapon;
    private WeaponStats weaponStats;

    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
    /// Awake() runs before Start().
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
        movementController = GetComponent<DroneMovementController>();
        shootingController = GetComponent<EnemyShootingController>();
        weapon = GetComponent<Weapon>();
        weaponStats = GetComponent<WeaponStats>();
    }

    //Triggered Methods------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Handles the destruction of drones.
    /// </summary>
    protected override void Die()
    {
        StartCoroutine(Explode());
    }

    /// <summary>
	/// Drones exploding upon their destruction.
	/// </summary>
	private IEnumerator Explode()
    {
        characterController.enabled = false;
        movementController.enabled = false;
        shootingController.enabled = false;
        weapon.enabled = false;
        weaponStats.enabled = false;
        body.SetActive(false);

        explosionFX.SetActive(true);
        audioSource.clip = explosionSFX;
        audioSource.loop = false;
        audioSource.Play();

        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }
}
