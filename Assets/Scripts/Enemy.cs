using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

   
    
    [SerializeField]
    private Transform exitPoint;
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private float navUpdate;

    [SerializeField]
    private int healthPoints;

    private int target = 0;
    private Transform enemy;
    private Collider2D enemyCollider;
    private Animator anim;
    private float navTime = 0;
    private bool isDead = false;


    [SerializeField] private int rewardAmount;


    public bool IsDead
    {
        get { return isDead; }
    }

	// Use this for initialization
	void Start () {
        GameController.Shared.RegisterEnemy(this);
        anim = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        enemy = GetComponent<Transform>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if(wayPoints != null && !isDead)
        {
            navTime += Time.deltaTime;
            if(navTime > navUpdate)
            {
                if(target < wayPoints.Length)
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, navTime);
                }
                else
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, navTime);
                }

                navTime = 0;
            }
        }
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Checkpoint")
        {
            target += 1;
            Debug.Log("You Hit Check Point");
        } else if (other.tag == "Finish")
        {
            GameController.Shared.RoundEscaped += 1;
            GameController.Shared.TotalEscaped += 1;
            GameController.Shared.UnregisterEnemy(this);
            GameController.Shared.IsWaveOver();
            Destroy(gameObject);
            Debug.Log("You hit the finish line");
        } else if(other.tag == "Projectile"){
            Projectile newP = other.gameObject.GetComponent<Projectile>();
            EnemyHit(newP.AttackStrength);
            Destroy(other.gameObject);
        }
    }

    public void EnemyHit(int hitPoints)
    {
        if (healthPoints - hitPoints > 0)
        {
            healthPoints -= hitPoints;
            //hurt animation
            anim.Play("Hurt");
            GameController.Shared.AudioSource.PlayOneShot(SoundController.Shared.Hit);
        } else {
            //die animation
            anim.SetTrigger("didDie");
            //enemy should die
            Die();
            
            
        }
    }

    public void Die()
    {
        isDead = true;
        GameController.Shared.TotalKilled += 1;
        enemyCollider.enabled = false;
        GameController.Shared.AddMoney(rewardAmount);
        GameController.Shared.IsWaveOver();
        GameController.Shared.AudioSource.PlayOneShot(SoundController.Shared.Death);
    }
}
