using System.Xml;
using UnityEngine;

public class HedgeboarController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidbody2d;

    private bool facingRight;
    private bool canCharge;
    private float nextFlip;
    private float flipTimer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();

        facingRight = false;
        canCharge = true;
        nextFlip = 0f;
        flipTimer = 2.5f;
    }

    private void Update()
    {
        if (Time.time > nextFlip)
        {
            if (Random.Range(0, 10) > 4) Flip();
            nextFlip = Time.time + flipTimer;
        }
    }


    private void Flip()
    {
        if (!canCharge) return;

        transform.localScale = new(
            transform.localScale.x * -1,
            transform.localScale.y,
            transform.localScale.z);

        facingRight = !facingRight;
    }
}
