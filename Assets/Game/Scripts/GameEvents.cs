using System;

namespace Game
{
    public class GameEvents
    {
        public static event Action<GameState> OnGameStateChangedEvent;
        public static event Action<BetType> OnBetPlacedEvent;


        public static void EmitGameStateChangedEvent(GameState gameState)
        {
            OnGameStateChangedEvent?.Invoke(gameState);
        }
        public static void EmitBetPlacedEvent(BetType betType)
        {
            OnBetPlacedEvent?.Invoke(betType);
        }
    }
}
