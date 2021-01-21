using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A base class for hands as grenades.
/// </summary>
public class Grenade : MonoBehaviour
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------  

    //Serialized Fields----------------------------------------------------------------------------    
    
    [Header("Explosion Game Objects")]
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private GameObject handRenderer;

    [Header("Explosion Stats")]
	[SerializeField] private float explosionRadius;
	[SerializeField] private float explosionForce;
	[SerializeField] private float explosionDamage;
	[SerializeField] private float cookTime = 3f;

    [Header("Audio")]
    [SerializeField] private AudioClip timerSFX;
    [SerializeField] private AudioClip explosionSFX;

	//Non-Serialized Fields------------------------------------------------------------------------                                                    

	private AudioSource audioSource;
	private bool exploding = false;
	private float timePassed;
	private ParticleSystem[] fxs;
    private WeaponStats stats;

	//Public Properties------------------------------------------------------------------------------------------------------------------------------

	//Complex Public Properties----------------------------------------------------------------------                                                                                                                          

	/// <summary>
	/// Is the grenade about to explode?
	/// </summary>
	public bool Exploding { get => exploding; }

	//Initialization Methods-------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
	/// Awake() runs before Start().
	/// </summary>
	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
        stats = GetComponent<WeaponStats>();
	}

	/// <summary>
	/// Start() is run on the frame when a script is enabled just before any of the Update methods are called for the first time. 
	/// Start() runs after Awake().
	/// </summary>
	private void Start()
	{
		handRenderer.SetActive(true);

		if (explosionFX != null)
		{
			explosionFX.SetActive(false);
			fxs = explosionFX.GetComponentsInChildren<ParticleSystem>();
		}

	}

	//Core Recurring Methods-------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Update() is run every frame.
	/// </summary>
	private void Update()
	{
		if (exploding)
		{
            Hand hand = GetComponent<Hand>();
            if (hand != null) hand.CanCollect = false;
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
    /// Triggers the grenade to explode.
    /// </summary>
    public void PullPin()
    {
        exploding = true;
        audioSource.clip = timerSFX;
        audioSource.loop = true;
        audioSource.Play();
    }

	/// <summary>
	/// Explode the grenade.
	/// </summary>
	private void Explode()
	{
        Debug.Log($"{this}.Grenade.Explode()");
        float explosionMultiplier = (stats == null ? 1 : stats.CurrentAmmo / stats.MaxAmmo);
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

		foreach (Collider collider in colliders)
		{
			Rigidbody rb = collider.GetComponent<Rigidbody>();
			Health health = collider.GetComponent<Health>();

			if (rb != null) rb.AddExplosionForce(explosionForce * explosionMultiplier, transform.position, explosionRadius, 1f, ForceMode.Impulse);
			if (health != null) health.TakeDamage(explosionDamage * explosionMultiplier);
		}

		handRenderer.SetActive(false);
		explosionFX.SetActive(true);

        audioSource.Stop();
        audioSource.clip = explosionSFX;
        audioSource.loop = false;
		audioSource.Play();

		//Return hand back to pool
		exploding = false;
		IEnumerator enumerator = Destroy();
		StartCoroutine(enumerator);
	}

	private bool FXStoppedPlaying()
	{
		bool isPlaying;
		foreach (ParticleSystem fx in fxs)
		{
			isPlaying = fx.isPlaying;
			if (fx.isPlaying)
				return false;
		}

		return true;
	}

	IEnumerator Destroy()
	{
		yield return new WaitForSeconds(4f);
		Destroy(gameObject);
	}
}
