using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    public List<GameObject> abilityButtons;

    
    public void AppendAbility (Ability ability)
    {

    }



    public void AddAbility(GameObject abilityToAdd)
    {
        abilityButtons.Add(abilityToAdd);
    }
}
