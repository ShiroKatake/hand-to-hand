using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A base interface for enemies aiming at the player.
/// </summary>
public abstract class EnemyAimController : MonoBehaviour
{

    //Private Fields---------------------------------------------------------------------------------------------------------------------------------  

    //Serialized Fields----------------------------------------------------------------------------                                                    

    [Header("Game Objects")]
    [SerializeField] protected Transform body;
    [SerializeField] protected Transform targeter;

    [Header("Looking Stats")]
    [Tooltip("What is the maximum acceptable difference between this enemy's current rotation and target rotation before it may shoot at its target?")]
    [SerializeField] protected float maxAimAngle;
    [SerializeField] protected float lookSpeed;
    //[SerializeField] protected Vector3 rotationOffset;

    //Non-Serialized Fields------------------------------------------------------------------------                                                    

    //Looking Variables
    private float currentRotation;
    private float rawRotation;
    private float targetRotation;
    private float deltaRotation = 9999;
    protected bool playerInRange = false;

    //Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------

    /// <summary>
    /// Is the player within the drone's detection range?
    /// </summary>
    public bool PlayerInRange { get => playerInRange; }

    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
    /// Awake() runs before Start().
    /// </summary>
    protected virtual void Awake()
    {
        Reset();
    }

    /// <summary>
    /// Setup / reset aiming variables for enemies.
    /// </summary>
    protected virtual void Reset()
    {
        currentRotation = 0;
        targetRotation = 0;
        body.localRotation = Quaternion.identity; ;
    }

    //Core Recurring Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// FixedUpdate() is run at a fixed interval independant of framerate.
    /// </summary>
    protected virtual void FixedUpdate()
    {
        if (playerInRange) Look();
    }

    //Recurring Methods (FixedUpdate())--------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// The drone rotates on the y-axis to look at the player.
    /// </summary>
    private void Look()
    {
        //Calculate Target Rotation
        targeter.LookAt(Player.Instance.Camera.transform.position);
        rawRotation = targeter.rotation.eulerAngles.y;
        targetRotation = MathUtility.NormaliseAngle(rawRotation);

        //Calculate Change In Rotation
        deltaRotation = Mathf.DeltaAngle(currentRotation, targetRotation);
        float rotationDirection = MathUtility.Sign(deltaRotation);
        deltaRotation = MathUtility.FloatMagnitude(deltaRotation);
        float fixedUpdateRotation = lookSpeed * Time.fixedDeltaTime;

        //Calculate New Rotation
        currentRotation += rotationDirection * Mathf.Min(deltaRotation, fixedUpdateRotation);
        currentRotation = MathUtility.NormaliseAngle(currentRotation);

        //Apply New Rotation
        body.localRotation = Quaternion.Euler(0, currentRotation, 0);
    }

    //Triggered Methods------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Is this enemy's current rotation within maxAimAngle of the target rotation?
    /// </summary>
    /// <returns>Is this enemy's current rotation within maxAimAngle of the target rotation?</returns>
    public virtual bool WithinMaxAimAngle()
    {
        return deltaRotation <= maxAimAngle;
    }

    //Triggered Methods (OnCollision)----------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// When a GameObject collides with another GameObject, Unity calls OnTriggerEnter.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") playerInRange = true;
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") playerInRange = false;
    }
}
