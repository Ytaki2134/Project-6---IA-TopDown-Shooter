using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool IsShieldActive { get;  set; }
    public float ShieldValue { get;  set; }

    public void ActivateShield()
    {
        IsShieldActive = true;
        // Vous pouvez ajouter ici du code pour visualiser le bouclier, par exemple en changeant le matériau ou en activant un effet visuel.
    }

    public void DeactivateShield()
    {
        IsShieldActive = false;
        // Et ici, code pour désactiver la visualisation du bouclier.
    }
}
