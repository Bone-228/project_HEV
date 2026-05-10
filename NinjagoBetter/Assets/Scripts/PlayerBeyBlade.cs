using UnityEngine;

public class PlayerBeyBlade : BeyBlade
{
    protected override Vector3 GetInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Transform camTransform = Camera.main.transform;

        Vector3 camForward = camTransform.forward;
        Vector3 camRight = camTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = (camForward * v) + (camRight * h);

        return moveDirection;
    }
}
