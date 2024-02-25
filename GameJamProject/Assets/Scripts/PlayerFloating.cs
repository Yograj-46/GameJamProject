using UnityEngine;

public class PlayerFloating : MonoBehaviour
{
    public float buoyancyForce = 50f;
    private bool isInWater = false;
    private Rigidbody rb;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
       animator  = rb.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            isInWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            isInWater = false;
            animator.SetBool("IsTreadingWater", false);

        }
    }

    private void FixedUpdate()
    {
        if (isInWater)
        {
           animator.SetBool("IsTreadingWater", true);
            Vector3 newPosition = rb.position + Vector3.up * buoyancyForce * Time.deltaTime;
            rb.MovePosition(newPosition);
        }
    }
}
