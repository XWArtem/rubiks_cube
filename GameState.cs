public enum GameStates
{
    none,
    isSolving,
    solved
}

public class GameState
{
    private GameStates currentState = GameStates.none;

    public GameStates ChangeGameState(GameStates gameState)
    {
        if (gameState == GameStates.isSolving)
        {
            currentState = gameState;
        }
        else if(gameState == GameStates.solved)
        {
            currentState = gameState;
            // go to UI
        }
        else
        {
            currentState = GameStates.none;
        }
        return currentState;
    }
}
