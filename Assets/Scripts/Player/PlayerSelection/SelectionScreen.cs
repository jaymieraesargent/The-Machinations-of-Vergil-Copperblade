using UnityEngine;

public class SelectionScreen : MonoBehaviour
{
    // List of avalable quirks of type scriptable object
    [SerializeField] 
    private Quirk[] availableQuirks;
    // List of avalable weapons of type scriptable object
    [SerializeField]
    private SoWeapon[] availableWeapons;

    public GameObject quirkPanelPrefab;
    public GameObject quirksHolder;

    public GameObject weaponPanelPrefab;
    public GameObject weaponsHolder;

    public PlayerSelection playerSelection;

    void Start()
    {
        SetupPanelSelection();
    }
    
    public void SetupPanelSelection()
    {
        GameObject tempQuirkPanel;
        foreach (Quirk quirk in availableQuirks)
        {
            tempQuirkPanel = Instantiate(quirkPanelPrefab);
            tempQuirkPanel.GetComponent<QuirkSelectPanel>().quirkSelect = quirk;
            tempQuirkPanel.transform.SetParent(quirksHolder.transform, false);
            tempQuirkPanel.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(delegate { playerSelection.SetQuirk(quirk); });
        }


        GameObject tempWeaponPanel;
        foreach (SoWeapon weapon in availableWeapons)
        {
            tempWeaponPanel = Instantiate(weaponPanelPrefab);
            tempWeaponPanel.GetComponent<WeaponSelectPanel>().weaponSelect = weapon;
            tempWeaponPanel.transform.SetParent(weaponsHolder.transform, false);
            tempWeaponPanel.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(delegate { playerSelection.SetWeapon(weapon); });
        }
    }

}
