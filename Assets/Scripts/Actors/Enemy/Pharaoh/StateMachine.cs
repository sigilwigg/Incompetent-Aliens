using UnityEngine;

namespace Enemy.Pharaoh
{
    public class StateMachine : Enemy.AICore
    {
        public override void Decide()
        {
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