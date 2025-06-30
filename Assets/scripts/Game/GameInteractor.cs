using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
public class GameInteractor : MonoBehaviour
{

    [Header("Raycast Features")]
    [SerializeField] private float raycastDistance = 5f;

    [Header("Raycast Features")]
    [SerializeField] private StarterAssetsInputs inputs; // Assuming you have a StarterAssetsInputs script for input handling
    [SerializeField] private Image crosshair;

    private Camera mainCamera;
    private GameItem gameItem;
    private GameItem equipedItem;
    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
        PerformRaycast();
        InteractionInput();

    }

    private void PerformRaycast()
    {
        if (Physics.Raycast(mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)), transform.forward, out RaycastHit hit, raycastDistance))
        {
            var _gameItem = hit.collider.GetComponent<GameItem>();
            if (_gameItem != null)
            {
                gameItem = _gameItem;
                HighlightCrosshair(true);

            }
            else
            {
                ClearItem();
            }
        }
        else
        {
            ClearItem();
        }
    }

    private void ClearItem()
    {
        if (gameItem != null)
        {
            gameItem = null;
            HighlightCrosshair(false);
        }
    }

    void HighlightCrosshair(bool on)
    {
        if (crosshair != null)
        {
            crosshair.color = on ? Color.red : Color.white;
        }
    }

    private void InteractionInput()
    {
        if (gameItem != null)
        {
            if (inputs.interact)
            {
                gameItem.ObjectInteraction();
                inputs.interact = false; // Reset the interact input to prevent multiple interactions
            }

            if (inputs.leftHandInteract)
            {
                gameItem.EquipItem(true);
                equipedItem = gameItem;
                equipedItem.GetComponent<Rigidbody>().isKinematic = true; // Disable physics while equipped
                inputs.leftHandInteract = false; // Reset the left hand interact input
            }
        }

        else if (equipedItem != null)
        {
            if (inputs.leftHandInteract)
            {
                //equipedItem.UnequipableItemInteraction();
                //equipedItem.GetComponent<Rigidbody>().isKinematic = false; // Re-enable physics when unequipped
                //equipedItem = null;
                equipedItem.EquipedItemInteraction();
                inputs.leftHandInteract = false; // Reset the left hand interact input
            }
        }
        
        inputs.interact = false;
        inputs.leftHandInteract = false;

    }
}
