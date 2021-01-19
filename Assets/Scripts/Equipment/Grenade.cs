using UnityEngine;

/// <summary>
/// A base class for hands as grenades.
/// </summary>
public class Grenade : MonoBehaviour
{
	//Private Fields---------------------------------------------------------------------------------------------------------------------------------  

	//Serialized Fields----------------------------------------------------------------------------    
    
	[SerializeField] private float explosionRadius;
	[SerializeField] private float explosionForce;
	[SerializeField] private float cookTime = 3f;

    //Non-Serialized Fields------------------------------------------------------------------------                                                    

    private AudioSource audioSource;
    private bool exploding = false;
	private float timePassed;

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
        audioSource = GetComponent<AudioSource>();
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
		if (exploding)
		{
			if (timePassed >= cookTime)
				Explode();
			timePassed += Time.deltaTime;
		}
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

	/// <summary>
	/// Explode the grenade.
	/// </summary>
	private void Explode()
	{
        Debug.Log($"{this}.Grenade.Explode()");
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

		foreach (Collider collider in colliders)
		{
			Rigidbody rb = collider.GetComponent<Rigidbody>();
			Health health = collider.GetComponent<Health>();

			if (rb != null)
				rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 1f, ForceMode.Impulse);

			if (health != null)
			{
				//Take damage
			}
		}

        audioSource.Play();

		//Return hand back to pool
		exploding = false;
	}
}
