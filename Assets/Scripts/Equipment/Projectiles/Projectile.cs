using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Projectiles to be shot at enemies.
/// </summary>
public class Projectile : MonoBehaviour
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------

    //Serialized Fields----------------------------------------------------------------------------

    [Header("Projectile Stats")]
    [SerializeField] private EProjectileType type;
    [SerializeField] private float damage;
    [SerializeField] private float range;

    //Non-Serialized Fields------------------------------------------------------------------------

    //Components
    private Collider collider;
    private Light light;
    private Rigidbody rigidbody;
    private MeshRenderer renderer;

    //Other
    private bool active = false;
    private Transform owner;
    private List<Collider> ownerColliders;
    private float timeOfLastShot;
    private Vector3 shotFrom;
    private float rangeSquared;

    //Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------

    /// <summary>
    /// Whether or not the projectile is active (i.e. has it been fired and is it currently moving).
    /// </summary>
    public bool Active { get => active; set => active = value; }

    /// <summary>
    /// The projectile's collider component.
    /// </summary>
    public Collider Collider { get => collider; }

    /// <summary>
    /// The projectile's light component.
    /// </summary>
    public Light Light { get => light; }

    /// <summary>
    /// The entity that fired the projectile. Should only be set by ProjectileFactory.
    /// </summary>
    public Transform Owner { get => owner; set => owner = value; }

    /// <summary>
    /// The colliders of the entity that fired the projectile. Should only be set by ProjectileFactory.
    /// </summary>
    public List<Collider> OwnerColliders { get => ownerColliders; set => ownerColliders = value; }

    /// <summary>
    /// The projectile's mesh renderer component.
    /// </summary>
    public MeshRenderer Renderer { get => renderer; }

    /// <summary>
    /// The projectile's rigidbody component.
    /// </summary>
    public Rigidbody Rigidbody { get => rigidbody; }

    /// <summary>
    /// The type of projectile a projectile is.
    /// </summary>
    public EProjectileType Type { get => type; }

    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
    /// Awake() runs before Start().
    /// </summary>
    private void Awake()
    {
        collider = GetComponent<Collider>();
        light = GetComponentInChildren<Light>();
        renderer = GetComponentInChildren<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();
        rangeSquared = range * range;
    }

    //Core Recurring Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// FixedUpdate() is run at a fixed interval independant of framerate.
    /// </summary>
    private void FixedUpdate()
    {
        if (active && MathUtility.DistanceSquaredXZ(transform.position, shotFrom) >= rangeSquared) ProjectileFactory.Instance.Destroy(this);
    }

    //Triggered Methods------------------------------------------------------------------------------------------------------------------------------

    //Shooting-------------------------------------------------------------------------------------
    
    /// <summary>
    /// Starts the coroutine that activates the projectile in the next frame.
    /// </summary>
    /// <param name="vector">The normalised direction of the projectile's velocity.</param>
    /// <param name="movementSpeed">The speed of the shooter in the direction the projectile will travel.</param>
    public void Shoot(/*(Vector3 vector, float movementSpeed*/ float force)
    {
        //Debug.Log($"{this}.Shoot()");
        StartCoroutine(Shooting(/*vector, movementSpeed*/force));
    }

    /// <summary>
    /// Activates a projectile, applying a velocity to it.
    /// </summary>
    /// <param name="vector">The normalised direction of the projectile's velocity.</param>
    IEnumerator Shooting(/*Vector3 vector, float movementSpeed*/float force)
    {
        yield return null;

        shotFrom = transform.position;
        active = true;
        rigidbody.isKinematic = false;
        collider.enabled = true;
        rigidbody.AddForce(transform.forward * force);
        ProjectileManager.Instance.RegisterProjectile(this);
    }

    //Collisions-----------------------------------------------------------------------------------

    /// <summary>
    /// When a GameObject collides with another GameObject, Unity calls OnTriggerEnter.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    public void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && !ownerColliders.Contains(other))
        {
            if (other.tag != owner.tag)
            {
                Health damageable = other.GetComponent<Health>();
                if (damageable != null) damageable.TakeDamage(damage);
            }
            
            ProjectileFactory.Instance.Destroy(this);
        }
    }

    ///// <summary>
    ///// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    ///// </summary>
    ///// <param name="other">The other Collider involved in this collision.</param>
    //public void OnTriggerExit(Collider other)
    //{
    //    //Debug.Log("ProjectileCollision OnTriggerExit");

    //    if (!leftOwnerCollider && other.CompareTag(owner.tag))
    //    {
    //        //Debug.Log("Left owner collider");
    //        leftOwnerCollider = true;
    //    }
    //}
}
