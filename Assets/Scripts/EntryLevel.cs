using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryLevel : MonoBehaviour
{
    public TextMeshProUGUI text;

    private string[] lines = {
        "the antidote must be somewhere at the other end of the corridor",
        "hope it doesn't affect the human...",
        "what a day",
    };

    private int index;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown){
            if (index + 1 == lines.Length)
            {
                SceneManager.LoadScene("Main");
            }
            else
            {
                text.text = lines[index++];
            }
        }
    }
}
