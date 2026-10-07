using TMPro;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public string abilityName;
    public AbilityType abilityType;
    public int hitChanceMin, hitChanceMax;
    public int damageRangeMin, damageRangeMax;
    public int shieldAmount;

    private Gamemode gamemodeRef;


    private void Start()
    {
        gamemodeRef = FindAnyObjectByType<Gamemode>();
    }


    public void AbilitySelected()
    {
        //Have reference to the gamemode and call new ability
        gamemodeRef.AddActionToQueue(this); 
    }


}
