using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 6f;
    public float sprintSpeed = 8f;

    public float amountWalking = 0.05f;
    public float amountSprinting = 0.05f;
    public float groundedBufferTime = 0.1f;

    [Header("Landing Bob")]
    public float landingBobAmount = 0.08f;  // <- aquí editable en el inspector

    private Vector3 startPos;
    private Vector3 targetPos;
    private CharacterController controller;
    private StarterAssets.StarterAssetsInputs input;

    private float groundedTimer = 0f;
    private bool wasGrounded = true;
    private float landingBob = 0f;

    void Start()
    {
        startPos = transform.localPosition;
        targetPos = startPos;
        controller = GetComponentInParent<CharacterController>();
        input = GetComponentInParent<StarterAssets.StarterAssetsInputs>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            groundedTimer += Time.deltaTime;
        }
        else
        {
            groundedTimer = 0f;
        }

        if (controller.velocity.magnitude > 0.1f && groundedTimer > groundedBufferTime)
        {
            float currentSpeed = input.sprint ? sprintSpeed : walkSpeed;
            float amount = input.sprint ? amountSprinting : amountWalking;

            float bobX = Mathf.Sin(Time.time * currentSpeed) * amount;
            float bobY = Mathf.Cos(Time.time * currentSpeed * 2) * amount;

            targetPos = startPos + new Vector3(bobX, bobY, 0);
        }
        else
        {
            targetPos = startPos;
        }

        // Detectar aterrizaje
        if (!wasGrounded && controller.isGrounded)
        {
            landingBob = -landingBobAmount;  // <- aquí se usa el valor editable
        }

        landingBob = Mathf.Lerp(landingBob, 0f, Time.deltaTime * 6f);

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos + new Vector3(0, landingBob, 0), Time.deltaTime * 10f);

        wasGrounded = controller.isGrounded;
    }
}
