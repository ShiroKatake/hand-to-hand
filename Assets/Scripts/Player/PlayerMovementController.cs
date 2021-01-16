using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : PrivateInstanceSerializableSingleton<PlayerMovementController>
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------  

    //Serialized Fields----------------------------------------------------------------------------                                                    

    [SerializeField] private float moveSpeed;
    [SerializeField] private float lookSpeed;
    [SerializeField] private float jumpForce;

    //Non-Serialized Fields------------------------------------------------------------------------                                                    



    //Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------                                                                                                                          



    //Complex Public Properties--------------------------------------------------------------------                                                    



    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
    /// Awake() runs before Start().
    /// </summary>
    protected override void Awake()
    {
        
    }

    /// <summary>
    /// Start() is run on the frame when a script is enabled just before any of the Update methods are called for the first time. 
    /// Start() runs after Awake().
    /// </summary>
    private void Start()
    {

    }

    //Core Recurring Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Update() is run every frame.
    /// </summary>
    private void Update()
    {
        Input();
        Look();
        Move();
    }

    /// <summary>
    /// FixedUpdate() is run at a fixed interval independant of framerate.
    /// </summary>
    private void FixedUpdate()
    {

    }

    //Recurring Methods (Update())-------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Gets the player's movement input.
    /// </summary>
    private void Input()
    {

    }

    /// <summary>
    /// The player looks around at their environment.
    /// </summary>
    private void Look()
    {

    }

    /// <summary>
    /// The player moves around their environment.
    /// </summary>
    private void Move()
    {

    }
}
