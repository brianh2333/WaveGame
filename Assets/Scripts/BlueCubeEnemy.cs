using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCubeEnemy : Enemy
{
    public string enemyName = "Blue Cube";
    public float damage;
    public float walkspeed;
    public float health = 100;
    private float maxHealth;

    public Transform[] spawnPoints;

    public Animator animator;

    public float maxDeathSeconds;

    [SerializeField]
    private GameObject target;

    private bool canHurt;
    private bool canDie;


    public BlueCubeEnemy(string name, float dmg, float speed, float hlth)
    {
        enemyName = name;
        damage = dmg;
        walkspeed = speed;
        health = hlth;
        SetHealth(health);
    }

    public override void SetHealth(float health)
    {
        if(gameObject.GetComponent<HealthSystem>() == null)
            gameObject.AddComponent<HealthSystem>();

        gameObject.GetComponent<HealthSystem>().SetHealth(health);
    }


    public override void SetDamage(float dmg)
    {
        damage = dmg;
    }

    public override void SetWalkspeed(float speed)
    {
        walkspeed = speed;
    }

    public void Event_TakeDamage(object sender, HealthSystem.OnDamagedEventArgs e)
    {
        health -= e.amount;
    }

    private void Awake()
    {
        int index = 0;
        foreach (GameObject t in GameObject.FindGameObjectsWithTag("EnemySpawn"))
        {
            spawnPoints[index] = t.transform;
            index++;
        }

        canHurt = true;
        canDie = true;
        SetHealth(health);
        maxHealth = health;
        gameObject.GetComponent<HealthSystem>().OnDamaged += Event_TakeDamage;

        target = GameObject.FindGameObjectWithTag("Target");
    }

    public void OnEnable()
    {

        transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

        animator = GetComponent<Animator>();
        SetHealth(health);
        
    }

    private void Update()
    {
        if ((health <= (maxHealth-(maxHealth*.50)) && health > 0) && canHurt)
        {
            animator.SetBool("Hurt", true);
            canHurt = false;
        }

        if (health <= 0 && canDie)
        {
            StartCoroutine(OnDeath());
            canDie = false;
        }

        Vector2 distance = target.transform.position - transform.position;
        if ( distance.magnitude > 1 )
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, walkspeed * Time.deltaTime);
    }


    private IEnumerator OnDeath()
    {
        float speed = walkspeed;
        animator.SetBool("Hurt", false);
        animator.SetTrigger("Dead");
        walkspeed = 0;
        yield return new WaitForSeconds(maxDeathSeconds);
        WaveSpawner.aliveEnemies--;
        animator.SetBool("Hurt", false);
        gameObject.SetActive(false);
        walkspeed = speed;
        canHurt = true;
        canDie = true;
    }

}
