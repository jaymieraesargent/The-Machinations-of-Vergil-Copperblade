using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    
    public Quirk selectedQuirk;
    public SoWeapon selectedWeapon;
    public string gameMode;
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void SetQuirk(Quirk selectQuirk)
    {
        selectedQuirk = selectQuirk;
    }

    public void SetWeapon(SoWeapon selectWeapon)
    {
        selectedWeapon = selectWeapon;        
    }


}
