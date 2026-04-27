using UnityEngine;
using Unity.Cinemachine;

[DefaultExecutionOrder(1000)]
public class CameraLockOn : MonoBehaviour
{
    [Header("Nastavení Cíle")]
    public Transform enemyTarget;

    [Header("Nastavení Pozice")]
    public Vector3 cameraOffset = new Vector3(0, 3, -5);

    [Header("Sklon Kamery")]
    [Range(-45f, 45f)]
    public float cameraTilt = 10f;

    void Start()
    {
        // Vypnout Cinemachine – jinak pøepíše vše co nastavíme ruènì
        var brain = Camera.main?.GetComponent<CinemachineBrain>();
        if (brain != null) brain.enabled = false;
    }

    void LateUpdate()
    {
        if (Camera.main == null) return;

        Camera.main.transform.position = this.transform.position + cameraOffset;

        if (enemyTarget != null)
        {
            // Použij root pozici nepøítele – ignoruje rotaci child objektù
            Vector3 targetPos = enemyTarget.root.position;

            // Volitelnì: ignoruj i výškové rozdíly (Y), kamera se nebude klonit nahoru/dolù
            targetPos.y = Camera.main.transform.position.y;

            Vector3 direction = targetPos - Camera.main.transform.position;
            Camera.main.transform.rotation = Quaternion.LookRotation(direction)
                                           * Quaternion.Euler(cameraTilt, 0f, 0f);
        }
    }
}