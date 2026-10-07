using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TexturesList;

public class PannelManager : MonoBehaviour
{
    public int ID;
    [SerializeField] private GameObject Planet;
    private GameObject Lock;
    bool isunlocked = false;
    [SerializeField] private ShopManager Shop;
    public TMP_Text Selected;
    [SerializeField] private int GCrystalcost = 0, RCrystalcost = 0, CCrystalcost = 0, GoldCrystalcost = 0;//cost
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Shop = GameObject.Find("ShopManager").GetComponent<ShopManager>();
        
        var(sp, id,lck) = Shop.GetPlannetData(ID);
        ID = id;
        Planet.GetComponent<Image>().sprite = sp;
        isunlocked = lck;
        if (isunlocked)
        {
            Lock.SetActive(false);
        }
    }
    
    public void Clicked()//preview
    {
        
            Shop.SetPreviousFalse();
            Selected.enabled = true;
            
            Shop.Equip(ID);
        
    }
    // Update is called once per frame
    public void Buy()
    {
        bool CanBuy = true;
        if ((Shop.GCrystal < GCrystalcost) && (CanBuy)) { CanBuy = false; }
        if ((Shop.RCrystal < RCrystalcost) && (CanBuy)) { CanBuy = false; }
        if ((Shop.CCrystal < CCrystalcost) && (CanBuy)) { CanBuy = false; }
        if ((Shop.GoldCrystal < GoldCrystalcost) && (CanBuy)) { CanBuy = false; }
        if (CanBuy)
        {
            Shop.ChangeCurrency(-GCrystalcost, -RCrystalcost, -CCrystalcost, -GoldCrystalcost);
        }
        Lock.SetActive(false);
        isunlocked = true;
        Shop.tex.UnlockNewTexture(ID);
        Shop.SaveBallsettings();
    }


}
    
