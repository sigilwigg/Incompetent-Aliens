using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Enemy.Pharaoh
{
    public class StateMachine : Enemy.AICore
    {
        public Enemy.Pharaoh.Actions m_actions;

        protected override void Start()
        {
            base.Start();
            m_actions = GetComponent<Enemy.Pharaoh.Actions>();
        }

        public override void Decide()
        {
            //temp decide function for testing. will be replaced with a more robust one later.
            if(!m_controller.m_blackboard.m_canSeePlayer)
            {
                ChangeStateTo(State.Walk);
            }
            else
                ChangeStateTo(State.Idle);

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

        }

        protected override void ExitWalk(Enemy.Controller controller)
        {
            
        }
        #endregion

        #region Chase State
        protected override void RunChase(Enemy.Controller controller)
        {

        }
        protected override void EnterChase(Enemy.Controller controller)
        {

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