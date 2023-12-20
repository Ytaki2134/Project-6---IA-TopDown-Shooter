using UnityEngine;

public class DeployMinesNode : ActionNode
{

    protected override void OnStop()
    {
        // Rien de sp�cial � faire � l'arr�t
    }
    private GameObject _minePrefab;
    private GameObject _tankGameObject;
    private float _mineSpacing = 2f; // Espace entre chaque mine
    private int _minesToDeploy = 3; // Nombre de mines � d�ployer
    private float _dropDistance = 3f; // Distance derri�re le tank pour d�poser les mines
    private float _cooldown = 20f; // Temps de recharge en secondes
    private float _lastDeployTime = -20f; // Initialisation pour permettre un d�ploiement imm�diat au d�but


    protected override void OnStart()
    {
        _minePrefab = blackboard.Get("minePrefab") as GameObject;
        _tankGameObject = blackboard.Get("targetGameObject") as GameObject;
    }

    protected override State OnUpdate()
    {
        // V�rifier si le temps de recharge est �coul�
        if (Time.time - _lastDeployTime < _cooldown)
        {
            return State.Failure; // Pas encore pr�t � d�ployer de nouvelles mines
        }

        // Direction oppos�e � la direction vers laquelle le tank regarde
        Vector3 backwardDirection = -_tankGameObject.transform.up;

        // Position initiale pour la premi�re mine directement derri�re le tank
        Vector3 deployPosition = _tankGameObject.transform.position + backwardDirection * _dropDistance;

        // D�caler la position de d�part pour la premi�re mine � gauche
        deployPosition -= _tankGameObject.transform.right * (_mineSpacing * (_minesToDeploy - 1) / 2);

        // D�ployer les mines
        for (int i = 0; i < _minesToDeploy; i++)
        {
            GameObject mine = GameObject.Instantiate(_minePrefab, deployPosition, Quaternion.identity);

            // D�caler la position pour la prochaine mine sur l'axe X
            deployPosition += _tankGameObject.transform.right * _mineSpacing;
        }

        // Mettre � jour le temps du dernier d�ploiement
        _lastDeployTime = Time.time;

        return State.Success; // Les mines ont �t� d�ploy�es
    }

}
