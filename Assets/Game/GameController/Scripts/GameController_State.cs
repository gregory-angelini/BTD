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
        GameRuleResult gameRuleResult;
        ObjectPool<Card> objectPool = new();
        [SerializeField] Card cardPrefab;
        [SerializeField] Transform playerSide;
        [SerializeField] Transform dealerSide;


        public void TransitionToState(GameState newState)
        {
            CurrentState = newState;

            switch (newState)
            {
                case GameState.Initialize:
                    InitializeGame();
                    break;

                case GameState.Start:
                    StartGame(); 
                    break;

                case GameState.Show_Dealer_Card:
                    ShowDealerCard(); 
                    break;

                case GameState.Await_Player_Bet:
                    //AwaitBet(); 
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

        void ShowDealerCard()
        {
            var cardProfile = deck.DrawCard();
            var card = objectPool.Get();
           
            card.Setup(cardProfile);
            card.SetParent(deck.transform.parent);
            card.SetPosition(deck.GetPosition());
            card.SetScale(0.3f);
            card.Move(dealerSide.localPosition, animate: true);

            TransitionToState(GameState.Await_Player_Bet);
        }

        void InitializeGame()
        {
            objectPool.Initialize(cardPrefab, size: 2);

            gameRuleResult = ApplyGameRules(gameConfig);

            deck.Initialize(
                gameConfig.Seed,
                gameConfig.WeightScaleFactor,
                gameRuleResult.CardChances,
                deckConfig);

            TransitionToState(GameState.Start);
        }

        void StartGame()
        {
            deck.ResetDeck();

            TransitionToState(GameState.Show_Dealer_Card);
        }
    }
}
