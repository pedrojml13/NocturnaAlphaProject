using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Transform keyPanel;
    [SerializeField] private GameObject keyImagePrefab;

    private Dictionary<Key, GameObject> keyImages = new Dictionary<Key, GameObject>();

    public static UIManager Instance { get; private set; }

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

    public void AddKeyToUI(Key key)
    {
        if(!keyImages.ContainsKey(key))
        {
            GameObject keyImage = Instantiate(keyImagePrefab, keyPanel);
            keyImage.GetComponent<Image>().sprite = key.keySprite;
            keyImages[key] = keyImage;
        }
        else
        {
            Debug.LogWarning($"Key {key.keyName} is already in the UI.");
        }
    }
}
