using UnityEngine;

namespace Enemy
{
    public class AICore : MonoBehaviour
    {
        private Enemy.Controller m_controller;

        public State m_currentState;
        public enum State
        {
            Sleep,
            Idle,
            Walk,
            Chase,
            Activity
        }

        private void Start()
        {
            m_controller = GetComponentInParent<Enemy.Controller>();
        }

        public void Evaluate()
        {
            Run(m_currentState);
        }

        void ChangeStateTo(State nextState)
        {
            // ----- stop from interrupting its own state -----
            if (m_currentState == nextState) return;

            // ----- continue to next state -----
            Exit(m_currentState);
            m_currentState = nextState;
            Enter(m_currentState);
        }

        void Exit(State state)
        {
            // ----- exit out of current state -----
            // used for clean-up in case there is anything that needs to get reset.

            switch (state)
            {
                case State.Sleep:
                    ExitSleep(m_controller);
                    break;
                case State.Idle:
                    ExitIdle(m_controller);
                    break;
                case State.Walk:
                    ExitWalk(m_controller);
                    break;
                case State.Chase:
                    ExitChase(m_controller);
                    break;
                case State.Activity:
                    ExitActivity(m_controller);
                    break;
                default:
                    // ----- error -----
                    Debug.Log("state for enter not found");
                    break;
            }
        }

        void Enter(State state)
        {
            // ----- enter state -----
            // used fro preperation into a state, in case the state needs any changes before runnign on loop

            switch (state)
            {
                case State.Sleep:
                    EnterSleep(m_controller);
                    break;
                case State.Idle:
                    EnterIdle(m_controller);
                    break;
                case State.Walk:
                    EnterWalk(m_controller);
                    break;
                case State.Chase:
                    EnterChase(m_controller);
                    break;
                case State.Activity:
                    EnterActivity(m_controller);
                    break;
                default:
                    // ----- error -----
                    Debug.Log("state for enter not found");
                    break;
            }
        }

        void Run(State state)
        {
            switch (state)
            {
                case State.Sleep:
                    RunSleep(m_controller);
                    break;
                case State.Idle:
                    RunIdle(m_controller);
                    break;
                case State.Walk:
                    RunWalk(m_controller);
                    break;
                case State.Chase:
                    RunChase(m_controller);
                    break;
                case State.Activity:
                    RunActivity(m_controller);
                    break;
                default:
                    // ----- error -----
                    Debug.Log("state for enter not found");
                    break;
            }
        }

        public virtual void Decide()
        {
            // template function overridden by our subclasses
        }

        protected virtual void RunSleep(Enemy.Controller controller)
        {
            // template function overridden by our subclasses
        }
        protected virtual void EnterSleep(Enemy.Controller controller)
        {
            // template function overridden by our subclasses
        }

        protected virtual void ExitSleep(Enemy.Controller controller)
        {
            // template function overridden by our subclasses
        }

        protected virtual void RunIdle(Enemy.Controller controller)
        {
            // template function overridden by our subclasses
        }
        protected virtual void EnterIdle(Enemy.Controller controller)
        {
            // template function overridden by our subclasses
        }

        protected virtual void ExitIdle(Enemy.Controller controller)
        {
            // template function overridden by our subclasses
        }

        protected virtual void RunWalk(Enemy.Controller controller)
        {
            // template function overridden by our subclasses
        }
        protected virtual void EnterWalk(Enemy.Controller controller)
        {
            // template function overridden by our subclasses
        }

        protected virtual void ExitWalk(Enemy.Controller controller)
        {
            // template function overridden by our subclasses
        }

        protected virtual void RunChase(Enemy.Controller controller)
        {
            // template function overridden by our subclasses
        }
        protected virtual void EnterChase(Enemy.Controller controller)
        {
            // template function overridden by our subclasses
        }

        protected virtual void ExitChase(Enemy.Controller controller)
        {
            // template function overridden by our subclasses
        }

        protected virtual void RunActivity(Enemy.Controller controller)
        {
            // template function overridden by our subclasses
        }
        protected virtual void EnterActivity(Enemy.Controller controller)
        {
            // template function overridden by our subclasses
        }

        protected virtual void ExitActivity(Enemy.Controller controller)
        {
            // template function overridden by our subclasses
        }
    }
}
