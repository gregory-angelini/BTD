using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        #region references
        [SerializeField] GameProfile gameConfig;
        [SerializeField] Deck deck;
        [SerializeField] DeckProfile deckConfig;
        #endregion


        void Start()
        {
            List<ItemChance<CardProfile>> selectedCards = ApplyGameRules(gameConfig);
            deck.Initialize(seed: 2, selectedCards, deckConfig);
        }

        List<ItemChance<CardProfile>> ApplyGameRules(GameProfile profile)
        {
            Dictionary<CardProfile, double> cardChanceMap = new();

            foreach (var rule in profile.Rules)
            {
                if (rule is CardChanceGameRuleProfile chanceRule)
                {
                    var matchingCards = chanceRule.Apply(deckConfig.Cards);

                    foreach (var card in matchingCards)
                    {
                        cardChanceMap[card] = chanceRule.chance;
                    }
                }
            }

            List<ItemChance<CardProfile>> selectedCards = cardChanceMap
                .Select(card => new ItemChance<CardProfile>()
                {
                    Chance = card.Value,
                    Item = card.Key
                })
                .ToList();

            return selectedCards;
        }
    }
}