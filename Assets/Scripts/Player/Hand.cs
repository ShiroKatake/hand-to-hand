using UnityEngine;

public enum HandSide
{
	Left,
	Right
}

public class Hand : MonoBehaviour
{
	[SerializeField] private HandSide handSide;
	[SerializeField] private Transform spawnPoint;

	private Rigidbody rb;

	public HandSide HandSide { get => handSide; }
	public Rigidbody Rigidbody { get => rb; }

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
}
