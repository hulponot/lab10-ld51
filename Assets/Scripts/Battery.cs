using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    private BoxCollider2D bc;
    public GameObject btn;

    private GG gg;
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        btn.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out gg))
        {
            if(gg.CanUseBattery())
            {
                btn.SetActive(true);
                gg.CanFix(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        btn.SetActive(false);
        gg.CannotFix();
    }
}
