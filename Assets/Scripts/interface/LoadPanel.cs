using UnityEngine;
public class LoadPanel : MonoBehaviour
{
    [SerializeField] GameObject Workshop;
    [SerializeField] GameObject Shop;
    [SerializeField] GameObject Guild;

    public void showPanel(string nameType) {
        switch (nameType) {
            case "workshop":
                Workshop.SetActive(true); break;
            case "shop":
                Shop.SetActive(true); break;
            case "guild":
                Guild.SetActive(true); break;
        }
    }

    public void hidePanel(string nameType) {
        switch (nameType) {
            case "workshop":
                Workshop.SetActive(false); break;
            case "shop":
                Shop.SetActive(false); break;
            case "guild":
                Guild.SetActive(false); break;
        }
    }

    public void hideAllPanel() {
        Workshop.SetActive(false);
        Shop.SetActive(false);
        Guild.SetActive(false);
    }
}
