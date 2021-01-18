using UnityEngine;


/// <summary>
/// An enum denoting different firing patterns for weapons.
/// </summary>
public enum EWeaponClass
{
    Manual,
    BurstManual,
    BurstAutomatic,
    FullyAutomatic
}

/// <summary>
/// A base class for weapons.
/// </summary>
public class Weapon : MonoBehaviour
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------

    //Serialized Fields----------------------------------------------------------------------------

    [Header("Weapon")]
    [SerializeField] private Transform barrelTip;

    [Header("Shooting Stats")]
    [SerializeField] private EWeaponClass weaponClass;
    [SerializeField] private EProjectileType projectileType;
    [SerializeField] private float maxAmmo;
    [SerializeField] private float pelletsPerShot;
    [SerializeField] private float spreadAngle;
    [SerializeField] private float maxElevation;
    [SerializeField] private float shotForce;
    [SerializeField] private float shotCooldown;

    [Header("Overheating Stats")]
    [SerializeField] private float heatPerShot;
    [SerializeField] private float coolingPerSecond;
    [SerializeField] private float overheatingThreshold;
    [SerializeField] private float overheatingCooldown;

    //Non-Serialized Fields------------------------------------------------------------------------

    private float currentAmmo;
    private float timeOfLastShot;
    private float timeOfLastTriggerRelease;
    private float timeOfLastOverheat;
    private bool wantToShoot;
    private float barrelHeat;
    private bool overheated;

    //Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------

    /// <summary>
    /// How much ammunition does this weapon have left?
    /// </summary>
    public float CurrentAmmo { get => currentAmmo; }

    /// <summary>
    /// What's the maximum amount of ammunition this weapon can have?
    /// </summary>
    public float MaxAmmo { get => maxAmmo; }

    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
    /// Awake() runs before Start().
    /// </summary>
    private void Awake()
    {
        timeOfLastShot = -1;
        timeOfLastTriggerRelease = -1;
        timeOfLastOverheat = -1;
        barrelHeat = 0;
        overheated = false;
        currentAmmo = maxAmmo;
    }

    //Core Recurring Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Update() is run every frame.
    /// </summary>
    private void Update()
    {
        CheckOverheating();
    }

    //Recurring Methods (Update())-------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Checks if it's overheated and needs to cool down.
    /// </summary>
    private void CheckOverheating()
    {
        if (overheated)
        {
            if (Time.time - timeOfLastOverheat > overheatingCooldown)
            {
                overheated = false;
                barrelHeat = 0;
            }
        }
        else
        {
            barrelHeat -= Mathf.Min(barrelHeat, coolingPerSecond * Time.fixedDeltaTime);
        }
    }

    //Triggered Methods------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Checks if the weapon is ready to shoot.
    /// </summary>
    /// <returns>Whether or not the weapon can shoot.</returns>
    public bool ReadyToShoot(bool triggerDown)
    {
        if (!triggerDown) timeOfLastTriggerRelease = Time.time;
        if (currentAmmo <= 0 || Time.time - timeOfLastShot < shotCooldown) return false;

        switch (weaponClass)
        { 
            case EWeaponClass.Manual:
                return timeOfLastTriggerRelease < Time.time; 
            case EWeaponClass.BurstManual:
                return !overheated && timeOfLastTriggerRelease < Time.time;
            case EWeaponClass.BurstAutomatic:
                return !overheated;
            case EWeaponClass.FullyAutomatic:
                return true;
            default:
                return false;            
        }
    }

    /// <summary>
    /// Fires a projectile from this weapon's fingertips.
    /// </summary>
    public void Shoot()
    {
        for (int i = 0; i < pelletsPerShot; i++)
        {
            //Quaternion randomRotation = Random.rotation;
            //Quaternion projectileRotation = Quaternion.RotateTowards(barrelTip.transform.rotation, randomRotation, spreadAngle);
            Quaternion projectileRotation = Quaternion.RotateTowards(barrelTip.transform.rotation, Random.rotation, spreadAngle);
            //Debug.Log($"randomRotation is {randomRotation} (Quaternion) / {randomRotation.eulerAngles} (EulerAngles)");
            //Debug.Log($"projectileRotation is {projectileRotation} (Quaternion) / {projectileRotation.eulerAngles} (EulerAngles)");
            Projectile projectile = ProjectileFactory.Instance.Get(transform, barrelTip.position, projectileRotation, projectileType);
            projectile.Shoot(shotForce);
            //Debug.Log($"{this}.Weapon.Shoot(), projectile is {projectile}");
            timeOfLastShot = Time.time;
            barrelHeat += heatPerShot;

            if (barrelHeat > overheatingThreshold)
            {
                overheated = true;
                timeOfLastOverheat = Time.time;
            }
        }
    }
}
