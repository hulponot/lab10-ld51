using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peekable : MonoBehaviour
{
    public Peekables type;

    private GG gg;
    private BoxCollider2D bc;
    public GameObject btn; 
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        gg = FindObjectOfType<GG>();
        btn.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        btn.SetActive(true);
        gg.CanPeek(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        btn.SetActive(false);
        gg.CannotPeek();
    }
}

