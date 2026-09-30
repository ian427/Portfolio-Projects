using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Chest2 : MonoBehaviour
{

    public Collider2D Collider;
    private bool isTriggerEnabled = false;
    public GameObject Enviroment;
    public GameObject _Chest;
    public Animator animator;
    public Hunt2 hunt2;

    //VFX
    public ParticleSystem openChestVFX;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("ChestOpen", false);
    }

    // Update is called once per frame
    void Update()
    {

        chestFound();
    
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            openChestVFX.Play();
            animator.SetBool("ChestOpen", true);
            hunt2.ClickOnChest();

            StartCoroutine(disableChest());
        }
    }

    public void chestFound()
    {

        if (Enviroment.transform.position.y == _Chest.transform.position.y)
        {

            Collider.isTrigger = false;


        }
        else
        {
            Collider.isTrigger = true;
        }


    }

    private IEnumerator disableChest()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}

