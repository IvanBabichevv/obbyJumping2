using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using YG;

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
        //YG2.onGetSDKData += InitializeData;
    }

    void OnDisable()
    {
        ShopManager.OnItemBought -= AddItem;
        //YG2.onGetSDKData -= InitializeData;
    }

    private void Start()
    {
        if (YG2.isSDKEnabled)
            InitializeData();

        if (inventoryWindow != null)
            inventoryWindow.SetActive(false);
    }

    private void InitializeData()
    {
        foreach (var petId in YG2.saves.itemInInventorySlotId)
        {
            PetItem pet = ListOfItems.Instance.GetPet(petId);
            UpdateInventoryUI(pet);
        }

        for (int i = 0; i < YG2.saves.slotsId.Count; i++)
        {
            int slotId = YG2.saves.slotsId[i];
            int petId = YG2.saves.itemInActiveSlotId[i];

            PetItem pet = ListOfItems.Instance.GetPet(petId);
            activeSlots[slotId].SetItem(pet);
            activeSlots[slotId].SetBusy(true);
            PetSpawner.Instance.SpawnPet(pet);
        }
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryWindow.SetActive(isOpen);
    }

    void AddItem(PetItem PetItem, bool ingoreCopy = false)
    {
        if (!ingoreCopy)
        {
            // Проверка на дубликаты
            foreach (var i in items)
            {
                if (i == PetItem)
                {
                    return;
                }
            }
        }

        UpdateInventoryUI(PetItem);
        
        YG2.saves.itemInInventorySlotId.Add(PetItem.petId);

        YG2.SaveProgress();
    }

    private void UpdateInventoryUI(PetItem petItem)
    {
        items.Add(petItem);

        // Создаём слот
        GameObject slotObj = Instantiate(inventorySlotPrefab, contentParent);
        InventorySlot slot = slotObj.GetComponent<InventorySlot>();
        slot.Setup(petItem);
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
                YG2.saves.itemInActiveSlotId.Add(slot.currentItem.petId);
                YG2.saves.slotsId.Add(holder.slotId);

                holder.SetItem(slot.currentItem);
                holder.SetBusy(true);

                slot.currentItem.isEquipped = true;

                PetSpawner.Instance.SpawnPet(slot.currentItem);
                
                YG2.saves.itemInInventorySlotId.Remove(slot.currentItem.petId);
                
                Destroy(slot.gameObject);

                YG2.SaveProgress();

                return;
            }
        }

        Debug.LogWarning("Нет свободных активных слотов");
    }

    public void UnequipItem(PetItem petItem, ActiveToolbarSlot activeSlot)
    {
        YG2.saves.itemInActiveSlotId.Remove(petItem.petId);
        YG2.saves.slotsId.Remove(activeSlot.slotId);
        
        activeSlot.SetItem(null);
        activeSlot.SetBusy(false);
        AddItem(petItem, true);

        PetSpawner.Instance.DespawnPet(petItem);
        
        YG2.SaveProgress();
    }
}