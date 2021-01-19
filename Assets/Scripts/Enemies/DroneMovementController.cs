using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovementController : MonoBehaviour
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------  

    //Serialized Fields----------------------------------------------------------------------------                                                    

    [Header("Game Objects")]
    [SerializeField] private Transform drone;
    [SerializeField] private Transform targeter;

    [Header("Stats")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lookSpeed;
    [SerializeField] private float minPlayerDistance;

    //Non-Serialized Fields------------------------------------------------------------------------                                                    

    //Components
    private CharacterController characterController;

    //Movement Variables
    private bool playerInRange = false;
    private float currentRotation;
    private float rawRotation;
    private float targetRotation;
    private float minPlayerDistSquared;

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
    private void Awake()
    {
        minPlayerDistSquared = minPlayerDistance * minPlayerDistance;
        characterController = GetComponent<CharacterController>();
        Reset();
    }

    /// <summary>
    /// Setup / reset code for shotgun turrets.
    /// </summary>
    private void Reset()
    {
        currentRotation = 0;
        targetRotation = 0;
        drone.localRotation = Quaternion.identity;
    }

    //Core Recurring Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// FixedUpdate() is run at a fixed interval independant of framerate.
    /// </summary>
    private void FixedUpdate()
    {
        if (playerInRange)
        {
            Look();
            Move();
        }
    }

    //Recurring Methods (FixedUpdate())--------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// The drone rotates on the y-axis to look at the player.
    /// </summary>
    private void Look()
    {
        //Calculate Target Rotation
        targeter.LookAt(Player.Instance.transform.position);
        rawRotation = targeter.rotation.eulerAngles.y;
        targetRotation = MathUtility.Instance.NormaliseAngle(rawRotation);

        //Calculate Angle To Rotate By
        float deltaRotation = Mathf.DeltaAngle(currentRotation, targetRotation);
        float rotationDirection = MathUtility.Instance.Sign(deltaRotation);
        deltaRotation = MathUtility.Instance.FloatMagnitude(deltaRotation);
        float fixedUpdateRotation = lookSpeed * Time.fixedDeltaTime;

        //Calculate New Rotation
        currentRotation += rotationDirection * Mathf.Min(deltaRotation, fixedUpdateRotation);
        currentRotation = MathUtility.Instance.NormaliseAngle(currentRotation);

        //Apply New Rotation
        drone.localRotation = Quaternion.Euler(0, currentRotation, 0);
    }

    /// <summary>
    /// The drone moves forwards if it's not within minPlayerDistance of the player.
    /// </summary>
    private void Move()
    {
        float deltaX = Player.Instance.transform.position.x - transform.position.x;
        float deltaZ = Player.Instance.transform.position.z - transform.position.z;
        float distSquared = deltaX * deltaX + deltaZ * deltaZ;

        if (distSquared > minPlayerDistSquared)
        {
            characterController.SimpleMove(transform.TransformDirection(Vector3.forward) * moveSpeed * Time.deltaTime);
        }
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
