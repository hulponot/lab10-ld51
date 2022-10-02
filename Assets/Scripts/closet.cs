using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closet : MonoBehaviour
{
    private GG gg;
    public GameObject btn;
    void Start()
    {
        gg = FindObjectOfType<GG>();
        btn.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        btn.SetActive(true);
        gg.NearCloset(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        btn.SetActive(false);
        gg.NearCloset(false);
    }
}
