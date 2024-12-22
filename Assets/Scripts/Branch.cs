using UnityEngine;

public class Branch : MonoBehaviour
{
    public GameObject button; // Her dal i�in button objesi
    public Animator animator;

    void Start()
    {
        button.SetActive(false); // Ba�lang��ta buton gizli
    }

    public void ShowButton()
    {
        button.SetActive(true); // Butonu g�ster
    }

    public void HideButton()
    {
        button.SetActive(false); // Butonu gizle
    }
}
