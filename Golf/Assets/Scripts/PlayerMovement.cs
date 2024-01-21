using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    Vector3 movementVector;
    [SerializeField] float speed;
    // Update is called once per frame
    void Update()
    {
        if (!IsLocalPlayer)
            return;

            transform.position += movementVector * speed * Time.deltaTime;
    }
    public void SetMovementVector(InputAction.CallbackContext input)
    {
        movementVector = input.ReadValue<Vector2>();
        movementVector.z = movementVector.y;
        movementVector.y = 0;
    }
}
