using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A health classa and destruction controller for hands.
/// </summary>
public class HandDestructionController : Health
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------

    //Non-Serialized Fields------------------------------------------------------------------------

    private Grenade grenade;

    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        grenade = GetComponent<Grenade>();
    }

    //Triggered Methods------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Handles the destruction of hands.
    /// </summary>
    protected override void Die()
    {
        grenade.PullPin();
        Destroy(this);
    }
}
