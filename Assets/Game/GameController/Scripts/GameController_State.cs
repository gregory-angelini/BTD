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
        BetType playerBet = BetType.No_Bet;
        Card playerCard;
        Card dealerCard;
        List<Card> createdCards = new List<Card>();


        void SetState(GameState newState)
        {
            CurrentState = newState;
            Debug.Log($"Game State: {newState}");

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

                case GameState.New_Round:
                    NewRound(newState);
                    break;

                case GameState.Show_Dealer_Card:
                    ShowDealerCard(newState); 
                    break;

                case GameState.Await_Player_Bet:
                    AwaitBet(newState); 
                    break;

                case GameState.Show_Player_Card:
                    ShowPlayerCard(newState); 
                    break;

                case GameState.Resolve_Bet_Result:
                    ResolveBetResult(newState); 
                    break;

                case GameState.Tie:
                    Tie(newState);
                    break;

                case GameState.Player_Win:
                    PlayerWin(newState);
                    break;

                case GameState.Dealer_Win:
                    DealerWin(newState);
                    break;

                case GameState.Round_Ended:
                    RoundEnded(newState);
                    break;

                case GameState.Deck_Empty:
                    //DeckEmpty(Next_Round); 
                    break;
            }
        }

        void NewRound(GameState newState)
        {
            foreach (var card in createdCards)
            {
                objectPool.Return(card);
            }
            createdCards.Clear();

            SetState(newState);
            TransitionToState(GameState.Show_Dealer_Card);
        }

        void RoundEnded(GameState newState)
        {
            SetState(newState);
        }

        void Tie(GameState newState)
        {
            SetState(newState);
            TransitionToState(GameState.Round_Ended);
        }

        void PlayerWin(GameState newState)
        {
            SetState(newState);
            TransitionToState(GameState.Round_Ended);
        }

        void DealerWin(GameState newState)
        {
            SetState(newState);
            TransitionToState(GameState.Round_Ended);
        }

        void ResolveBetResult(GameState newState)
        {
            int dealerRank = (int)dealerCard.Rank;
            int playerRank = (int)playerCard.Rank;

            if (playerRank == dealerRank)
            {
                SetState(newState);
                TransitionToState(GameState.Tie);
                return;
            }

            bool isPlayerWon =
                (playerBet == BetType.Higher && playerRank > dealerRank) ||
                (playerBet == BetType.Lower && playerRank < dealerRank);

            GameState nextState = isPlayerWon ? GameState.Player_Win : GameState.Dealer_Win;

            if (isPlayerWon)
                playerArea.Score = playerArea.Score + 1;
            else
                dealerArea.Score = playerArea.Score + 1;

            SetState(newState);
            TransitionToState(nextState);
        }

        void AwaitBet(GameState newState)
        {
            SetState(newState);
        }

        void ShowPlayerCard(GameState newState)
        {
            DrawCard(
                newState,
                onComplete: (card) =>
                {
                    card.Flip(animate: true);
                    SetState(newState);
                    TransitionToState(GameState.Resolve_Bet_Result);
                });
        }

        void ShowDealerCard(GameState newState)
        {
            DrawCard(
                newState,
                onComplete: (card) =>
                {
                    card.Flip(animate: true);
                    SetState(newState);
                    TransitionToState(GameState.Await_Player_Bet);
                });
        }

        void DrawCard(GameState newState, Action<Card> onComplete)
        {
            var cardProfile = deck.DrawCard();
            var card = objectPool.Get();
            createdCards.Add(card);

            card.Setup(
                cardProfile,
                deckConfig.BackSprite,
                isFaceUp: false);

            card.SetParent(deck.transform.parent);
            card.SetPosition(deck.GetPosition());
            card.SetScale(visualSettings.CardScale);
            Vector3 cardTargetPos;

            switch (newState)
            {
                case GameState.Show_Dealer_Card:
                    cardTargetPos = dealerArea.CardSlotPositon();
                    dealerCard = card;
                    break;

                case GameState.Show_Player_Card:
                    playerCard = card;
                    cardTargetPos = playerArea.CardSlotPositon();
                    break;

                default:
                    throw new ArgumentException($"Game state ({newState}) is invalid.");
            }

            card.View.Move(
                cardTargetPos,
                animate: true,
                onComplete: () =>
                {
                    onComplete?.Invoke(card);
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
            playerArea.Score = 0;
            dealerArea.Score = 0;

            SetState(newState);

            TransitionToState(GameState.New_Round);
        }
    }
}
