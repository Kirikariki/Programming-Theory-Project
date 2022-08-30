using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
// ShootableBase serves as parent class for Plane, Bomb and Balloon classes
// All inheriting classes will have access to the protected and public fields/methods of this class
/// <summary>
/// Handles basic behaviour of shootable objects
/// </summary>
public abstract class ShootableBase : MonoBehaviour
{
    // To be set in Unity
    [SerializeField] protected GameObject mesh;
    [SerializeField] protected ParticleSystem explosion;
    [SerializeField] protected AudioSource explosion_SFX;

    // Boundaries for spawning objects
    protected float boundX = 9f, boundY = 4f, boundOffset = 3.5f;

    // initialized in Start method
    protected Rigidbody rb;
    protected GameManager gameManager;

    // object variables - initialized in Initialize method
    protected Vector3 moveDirection = new();
    protected float speed = 0f;
    protected int points = 0;
    protected bool deactivationTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (mesh.activeInHierarchy)
        {
            rb.AddForce(speed * Time.deltaTime * moveDirection, ForceMode.Force);
        }
    }

    // OnMouseDown is called when the user has pressed the mouse button while over the GUIElement or Collider
    private void OnMouseDown()
    {
        if (mesh.activeInHierarchy)
        {
            Explode();
            AddPlayerReward();
        }
    }

    // OnTriggerEnter is called when the Collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            DeactivateGameObject();
        }
    }

    /// <summary>
    /// Makes the object explode and deactivates it
    /// </summary>
    /// TODO: implement effects of destroying shootables (eg. give points to player)
    protected void Explode()
    {
        deactivationTriggered = true;
        mesh.SetActive(false);
        explosion.Play();
        explosion_SFX.Play();
        Invoke(nameof(DeactivateGameObject), 2f);
    }

    /// <summary>
    /// Adds points to the player
    /// </summary>
    /// TODO: implement effects of destroying shootables (eg. give points to player)
    protected virtual void AddPlayerReward()
    {
        gameManager.AddPoints(points);
    }

    // ABSTRACTION
    // Method can be called with one line instead of rewriting/copying the code
    /// <summary>
    /// Initializes the object in a random position in the game
    /// </summary>
    public void Initialize()
    {
        InitializeStats();
        transform.position = RandomStartPosition();
        gameObject.SetActive(true);
        mesh.SetActive(true);
    }

    // ABSTRACTION
    // Method can be called with one line instead of rewriting/copying the code
    /// <summary>
    /// Stops the object's movement and deactivates it
    /// </summary>
    protected void DeactivateGameObject()
    {
        if (deactivationTriggered)
        {
            deactivationTriggered = false;
            rb.velocity = new(0, 0, 0);
            gameObject.SetActive(false);
        }
    }

    // POLYMORPHISM
    // All child classes have to implement abstract methods individually
    /// <summary>
    /// Initialize object variables
    /// </summary>
    protected abstract void InitializeStats();

    // POLYMORPHISM
    // All child classes have to implement abstract methods individually
    /// <summary>
    /// Calculates a random position for the object to spawn in
    /// </summary>
    /// <returns>the objects spawn position as a Vector3</returns>
    protected abstract Vector3 RandomStartPosition();

    // ABSTRACTION
    // Method can be called with one line instead of rewriting/copying the code
    /// <summary>
    /// Calculates a float value within -bound and bound or outside of that range
    /// </summary>
    /// <param name="bound">absolute value of the range</param>
    /// <param name="withinBounds">true for random value between -bound and bound, false for value outside of that range</param>
    /// <returns>the calculated value as a float</returns>
    protected float RandomPos(float bound, bool withinBounds)
    {
        if (withinBounds)
        {
            return Random.Range(-bound, bound);
        }
        else
        {
            return bound + boundOffset;
        }
    }
}
