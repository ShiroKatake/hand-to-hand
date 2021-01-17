using UnityEngine;

/// <summary>
/// Which handside the object is.
/// </summary>
public enum HandSide
{
    None,
	Left,
	Right
}

/// <summary>
/// A base class for hands.
/// </summary>
public class Hand : MonoBehaviour
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------

    //Serialized Fields----------------------------------------------------------------------------

    [SerializeField] private HandSide handSide;
    
    //Non-Serialized Fields------------------------------------------------------------------------

	private Rigidbody rb;
    private Grenade grenade;
    private Weapon weapon;

	//Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------

    /// <summary>
    /// This hand's grenade component.
    /// </summary>
    public Grenade Grenade { get => grenade; }

    /// <summary>
    /// Which side is this hand on?
    /// </summary>
	public HandSide HandSide { get => handSide; }

    /// <summary>
    /// This hand's rigidbody component
    /// </summary>
	public Rigidbody Rigidbody { get => rb; }

    /// <summary>
    /// This hand's weapon component.
    /// </summary>
    public Weapon Weapon { get => weapon; }

    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
    /// Awake() runs before Start().
    /// </summary>
    private void Awake()
	{
		rb = GetComponent<Rigidbody>();
        grenade = GetComponent<Grenade>();
        weapon = GetComponent<Weapon>();
	}
}
