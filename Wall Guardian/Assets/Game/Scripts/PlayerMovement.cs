using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float runSpeed =3f;

    void Start()
    {
    }

    public void Movement(Vector2 inputVector)
    {
        Vector2 moveDirection = inputVector;

        if (moveDirection != Vector2.zero)
        {
            // Move the player using Rigidbody
            Vector2 moveVector = moveDirection * runSpeed * Time.deltaTime;
            transform.position += (Vector3)moveVector;

            // Rotate the player to face the movement direction
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

}

