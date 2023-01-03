//Libraries..
using UnityEngine;

public interface IDamageable
{
    void Damage(float damageAmount);
    bool IsObjectDestroyed();
}
