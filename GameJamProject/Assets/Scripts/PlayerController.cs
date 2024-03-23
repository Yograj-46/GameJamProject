using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{
    //Third Person Controller References
    private StarterAssetsInputs _input;

    [Header("Sword Equip Unequip")]
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject swordOnShoulder;
    public bool isEquipping;
    public bool isEquipped;

    [Header("Attack Parameters")]
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask enemyMask;
    private float timeSinceAttack;
    public int currentAttack = 0;
    public bool isAttacking;
    
    [Header("PowerUps")]
    public PowerUpType currentPowerUp = PowerUpType.None;
    public Coroutine powerUpCountDown;
    public bool hasPowerUp;
    
    [Header ("Script References")]
    [SerializeField] EnemyHealth enemyHealth;  //Enemy health script reference
    [SerializeField] public AudioManager sfx; //Audio Manager script reference

    [Header("Others")]
    [SerializeField] public static Animator playerAnim;
    public VisualEffect levelUp; //For LevelUp Effect
    public bool isBlocking;
    public bool isKicking;
    public static bool summoning;

    private void Start(){
        levelUp.enabled = false;
        _input = GetComponent<StarterAssetsInputs>();
        playerAnim = GetComponent<Animator>();
    }
    private void Update(){
        timeSinceAttack += Time.deltaTime;
        CheckEnemy();

        UsePowerUps(); //Activated when player has powerup
    }
    public void CheckEnemy(){
        Collider[] colInfo = Physics.OverlapSphere(attackPoint.position, attackRange, enemyMask);
        foreach (Collider col in colInfo){
            enemyHealth = col.GetComponent<EnemyHealth>();
        }
    }
    public void Equip(){
        if (_input.equip && playerAnim.GetBool("Grounded") && PlayerHealth.isAlive)
            isEquipping = true;
            playerAnim.SetTrigger("Equip");
            _input.equip = false;
    }
    public void ActiveWeapon(){
        if (!isEquipped) {
            sword.SetActive(true);
            swordOnShoulder.SetActive(false);
            isEquipped = !isEquipped;
            sfx.clip(sfx.swordDraw);
        }
        else {
            sword.SetActive(false);
            swordOnShoulder.SetActive(true);
            isEquipped = !isEquipped;
            sfx.clip(sfx.swordKeep);
        }
    }
    void Power(){
        levelUp.enabled = true;
        playerAnim.SetTrigger("PowerUp");
        levelUp.Play();
    }
    public void Equipped(){
        isEquipping = false;
    }

    public void Block(){
        if (Input.GetKey(KeyCode.Q) && playerAnim.GetBool("Grounded") && PlayerHealth.isAlive){
            playerAnim.SetBool("Block", true);
            isBlocking = true;
        }
        else{
            playerAnim.SetBool("Block", false);
            isBlocking = false;
        }
    }

    public void Kick(){
        if (_input.kick && playerAnim.GetBool("Grounded") && PlayerHealth.isAlive){
            playerAnim.SetTrigger("Kicking");
            sfx.clip(sfx.whip);
            isKicking = true;
            _input.kick = false;
        }
        else{
            isKicking = false;
        }
    }

    public void LightAttack() {

        if (_input.lightAttack && playerAnim.GetBool("Grounded") && timeSinceAttack > 0.8f && PlayerHealth.isAlive) {
            if (!isEquipped)
                return;

            currentAttack++;
            isAttacking = true;

            if (currentAttack > 3)
                currentAttack = 1;

            //Reset Attack
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
    public void AttackEnemy(int damage) {
        if (enemyHealth.blockCount > 0 && !enemyHealth.isBlocking && PlayerHealth.isAlive) {
            Collider[] colInfo = Physics.OverlapSphere(attackPoint.position, attackRange, enemyMask);
            foreach (Collider col in colInfo) {
                col.GetComponent<EnemyHealth>().TakeDamage(damage);
                if (enemyHealth.blockCount == 0) {
                    enemyHealth.isBlocking = true;
                    if (enemyHealth.isBlocking) {
                        Animator enemy = col.GetComponent<Animator>();
                        enemy.SetTrigger("IsBlocking");
                        damage = 0;
                    }
                }
            }
        }
    }
    public void HeavyAttack() {

        if (_input.heavyAttack && playerAnim.GetBool("Grounded") && timeSinceAttack > 0.8f && PlayerHealth.isAlive)
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
            _input.heavyAttack = false;
        }
    }
    //This will be used at animation event
    public void ResetAttack() {
        isAttacking = false;
    }
    public void LightAttackReaction()
    {

        Collider[] colInfo = Physics.OverlapSphere(attackPoint.position, attackRange, enemyMask);

        foreach (Collider col in colInfo) {
            if (!enemyHealth.isBlocking) {
                AttackEnemy(10);
                Animator animator = col.GetComponent<Animator>();
                animator.SetTrigger("LightAttack");
            }
        }
    }
    public void HeavyAttackReaction()
    {
        Collider[] colInfo = Physics.OverlapSphere(attackPoint.position, attackRange, enemyMask);

        foreach (Collider col in colInfo) {
            if (!enemyHealth.isBlocking) {
                AttackEnemy(20);
                Animator animator = col.GetComponent<Animator>();
                animator.SetTrigger("HeavyAttack");
            }
        }
    }

    //This method checks whether player has powerup or not.
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PowerUp")) {
            hasPowerUp = true;
            currentPowerUp = PowerUp.powerUpType;
            Destroy(other.gameObject);
        }

        if (powerUpCountDown != null) {
            StopCoroutine(powerUpCountDown);
        }

        powerUpCountDown = StartCoroutine(PowerUpCountDownRoutine());
    }

    //Collecting Orb Animation
    public void CollectOrb(){
        if (_input.summon) {
            summoning = !summoning;
            _input.summon = false;
        }
        if(summoning){
            playerAnim.SetTrigger("Summoning");
        }
        else playerAnim.SetTrigger("ReturnToIdle");
    }

    //PowerUp Abilities
    private void UsePowerUps(){
        if (currentPowerUp == PowerUpType.MovementSpeed) {
            PowerUps.MovementSpeedIncrease();
        }

        if (currentPowerUp == PowerUpType.ExtraArmor) {
            PowerUps.IncreaseArmor();
        }

        if (currentPowerUp == PowerUpType.Attack) {
            PowerUps.EfficientAttack();
        }

        if (currentPowerUp == PowerUpType.InstantHealing) {
            PowerUps.HealingOverTime();
        }
    }
    IEnumerator PowerUpCountDownRoutine(){
        yield return new WaitForSeconds(10f);
        hasPowerUp = false;
        currentPowerUp = PowerUpType.None;
    }
}

public class PowerUps
{
    public static void MovementSpeedIncrease(){
        ThirdPersonController.MoveSpeed = 5f;
        ThirdPersonController.SprintSpeed = 10f;
    }

    public static void IncreaseArmor(){
        PlayerHealth.TakeDamage(5);
    }

    public static void EfficientAttack(){
        EnemyHealth.currentHealth -= 25;
    }

    public static void HealingOverTime(){
        float healingSpeed = 15f;
        PlayerHealth.healthSlider.value += healingSpeed;
    }
}
