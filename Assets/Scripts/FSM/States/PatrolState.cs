using UnityEngine;

namespace Assets.Scripts.FSM.States
{
    [CreateAssetMenu(fileName = "PatrolState", menuName = "Unity-FSM/States/Patrol", order = 1)]
    public class PatrolState : AbstractFSMState
    {
        //NPCPatrolPoint[] _patrolPoints;
        int _patrolPointIndex;

        public override void OnEnable()
        {
            base.OnEnable();
            _patrolPointIndex = -1;
        }

        public override bool EnterState()
        {
            if (_patrolPointIndex >= 0)
            {
                //attraper et socker les points de patrouilles

            }
            return base.EnterState();
        }

        public override void UpdateState()
        {
            throw new System.NotImplementedException();
        }
    }
}