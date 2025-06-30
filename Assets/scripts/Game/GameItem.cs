using UnityEngine;

public class GameItem : MonoBehaviour
{
    public enum InteractableItemType
    {
        None,
        Door,
        Key
    }

    public enum EquipableItemType
    {
        None,
        Flashlight,
    }


    [Header("Item Type")]
    [SerializeField] private InteractableItemType itemType = InteractableItemType.None;
    [SerializeField] private EquipableItemType equipableItemType = EquipableItemType.None;
    private DoorInteractable doorInteractable;
    private ItemCollectable itemCollectable;

    private EquipableItem equipableItem;
    private void Awake()
    {
        switch (itemType)
        {
            case InteractableItemType.Door:
                doorInteractable = GetComponent<DoorInteractable>();
                break;
            case InteractableItemType.Key:
                itemCollectable = GetComponent<ItemCollectable>();
                break;

            default:
                break;
        }
        
        switch (equipableItemType)
        {
            case EquipableItemType.Flashlight:
                equipableItem = GetComponent<EquipableItem>();
                break;
            default:
                break;
        }

    }

    public void ObjectInteraction()
    {
        switch (itemType)
        {
            case InteractableItemType.Door:
                doorInteractable?.ToggleDoor();
                break;
            case InteractableItemType.Key:
                itemCollectable?.itemPickedUp();
                break;
            default:
                Debug.LogWarning("No interaction defined for this item type.");
                break;
        }
    }

    public void EquipItem(bool equip)
    {
        if (equipableItem != null)
        {
            equipableItem.EquipToHand(equip);
        }
        else
        {
            Debug.LogWarning("No EquipableItem component found on this GameItem.");
        }
    }

    public void EquipedItemInteraction()
    {
        if (equipableItemType == EquipableItemType.Flashlight)
        {
            Light light = equipableItem.GetComponentInChildren<Light>();
            if (light != null)
            {
                light.enabled = !light.enabled;
            }
            else
            {
                Debug.LogWarning("No Light component found in children of the equipped item.");
            }

        }

    }
    
    public void UnequipableItemInteraction()
    {

        equipableItem.UnequipFromHand();

    }

}
