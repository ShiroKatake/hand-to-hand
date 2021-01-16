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

    [Header("Looking")]
    [SerializeField] private float minYAxis;
    [SerializeField] private float maxYAxis;
    [Tooltip("If the range should be min to 0 and 360 to max rather than min to max, when the range for all angles is [0, 360] not [-180, 180].")]
    [SerializeField] private bool excludeRange;
    [SerializeField] private float lookSpeed;
    [SerializeField] private bool invertYAxis;

    //Non-Serialized Fields------------------------------------------------------------------------                                                    

    //Components

    private CharacterController characterController;
    private Camera camera;

    //Movement Variables

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
        characterController = GetComponent<CharacterController>();
        camera = GetComponentInChildren<Camera>();
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
        Vector3 cameraRotation = camera.transform.localRotation.eulerAngles;
        float newRotation = cameraRotation.x + lookUD * lookSpeed * Time.deltaTime * (invertYAxis ? 1 : -1);
        Debug.Log($"Raw new rotation: {newRotation}");
        cameraRotation.x = ClampAngle(cameraRotation.x, newRotation, minYAxis, maxYAxis);
        camera.transform.localRotation = Quaternion.Euler(cameraRotation);
    }

    private float ClampAngle(float oldValue, float newValue, float min, float max)
    {
        if (min == max) return min;

        while (newValue > 180) newValue -= 360;
        while (newValue < -180) newValue += 360;

        //Debug.Log($"min: {min}, newValue: {newValue}, newValue < min: {newValue < min}");
        //Debug.Log($"max: {max}, newValue: {newValue}, newValue > max: {newValue > max}");

        if (newValue < min) return min;
        if (newValue > max) return max;

        //if (newValue < min ) return min;
        //{
        //Debug.Log($"newValue {newValue} < min {min}, returning min value");
        //return min;
        //}

        //if (newValue > max) return max;
        //{
            //Debug.Log($"newValue {newValue} > max {max}, returning max value");
            //return max;
        //}

        return newValue;        
    }

    /// <summary>
    /// The player moves around their environment.
    /// </summary>
    private void Move()
    {
        Vector3 movement = transform.TransformDirection(Vector3.forward) * moveFB + transform.TransformDirection(Vector3.right) * moveLR;
        characterController.SimpleMove(movement * moveSpeed * Time.deltaTime);
    }
}
