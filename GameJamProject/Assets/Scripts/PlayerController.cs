using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Third Person Controller References
    [SerializeField]
    private Animator playerAnim;


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

    private void Update()
    {
        timeSinceAttack += Time.deltaTime;

        LightAttack();
        HeavyAttack();

        Equip();
        Block();
        Kick();
    }

    private void Equip()
    {
        if (Input.GetKeyDown(KeyCode.R) && playerAnim.GetBool("Grounded"))
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
        if (Input.GetKey(KeyCode.Q) && playerAnim.GetBool("Grounded"))
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
        if (Input.GetKey(KeyCode.LeftControl) && playerAnim.GetBool("Grounded"))
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

        if (Input.GetMouseButtonDown(0) && playerAnim.GetBool("Grounded") && timeSinceAttack > 0.8f)
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
    void AttackEnemy(int damage)
    {
        Collider[] colInfo = Physics.OverlapSphere(attackPoint.position, attackRange, enemyMask);

        foreach (Collider col in colInfo)
        {
            col.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    private void HeavyAttack()
    {

        if (Input.GetMouseButtonDown(1) && playerAnim.GetBool("Grounded") && timeSinceAttack > 0.8f)
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
            AttackEnemy(10);
            Animator animator = col.GetComponent<Animator>();
            animator.SetTrigger("LightAttack");
        }
    }
    public void HeavyAttackReaction()
    {
        Collider[] colInfo = Physics.OverlapSphere(attackPoint.position, attackRange, enemyMask);

        foreach (Collider col in colInfo)
        {
            AttackEnemy(20);
            Animator animator = col.GetComponent<Animator>();
            animator.SetTrigger("HeavyAttack");
        }
    }
}
