

abstract public class State <T>
{
	abstract public void Enter (T entity);
	abstract public void Execute (T entity);
	abstract public void Exit (T entity);	
	abstract public void SetNextState(GameManager g, string state);
}
