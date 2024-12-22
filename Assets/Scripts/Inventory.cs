using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public SCInventory playerInventory; // Envanter verileri
    public GameObject inventoryUI; // Envanter UI Paneli
    private bool isInventoryOpen = false; // Envanterin a��k m� kapal� m� oldu�unu kontrol eden de�i�ken
    InventoryUIController inventoryUIController;
    bool isSwapping;
    int tempIndex;
    Slot tempSlot;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // Tab tu�una bas�ld���nda
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen; // Durumu tersine �evir
        inventoryUI.SetActive(isInventoryOpen); // UI panelini aktif/pasif yap
    }

    public void DropItem()
    {
        if (isSwapping == true)
        {
            // Karakterin pozisyonuna g�re d���r
            Vector3 dropPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);

            playerInventory.DropItem(tempIndex, dropPosition);
            isSwapping = false;

                inventoryUIController.UpdateUI();
        }
    }

    public void SwapItem(int index)
    {
        if (isSwapping == false)
        {
            tempIndex = index;
            tempSlot = playerInventory.inventorySlots[tempIndex];
            isSwapping = true;
        }
        else if (isSwapping == true)
        {
            playerInventory.inventorySlots[tempIndex] = playerInventory.inventorySlots[index];
            playerInventory.inventorySlots[index] = tempSlot;
            isSwapping = false;
        }
    }
}
