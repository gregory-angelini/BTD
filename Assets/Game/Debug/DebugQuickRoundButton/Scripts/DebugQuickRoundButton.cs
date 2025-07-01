using Common;
using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameDebug
{
    public class DebugQuickRoundButton : MonoBehaviour
    {
        Canvas canvas;
        Deck deck;
        GameController gameController;
        Randomizer randomizer = new Randomizer();


        void Start()
        {
            deck = FindObjectOfType<Deck>();

            if (deck == null)
            {
                Debug.LogWarning("Deck not found on scene!");
            }

            canvas = FindObjectOfType<Canvas>();

            if (canvas == null)
            {
                Debug.LogError("Canvas not found!");
            }

            gameController = FindObjectOfType<GameController>();

            if (gameController == null)
            {
                Debug.LogError("Game Controller not found!");
            }
        }

        public void OnClick()
        {
            GameEvents.EmitStartEvent();
            GameEvents.EmitTryAgainEvent();
        
            switch (gameController.CurrentState)
            {
                case GameState.Await_Player_Bet:  
                    bool higherOrLower = randomizer.RandomBool();
                    GameEvents.EmitBetPlacedEvent(higherOrLower ? BetType.Higher : BetType.Lower);
                    break;
            }
        }
    }
}
