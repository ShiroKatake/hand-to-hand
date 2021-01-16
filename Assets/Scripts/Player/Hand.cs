using UnityEngine;

/// <summary>
/// Which handside the object is.
/// </summary>
public enum HandSide
{
	Left,
	Right
}

public class Hand : MonoBehaviour
{
	//Private Fields---------------------------------------------------------------------------------------------------------------------------------

	private Rigidbody rb;

	//Serialized Fields----------------------------------------------------------------------------

	[SerializeField] private HandSide handSide;
	[SerializeField] private Transform spawnPoint;

	//Public Properties------------------------------------------------------------------------------------------------------------------------------

	public HandSide HandSide { get => handSide; }
	public Rigidbody Rigidbody { get => rb; }
	
	//Initialization Methods-------------------------------------------------------------------------------------------------------------------------
	
	/// <summary>
	/// Start() is run on the frame when a script is enabled just before any of the Update methods are called for the first time. 
	/// Start() runs after Awake().
	/// </summary>
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
}
