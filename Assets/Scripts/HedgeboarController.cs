using UnityEngine;

public class HedgeboarController : MonoBehaviour
{
    [SerializeField] private float speed = 7.0f;

    private Animator animator;
    private Rigidbody2D rigidbody2d;

    private bool facingRight;
    private bool canFlip;
    private bool isCharging;

    private float nextFlip;
    private float flipTimer;
    private float startChargeTime;
    private float chargeDelay;

    private void Start()
    {   
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();

        facingRight = false;
        canFlip = true;
        isCharging = false;
        nextFlip = 0f;
        flipTimer = 2.5f;
        chargeDelay = .35f;
        startChargeTime = 0f;
        
    }

    private void Update()
    {
        if (Time.time > nextFlip)
        {
            if (canFlip && Random.Range(0, 10) > 4) Flip();
            nextFlip = Time.time + flipTimer;
        }
        animator.SetBool("isCharging", isCharging);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (facingRight != other.transform.position.x > transform.position.x) Flip();
            canFlip = false;
            isCharging = true;
            startChargeTime = Time.time + chargeDelay;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Time.time > startChargeTime)
        {
            rigidbody2d.AddForce(new Vector2(x: facingRight ? +1 : -1, y: 0) * speed);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canFlip = true;
            isCharging = false;
            rigidbody2d.velocity = Vector2.zero;
        }
    }

    private void Flip()
    {
        transform.localScale = new(
            transform.localScale.x * -1,
            transform.localScale.y,
            transform.localScale.z);
        facingRight = !facingRight;
    }
}
