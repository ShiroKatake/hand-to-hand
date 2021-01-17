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
/// 
/// </summary>
public class Hand : MonoBehaviour
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------

    //Serialized Fields----------------------------------------------------------------------------

    [SerializeField] private HandSide handSide;
    
    //Non-Serialized Fields------------------------------------------------------------------------

	private Rigidbody rb;
	private Collider handCollider;

	//Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------

    /// <summary>
    /// Which side is this hand on?
    /// </summary>
	public HandSide HandSide { get => handSide; }

    /// <summary>
    /// This hand's rigidbody component
    /// </summary>
	public Rigidbody Rigidbody { get => rb; }
	public Collider Collider { get => handCollider; }

	//Initialization Methods-------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
	/// Awake() runs before Start().
	/// </summary>
	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		handCollider = GetComponent<Collider>();
	}

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
