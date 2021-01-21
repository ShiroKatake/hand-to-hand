using UnityEngine;

/// <summary>
/// A controller class for the enemies shooting with their weapons.
/// </summary>
public class EnemyShootingController : MonoBehaviour
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------

    [SerializeField] private float shootRange;

    //Non-Serialized Fields------------------------------------------------------------------------

    //Components
    private EnemyAimController aimer;
    private Weapon weapon;

    //Shooting Variables
    private bool canShoot = true;
    private float shootRangeSquared;

    //Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------

    /// <summary>
    /// Is the enemy allowed to shoot?
    /// </summary>
    public bool CanShoot { get => canShoot; set => canShoot = value; }

    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
    /// Awake() runs before Start().
    /// </summary>
    private void Awake()
    {
        weapon = GetComponent<Weapon>();
        aimer = GetComponent<EnemyAimController>();
        shootRangeSquared = shootRange * shootRange;
    }

    /// <summary>
    /// Start() is run on the frame when a script is enabled just before any of the Update methods are called for the first time. 
    /// Start() runs after Awake().
    /// </summary>
    private void Start()
    {
        weapon.CurrentStats = GetComponent<WeaponStats>();
    }

    //Core Recurring Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Update() is run every frame.
    /// </summary>
    private void Update()
    {
        if (ReadyToShoot()) weapon.Shoot();
    }

    //Recurring Methods (Update())-------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Checks if the enemy is able to shoot and their weapon can shoot.
    /// </summary>
    /// <returns>Is the player able to shoot and can their weapon shoot right now?</returns>
    private bool ReadyToShoot()
    {
        return canShoot 
            && aimer.PlayerInRange 
            && aimer.WithinMaxAimAngle()
            && weapon.ReadyToShoot(true)
            && MathUtility.DistanceSquaredXZ(Player.Instance.transform.position, transform.position) <= shootRangeSquared;
    }
}