using System.Collections;
using StarterAssets;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Third Person Controller References
    private ThirdPersonController thirdPersonController;
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

    [Header ("PowerUps")]
    public PowerUpType currentPowerUp = PowerUpType.None;
    public Coroutine powerUpCountDown;
    public bool hasPowerUp;
    private void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        playerHealth = GetComponent<PlayerHealth>();
    }
    private void Update()
    {
        timeSinceAttack += Time.deltaTime;
        CheckEnemy();
        LightAttack();
        HeavyAttack();
        PickUpObjects();
        Equip();
        Block();
        Kick();

        UsePowerUps(); //Activated when player has powerup
    }
    public void CheckEnemy()
    {
        Collider[] colInfo = Physics.OverlapSphere(attackPoint.position, attackRange, enemyMask);
        foreach (Collider col in colInfo)
        {
            enemyHealth =col.GetComponent<EnemyHealth>();
        }

    }
    void PickUpObjects()
    {
        if (Input.GetKey(KeyCode.P) && playerHealth.isAlive)
            playerAnim.SetTrigger("Picking");
    }
    private void Equip()
    {
        if (Input.GetKeyDown(KeyCode.R) && playerAnim.GetBool("Grounded") && playerHealth.isAlive)
        {
            isEquipping = true;
            playerAnim.SetTrigger("Equip");
        }
    }

    public void ActiveWeapon()
    {
        if (!isEquipped)
        {
            sword.SetActive(true);
            swordOnShoulder.SetActive(false);
            isEquipped = !isEquipped;
        }
        else
        {
            sword.SetActive(false);
            swordOnShoulder.SetActive(true);
            isEquipped = !isEquipped;
        }
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
        if (Input.GetKey(KeyCode.LeftControl) && playerAnim.GetBool("Grounded") && playerHealth.isAlive)
        {
            playerAnim.SetBool("Kick", true);
            isKicking = true;
        }
        else
        {
            playerAnim.SetBool("Kick", false);
            isKicking = false;
        }
    }

    private void LightAttack()
    {

        if (Input.GetMouseButtonDown(0) && playerAnim.GetBool("Grounded") && timeSinceAttack > 0.8f && playerHealth.isAlive)
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

            //Reset Timer
            timeSinceAttack = 0;
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

        if (Input.GetMouseButtonDown(1) && playerAnim.GetBool("Grounded") && timeSinceAttack > 0.8f && playerHealth.isAlive)
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

            //Reset Timer
            timeSinceAttack = 0;
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
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("PowerUp")){
            hasPowerUp = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            Destroy(other.gameObject);
        }

        if(powerUpCountDown != null){
            StopCoroutine(powerUpCountDown);
        }

        powerUpCountDown = StartCoroutine(PowerUpCountDownRoutine());
    }

    //PowerUp Abilities
    private void UsePowerUps(){
        if(currentPowerUp == PowerUpType.MovementSpeed){
            MovementSpeedIncrease();
        }

        if(currentPowerUp == PowerUpType.ExtraArmor){
            IncreaseArmor();
        }

        if(currentPowerUp == PowerUpType.Attack){
            EfficientAttack();
        }

        if(currentPowerUp == PowerUpType.InstantHealing){
            HealingOverTime();
        }
    }
    private void MovementSpeedIncrease(){
        thirdPersonController.MoveSpeed = 5f;
        thirdPersonController.SprintSpeed = 10f;
    }

    private void IncreaseArmor(){
        playerHealth.TakeDamage(5);
    }

    private void EfficientAttack(){
        AttackEnemy(25);
    }

    private void HealingOverTime(){
        float healingSpeed = 15f;
        playerHealth.healthSlider.value += healingSpeed; 
    }

    IEnumerator PowerUpCountDownRoutine(){
        yield return new WaitForSeconds(10f);
        hasPowerUp = false;
        currentPowerUp = PowerUpType.None;
    }
}
