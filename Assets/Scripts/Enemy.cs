using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //protected string enemyName;
    //protected float damage;
    //protected float walkspeed;
    //protected float health;

    //protected abstract Enemy(string name, float dmg, float speed, float hlth);


    public abstract void SetHealth(float hlth);

    public abstract void SetDamage(float dmg);

    public abstract void SetWalkspeed(float speed);

}
