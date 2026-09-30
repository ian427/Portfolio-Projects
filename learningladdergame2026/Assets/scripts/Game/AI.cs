using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] private FightController controller;
    [SerializeField] private PlayerController Player;
    [SerializeField] private PlayerController Bot;
    [SerializeField] private int PercentChanceMistakemadepunching = 50;
    [SerializeField] private int ChanceToBlock = 50;
    [SerializeField] private int Minigamepresspattenrepeatetime = 100;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BotClock());
    }

    // Update is called once per frame
    void UpdateBot()
    {
        if (Bot.isdown)
        {
            bool pathchosen = false;
           
                if (!pathchosen)
                {
                    pathchosen = true;
                    int choice = Random.Range(1, 4);
                    if (choice == 1)
                    {
                        StartCoroutine(MiniGamePattern1(pathchosen));
                    }
                    else if (choice == 2)
                    {
                        StartCoroutine(MiniGamePattern2(pathchosen));
                    }
                    else if (choice == 3)
                    {
                        StartCoroutine(MiniGamePattern3(pathchosen));
                    }
                    else
                    {
                        StartCoroutine(MiniGamePattern4(pathchosen));
                    }

                }

        }
            controller.minigame.PressSpace();

        
        if (Player.CanBeHit)//try punch
        {
            bool donepunch = false;
            if (!Player.isBlocking)//perfect strike
            {
                int choice = Random.Range(1, 100);
                if (choice < PercentChanceMistakemadepunching)
                {
                    Bot.GoPunch();
                    donepunch = true;
                }

            }
            else if ((Player.isBlocking) && (!donepunch))//punch block
            {
                int choice = Random.Range(1, 100);
                if (choice < PercentChanceMistakemadepunching)
                {
                    Bot.GoPunch();
                }
            }
        }
        else if ((!Player.isBlocking) && (!Player.CantakeAction))
        {
            int i = Random.Range(1, 100);
            if (i > ChanceToBlock)
            {
                int choice = Random.Range(1, 2);
                if (choice == 1)
                {
                    Bot.GoBlock();
                }
                else
                {
                    Bot.GoDodge();
                }


            }


        }
        StartCoroutine(BotClock());
    }
    IEnumerator MiniGamePattern1(bool path)
    {

        for (int i = 0; i < Minigamepresspattenrepeatetime; i++)
        {
            if (Bot.isdown)
            {
                yield return new WaitForSeconds(0.1f);
                controller.minigame.PressSpace();
            }
        }
        path = false;
    }
    IEnumerator MiniGamePattern2(bool path)
    {

        for (int i = 0; i < Minigamepresspattenrepeatetime; i++)
        {
            if (Bot.isdown)
            {
                yield return new WaitForSeconds(0.2f);
                controller.minigame.PressSpace();
            }
        }
        path = false;
    }
    IEnumerator MiniGamePattern3(bool path)
    {
        if (Bot.isdown)
        {
            for (int i = 0; i < Minigamepresspattenrepeatetime; i++)
            {
                if (Bot.isdown)
                {
                    yield return new WaitForSeconds(0.4f);
                    controller.minigame.PressSpace();
                }
            }
            path = false;
        }
    }
    IEnumerator MiniGamePattern4(bool path)
    {

        for (int i = 0; i < Minigamepresspattenrepeatetime; i++)
        {
            if (Bot.isdown)
            {
                yield return new WaitForSeconds(0.6f);
                controller.minigame.PressSpace();
            }
        }
        path = false;
    }
    IEnumerator BotClock()
    {
        yield return new WaitForSeconds(1f);
        UpdateBot();
    }
}
