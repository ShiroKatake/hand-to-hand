using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovementController : EnemyAimController
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------  

    //Serialized Fields----------------------------------------------------------------------------                                                    

    [Header("Movement Stats")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float minPlayerDistance;

    //Non-Serialized Fields------------------------------------------------------------------------

    //Components
    private CharacterController characterController;

    //Movement Variables
    private float minPlayerDistSquared;

    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
    /// Awake() runs before Start().
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        characterController = GetComponent<CharacterController>();
        minPlayerDistSquared = minPlayerDistance * minPlayerDistance;
    }

    //Core Recurring Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// FixedUpdate() is run at a fixed interval independant of framerate.
    /// </summary>
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (playerInRange) Move();
    }

    //Recurring Methods (FixedUpdate())--------------------------------------------------------------------------------------------------------------

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
}
