using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class GG : MonoBehaviour
{
    public float sleepInterval = 10;
    public float Speed = 1f;
    public float DestriyDistance = 2f;
    public GameObject WinUI;
    public AudioClip WinMusic;
    public GameObject LoseUI;

    private Animator animator;
    private FadeCamera fader;

    private HashSet<Peekables> peeked = new HashSet<Peekables>();
    private Destroyable[] destroyables;
    private Storog[] storogs;

    private Peekable canPeek;
    private Battery battery;
    private bool fixdToBattery = false;
    private bool fallen = false;
    private bool nearCloset = false;
    private bool win = false;

    public AudioSource musicSource;
    public AudioSource topSource;
    public AudioSource effectsSource;
    private WaitForSeconds waitFallAnimation = new WaitForSeconds(1);
    private WaitForSeconds waitHalfFadeout = new WaitForSeconds(1.25f);
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        destroyables = FindObjectsOfType<Destroyable>();
        storogs = FindObjectsOfType<Storog>();
        fader = FindObjectOfType<FadeCamera>();
        WinUI.SetActive(false);
        LoseUI.SetActive(false);
        musicSource.volume = Settings.MusicVolume;
    }

    private void Update()
    {
        TryWin();
        if (win) return;
        TryFix();
        TryPeek();
        Move();
    }

    private void TryPeek(){
        if (fixdToBattery || fallen) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(canPeek != null)
            {
                GetPeekable();
            }
        }
    }
    public void FadeOut(){
        if (fallen || win) return;
        {
            if(Settings.FadeOut){
                fader.RedoFade();
            }
            fallen = true;
            if (!fixdToBattery)
            {
                animator.SetBool("fall", true);
            }
        }
    }

    public void FadeIn(){
        if ( win) return;
        if (!fixdToBattery){
            for (int i = 0; i < destroyables.Length; i++)
            {
                destroyables[i].TryDestroy();
            }
            for (int i = 0; i < storogs.Length; i++)
            {
                if (Vector3.Distance(transform.position, storogs[i].transform.position) < DestriyDistance)
                {
                    SceneManager.LoadScene("shocked");
                }
            }
            animator.SetBool("fall", false);
        }
        if (Settings.FadeOut)
        {
            fader.AddTime();
        }
        fallen = false;
    }

    private void TryFix(){
        if (fallen) return;
        if (battery == null)
        {
            if(fixdToBattery){
                fixdToBattery = false;
                animator.SetTrigger("unfixed");
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if(fixdToBattery){
                fixdToBattery = false;
                animator.SetTrigger("unfixed");
            } else {
                fixdToBattery = true;
                animator.SetTrigger("fixed");
            }
        }
    }
    private void TryWin(){
        if (fallen || !nearCloset) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            WinUI.SetActive(true);
            Speed = 0;
            win = true;
            musicSource.clip = WinMusic;
            musicSource.Play();
            effectsSource.volume = 0;
        }
    }
    public void Lose(){
        LoseUI.SetActive(true);
        Speed = 0;    }

    private void Move(){
        if (fixdToBattery || fallen) return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1);
        }
        if (horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1);
        }

        var vel = new Vector3(horizontal, 0).normalized * Speed * Time.deltaTime;
        if (horizontal == 0f)
        {
            animator.SetFloat("speed", 0);
        } else {
            animator.SetFloat("speed", 1);
        }
        transform.position = transform.position + vel;
    }

    public void CanPeek(Peekable obj){
        canPeek = obj;
    }

    public void CannotPeek(){
        canPeek = null;
    }

    public void GetPeekable(){
        if (canPeek == null) return;
        peeked.Add(canPeek.type);
        canPeek.gameObject.SetActive(false);
        canPeek = null;
    }

    public void CanFix(Battery b){
        battery = b;
    }
    
    public void CannotFix(){
        battery = null;
    }

    public bool CanUseBattery(){
        return peeked.Contains(Peekables.Handcuffs);
    }

    public void NearCloset(bool near){
        nearCloset = near;
    }

    public void ExitMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void SetMusicVolume(System.Single newV){
        musicSource.volume = newV;
        Settings.MusicVolume = newV;
    }
    public void SetEffectsVolume(System.Single vol)
    {
        topSource.volume = vol;
        effectsSource.volume = vol;
        Settings.EffectsVolume = vol;
    }
    public void SetFading(bool fadeAway)
    {
        Settings.FadeOut = fadeAway;
    }
}
