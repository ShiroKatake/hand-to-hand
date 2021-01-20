using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A base interface for enemies aiming at the player.
/// </summary>
public abstract class EnemyAimer : MonoBehaviour
{
    //Private Fields

    //Serialized Fields

    [Tooltip("What is the maximum acceptable difference between this enemy's current rotation and target rotation before it may shoot at its target?")]
    [SerializeField] protected float maxAimAngle;

    //Non-Serialized Fields--------------------------------------------------------------------------------------------------------------------------

    protected bool playerInRange = false;

    //Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------

    /// <summary>
    /// Is the player within the drone's detection range?
    /// </summary>
    public bool PlayerInRange { get => playerInRange; }

    //Triggered Methods------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Is this enemy's current rotation within maxAimAngle of the target rotation?
    /// </summary>
    /// <returns>Is this enemy's current rotation within maxAimAngle of the target rotation?</returns>
    public abstract bool WithinMaxAimAngle();
}
