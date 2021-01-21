using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDestructionController : Health
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------

    //Serialized Fields----------------------------------------------------------------------------

    [Header("Turret")]
    [SerializeField] private GameObject head;
    [SerializeField] private List<Collider> headColliders;

    [Header("Explosion")]
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private AudioClip explosionSFX;

    //Non-Serialized Fields------------------------------------------------------------------------                                                    

    //Components
    private AudioSource audioSource;
    private TurretAimingController aimingController;
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
        aimingController = GetComponent<TurretAimingController>();
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
        head.SetActive(false);

        foreach (Collider c in headColliders)
        {
            Destroy(c);
        }

        aimingController.enabled = false;
        shootingController.enabled = false;
        weapon.enabled = false;
        weaponStats.enabled = false;

        explosionFX.SetActive(true);
        audioSource.clip = explosionSFX;
        audioSource.loop = false;
        audioSource.Play();

        yield return new WaitForSeconds(3);

        explosionFX.SetActive(false);
        Destroy(this);
    }
}
