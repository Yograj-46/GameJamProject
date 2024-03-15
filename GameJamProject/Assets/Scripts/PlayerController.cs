using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{
    //public GameObject potionHand;
    //levlling
    public VisualEffect levelUp;
    //sfx
    public AudioManager sfx;
    //Third Person Controller References
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs _input;

    [SerializeField]
    private Animator playerAnim;

    [SerializeField] EnemyHealth enemyHealth;

    //Equip-Unequip parameters
    [SerializeField]
    private GameObject sword;
    [SerializeField]
    private GameObject swordOnShoulder;
    public bool isEquipping;
    public bool isEquipped;

    //Blocking Parameters
    public bool isBlocking;

    //Kick Parameters
    public bool isKicking;

    //Attack Parameters
    public bool isAttacking;
    private float timeSinceAttack;
    public int currentAttack = 0;


    //Attack Range
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask enemyMask;

    // Player Health
    PlayerHealth playerHealth;

    [Header("PowerUps")]
    public PowerUpType currentPowerUp = PowerUpType.None;
    public Coroutine powerUpCountDown;
    public bool hasPowerUp;
    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        playerHealth = GetComponent<PlayerHealth>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Power();
        }
        timeSinceAttack += Time.deltaTime;
        CheckEnemy();
        LightAttack();
        HeavyAttack();
        Equip();
        Block();
        Kick();

        UsePowerUps(); //Activated when player has powerup
        //CollectOrb();
    }
    public void CheckEnemy()
    {
        Collider[] colInfo = Physics.OverlapSphere(attackPoint.position, attackRange, enemyMask);
        foreach (Collider col in colInfo)
        {
            enemyHealth = col.GetComponent<EnemyHealth>();
        }

    }
    private void Equip()
    {
        if (_input.equip && playerAnim.GetBool("Grounded") && playerHealth.isAlive)
        {
            isEquipping = true;
            playerAnim.SetTrigger("Equip");
            _input.equip = false;
        }
       
    }

    public void ActiveWeapon()
    {
        if (!isEquipped)
        {
            sword.SetActive(true);
            swordOnShoulder.SetActive(false);
            isEquipped = !isEquipped;
            sfx.clip(sfx.swordDraw);
        }
        else
        {
            sword.SetActive(false);
            swordOnShoulder.SetActive(true);
            isEquipped = !isEquipped;
            sfx.clip(sfx.swordKeep);
        }
    }

    void Power()
    {

        playerAnim.SetTrigger("PowerUp");
        levelUp.Play();
    }
    public void Equipped()
    {
        isEquipping = false;
    }

    private void Block()
    {
        if (Input.GetKey(KeyCode.Q) && playerAnim.GetBool("Grounded") && playerHealth.isAlive)
        {
            playerAnim.SetBool("Block", true);
            isBlocking = true;
        }
        else
        {
            playerAnim.SetBool("Block", false);
            isBlocking = false;
        }
    }

    public void Kick()
    {
        if (_input.kick && playerAnim.GetBool("Grounded") && playerHealth.isAlive)
        {
            playerAnim.SetTrigger("Kicking");
            sfx.clip(sfx.whip);
            isKicking = true;
            _input.kick = false;
        }
        else
        {
            isKicking = false;
        }
    }

    private void LightAttack()
    {

        if (_input.lightAttack && playerAnim.GetBool("Grounded") && timeSinceAttack > 0.8f && playerHealth.isAlive)
        {
            if (!isEquipped)
                return;

            currentAttack++;
            isAttacking = true;

            if (currentAttack > 3)
                currentAttack = 1;


            //Reset
            if (timeSinceAttack > 1.0f)
                currentAttack = 1;

            //Call Attack Triggers
            playerAnim.SetTrigger("LightAttack" + currentAttack);
            sfx.clip(sfx.attack);

            //Reset Timer
            timeSinceAttack = 0;
            _input.lightAttack = false;
        }


    }
    public void AttackEnemy(int damage)
    {
        if (enemyHealth.blockCount > 0 && !enemyHealth.isBlocking && playerHealth.isAlive)
        {
            Collider[] colInfo = Physics.OverlapSphere(attackPoint.position, attackRange, enemyMask);
            foreach (Collider col in colInfo)
            {
                col.GetComponent<EnemyHealth>().TakeDamage(damage);
                if (enemyHealth.blockCount == 0)
                {
                    enemyHealth.isBlocking = true;
                    if (enemyHealth.isBlocking)
                    {
                        Animator enemy = col.GetComponent<Animator>();
                        enemy.SetTrigger("IsBlocking");
                        damage = 0;
                    }
                }
            }
        }
    }
    //void OnDrawGizmosSelected()
    //{
    //    if (attackPoint == null)
    //        return;
    //    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    //}
    private void HeavyAttack()
    {

        if (_input.heavytAttack && playerAnim.GetBool("Grounded") && timeSinceAttack > 0.8f && playerHealth.isAlive)
        {
            if (!isEquipped)
                return;

            currentAttack++;
            isAttacking = true;

            if (currentAttack > 3)
                currentAttack = 1;


            //Reset
            if (timeSinceAttack > 1.0f)
                currentAttack = 1;

            //Call Attack Triggers
            playerAnim.SetTrigger("HeavyAttack" + currentAttack);
            sfx.clip(sfx.attack);

            //Reset Timer
            timeSinceAttack = 0;
            _input.heavytAttack = false;
        }
    }
    //This will be used at animation event
    public void ResetAttack()
    {
        isAttacking = false;
    }
    public void LightAttackReaction()
    {

        Collider[] colInfo = Physics.OverlapSphere(attackPoint.position, attackRange, enemyMask);

        foreach (Collider col in colInfo)
        {
            if (!enemyHealth.isBlocking)
            {
                AttackEnemy(10);
                Animator animator = col.GetComponent<Animator>();
                animator.SetTrigger("LightAttack");
            }
        }
    }
    public void HeavyAttackReaction()
    {
        Collider[] colInfo = Physics.OverlapSphere(attackPoint.position, attackRange, enemyMask);

        foreach (Collider col in colInfo)
        {
            if (!enemyHealth.isBlocking)
            {
                AttackEnemy(20);
                Animator animator = col.GetComponent<Animator>();
                animator.SetTrigger("HeavyAttack");
            }
        }
    }

    //This method checks whether player has powerup or not.
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(gameObject.transform.position);
        if (other.gameObject.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            Destroy(other.gameObject);
        }

        if (powerUpCountDown != null)
        {
            StopCoroutine(powerUpCountDown);
        }

        powerUpCountDown = StartCoroutine(PowerUpCountDownRoutine());
    }

    //Collecting Orb Animation
    private void CollectOrb()
    {
        if (Input.GetKey(KeyCode.V))
        {
            playerAnim.SetTrigger("Summoning");
        }
    }

    //PowerUp Abilities
    private void UsePowerUps()
    {
        if (currentPowerUp == PowerUpType.MovementSpeed)
        {
            MovementSpeedIncrease();
        }

        if (currentPowerUp == PowerUpType.ExtraArmor)
        {
            IncreaseArmor();
        }

        if (currentPowerUp == PowerUpType.Attack)
        {
            EfficientAttack();
        }

        if (currentPowerUp == PowerUpType.InstantHealing)
        {
            HealingOverTime();
        }
    }
    private void MovementSpeedIncrease()
    {
        thirdPersonController.MoveSpeed = 5f;
        thirdPersonController.SprintSpeed = 10f;
    }

    private void IncreaseArmor()
    {
        playerHealth.TakeDamage(5);
    }

    private void EfficientAttack()
    {
        AttackEnemy(25);
    }

    private void HealingOverTime()
    {
        float healingSpeed = 15f;
        playerHealth.healthSlider.value += healingSpeed;
    }

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(10f);
        hasPowerUp = false;
        currentPowerUp = PowerUpType.None;
    }



}
