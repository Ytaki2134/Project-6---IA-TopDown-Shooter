using UnityEngine;

public class ActionFeignedRetreat : ActionNode
{
    private Transform bossTransform;
    private Transform playerTransform;
    private float retreatSpeed;
    private float retreatDuration;
    private float timer;

    protected override void OnStart()
    {
        bossTransform = blackboard.Get<Transform>("bossTransform");
        playerTransform = blackboard.Get<Transform>("playerTransform");
        retreatSpeed = blackboard.Get<float>("retreatSpeed");
        retreatDuration = blackboard.Get<float>("retreatDuration");
        timer = 0f;
    }
    protected override void OnStop()
    {

    }
    protected override State OnUpdate()
    {
        if (timer < retreatDuration)
        {
            // Recule du joueur
            Vector2 direction = (bossTransform.position - playerTransform.position).normalized;
            bossTransform.position += (Vector3)direction * retreatSpeed * Time.deltaTime;
            timer += Time.deltaTime;
            return State.Running;
        }
        else
        {
            // Après la durée, se retourne pour attaquer ou mettre en place une embuscade
            // Ici, vous pouvez déclencher une animation ou un changement d'état pour l'attaque
            return State.Success;
        }
    }
}
