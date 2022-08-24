using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
// Balloon inherits features of ShootableBase. No need to rewrite/copy code from parent class(es).
public class Balloon : ShootableBase
{
    // POLYMORPHISM
    // Overrides abstract method of parent class with individual implementation
    protected override void InitializeStats()
    {
        float rand = Random.Range(1f, 2f);
        transform.localScale = new(rand, rand, rand);
        moveDirection.y = 1;
        speed = 24f - (rand * 2f);
        points = Mathf.CeilToInt(6 / rand);
    }

    // POLYMORPHISM
    // Overrides abstract method of parent class with individual implementation
    protected override Vector3 RandomStartPosition()
    {
        return new(RandomPos(boundX, true), -RandomPos(boundY, false), Random.Range(1, 5));
    }
}
