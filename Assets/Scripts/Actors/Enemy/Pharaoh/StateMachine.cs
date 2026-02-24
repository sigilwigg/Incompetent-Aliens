using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Enemy.Pharaoh
{
    public class StateMachine : Enemy.AICore
    {
        public Enemy.Pharaoh.Actions m_actions;

        [Header("Speed Parameters")]
        public float m_walkSpeed = 1.5f;
        public float m_chaseSpeed = 6f;

        [Header("Vision Parameters")]
        public float m_walkVisionAngle = 140f;
        public float m_chaseVisionAngle = 200;

        [Header("References")]
        public Transform[] m_players;

        protected override void Start()
        {
            base.Start();
            m_actions = GetComponent<Enemy.Pharaoh.Actions>();

            //----- If array isn't set in inspector, try to find all Player objects in the scene -----
            if (m_players == null || m_players.Length == 0)
            {
                GameObject[] found = GameObject.FindGameObjectsWithTag("Player");
                if (found.Length == 0)
                {
                    Debug.LogError("Player not found in scene. Make sure there is a GameObject with the tag 'Player' in the scene.");
                    return;
                }

                m_players = new Transform[found.Length];
                for (int i = 0; i < found.Length; i++)
                {
                    m_players[i] = found[i].transform;
                }

                return;
            }
        }

        public override void Decide()
        {
            //temp decide function for testing. will be replaced with a more robust one later.
            if(!m_controller.m_blackboard.m_canSeePlayer)
            {
                ChangeStateTo(State.Walk);
            }
            else
                ChangeStateTo(State.Chase);

            //if pharaoh in acvitity zone and walk state.
            //exit walk state.
            //enter activity state, do activty state stuff.

            //if player seen and not in sleep state or distract.
            //exit current state.
            //enter chase state.

        }

        #region Sleep State
        protected override void RunSleep(Enemy.Controller controller)
        {

        }
        protected override void EnterSleep(Enemy.Controller controller)
        {

        }

        protected override void ExitSleep(Enemy.Controller controller)
        {

        }
        #endregion

        #region Idle State
        protected override void RunIdle(Enemy.Controller controller)
        {

        }
        protected override void EnterIdle(Enemy.Controller controller)
        {

        }

        protected override void ExitIdle(Enemy.Controller controller)
        {

        }
        #endregion

        #region Walk State
        protected override void RunWalk(Enemy.Controller controller)
        {
            m_actions.WalkCycle(controller);
        }
        protected override void EnterWalk(Enemy.Controller controller)
        {
            m_actions.ChangeSpeed(controller, m_walkSpeed);
            m_actions.ChangeVisionAngle(controller, m_walkVisionAngle);
        }

        protected override void ExitWalk(Enemy.Controller controller)
        {

        }
        #endregion

        #region Chase State
        protected override void RunChase(Enemy.Controller controller)
        {
            m_actions.ChasePlayer(controller, m_players);
        }
        protected override void EnterChase(Enemy.Controller controller)
        {
            m_actions.ChangeSpeed(controller, m_chaseSpeed);
            m_actions.ChangeVisionAngle(controller, m_chaseVisionAngle);
        }

        protected override void ExitChase(Enemy.Controller controller)
        {
        }
        #endregion

        #region Activity State
        protected override void RunActivity(Enemy.Controller controller)
        {

        }
        protected override void EnterActivity(Enemy.Controller controller)
        {

        }

        protected override void ExitActivity(Enemy.Controller controller)
        {

        }
        #endregion
    }
}