using UnityEngine;

public class EnableShieldNode : ActionNode
{
    private Shield _shieldComponent;

    protected override void OnStart()
    {
        // R�cup�ration du composant Shield du GameObject
        GameObject tankGameObject = blackboard.Get("targetGameObject") as GameObject;
        _shieldComponent = tankGameObject.GetComponent<Shield>();

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
         if(_shieldComponent.ShieldValue <= 0)
         {
             _shieldComponent.DeactivateShield();
             return State.Failure;
         }

        return State.Success;
    }
}
