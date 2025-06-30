using UnityEngine;

[ExecuteAlways]
public class DoorInteractable : MonoBehaviour
{
    [Header("Door Settings")]
    [SerializeField] private Transform doorPivot;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float closeAngle = 0f;
    [SerializeField] private float doorSpeed = 2f;

    [Header("Lock Settings")]
    [SerializeField] private bool isLocked = false;
    [SerializeField] private Key requiredKey;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip doorCloseSound;
    [SerializeField] private AudioClip doorLockedSound;
    private AudioSource audioSource;

    private bool isOpen = false;
    private bool isAnimating = false;
    private Quaternion targetRotation;

    public Color colorPuerta = Color.white;

    void OnValidate()
    {
        if (!Application.isPlaying)
        {
            Renderer rend = GetComponent<Renderer>();
            if (rend != null)
            {
                rend.sharedMaterial.color = colorPuerta;
            }
        }
    }

    void Start()
    {
        if (Application.isPlaying)
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material = new Material(rend.material);
            rend.material.color = colorPuerta;

            audioSource = GetComponent<AudioSource>();

            targetRotation = Quaternion.Euler(0f, closeAngle, 0f);

        }


    }
    private void Update()
    {
        if (isAnimating)
        {
            doorPivot.localRotation = Quaternion.Lerp(doorPivot.localRotation, targetRotation, Time.deltaTime * doorSpeed);
            if (Quaternion.Angle(doorPivot.localRotation, targetRotation) < 0.3f)
            {
                doorPivot.localRotation = targetRotation;
                isAnimating = false;
            }
        }
    }


    public void ToggleDoor()
    {
        if (isLocked)
        {
            if (requiredKey != null && KeyInventory.Instance.HasKey(requiredKey.id))
            {
                PlaySound(doorOpenSound);
                isLocked = false;
                Debug.Log($"Door unlocked with key: {requiredKey.keyName}");
            }
            else
            {
                PlaySound(doorLockedSound);
                Debug.LogWarning("Door is locked and you do not have the required key.");
                return;
            }
        }
        if (!isAnimating || Quaternion.Angle(doorPivot.localRotation, targetRotation) > 0.3f)
        {

            isOpen = !isOpen;
            targetRotation = Quaternion.Euler(0f, isOpen ? openAngle : closeAngle, 0f);
            isAnimating = true;
            
            if (isOpen)
            {
                PlaySound(doorOpenSound);
            }
            else
            {
                PlaySound(doorCloseSound);
            }
        }
    }
    
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("AudioSource or sound clip is not assigned.");
        }
    }
}
