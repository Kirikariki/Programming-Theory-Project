using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
// Plane inherits features of ShootableBase. No need to rewrite/copy code from parent class(es).
public class Plane : ShootableBase
{
    private readonly float minSpeed = 1000f;
    private readonly float maxSpeed = 3000f;

    // POLYMORPHISM
    // Overrides abstract method of parent class with individual implementation
    protected override void InitializeStats()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        moveDirection.x = Mathf.RoundToInt(speed) % 2 == 1 ? 1 : -1;
        points = Mathf.RoundToInt(speed / minSpeed) * 2;
    }

    // POLYMORPHISM
    // Overrides abstract method of parent class with individual implementation
    protected override Vector3 RandomStartPosition()
    {
        float posX = moveDirection.x < 0 ? RandomPos(boundX, false) : -RandomPos(boundX, false);
        return new(posX, RandomPos(boundY, true), Random.Range(3, 6));
    }
}