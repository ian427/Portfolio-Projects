using UnityEngine;
using static Spawnpointhandler;

public class ChangeCorner : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Color Default = Color.white;
    [SerializeField] private Color Desert = Color.white;
    [SerializeField] private Color Arctic = Color.white;
    [SerializeField] private Color Jungle = Color.white;
    [SerializeField] private Color Volcano = Color.white;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void SetColorDefault (){sr.color = Default;}
    public void SetColorDesert() {sr.color = Desert;}
    public void SetColorArctic() {sr.color = Arctic;}
    public void SetColorJungle() {sr.color = Jungle;}
    public void SetColorVolcano() {sr.color = Volcano;}


}
