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
	private Collider handCollider;
    private MeshRenderer meshRenderer;

    //Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------

    /// <summary>
    /// The hand's collider component.
    /// </summary>
    public Collider Collider { get => handCollider; }

    /// <summary>
    /// This hand's grenade component.
    /// </summary>
    public Grenade Grenade { get => grenade; }

    /// <summary>
    /// Which side is this hand on?
    /// </summary>
	public HandSide HandSide { get => handSide; }

    /// <summary>
    /// The hand's mesh renderer component.
    /// </summary>
    public MeshRenderer MeshRenderer { get => meshRenderer; }

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
		handCollider = GetComponent<Collider>();
		meshRenderer = GetComponentInChildren<MeshRenderer>();
	}

    //Triggered Methods------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Launch the grenade hand.
	/// </summary>
	public void Launch(Vector3 direction, float range)
	{
		handCollider.enabled = true;
		rb.useGravity = true;
		rb.AddForce(direction * range, ForceMode.Impulse);
		transform.parent = null;
	}
}
