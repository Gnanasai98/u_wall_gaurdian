using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float runSpeed =3f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Movement(Vector2 inputVector)
    {
        Vector2 moveDirection = inputVector;

        if (moveDirection != Vector2.zero)
        {
            // Move the player using Rigidbody
            Vector2 moveVector = moveDirection * runSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + moveVector);

            // Rotate the player to face the movement direction
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

}

