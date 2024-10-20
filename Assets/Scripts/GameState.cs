public enum GameState
{
    PLAYING,
    PAUSED,
    WIN,
    LOSS
}

public static class CurrentGameState
{
    static GameState currentState = GameState.PAUSED;
    public static GameState getGameState()
    {
        return currentState;
    }

    public static void setState(GameState state) 
    {
        currentState = state;
    }
}