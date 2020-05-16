using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectPanel : MonoBehaviour
{
    public SoWeapon weaponSelect;
    public Text nameText;
    public Text descriptionText;
    public Image iconImage;

    // Start is called before the first frame update
    void Start()
    {
        SetupPanel();
    }

    public void SetupPanel()
    {
        if (weaponSelect)
        {
            nameText.text = weaponSelect.weaponName;
            descriptionText.text = weaponSelect.weaponDescription;
            iconImage.sprite = weaponSelect.weaponIcon;
        }
    }
    
}
