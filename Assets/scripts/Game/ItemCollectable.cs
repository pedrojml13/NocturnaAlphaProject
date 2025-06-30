using UnityEngine;

public class ItemCollectable : MonoBehaviour
{
    [SerializeField] private Key key;

    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioSource audioSource;
    public void itemPickedUp()
    {
        if (key != null)
        {
            KeyInventory.Instance.AddKey(key);

            if (audioSource != null && pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }
            else
            {
                Debug.LogWarning("AudioSource or pickupSound is not assigned.");
            }

            gameObject.SetActive(false);

        }
        else
        {
            Debug.LogWarning("No key assigned to this collectable item.");
        }
    }
}
