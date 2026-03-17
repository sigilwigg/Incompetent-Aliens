using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Enemy.Pharaoh
{
    public class StateMachine : Enemy.AICore
    {
        private Enemy.Pharaoh.Actions m_actions;

        [Header("Throwing Paramaters")]
        public float m_throwCooldown = 5f;
        public float m_throwForce = 0.5f;

        [Header("Speed Parameters")]
        public float m_walkSpeed = 1.5f;
        public float m_chaseSpeed = 6f;

        [Header("Vision Range Parameters")]
        public float m_deafultVisionRange = 15f;
        public float m_defaultCatchVisionRange = 1.5f;

        [Header("Vision Angle Parameters")]
        public float m_defaultVisionAngle = 140f;
        public float m_chaseVisionAngle = 200f;

        [Header("Paths")]
        public Path m_walkStatePath;
        public Path m_sleepStatePath;
        public Path m_activityStatePath;

        [Header("Pathing Parameters")]
        public float m_waypointDistanceTheshold = 0.2f;

        //----- Sleep State Pathing Parameters -----
        public float m_sleepStateWaypointWaitTime = 5f;
        private int m_sleepStateWaypointIndex = 0;

        //----- Walk State Pathing Parameters -----
        public float m_walkStateWaypointWaitTime = 0.2f;
        private int m_walkStateWaypointIndex = 0;

        //---- Activity State Pathing Parameters -----
        public float m_activityStateWaypointWaitTime = 0.2f;
        private int m_activityStateWaypointIndex = 0;

        //----- references -----
        private Blackboard m_pharaohBlackboard;

        protected override void Start()
        {
            base.Start();
            m_actions = GetComponent<Actions>();
            m_pharaohBlackboard = GetComponent<Blackboard>();

        }

        public override void Decide()
        {
            //----- check if player is caught -----
            if (m_pharaohBlackboard.m_canCatchPlayer && !m_actions.m_isThrowing)
            {
                StartCoroutine(m_actions.ThrowPlayerCoroutine(m_throwCooldown, m_throwForce));
            }

            //----- decide state -----
            if (Controller.m_blackboard.m_canSeePlayer)
            {
                ChangeStateTo(State.Chase);
            }
            else if (m_pharaohBlackboard.m_isInSleepZone)
            {
                ChangeStateTo(State.Sleep);
            }
            else if (m_pharaohBlackboard.m_isPharaohInMirrorZone)
            {
                ChangeStateTo(State.Activity);
            }
            else
                ChangeStateTo(State.Walk);

        }

        #region Sleep State
        protected override void RunSleep(Enemy.Controller controller)
        {
            m_sleepStateWaypointIndex = m_actions.PathNavigationCycle
                (m_sleepStatePath, m_sleepStateWaypointWaitTime, m_waypointDistanceTheshold, m_sleepStateWaypointIndex);
        }
        protected override void EnterSleep(Enemy.Controller controller)
        {
            Agent.ResetPath();
            Agent.SetDestination(m_sleepStatePath.m_waypoints[0].position);
            m_actions.ChangeVisionRange(0f);
            m_actions.ChangeCatchRange(0f);
        }

        protected override void ExitSleep(Enemy.Controller controller)
        {
            m_actions.ChangeVisionRange(m_deafultVisionRange);
            m_actions.ChangeCatchRange(m_defaultCatchVisionRange);
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
            m_walkStateWaypointIndex = m_actions.PathNavigationCycle
                (m_walkStatePath, m_walkStateWaypointWaitTime, m_waypointDistanceTheshold, m_walkStateWaypointIndex);
        }
        protected override void EnterWalk(Enemy.Controller controller)
        {
            m_actions.ChangeSpeed(m_walkSpeed);
            m_actions.ChangeVisionAngle(m_defaultVisionAngle);
        }

        protected override void ExitWalk(Enemy.Controller controller)
        {

        }
        #endregion

        #region Chase State
        protected override void RunChase(Enemy.Controller controller)
        {
            m_actions.ChasePlayer();
        }
        protected override void EnterChase(Enemy.Controller controller)
        {
            m_actions.ChangeSpeed(m_chaseSpeed);
            m_actions.ChangeVisionAngle(m_chaseVisionAngle);
        }

        protected override void ExitChase(Enemy.Controller controller)
        {
        }
        #endregion

        #region Activity State
        protected override void RunActivity(Enemy.Controller controller)
        {
            m_activityStateWaypointIndex = m_actions.PathNavigationCycle
                (m_activityStatePath, m_activityStateWaypointWaitTime, m_waypointDistanceTheshold, m_activityStateWaypointIndex);

            if (m_pharaohBlackboard.m_isMirrorHeldByPlayers)
            {
                m_pharaohBlackboard.m_isDistracted = true;

                m_activityStateWaypointWaitTime = Mathf.Infinity;

                m_actions.ChangeVisionRange(0f);
                m_actions.ChangeCatchRange(0f);

                //play distracted animation
            }
            else
            {
                m_pharaohBlackboard.m_isDistracted = false;

                m_activityStateWaypointWaitTime = 0.2f;

                m_actions.ChangeVisionRange(m_deafultVisionRange);
                m_actions.ChangeCatchRange(m_defaultCatchVisionRange);

                // play mad at mirror animation
            }
        }
        protected override void EnterActivity(Enemy.Controller controller)
        {
            Agent.ResetPath();
            Agent.SetDestination(m_activityStatePath.m_waypoints[0].position);
        }

        protected override void ExitActivity(Enemy.Controller controller)
        {

        }
        #endregion

    }
}