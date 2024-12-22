using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public SCInventory playerInventory; // Envanter verileri
    public GameObject inventoryUI; // Envanter UI Paneli
    private bool isInventoryOpen = false; // Envanterin açýk mý kapalý mý olduðunu kontrol eden deðiþken
    InventoryUIController inventoryUIController;
    bool isSwapping;
    int tempIndex;
    Slot tempSlot;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // Tab tuþuna basýldýðýnda
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen; // Durumu tersine çevir
        inventoryUI.SetActive(isInventoryOpen); // UI panelini aktif/pasif yap
    }

    public void DropItem()
    {
        if (isSwapping == true)
        {
            // Karakterin pozisyonuna göre düþür
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
