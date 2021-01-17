using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A controller class for the player managing their hand grenades.
/// </summary>
public class PlayerHandController : PrivateInstanceSerializableSingleton<PlayerHandController>
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------

    //Serialized Fields----------------------------------------------------------------------------

    [SerializeField] private List<Hand> handsOnAwake;
    [SerializeField] private Transform leftHandSpawn;
    [SerializeField] private Transform rightHandSpawn;

    //Non-Serialized Fields------------------------------------------------------------------------

    private Dictionary<HandSide, List<Hand>> hands;
    private bool swapLeft;
    private bool swapRight;

    //Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------                                                                                                                          



    //Complex Public Properties--------------------------------------------------------------------                                                    

    /// <summary>
    /// The currently equipped left hand.
    /// </summary>
    public Hand LeftHand { get => hands[HandSide.Left].Count > 0 ? hands[HandSide.Left][0] : null; }

    /// <summary>
    /// The transform the left hand gets childed to.
    /// </summary>
    public Transform LeftHandSpawn { get => leftHandSpawn; }

    /// <summary>
    /// The currently equipped right hand.
    /// </summary>
    public Hand RightHand { get => hands[HandSide.Right].Count > 0 ? hands[HandSide.Right][0] : null; }

    /// <summary>
    /// The transformt he right hand gets childed to.
    /// </summary>
    public Transform RightHandSpawn { get => rightHandSpawn; }

    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
    /// Awake() runs before Start().
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        hands = new Dictionary<HandSide, List<Hand>>();
        hands[HandSide.Left] = new List<Hand>();
        hands[HandSide.Right] = new List<Hand>();        
    }

    /// <summary>
    /// Start() is run on the frame when a script is enabled just before any of the Update methods are called for the first time. 
    /// Start() runs after Awake().
    /// </summary>
    private void Start()
    {
        if (handsOnAwake != null && handsOnAwake.Count > 0)
        {
            foreach (Hand h in handsOnAwake)
            {
                hands[h.HandSide].Add(h);
                h.Collider.enabled = false;
                DisableHand(h);
            }
        }

        if (hands[HandSide.Left].Count > 0) EnableHand(hands[HandSide.Left][0]);
        if (hands[HandSide.Right].Count > 0) EnableHand(hands[HandSide.Right][0]);
    }

    //Core Recurring Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Update() is run every frame.
    /// </summary>
    private void Update()
	{
        GetInput();
        UpdateHands();
	}

    //Recurring Methods (Update())-------------------------------------------------------------------------------------------------------------------

    private void GetInput()
    {
        bool tab = Input.GetButtonDown("Tab");
        swapLeft = tab && Input.GetButtonDown("Left Hand");
        swapRight = tab && Input.GetButtonDown("Right Hand");
    }

    //Recurring Methods (FixedUpdate())--------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// The "update" part of the hands' Update() code.
    /// </summary>
    private void UpdateHands()
    {
        if (swapLeft) SwapHands(HandSide.Left);
        if (swapRight) SwapHands(HandSide.Right);
    }

    /// <summary>
    /// If the player has more than one hand for their left or right, swaps the current hand for the next one in the list.
    /// </summary>
    /// <param name="side"></param>
    private void SwapHands(HandSide side)
    {
        if (hands[side].Count > 1)
        {
            ReQueueHand(side);
            DisableHand(hands[side][hands[side].Count - 1]);
            EnableHand(hands[side][0]);
        }
    }

    //Triggered Methods------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Enables a hand.
    /// </summary>
    /// <param name="hand">The hand to be enabled.</param>
    private void EnableHand(Hand hand)
    {
        hand.MeshRenderer.enabled = true;
    }

    /// <summary>
    /// Disables a hand.
    /// </summary>
    /// <param name="hand">The hand to be disabled.</param>
    private void DisableHand(Hand hand)
    {
        hand.MeshRenderer.enabled = false;
    }

    /// <summary>
    /// Moves a hand to the back of the queue to update the current left/right hand.
    /// </summary>
    /// <param name="side"></param>
    private void ReQueueHand(HandSide side)
    {
        Hand hand = hands[side][0];
        hands[side].Remove(hand);
        hands[side].Add(hand);
    }

    /// <summary>
    /// Removes a hand from the list of left/right hands.
    /// </summary>
    /// <param name="hand">The hand to remove.</param>
    public void RemoveHand(Hand hand)
    {
        if (hands[hand.HandSide].Contains(hand))
        {
            if (hands[hand.HandSide][0] == hand) EnableHand(hands[hand.HandSide][1]);
            hands[hand.HandSide].Remove(hand);
        }
    }

    /// <summary>
	/// Load up a hand if collided.
	/// </summary>
	/// <param name="other">The object collided with.</param>
	private void OnCollisionEnter(Collision other)
    {
        Debug.Log($"Collision with {other.collider}");
        if (!other.gameObject.CompareTag("Hand")) return;

        Hand hand = other.gameObject.GetComponent<Hand>();

        if (hand != null)
        {
            if (hands[hand.HandSide].Count > 0) DisableHand(hands[hand.HandSide][0]);
            hands[hand.HandSide].Insert(0, hand);
            hand.transform.parent = (hand.HandSide == HandSide.Left ? leftHandSpawn : rightHandSpawn);
            hand.Collider.enabled = false;
            hand.transform.position = Vector3.zero;
            hand.transform.rotation = Quaternion.identity;
        }
    }
}
