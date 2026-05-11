using UnityEngine;
using TMPro;
using System.Collections;

public class SpecialMoveController : MonoBehaviour
{
    [Header("Nastavení nabíjení")]
    public float currentCharge = 0f;
    public float maxCharge = 100f;
    public float chargeSpeed = 10f; // Kolik se dobije za sekundu
    public float dashForce = 50f;

    [Header("UI Reference")]
    public TextMeshProUGUI statusText; // Přetáhni sem svůj TextMeshPro z UI

    private bool isReady = false;
    private bool messageShown = false;

    void Start()
    {
        if (statusText != null)
            statusText.text = ""; // Na začátku schovat
    }

    void Update()
    {
        // 1. Nabíjení
        if (currentCharge < maxCharge)
        {
            currentCharge += chargeSpeed * Time.deltaTime;
        }
        else if (!isReady)
        {
            PrepareSpecialMove();
        }

        // 2. Aktivace (třeba klávesou Mezerník)
        if (isReady && Input.GetKeyDown(KeyCode.Space))
        {
            ExecuteSpecialMove();
        }
    }

    void PrepareSpecialMove()
    {
        isReady = true;
        if (!messageShown)
        {
            StartCoroutine(ShowReadyMessage());
        }
    }

    IEnumerator ShowReadyMessage()
    {
        messageShown = true;
        statusText.text = "SPECIÁLKA PŘIPRAVENA! (Space)";

        yield return new WaitForSeconds(3f);

        statusText.text = "";
    }

    void ExecuteSpecialMove()
    {
        Debug.Log("BUM! Speciální útok aktivován.");

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            if (rb.linearVelocity.magnitude > 0.1f)
            {
                Vector3 dashDirection = rb.linearVelocity.normalized;
                rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
            }
            else
            {

                rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
            }
        }

        currentCharge = 0f;
        isReady = false;
        messageShown = false;
        statusText.text = "";
    }
}
