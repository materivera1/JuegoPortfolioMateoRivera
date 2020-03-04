using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletBehaviour
{
    void Reset();
    void CheckTrigger();
    void Update();
    void Collision(Collision2D col);
}
