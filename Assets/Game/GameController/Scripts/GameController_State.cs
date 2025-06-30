using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public partial class GameController
    {
        public GameState CurrentState { get; private set; }
        ObjectPool<Card> objectPool = new();
        [SerializeField] Card cardPrefab;
        [SerializeField] Transform playerSide;
        [SerializeField] Transform dealerSide;
        BetType playerBet = BetType.No_Bet;


        void SetState(GameState newState)
        {
            CurrentState = newState;
            Debug.Log($"Game State: {newState.ToString()}");

            GameEvents.EmitGameStateChangedEvent(newState);
        }

        public void TransitionToState(GameState newState)
        {
            switch (newState)
            {
                case GameState.Initialize:
                    InitializeGame(newState);
                    break;

                case GameState.Start:
                    StartGame(newState); 
                    break;

                case GameState.Show_Dealer_Card:
                    ShowDealerCard(newState); 
                    break;

                case GameState.Await_Player_Bet:
                    AwaitBet(newState); 
                    break;

                case GameState.Show_Player_Card:
                    //DealToPlayer(); 
                    break;

                case GameState.Resolve_Bet_Result:
                    //ResolveBet(); 
                    break;

                case GameState.Restart:
                    //Restart(); 
                    break;
            }
        }

        void AwaitBet(GameState newState)
        {
            SetState(newState);
        }

        void ShowDealerCard(GameState newState)
        {
            var cardProfile = deck.DrawCard();
            var card = objectPool.Get();
           
            card.Setup(
                cardProfile, 
                deckConfig.BackSprite, 
                isFaceUp: false);

            card.SetParent(deck.transform.parent);
            card.SetPosition(deck.GetPosition());
            card.SetScale(visualSettings.CardScale);

            card.View.Move(
                dealerSide.localPosition, 
                animate: true, 
                onComplete: () =>
                {
                    card.Flip(animate: true);
                    SetState(newState);
                    TransitionToState(GameState.Await_Player_Bet);
                });
        }

        void InitializeGame(GameState newState)
        {
            objectPool.Initialize(cardPrefab, size: 2);

            var gameRuleResult = ApplyGameRules(gameConfig);

            deck.Initialize(
                gameConfig.Seed,
                gameConfig.WeightScaleFactor,
                gameRuleResult.CardChances,
                deckConfig);

            SetState(newState);

            TransitionToState(GameState.Start);
        }

        void StartGame(GameState newState)
        {
            deck.ResetDeck();

            SetState(newState);

            TransitionToState(GameState.Show_Dealer_Card);
        }
    }
}
