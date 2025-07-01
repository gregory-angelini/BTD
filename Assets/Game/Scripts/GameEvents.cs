using System;

namespace Game
{
    public class GameEvents
    {
        public static event Action<GameState> OnGameStateChangedEvent;
        public static event Action<BetType> OnBetPlacedEvent;
        public static event Action OnTryAgainEvent;
        public static event Action OnStartEvent;
        public static event Action OnRestartEvent;
        public static event Action<int> OnDeckChangedEvent;


        public static void EmitDeckChangedEvent(int newSize)
        {
            OnDeckChangedEvent?.Invoke(newSize);
        }

        public static void EmitStartEvent()
        {
            OnStartEvent?.Invoke();
        }

        public static void EmitRestartEvent()
        {
            OnRestartEvent?.Invoke();
        }

        public static void EmitTryAgainEvent()
        {
            OnTryAgainEvent?.Invoke();
        }

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
