using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGrow : MonoBehaviour
{
    private GameControler controler;
    [SerializeField] private float MonsterSize = 1f;
    private FurnitureData furnitureData;
    // Start is called before the first frame update
    void Start()
    {
        controler = GameObject.Find("Controller").GetComponent<GameControler>();
        
    }
    public void UpdateMonsterSize()
    {
        MonsterSize = controler.GetMonsterScale();
        Debug.Log("size"+MonsterSize);
        this.transform.localScale = new Vector3(MonsterSize, MonsterSize, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
