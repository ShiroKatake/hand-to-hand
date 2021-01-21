using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : PrivateInstanceSerializableSingleton<PlayerMovementController>
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------  

    //Serialized Fields----------------------------------------------------------------------------                                                    
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private AnimationCurve jumpFallOff;

    [Header("Looking")]
    [SerializeField] private float lookSpeed;
    [SerializeField] private float minYAxis;
    [SerializeField] private float maxYAxis;
    [SerializeField] private bool invertYAxis;

    //Non-Serialized Fields------------------------------------------------------------------------                                                    

    //Components
    private CharacterController characterController;
    private Camera playerCamera;

    //Movement Variables
    private Vector3 movement;
    private float moveLR;
    private float moveFB;
    private bool jump;
    private bool jumping;

    //Looking Variables
    private float lookLR;
    private float lookUD;

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
        base.Awake();
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
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
        GetInput();
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
    private void GetInput()
    {
        moveLR = Input.GetAxis("Move LR");
        moveFB = Input.GetAxis("Move FB");
        jump = Input.GetButtonDown("Jump");
        lookLR = Input.GetAxis("Look LR");
        lookUD = Input.GetAxis("Look UD");
    }

    /// <summary>
    /// The player looks around at their environment.
    /// </summary>
    private void Look()
    {
        //Left and right
        transform.Rotate(0, lookLR * lookSpeed * Time.deltaTime, 0);

        //Up and down
        Vector3 cameraRotation = playerCamera.transform.localRotation.eulerAngles;
        float newRotation = cameraRotation.x + lookUD * lookSpeed * Time.deltaTime * (invertYAxis ? 1 : -1);
        cameraRotation.x = ClampAngle(newRotation, minYAxis, maxYAxis);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation);
    }

    /// <summary>
    /// Clamps a rotation to an acceptable range between -180 degrees and 180 degrees.
    /// </summary>
    /// <param name="value">The value to change the rotation to, if it is not out of bounds.</param>
    /// <param name="min">The minimum acceptable value.</param>
    /// <param name="max">The maximum acceptable value.</param>
    /// <returns></returns>
    private float ClampAngle(float value, float min, float max)
    {
        if (min == max) return min;

        while (value > 180) value -= 360;
        while (value < -180) value += 360;

        //Debug.Log($"min: {min}, newValue: {newValue}, newValue < min: {newValue < min}");
        //Debug.Log($"max: {max}, newValue: {newValue}, newValue > max: {newValue > max}");

        if (value < min) return min;
        if (value > max) return max;

        return value;        
    }

    /// <summary>
    /// The player runs around their environment.
    /// </summary>
    private void Move()
    {
        if (!jumping) movement = transform.TransformDirection(Vector3.forward) * moveFB + transform.TransformDirection(Vector3.right) * moveLR;
        characterController.SimpleMove(movement * moveSpeed * Time.deltaTime);
        if (jump && !jumping) StartCoroutine(Jump());
    }

    /// <summary>
    /// The player jumps around their environment.
    /// </summary>
    private IEnumerator Jump()
    {
        jumping = true;
        float timeInAir = 0f;
        bool collisionAbove;

        do
        {
            float remainingForce = jumpForce * jumpFallOff.Evaluate(timeInAir);
            characterController.Move(Vector3.up * remainingForce * Time.deltaTime);
            timeInAir += Time.deltaTime;
            collisionAbove = characterController.collisionFlags == CollisionFlags.Above;
            //Debug.Log($"Jumping up, characterController.isGrounded: {characterController.isGrounded}, collisionAbove: {collisionAbove}");
            yield return null;
        }
        while (!characterController.isGrounded && !collisionAbove);

        while (!characterController.isGrounded) yield return null;

        jumping = false;
    }
}
