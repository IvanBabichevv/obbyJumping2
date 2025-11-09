using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("UI ссылки")] [SerializeField] GameObject inventoryWindow;
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private List<ActiveToolbarSlot> activeSlots;

    private readonly List<PetItem> items = new();
    private bool isOpen = false;

    void Awake() => Instance = this;


    void OnEnable()
    {
        ShopManager.OnItemBought += AddItem;
    }

    void OnDisable()
    {
        ShopManager.OnItemBought -= AddItem;
    }

    private void Start()
    {
        if (inventoryWindow != null)
            inventoryWindow.SetActive(false);
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryWindow.SetActive(isOpen);
    }

    void AddItem(PetItem PetItem, bool ingoreCopy = false)
    {
        Debug.Log($"Покупка получена в инвентарь: {PetItem.PetName}");

        if (!ingoreCopy)
        {
            // Проверка на дубликаты
            foreach (var i in items)
            {
                if (i == PetItem)
                {
                    Debug.Log($"{PetItem.PetName} уже есть в инвентаре");
                    return;
                }
            }
        }

        items.Add(PetItem);

        // Создаём слот
        GameObject slotObj = Instantiate(inventorySlotPrefab, contentParent);
        InventorySlot slot = slotObj.GetComponent<InventorySlot>();
        slot.Setup(PetItem);

        Debug.Log($"В инвентарь добавлен: {PetItem.PetName}");
    }


    public void EquipItem(InventorySlot slot)
    {
        if (slot == null || slot.Icon == null)
        {
            Debug.LogWarning("Слот или иконка не найдены");
            return;
        }

        foreach (var holder in activeSlots)
        {
            if (!holder.IsBusy)
            {
                holder.SetItem(slot.currentItem);
                holder.SetBusy(true);

                slot.currentItem.isEquipped = true;

                Debug.Log($"{slot.currentItem.PetName} экипирован в {holder.name}");
                Destroy(slot.gameObject);
                
                PetSpawner.Instance.SpawnPet(slot.currentItem);
                return;
            }
        }

        Debug.LogWarning("Нет свободных активных слотов");
    }

    public void UnequipItem(PetItem petItem, ActiveToolbarSlot activeSlot)
    {
        activeSlot.SetItem(null);
        activeSlot.SetBusy(false);
        AddItem(petItem, true);
        
        PetSpawner.Instance.DespawnPet(petItem);
    }
    
}