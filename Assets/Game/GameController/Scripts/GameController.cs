using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] Deck deck;
        [SerializeField] DeckProfile deckConfig;
        WeightedRandomSelector weightedRandomSelector;


        void Start()
        {
            weightedRandomSelector = new WeightedRandomSelector(seed: 2);
           
            deck.Initialize(weightedRandomSelector.Randomizer, deckConfig);


            IEnumerable<CardProfile> filtered = CardUtils.FilterByRank(deckConfig.Cards, Rank.Ace);
            filtered = CardUtils.FilterBySuit(filtered, Suit.Spades);

            var chances = new List<ItemChance<string>>
        {
            new() { Item = "Ace of Spades", Chance = 3.2 },
            new() { Item = "2 of Hearts", Chance = 2.7 },
            new() { Item = "10 of Clubs", Chance = 1.0 },
        };

            Func<double, bool> validateChance = chance => chance > 0 && chance <= 100;

            var weightedCards = WeightUtils.ConvertChancesToWeights(
                chances,
                validateChance,
                precision: 100);

            

            var drawnCard = weightedRandomSelector.PickRandom(weightedCards);
            Debug.Log($"Drawn card: {drawnCard.Item}");
        }
    }
}