using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] public Image icon;
    [SerializeField] private Button equipButton;
    [SerializeField] private TMP_Text itemName;
    [HideInInspector] public PetItem currentItem;

    public Image Icon => icon;

    public void Setup(PetItem item)
    {
        currentItem = item;
        itemName.text = item.PetName;
        icon.sprite = item.Icon;
        
        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(() => InventoryManager.Instance.EquipItem(this));
    }

    private void OnEquip()
    {
        InventoryManager.Instance.EquipItem(this);
    }
}