using UnityEngine;

public class DeployMinesNode : ActionNode
{
    private GameObject _minePrefab;
    private GameObject _tankGameObject;
    private float _mineSpacing = 2f; // Espace entre chaque mine
    private int _minesToDeploy = 3; // Nombre de mines à déployer
    private float _dropDistance = 3f; // Distance derrière le tank pour déposer les mines
    private float _cooldown = 20f; // Temps de recharge en secondes
    private float _lastDeployTime = -20f; // Initialisation pour permettre un déploiement immédiat au début


    protected override void OnStart()
    {
        _minePrefab = blackboard.Get("minePrefab") as GameObject;
        _tankGameObject = blackboard.Get("targetGameObject") as GameObject;
    }

    protected override void OnStop()
    {
        // Rien de spécial à faire à l'arrêt
    }

    protected override State OnUpdate()
    {
        // Vérifier si le temps de recharge est écoulé
        if (Time.time - _lastDeployTime < _cooldown)
        {
            return State.Failure; // Pas encore prêt à déployer de nouvelles mines
        }

        // Direction opposée à la direction vers laquelle le tank regarde
        Vector3 backwardDirection = -_tankGameObject.transform.up;

        // Position initiale pour la première mine directement derrière le tank
        Vector3 deployPosition = _tankGameObject.transform.position + backwardDirection * _dropDistance;

        // Décaler la position de départ pour la première mine à gauche
        deployPosition -= _tankGameObject.transform.right * (_mineSpacing * (_minesToDeploy - 1) / 2);

        // Déployer les mines
        for (int i = 0; i < _minesToDeploy; i++)
        {
            GameObject mine = GameObject.Instantiate(_minePrefab, deployPosition, Quaternion.identity);

            // Décaler la position pour la prochaine mine sur l'axe X
            deployPosition += _tankGameObject.transform.right * _mineSpacing;
        }

        // Mettre à jour le temps du dernier déploiement
        _lastDeployTime = Time.time;

        return State.Success; // Les mines ont été déployées
    }

}
