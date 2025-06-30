using UnityEngine;

public class EquipableItem : MonoBehaviour
{
    public Transform gripPoint;

    private bool isEquipped = false;
    public void EquipToHand(bool rightHand)
    {
        PlayerHands.Instance.Equip(this.gameObject, gripPoint, rightHand);
        isEquipped = true;
    }

    public void UnequipFromHand()
    {
        if (isEquipped)
        {
            PlayerHands.Instance.Unequip(this.gameObject);
            isEquipped = false;
        }
    }
}