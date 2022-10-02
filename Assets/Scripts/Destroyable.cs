
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public Sprite healthy;
    public Sprite destroyed;
    public GameObject surprise;

    private GG gg;
    private SpriteRenderer sr;
    private bool beenDestroyed = false;

    void Start()
    {
        gg = FindObjectOfType<GG>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = healthy;
    }

    public void TryDestroy(){
        if (!beenDestroyed && Vector3.Distance(transform.position, gg.transform.position) < gg.DestriyDistance){
            beenDestroyed = true;
            if (surprise != null){
                sr.sprite = null;
                Instantiate(surprise, transform.position, Quaternion.identity, transform);
            } else {
                sr.sprite = destroyed;
                if (name == "closet"){
                    gg.Lose();
                }
            }
        }
    }
}
