using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EnableShieldNode : ActionNode
{
    private Shield _shieldComponent;

    protected override void OnStart()
    {
        // R�cup�ration du composant Shield du GameObject
       
        _shieldComponent = blackboard.Get("shield") as Shield;

        if (_shieldComponent == null)
        {
            Debug.LogError("Shield component not found on the tank game object.");
            return;
        }

        // Activation du bouclier
        _shieldComponent.ActivateShield();
        Debug.Log("Shield enabled.");
    }

    protected override void OnStop()
    {
        // Optionnel: D�sactivation du bouclier si n�cessaire � l'arr�t
    }

    protected override State OnUpdate()
    {
        // V�rification si le bouclier est toujours actif
        if (_shieldComponent != null && _shieldComponent.IsShieldActive)
        {
            return State.Success; // Le bouclier est toujours actif
        }

        // Si le bouclier n'est pas actif pour une raison quelconque, on retourne Failure
        return State.Failure;
    }
}
