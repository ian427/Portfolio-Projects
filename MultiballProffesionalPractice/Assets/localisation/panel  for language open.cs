using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject blurOverlay;

    public void OpenPanel()
    {
        blurOverlay.SetActive(true);
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        blurOverlay.SetActive(false);
    }
}

