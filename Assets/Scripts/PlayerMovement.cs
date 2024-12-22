using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public GameObject buttonPrefab; // Buton prefab'�
    private GameObject currentButton; // Dinamik olarak olu�turulan buton
    private Branch currentBranch; // Dinamik olarak g�ncellenen branch referans�
    private bool isClose = false;
    public int totalBranch=0;

    public BranchSpawner branchSpawner; // BranchSpawner referans�
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
            branchSpawner.SpawnBranchAtRandomPosition(); // Rastgele pozisyonda yeni branch olu�turulacak
            isClose = false; // Yak�nl�k s�f�rlan�r

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("branch"))
        {
            currentBranch = collision.GetComponent<Branch>(); // Tetikleyici branch'i al
            if (currentBranch != null)
            {
                // Yeni bir buton klonu olu�tur ve dal�n yan�na yerle�tir
                currentButton = Instantiate(buttonPrefab, currentBranch.transform.position + new Vector3(0.3f, 0.3f, 0), Quaternion.identity);
                currentButton.SetActive(true); // Butonu aktif et

                currentBranch.animator.SetBool("isNear", true); // Animasyon ba�lat�l�r
                isClose = true; // Yak�nl�k aktif
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
                branch.animator.SetBool("isNear", false); // Dal�n animator'�n� de�i�tir
            }

            if (currentBranch != null && other.gameObject == currentBranch.gameObject)
            {
                currentBranch = null; // Mevcut branch referans�n� s�f�rla

                if (currentButton != null)
                {
                    Destroy(currentButton); // Mevcut butonu yok et
                }

                isClose = false; // Yak�nl�k s�f�rla
            }
        }
    }
}
