using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMoveable
{
    Rigidbody2D rigidBody { get; set; }
    bool isFacingRight { get; set; }

    void MoveEnemy(Vector2 velocity);

    void FaceCorrectDirection(Vector2 velocity);
}
