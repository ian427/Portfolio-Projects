using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class How2Play : MonoBehaviour
{
    public GameObject controls;
    public GameObject pcontrols;
    public GameObject taphereCircle;

    private void Start()
    {
        StartCoroutine(H2P());
    }

    IEnumerator H2P()
    {
        yield return new WaitForSeconds(3);
        controls.SetActive(false);
        pcontrols.SetActive(false);
        taphereCircle.SetActive(false);
    }
}
