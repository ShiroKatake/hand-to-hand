using UnityEngine;

/// <summary>
/// A base class for hands as weapons.
/// </summary>
public class Weapon : MonoBehaviour
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------

    //Serialized Fields----------------------------------------------------------------------------

    [Header("Hand")]
    [SerializeField] private Transform fingertip;

    [Header("Shooting Stats")]
    [SerializeField] private EProjectileType projectileType;
    [SerializeField] private float spreadAngle;
    [SerializeField] private float maxElevation;
    [SerializeField] private float shotForce;
    [SerializeField] private float chargePerShot;
    [SerializeField] private float shotCooldown;

    [Header("Overheating Stats")]
    [SerializeField] private float heatPerShot;
    [SerializeField] private float coolingPerSecond;
    [SerializeField] private float overheatingThreshold;
    [SerializeField] private float overheatingCooldown;

    //Non-Serialized Fields------------------------------------------------------------------------

    private float timeOfLastShot;
    private float timeOfLastOverheat;
    private bool wantToShoot;
    private float barrelHeat;
    private bool overheated;

    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
    /// Awake() runs before Start().
    /// </summary>
    private void Awake()
    {
        timeOfLastShot = -1;
        timeOfLastOverheat = -1;
        barrelHeat = 0;
        overheated = false;
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
            float timeSinceOverheat = Time.time - timeOfLastOverheat;

            if (timeSinceOverheat > overheatingCooldown)
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
    public bool ReadyToShoot()
    {
        return !overheated && Time.time - timeOfLastShot > shotCooldown;
    }

    /// <summary>
    /// Fires a projectile from this weapon's fingertips.
    /// </summary>
    public void Shoot()
    {
        //Quaternion randomRotation = Random.rotation;
        //Quaternion projectileRotation = Quaternion.RotateTowards(barrelTip.transform.rotation, randomRotation, spreadAngle);
        Quaternion projectileRotation = Quaternion.RotateTowards(fingertip.transform.rotation, Random.rotation, spreadAngle);
        //Debug.Log($"randomRotation is {randomRotation} (Quaternion) / {randomRotation.eulerAngles} (EulerAngles)");
        //Debug.Log($"projectileRotation is {projectileRotation} (Quaternion) / {projectileRotation.eulerAngles} (EulerAngles)");
        Projectile projectile = ProjectileFactory.Instance.Get(transform, fingertip.position, projectileRotation, projectileType);
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
