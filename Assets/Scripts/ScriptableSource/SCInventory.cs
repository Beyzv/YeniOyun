using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable/Inventory")]
public class SCInventory : ScriptableObject
{
   public List<Slot> inventorySlots = new List<Slot>();
    int stackLimit = 4;
    InventoryUIController inventoryUIController;
    public void DeleteItem(int index)
    {
        inventorySlots[index].isFull = false;
        inventorySlots[index].itemCount = 0;
        inventorySlots[index].item=null;
        
    }
    public void DropItem(int index, Vector3 position)
    {
        if (inventorySlots[index].item != null && inventorySlots[index].item.itemPrefab != null)
        {
            for (int i = 0; i < inventorySlots[index].itemCount; i++)
            {
                GameObject droppedItem = Instantiate(inventorySlots[index].item.itemPrefab);
                droppedItem.transform.position = new Vector3(position.x + Random.Range(-0.5f, 0.5f), position.y + Random.Range(-0.5f, 0.5f), 0); // Etrafa biraz rastgelelik ekle
            }

            DeleteItem(index);
             inventoryUIController.UpdateUI();
            
        }
        else
        {
            Debug.LogWarning("Slot boþ veya item prefab atanmadý!");
        }
    }

    public bool AddItem(SCItem item)
    {
        foreach (Slot slot in inventorySlots)
        {
            if(slot.item == item)
            {
                if(slot.item.canStackable)
                {
                    if(slot.itemCount < stackLimit)
                    {
                        slot.itemCount++;
                        if(slot.itemCount >= stackLimit)
                        {
                            slot.isFull = true;
                        }
                        return true;
                    }
                }
            }
            else if(slot.itemCount == 0 )
            {
                slot.AddItemToSlot(item);
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public class Slot
{
    public bool isFull;
    public int itemCount;
    public SCItem item;

    public void AddItemToSlot(SCItem item)
    {
        this.item = item;
        if(item.canStackable == false)
        {
            isFull= true;
        }
        itemCount++;
    }

}
