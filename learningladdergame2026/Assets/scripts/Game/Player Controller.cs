using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private KeyCode Block;
    [SerializeField] private KeyCode Punch;
    [SerializeField] private KeyCode Dodge;
  
    [SerializeField] private string BlockAnimation;
    
    [SerializeField] private string PunchAnimation1;
    
    [SerializeField] private string PunchAnimation2;
    [SerializeField] private string Getupanimation;
    [SerializeField] private string DodgeAnimation;
    [SerializeField] private string KnockeddownAnimation;
    public string IdleAnimation;
    public string HitAnimation;
    [SerializeField] private string DazedInAnimation;
    [SerializeField] private string DazedExitAnimation;
    [SerializeField] private FightController fightcontroler;
    [SerializeField] public Animator anim;
    [Header("Do Not touch")]
    public bool isdown = false;
    public bool CantakeAction = true;
    public bool CanBeHit = true;
    public bool isBlocking = false;
    public bool justGotUp = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((CantakeAction) && (Input.GetKeyDown(Block)))//set up do use a switch statement and only trigger on key press
        {
            GoBlock();
        }
        if ((CantakeAction) && (Input.GetKeyDown(Punch)))
        {
            GoPunch();
        }
        if ((CantakeAction) && (Input.GetKeyDown(Dodge)))
        {
            GoDodge();
        }
        if ((!CantakeAction) && (isBlocking) && (Input.GetKeyUp(Block)))
        {
            EndBlock();
        }


    }
    public void GoPunch()
    {
       // Debug.Log(name + " GoPunch");
        //fightcontroler.ActionTaken(this);
        CantakeAction = false;
        int choice = Random.Range(1, 3);
        if (choice == 1)
        {
            anim.Play(PunchAnimation1);
        }
        else
        {
            anim.Play(PunchAnimation2);
        }
    }

    public void Contact()
    {
        fightcontroler.ActionTaken(this);
    }
    public void GoDodge()
    {
        CantakeAction = false;
        CanBeHit = false;
        anim.Play(DodgeAnimation);

    }
    public void GoBlock()
    {
        CantakeAction = false;
        isBlocking = true;
        anim.Play(BlockAnimation);
    }
    public void EndBlock()
    {
       // Debug.Log("Close one");
       isBlocking = false;
        CantakeAction = true;
        anim.Play(IdleAnimation);
    }
    public void EndPunch()
    {
      //  Debug.Log("Take that");
        CantakeAction = true;
       // Debug.Log(name + " punchout");
    }
    public void EndDodge()
    {
      //  Debug.Log("Dodged that");
        CantakeAction = true;
        CanBeHit = true;
    }
    public void Dazed()
    {

        CantakeAction = false;
        CanBeHit = true;
        anim.Play(DazedInAnimation);
       // Debug.Log("dazed");
    }

    public void EndDazed()
    {
        CantakeAction = true;
        anim.Play(DazedExitAnimation);
    }
    public void KnockedDown()
    {
        CanBeHit = false;
        CantakeAction = false;
        anim.Play(KnockeddownAnimation);
        isdown = true;
        Debug.Log(name + " KO called");
        PlayMinigame();


    }
    public void GetBackUp()
    {
        Debug.Log(name + " GetBackUp called");
       
        CantakeAction = true;
        isdown = false;
        StartCoroutine(GetUpProtection());
        anim.Play(Getupanimation);
        this.enabled = true;
        StartCoroutine(RecoveryWindow());
    }
    public void PlayMinigame ()
    {
        fightcontroler.minigame.StartMinigame(this);
    }
    public void Hit()
    {
        CantakeAction = true;
        isBlocking = false;
        anim.Play(HitAnimation);
    }
    IEnumerator GetUpProtection()
    {
        justGotUp = true;
        yield return new WaitForSeconds(1f);
        justGotUp = false;
    }
    IEnumerator RecoveryWindow()
    {    yield return new WaitForSeconds(1f);
        CanBeHit = true;
    }
}
