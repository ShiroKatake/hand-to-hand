using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A base class for fake hands as landmines. Handmines, if you will.
/// </summary>
public class Landmine : MonoBehaviour
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------

    //Non-Serialized Fields------------------------------------------------------------------------

    private Grenade grenade;

    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
    /// Awake() runs before Start().
    /// </summary>
    private void Awake()
    {
        grenade = GetComponent<Grenade>();
    }

    //Triggered Methods (OnCollision)----------------------------------------------------------------------------------------------------------------

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") grenade.PullPin();
    }
}
