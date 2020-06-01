using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    [System.Serializable]
    public class EnemyStats
    {
        public int maxHealth = 100;

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth ); }
        }

        public int damage = 40;

        public void Init()
        {
            curHealth = maxHealth;
        }
    }

    public EnemyStats stats = new EnemyStats();

    public Transform deathParticles;
    public GameObject player;
    private Animator anim;

    public float attackCooldown = 2;
    public float shakeAmt = 0.1f;
    public float shakeLength = 0.1f;

    public string deathSoundName = "Explosion";

    [Header ("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;


    private void Update()
    {
        if (player == null && GameMaster.gm.PlayerDead != true)
        {
            //Debug.Log("No player found");
            player = GameObject.FindWithTag("Player");
        }
    }
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player");

        stats.Init();

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }

        if (deathParticles == null)
        {
            Debug.LogError("No death particles referenced on enemy");
        }
    }


    public void DamageEnemy(int damage)
    {
        stats.curHealth -= damage;
        if (stats.curHealth <= 0)
        {
            GameMaster.KillEnemy(this);
        }

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }


    IEnumerator EnemyAttack(float attackCooldown)
    {
        while (true)
        {
            anim.SetBool("isAttacking", true);
            player.GetComponent<Player>().DamagePlayer(stats.damage);       //reference player damage to -health and then loop cooldown
            yield return new WaitForSeconds(attackCooldown);    
            Debug.Log("in range");
        }
    }
    private void OnCollisionEnter2D(Collision2D _colInfo)
    {
        if (_colInfo.gameObject.CompareTag("Weapon"))
        {
            //anim.SetBool("isDead", true);
            //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            //DamageEnemy(99999);
            //Debug.Log("collided with umbrella");
        }

        Player _player = _colInfo.collider.GetComponent<Player>();
        if (_player != null && !_colInfo.gameObject.CompareTag("Weapon") && player.GetComponent<PlatformerCharacter2D>().isSliding == false)
        {
            StartCoroutine(EnemyAttack(attackCooldown));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            anim.SetBool("isAttacking", false);
            Debug.Log("out of range");
        }
    }

    private void OnCollisionStay2D(Collision2D _colInfo)
    {


    }
}