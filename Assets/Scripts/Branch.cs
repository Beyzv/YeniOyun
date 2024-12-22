using UnityEngine;

public class Branch : MonoBehaviour
{
    public GameObject button; // Her dal için button objesi
    public Animator animator;

    void Start()
    {
        button.SetActive(false); // Baþlangýçta buton gizli
    }

    public void ShowButton()
    {
        button.SetActive(true); // Butonu göster
    }

    public void HideButton()
    {
        button.SetActive(false); // Butonu gizle
    }
}
