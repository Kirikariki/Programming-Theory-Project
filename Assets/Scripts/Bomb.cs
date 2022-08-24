using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
// Bomb inherits features of ShootableBase. No need to rewrite/copy code from parent class(es).
public class Bomb : ShootableBase
{
    private readonly float minSpeed = 0f;
    private readonly float maxSpeed = 250f;

    // POLYMORPHISM
    // Overrides abstract method of parent class with individual implementation
    protected override void InitializeStats()
    {
        float rand = Random.Range(0.5f, 2f);
        rb.mass = rand;
        rb.transform.localScale = new(rand, rand, rand);
        speed = Random.Range(minSpeed, maxSpeed) * rand;
        moveDirection.x = Mathf.RoundToInt(speed) % 2 == 1 ? 1 : -1;
        points = Mathf.RoundToInt(5f / rand);
    }

    // POLYMORPHISM
    // Overrides abstract method of parent class with individual implementation
    protected override Vector3 RandomStartPosition()
    {
        return new(RandomPos(boundX, true), RandomPos(boundY, false), Random.Range(0, 2));
    }

    // OnTriggerEnter is called when the Collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (!deactivationTriggered && other.CompareTag("Ground"))
        {
            Debug.LogWarning("Bomb hit bottom!!!");
            DeactivateGameObject();
        }
        else if (other.CompareTag("Boundary"))
        {
            DeactivateGameObject();
        }
    }
}
