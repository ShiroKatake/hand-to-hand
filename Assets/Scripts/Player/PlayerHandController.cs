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



    //Non-Serialized Fields------------------------------------------------------------------------

    private Dictionary<HandSide, List<Hand>> hands;
    private bool swapLeft;
    private bool swapRight;
    private bool pickUpHand;

    //Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------                                                                                                                          



    //Complex Public Properties--------------------------------------------------------------------                                                    

    /// <summary>
    /// The currently equipped left hand.
    /// </summary>
    public Hand LeftHand { get => hands[HandSide.Left].Count > 0 ? hands[HandSide.Left][0] : null; }

    /// <summary>
    /// The currently equipped right hand.
    /// </summary>
    public Hand RightHand { get => hands[HandSide.Right].Count > 0 ? hands[HandSide.Right][0] : null; }

    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
    /// Awake() runs before Start().
    /// </summary>
    protected override void Awake()
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
        GetInput();
        UpdateHands();
	}

 //   /// <summary>
 //   /// FixedUpdate() is run at a fixed interval independant of framerate.
 //   /// </summary>
	//void FixedUpdate()
 //   {

	//}

    //Recurring Methods (Update())-------------------------------------------------------------------------------------------------------------------

    private void GetInput()
    {
        bool tab = Input.GetButtonDown("Tab");
        swapLeft = tab && Input.GetButtonDown("Left Hand");
        swapRight = tab && Input.GetButtonDown("Right Hand");
        pickUpHand = Input.GetButtonDown("Pick Up");
    }

    //Recurring Methods (FixedUpdate())--------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// The "update" part of the hands' Update() code.
    /// </summary>
    private void UpdateHands()
    {
        if (swapLeft) SwapHands(HandSide.Left);
        if (swapRight) SwapHands(HandSide.Right);
        if (pickUpHand) PickUpHand();
    }

    /// <summary>
    /// If the player has more than one hand for their left or right, swaps the current hand for the next one in the list.
    /// </summary>
    /// <param name="side"></param>
    private void SwapHands(HandSide side)
    {
        if (hands[side].Count > 1)
        {
            Debug.Log("PlayerHandController.SwapHands(), EnableHand() and DisableHand() aren't implemented yet, so not running ReQueueHand() yet either.");
            //ReQueueHand(side);
            DisableHand(hands[side][hands[side].Count - 1]);
            EnableHand(hands[side][0]);
        }
    }

    /// <summary>
    /// Check if the player can pick up a hand, then picks up the hand and adds it to hands.
    /// </summary>
    private void PickUpHand()
    {
        Debug.Log($"PlayerHandController.PickUpHand() has not been implemented yet.");
    }

    //Triggered Methods------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Enables a hand.
    /// </summary>
    /// <param name="hand">The hand to be enabled.</param>
    private void EnableHand(Hand hand)
    {
        Debug.Log($"PlayerHandController.EnableHand() has not been implemented yet.");
    }

    /// <summary>
    /// Disables a hand.
    /// </summary>
    /// <param name="hand">The hand to be disabled.</param>
    private void DisableHand(Hand hand)
    {
        Debug.Log($"PlayerHandController.DisableHand() has not been implemented yet.");
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
}
