using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Enemy.Pharaoh
{
    public class StateMachine : Enemy.AICore
    {
        private Enemy.Pharaoh.Actions m_actions;

        [Header("Navigation")]
        public Path m_walkStatePath;
        public Path m_sleepStatePath;

        [Header("Sleep State Parameters")]
        public float m_sleepStateWaypointWaitTime = 5f;
        public float m_sleepStateWaypointDistanceThreshold = 0.2f;

        [Header("Walk State Parameters")]
        public float m_walkStateWaypointWaitTime = 0.2f;
        public float m_walkStateWaypointDistanceThreshold = 0.2f;

        [Header("Speed Parameters")]
        public float m_walkSpeed = 1.5f;
        public float m_chaseSpeed = 6f;

        [Header("Vision Parameters")]
        public float m_walkVisionAngle = 140f;
        public float m_chaseVisionAngle = 200f;
        public float m_deafultVisionRange = 15f;
        public float m_sleepVisionRange = 0f;

        private Blackboard m_pharaohBlackboard;

        private enum SleepSubState
        {
            WalkToSarcophagus,
            InSarcophagus,
            ExitSarcophagus
        }

        private enum ActivitySubState
        {
            WalkToMirrorPlace,
            Distracted,
            Annoyed,
            ExitMirrorPlace
        }

        protected override void Start()
        {
            base.Start();
            m_actions = GetComponent<Enemy.Pharaoh.Actions>();
            m_pharaohBlackboard = (Blackboard)AIBlackboard;

        }

        public override void Decide()
        {
            //temp decide function for testing. will be replaced with a more robust one later.
            if (Controller.m_blackboard.m_canSeePlayer)
            {
                ChangeStateTo(State.Chase);
            }
            else if (m_pharaohBlackboard.m_isInSleepZone)
            {
                ChangeStateTo(State.Sleep);
            }
            else
                ChangeStateTo(State.Walk);

        }

        #region Sleep State
        protected override void RunSleep(Enemy.Controller controller)
        {
            m_actions.PathNavigationCycle(controller, m_sleepStatePath, m_sleepStateWaypointWaitTime, m_sleepStateWaypointDistanceThreshold);
        }
        protected override void EnterSleep(Enemy.Controller controller)
        {
            Agent.ResetPath();
            Agent.SetDestination(m_sleepStatePath.m_waypoints[0].position);
            m_actions.ChangeVisionRange(controller, m_sleepVisionRange);

        }

        protected override void ExitSleep(Enemy.Controller controller)
        {
            m_actions.ChangeVisionRange(controller, m_deafultVisionRange);
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
            m_actions.PathNavigationCycle(controller, m_walkStatePath, m_walkStateWaypointWaitTime, m_walkStateWaypointDistanceThreshold);
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
            m_actions.ChasePlayer(controller);
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