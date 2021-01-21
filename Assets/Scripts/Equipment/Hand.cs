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
    [SerializeField] private Collider pickUpCollider;
    [SerializeField] private Collider bodyCollider;
    
    //Non-Serialized Fields------------------------------------------------------------------------

    //Components
	private Rigidbody rb;
    private Grenade grenade;
    private Weapon weapon;
    private SkinnedMeshRenderer meshRenderer;
    private WeaponStats stats;
	private bool canCollect = true;
	private Animator handAnimator;

    //Other
    bool hasGottenComponents;

    //Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------

    /// <summary>
    /// How much charge does the hand's battery have left out of 100?
    /// </summary>
    public float BatteryCharge { get => stats.CurrentAmmo * 100 / stats.MaxAmmo; }

    /// <summary>
    /// The hand's non-trigger collider component.
    /// </summary>
    public Collider BodyCollider { get => bodyCollider; }

    /// <summary>
    /// This hand's grenade component.
    /// </summary>
    public bool CanCollect { get => canCollect; set => canCollect = value; }

    /// <summary>
    /// This hand's grenade component.
    /// </summary>
    public Grenade Grenade { get => grenade; }

    /// <summary>
    /// Which side is this hand on?
    /// </summary>
	public HandSide HandSide { get => handSide; }

    /// <summary>
    /// Has this hand run its awake method and retrieved its other components yet?
    /// </summary>
    public bool HasGottenComponents { get => hasGottenComponents; }

    /// <summary>
    /// The hand's mesh renderer component.
    /// </summary>
    public SkinnedMeshRenderer MeshRenderer { get => meshRenderer; }

    /// <summary>
    /// The hand's trigger collider component.
    /// </summary>
    public Collider PickUpCollider { get => pickUpCollider; }

    /// <summary>
    /// This hand's rigidbody component
    /// </summary>
	public Rigidbody Rigidbody { get => rb; }

    /// <summary>
    /// This hand's weapon stats component.
    /// </summary>
    public WeaponStats Stats { get => stats; }

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
        if (!hasGottenComponents) GetComponents();
	}

	private void Start()
	{
		handAnimator = handSide == HandSide.Left ? Player.Instance.LeftHandAnimator : Player.Instance.RightHandAnimator;
	}

	/// <summary>
	/// Gets this hand's other components.
	/// </summary>
	public void GetComponents()
    {
        hasGottenComponents = true;
        rb = GetComponent<Rigidbody>();
        grenade = GetComponent<Grenade>();
        weapon = GetComponent<Weapon>();
        stats = GetComponent<WeaponStats>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    //Triggered Methods------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Launch the grenade hand.
	/// </summary>
    /// <param name="direction">What direction should the hand be launched in?</param>
    /// <param name="range">How far should the hand be launched?</param>
	public void Launch(Vector3 direction, float range)
	{
        grenade.PullPin();
		rb.AddForce(direction * range, ForceMode.Impulse);
	}

    /// <summary>
    /// Sets the enabled properties of this hand's colliders.
    /// </summary>
    /// <param name="enabled">Should the hand's colliders be enabled?</param>
    public void SetCollidersEnabled(bool enabled)
    {
        bodyCollider.enabled = enabled;
        pickUpCollider.enabled = enabled;
    }

    //Triggered Methods (OnCollision Methods)--------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Load up a hand if collided.
    /// </summary>
    /// <param name="other">The object collided with.</param>
    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Player" && canCollect)
		{
			Player.Instance.HandController.AddHand(this);
			Player.Instance.HandController.ResetAnimationToIdle(handAnimator, handSide);
		}
    }
}
