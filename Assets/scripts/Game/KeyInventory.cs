using UnityEngine;
using System.Collections.Generic;
public class KeyInventory : MonoBehaviour
{
    [SerializeField] private List<int> keysIds = new List<int>();

    public static KeyInventory Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddKey(Key key)
    {
        if (key != null && !keysIds.Contains(key.id))
        {
            keysIds.Add(key.id);
            Debug.Log($"Key {key.keyName} with ID {key.id} has been added to the inventory.");
            UIManager.Instance.AddKeyToUI(key);
        }
        else
        {
            Debug.LogWarning("Key is null or already exists in the inventory.");
        }
    }

    public bool HasKey(int keyId)
    {
        return keysIds.Contains(keyId);
    }
}
