using UnityEngine;

public class PlayerBeyBlade : BeyBlade
{
    protected override Vector3 GetInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        return new Vector3(h, 0, v);
    }
}
