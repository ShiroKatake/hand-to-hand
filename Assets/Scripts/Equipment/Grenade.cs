using UnityEngine;

/// <summary>
/// A base class for hands as grenades.
/// </summary>
public class Grenade : MonoBehaviour
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------  

    //Serialized Fields----------------------------------------------------------------------------                                                    



    //Non-Serialized Fields------------------------------------------------------------------------                                                    

    private bool exploding;

    //Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------                                                                                                                          

    /// <summary>
    /// Is the grenade about to explode?
    /// </summary>
    public bool Exploding { get => exploding; set => exploding = value; }

    //Complex Public Properties--------------------------------------------------------------------                                                    



    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
    /// Awake() runs before Start().
    /// </summary>
    private void Awake()
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

    }

    /// <summary>
    /// FixedUpdate() is run at a fixed interval independant of framerate.
    /// </summary>
    private void FixedUpdate()
    {

    }

    //Recurring Methods (Update())------------------------------------------------------------------------------------------------------------------  



    //Recurring Methods (FixedUpdate())--------------------------------------------------------------------------------------------------------------



    //Recurring Methods (Other)----------------------------------------------------------------------------------------------------------------------



    //Triggered Methods------------------------------------------------------------------------------------------------------------------------------

}
