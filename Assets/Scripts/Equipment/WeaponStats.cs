using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An enum denoting different firing patterns for weapons. Multiple different weapons can fall within the same class of firing pattern.
/// </summary>
public enum EWeaponClass
{
    Manual,
    BurstManual,
    BurstAutomatic,
    FullyAutomatic
}

/// <summary>
/// An enum denoting the differen types of weapons.
/// </summary>
public enum EWeaponType
{
    Pistol,
    AutoRifle,
    BurstRifle,
    Shotgun
}

/// <summary>
/// A base class for all of the stats of a weapon.
/// </summary>
public class WeaponStats : MonoBehaviour
{
    //Serialized Fields----------------------------------------------------------------------------

    [Header("Appearance")]
    [SerializeField] private Material material;

    [Header("Shooting Stats")]
    [SerializeField] private EWeaponClass weaponClass;
    [SerializeField] private EWeaponType weaponType;
    [SerializeField] private EProjectileType projectileType;
    [SerializeField] private float maxAmmo;
    [SerializeField] private float pelletsPerShot;
    [SerializeField] private float spreadAngle;
    [SerializeField] private float shotForce;
    [SerializeField] private float shotCooldown;

    [Header("Overheating Stats")]
    [SerializeField] private float heatPerShot;
    [SerializeField] private float coolingPerSecond;
    [SerializeField] private float overheatingThreshold;
    [SerializeField] private float overheatingCooldown;

    //Non-Serialized Fields------------------------------------------------------------------------

    private float currentAmmo;
    private float timeOfLastShot = -1;
    private float timeOfLastTriggerRelease = -1;
    private float timeOfLastOverheat = -1;
    private float barrelHeat = 0;
    private bool overheated = false;
    private bool triggerDown = false;

    //Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------

    /// <summary>
    /// How hot is this weapon at the moment?
    /// </summary>
    public float BarrelHeat { get => barrelHeat; set => barrelHeat = value; }

    /// <summary>
    /// How much does this weapon cool off per second?
    /// </summary>
    public float CoolingPerSecond { get => coolingPerSecond; }

    /// <summary>
    /// How much ammunition does this weapon currently have left?
    /// </summary>
    public float CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }

    /// <summary>
    /// How much heat does each shot contribute to overheating?
    /// </summary>
    public float HeatPerShot { get => heatPerShot; }

    /// <summary>
    /// What material should be applied to this weapon?
    /// </summary>
    public Material Material { get => material; }

    /// <summary>
    /// What is the maximum ammunition of this weapon?
    /// </summary>
    public float MaxAmmo { get => maxAmmo; }

    /// <summary>
    /// Is this weapon currently overheated?
    /// </summary>
    public bool Overheated { get => overheated; set => overheated = value; }

    /// <summary>
    /// When this weapon has overheated, how long does it take for it to cool back down?
    /// </summary>
    public float OverheatingCooldown { get => overheatingCooldown; }

    /// <summary>
    /// What amount of heat means that this weapon has overheated?
    /// </summary>
    public float OverheatingThreshold { get => overheatingThreshold; }

    /// <summary>
    /// How many pellets get shot at once by this weapon?
    /// </summary>
    public float PelletsPerShot { get => pelletsPerShot; }

    /// <summary>
    /// What type of projectile does this weapon shoot?
    /// </summary>
    public EProjectileType ProjectileType { get => projectileType; }

    /// <summary>
    /// How long should this weapon wait between shots?
    /// </summary>
    public float ShotCooldown { get => shotCooldown; }

    /// <summary>
    /// How much force should be applied to each projectile this weapon shoots?
    /// </summary>
    public float ShotForce { get => shotForce; }

    /// <summary>
    /// What angle within which do all of this weapons pellets get dispersed?
    /// </summary>
    public float SpreadAngle { get => spreadAngle; }

    /// <summary>
    /// When did this weapon last overheat?
    /// </summary>
    public float TimeOfLastOverheat { get => timeOfLastOverheat; set => timeOfLastOverheat = value; }

    /// <summary>
    /// When did this weapon last shoot a projectile?
    /// </summary>
    public float TimeOfLastShot { get => timeOfLastShot; set => timeOfLastShot = value; }

    /// <summary>
    /// When did this weapon's user last release this weapon's trigger?
    /// </summary>
    public float TimeOfLastTriggerRelease { get => timeOfLastTriggerRelease; set => timeOfLastTriggerRelease = value; }

    /// <summary>
    /// Is this weapon's trigger currently being held down?
    /// </summary>
    public bool TriggerDown { get => triggerDown; set => triggerDown = value; }

    /// <summary>
    /// What general class of weapon is this weapon?
    /// </summary>
    public EWeaponClass WeaponClass { get => weaponClass; }

    /// <summary>
    /// What specific type of weapon is this weapon?
    /// </summary>
    public EWeaponType WeaponType { get => weaponType; }

    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
    /// Awake() runs before Start().
    /// </summary>
    private void Awake()
    {
        currentAmmo = maxAmmo;
    }
}
