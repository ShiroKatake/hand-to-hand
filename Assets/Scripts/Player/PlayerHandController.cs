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
    [SerializeField] private Weapon leftHandWeapon;
    [SerializeField] private SkinnedMeshRenderer leftHandRenderer;
    [SerializeField] private Transform leftHandSpawn;
    [SerializeField] private Weapon rightHandWeapon;
    [SerializeField] private SkinnedMeshRenderer rightHandRenderer;
    [SerializeField] private Transform rightHandSpawn;
    [SerializeField] private Transform handPool;

    //Non-Serialized Fields------------------------------------------------------------------------

    private Dictionary<HandSide, List<Hand>> hands;
    private bool tab;
    private bool swapLeft;
    private bool swapRight;

    //Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------                                                                                                                          



    //Complex Public Properties--------------------------------------------------------------------                                                    

    /// <summary>
    /// The currently equipped left hand.
    /// </summary>
    public Hand LeftHand { get => (hands[HandSide.Left].Count > 0 ? hands[HandSide.Left][0] : null); }

    /// <summary>
    /// The transform the left hand gets childed to.
    /// </summary>
    public Transform LeftHandSpawn { get => leftHandSpawn; }

    /// <summary>
    /// The currently equipped right hand.
    /// </summary>
    public Hand RightHand { get => (hands[HandSide.Right].Count > 0 ? hands[HandSide.Right][0] : null); }

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

        if (handsOnAwake != null && handsOnAwake.Count > 0)
        {
            foreach (Hand h in handsOnAwake)
            {
                if (!h.HasGottenComponents) h.GetComponents();
                AddHand(h);
            }
        }
    }

    /// <summary>
    /// Start() is run on the frame when a script is enabled just before any of the Update methods are called for the first time. 
    /// Start() runs after Awake().
    /// </summary>
    private void Start()
    {
        if (hands[HandSide.Left].Count > 0) SetCurrentWeapon(HandSide.Left);
        if (hands[HandSide.Right].Count > 0) SetCurrentWeapon(HandSide.Right);
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
        tab = Input.GetButton("Tab");
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
            SetCurrentWeapon(side);
        }
    }

    //Triggered Methods------------------------------------------------------------------------------------------------------------------------------

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
    /// Enables a hand.
    /// </summary>
    /// <param name="hand">The hand to be enabled.</param>
    private void SetCurrentWeapon(HandSide side)
    {
        Weapon weapon = (side == HandSide.Left ? leftHandWeapon : rightHandWeapon);
        SkinnedMeshRenderer weaponRenderer = (side == HandSide.Left ? leftHandRenderer : rightHandRenderer);
        weapon.CurrentStats = hands[side][0].Stats;
        if (!weaponRenderer.enabled) weaponRenderer.enabled = true;

        Material[] materials = new Material[5];

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = hands[side][0].Stats.Material;
        }

        weaponRenderer.materials = materials;
    }

    /// <summary>
    /// Adds a hand to the player.
    /// </summary>
    /// <param name="hand">The hand to add.</param>
    public void AddHand(Hand hand)
    {
        Debug.Log($"PlayerHandController.AddHand()");
        if (!hands[hand.HandSide].Contains(hand))
        {
            Debug.Log($"Adding Hand");
            hands[hand.HandSide].Insert(0, hand);
            SetCurrentWeapon(hand.HandSide);

            hand.SetCollidersEnabled(false);
            hand.Rigidbody.useGravity = false;
            hand.Rigidbody.isKinematic = true;

            hand.transform.parent = handPool;

            hand.Rigidbody.velocity = Vector3.zero;
            hand.Rigidbody.angularVelocity = Vector3.zero;
            hand.transform.localPosition = Vector3.zero;
            hand.transform.localRotation = Quaternion.identity;
        }
    }

    /// <summary>
    /// Removes a hand from the player.
    /// </summary>
    /// <param name="hand">The hand to remove.</param>
    public void RemoveHand(Hand hand)
    {
        if (hands[hand.HandSide].Contains(hand))
        {
            Weapon weapon = (hand.HandSide == HandSide.Left ? leftHandWeapon : rightHandWeapon);
            SkinnedMeshRenderer weaponRenderer = (hand.HandSide == HandSide.Left ? leftHandRenderer : rightHandRenderer);
            Transform weaponSpawn = (hand.HandSide == HandSide.Left ? leftHandSpawn : rightHandSpawn);
            hands[hand.HandSide].Remove(hand);

            if (hands[hand.HandSide].Count == 0)
            {
                weapon.CurrentStats = null;
                weaponRenderer.enabled = false;
            }
            else if (hands[hand.HandSide][0].Stats != weapon.CurrentStats)
            {
                SetCurrentWeapon(hand.HandSide);
            }

            hand.gameObject.SetActive(true);
            hand.transform.parent = null;
            hand.SetCollidersEnabled(true);
            hand.Rigidbody.useGravity = true;
            hand.Rigidbody.isKinematic = false;

            hand.transform.parent = weaponSpawn;

            hand.Rigidbody.velocity = Vector3.zero;
            hand.Rigidbody.angularVelocity = Vector3.zero;
            hand.transform.localPosition = Vector3.zero;
            hand.transform.localRotation = Quaternion.identity;

            hand.transform.parent = null;            
        }
    }
}
