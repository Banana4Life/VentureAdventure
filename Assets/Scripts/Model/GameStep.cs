using System.Collections;

namespace Model
{
    public abstract class GameStep 
    {
        protected GameStep(GameState state)
        {
            State = state;
        }

        protected GameState State;
        public bool Complete { get; protected set; }
        public abstract IEnumerator DoLoop();
    }
}