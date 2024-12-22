using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public GameObject buttonPrefab; // Buton prefab'ý
    private GameObject currentButton; // Dinamik olarak oluþturulan buton
    private Branch currentBranch; // Dinamik olarak güncellenen branch referansý
    private bool isClose = false;
    public int totalBranch=0;

    public BranchSpawner branchSpawner; // BranchSpawner referansý
    private Rigidbody2D rb;

    public SCInventory playerInventory;
    InventoryUIController inventoryUI;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventoryUI = gameObject.GetComponent<InventoryUIController>();

    }

    void Update()
    {
        float speedX = Input.GetAxisRaw("Horizontal") * speed;
        float speedY = Input.GetAxisRaw("Vertical") * speed;
        rb.velocity = new Vector2(speedX, speedY);

        if (isClose && Input.GetKeyDown(KeyCode.E) && currentBranch != null)
        {
            playerInventory.AddItem(currentBranch.gameObject.GetComponent<Item>().item);
            inventoryUI.UpdateUI();
            Destroy(currentBranch.gameObject); // Branch yok edilir
                
            Destroy(currentButton); // Buton yok edilir
            // Yeni branch rastgele pozisyonda spawnlanacak
            branchSpawner.SpawnBranchAtRandomPosition(); // Rastgele pozisyonda yeni branch oluþturulacak
            isClose = false; // Yakýnlýk sýfýrlanýr

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("branch"))
        {
            currentBranch = collision.GetComponent<Branch>(); // Tetikleyici branch'i al
            if (currentBranch != null)
            {
                // Yeni bir buton klonu oluþtur ve dalýn yanýna yerleþtir
                currentButton = Instantiate(buttonPrefab, currentBranch.transform.position + new Vector3(0.3f, 0.3f, 0), Quaternion.identity);
                currentButton.SetActive(true); // Butonu aktif et

                currentBranch.animator.SetBool("isNear", true); // Animasyon baþlatýlýr
                isClose = true; // Yakýnlýk aktif
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("branch"))
        {
            Branch branch = other.GetComponent<Branch>();
            if (branch != null)
            {
                branch.animator.SetBool("isNear", false); // Dalýn animator'ünü deðiþtir
            }

            if (currentBranch != null && other.gameObject == currentBranch.gameObject)
            {
                currentBranch = null; // Mevcut branch referansýný sýfýrla

                if (currentButton != null)
                {
                    Destroy(currentButton); // Mevcut butonu yok et
                }

                isClose = false; // Yakýnlýk sýfýrla
            }
        }
    }
}
