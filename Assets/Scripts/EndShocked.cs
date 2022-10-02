using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndShocked : MonoBehaviour
{
    public Animator storog;
    public Animator beast;
    public Animator girl;
    public GameObject exitBtn;

    private WaitForSeconds waitFor3 = new WaitForSeconds(4);
    private WaitForSeconds waitFor1 = new WaitForSeconds(1);
    private void Start()
    {
        girl.gameObject.SetActive(false);
        exitBtn.SetActive(false);
        StartCoroutine(FinalSeq());
    }

    public IEnumerator FinalSeq(){
        yield return waitFor1;
        storog.SetTrigger("aaa");
        yield return waitFor1;
        beast.SetTrigger("shock");
        yield return waitFor3;
        beast.gameObject.SetActive(false);
        girl.gameObject.SetActive(true);
        girl.SetTrigger("fall");
        girl.speed = 0.5f;

        yield return waitFor3;

        exitBtn.SetActive(true);
    }

    public void ToMainMenu(){
        SceneManager.LoadScene("Menu");
    }
}
