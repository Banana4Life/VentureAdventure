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
        protected abstract IEnumerator DoLoop();

        public IEnumerator StartWork()
        {
            this.Complete = false;

            return DoLoop();
        }
    }
}