using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Game
{
    public partial class GameController : MonoBehaviour
    {
        [SerializeField] GameProfile gameConfig;
        [SerializeField] Deck deck;
        [SerializeField] DeckProfile deckConfig;


        void Start()
        {
            TransitionToState(GameState.Initialize);
        }

        GameRuleResult ApplyGameRules(GameProfile profile)
        {
            IEnumerable<CardProfile> currentPool = deckConfig.Cards;
            var cardChances = new Dictionary<CardProfile, double>();

            foreach (var rule in profile.Rules)
            {
                switch (rule.Type)
                {
                    case RuleType.Modify_Set:
                        {
                            currentPool = rule.Apply(currentPool);
                            break;
                        }

                    case RuleType.Modify_Value:
                        {
                            if (rule is CardChanceGameRuleProfile chanceRule)
                            {
                                var matchingCards = chanceRule.Apply(currentPool);

                                foreach (var card in matchingCards)
                                {
                                    cardChances[card] = chanceRule.chance;
                                }
                            }
                            break;
                        }
                }
            }

            cardChances = cardChances
                .Where(cardChance => currentPool.Contains(cardChance.Key))
                .ToDictionary(cardChance => cardChance.Key, cardChance => cardChance.Value);

            return new GameRuleResult()
            {
                Cards = currentPool.ToArray(),
                CardChances = cardChances
            };
        }
    }
}