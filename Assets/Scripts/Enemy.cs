using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public abstract void SetHealth(float hlth);

    public abstract void SetDamage(float dmg);

    public abstract void SetWalkspeed(float speed);

}
