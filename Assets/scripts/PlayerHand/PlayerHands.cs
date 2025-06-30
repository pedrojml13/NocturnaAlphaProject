using UnityEngine;
using System.Collections;

public class PlayerHands : MonoBehaviour
{
    public static PlayerHands Instance { get; private set; }

    [Header("Referencias de manos")]
    public Transform rightHand;
    public Transform leftHand;

    [Header("Ajustes de visualización en cámara")]
    public Transform cameraTransform;

    [Header("Posición del objeto al equipar (local a cámara)")]
    public Vector3 customEquipPosition = new Vector3(0.2f, 0.4f, 0.5f);

    [Header("Rotación del objeto al equipar (Euler)")]
    public Vector3 customEquipRotation = new Vector3(317.91f, 216.48f, 40.6f);

    [Header("Velocidad de animación al equipar")]
    public float equipLerpDuration = 0.3f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    public void Equip(GameObject item, Transform gripPoint, bool rightHanded)
    {
        if (cameraTransform == null)
        {
            Debug.LogWarning("La cámara no está asignada en PlayerHands.");
            return;
        }

        item.transform.SetParent(cameraTransform);

        //item.transform.SetParent(rightHanded ? rightHand : leftHand);
        // Asignamos rotación al instante
        item.transform.localEulerAngles = customEquipRotation;

        // Posición de destino en local respecto a la cámara
        Vector3 targetLocalPosition = customEquipPosition;

        // Comenzamos animación suave
        StartCoroutine(MoveToLocalPositionSmooth(item.transform, targetLocalPosition));
    }

    public void Unequip(GameObject item)
    {
        if (item == null) return;

        item.transform.SetParent(null);
        item.transform.position = cameraTransform.position + cameraTransform.forward * 0.5f;
        item.transform.rotation = cameraTransform.rotation;

        // Aquí puedes añadir animación de soltado si quieres
    }

    private IEnumerator MoveToLocalPositionSmooth(Transform item, Vector3 targetLocalPosition)
    {
        Vector3 startLocalPos = item.localPosition;
        float elapsed = 0f;

        while (elapsed < equipLerpDuration)
        {
            item.localPosition = Vector3.Lerp(startLocalPos, targetLocalPosition, elapsed / equipLerpDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        item.localPosition = targetLocalPosition;
    }
}