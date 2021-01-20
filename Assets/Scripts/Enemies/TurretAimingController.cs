using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAimingController : EnemyAimController
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------  

    //Serialized Fields----------------------------------------------------------------------------                                                    

    [Header("Elevation Game Objects")]
    [SerializeField] private Transform head;

    [Header("Elevation Stats")]
    [SerializeField] private float elevationSpeed;
    [SerializeField] private float minElevation;
    [SerializeField] private float maxElevation;
    //[SerializeField] private Vector3 elevationOffset;

    //Non-Serialized Fields------------------------------------------------------------------------                                                    

    //Elevation Variables
    private float currentElevation;
    private float targetElevation;
    private float deltaElevation;

    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
    /// Awake() runs before Start().
    /// </summary>
    protected override void Awake()
    {
        
    }

    /// <summary>
    /// Setup / reset aiming variables for turrets.
    /// </summary>
    protected override void Reset()
    {
        base.Reset();
        currentElevation = 0;
        targetElevation = 0;
        head.localRotation = Quaternion.identity;
    }

    //Core Recurring Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// FixedUpdate() is run at a fixed interval independant of framerate.
    /// </summary>
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (playerInRange) Elevate();
    }

    //Recurring Methods (FixedUpdate())--------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Elevate the barrel to aim at the player.
    /// </summary>
    private void Elevate()
    {
        //Calculate Target Elevation
        float rawElevation = targeter.rotation.eulerAngles.x;// + elevationOffset.y;
        targetElevation = (rawElevation > 90 ? rawElevation - 360 : rawElevation);
        ClampElevation();

        //Calculate Change In Elevation
        deltaElevation = Mathf.DeltaAngle(currentElevation, targetElevation);
        float elevationDirection = MathUtility.Instance.Sign(deltaElevation);
        deltaElevation = MathUtility.Instance.FloatMagnitude(deltaElevation);
        float fixedUpdateElevation = elevationSpeed * Time.fixedDeltaTime;

        //Calculate New Elevation
        currentElevation += elevationDirection * Mathf.Min(deltaElevation, fixedUpdateElevation);

        //Apply New Elevation
        head.localRotation = Quaternion.Euler(currentElevation, 0, 0);
    }

    /// <summary>
    /// Restricts the elevation of the turret's barrel to within predefined limits.
    /// </summary>
    private void ClampElevation()
    {
        targetElevation = (targetElevation > maxElevation ? maxElevation : targetElevation < minElevation ? minElevation : targetElevation);
    }

    //Triggered Methods------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Is this enemy's current rotation within maxAimAngle of the target rotation?
    /// </summary>
    /// <returns>Is this enemy's current rotation within maxAimAngle of the target rotation?</returns>
    public override bool WithinMaxAimAngle()
    {
        return base.WithinMaxAimAngle() && deltaElevation <= maxAimAngle;
    }
}
