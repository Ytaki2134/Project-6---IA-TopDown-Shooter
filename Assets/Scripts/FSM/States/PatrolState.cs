using UnityEngine;
using Assets.Scripts.NPCCode;

namespace Assets.Scripts.FSM
{
    [CreateAssetMenu(fileName = "PatrolState", menuName = "Unity-FSM/States/Patrol", order = 2)]
    public class PatrolState : AbstractFSMState
    {
        PatrolPoints[] _patrolPoints;

        private int _currentPatrolIndex;

        private Movement m_movement;
        private float _distance;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.PATROL;
            _currentPatrolIndex = -1;
        }

        public override bool EnterState(FiniteStateMachine context)
        {
            EnteredState = false;
            if (base.EnterState(context))
            {
                Debug.Log("ENTERED PATROL STATE");

                //attraper et socker les points de patrouilles
                _patrolPoints = _npc.PatrolPoints;

                if (_patrolPoints == null || _patrolPoints.Length == 0)
                {
                    Debug.Log("PatrolState: failed to grab patrol points from the npc");
                }
                else
                {
                    if (_currentPatrolIndex < 0)
                    {
                        _currentPatrolIndex = UnityEngine.Random.Range(0, _patrolPoints.Length);
                    }
                    else
                    {
                        _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Length;
                    }

                    ChangePatrolPoint();
                    EnteredState = true;
                }
            }

            return EnteredState;
        }


        public override void UpdateState(FiniteStateMachine context)
        {
            if (EnteredState)
            {
                //Debug.Log("UPDATING PATROL STATE");
                _distance = Vector2.Distance(_npc.transform.position, _patrolPoints[_currentPatrolIndex].transform.position);

                if (context.TankStatistics.Health <= 0)
                {
                    _fsm.EnterState(FSMStateType.DEAD);
                }
                else
                {
                    switch (context.Index)
                    {
                        case 1:
                        case 2:
                        case 5:
                            if (_distance <= 15f)
                            {
                                _fsm.EnterState(FSMStateType.CHASE);
                            }
                            break;

                        case 3:
                            break;

                        case 4:
                            break;
                    }
                    if (m_movement != null)
                    {
                        m_movement.SetCurrentMovement(((Vector2)_patrolPoints[_currentPatrolIndex].transform.position - (Vector2)_npc.transform.position).normalized);
                        m_movement.Move();
                    }
                }
            }
        }

        private void ChangePatrolPoint()
        {
            _currentPatrolIndex++;

            if (_currentPatrolIndex >= _patrolPoints.Length)
            {
                _currentPatrolIndex = 0;
            }
        }
    }
}