using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Game
{
    [CreateAssetMenu(fileName = "GameRuleProfile", menuName = "Game/Rules/CardChanceGameRuleProfile")]
    public class CardChanceGameRuleProfile : GameRuleProfile
    {
        public Rank[] ranks;
        public Suit[] suits;
        public double chance;

        public override IEnumerable<CardProfile> Apply(CardProfile[] cards)
        {
            var result = cards
                .Where(card => (ranks.Length == 0 || ranks.Contains(card.Rank)) &&
                               (suits.Length == 0 || suits.Contains(card.Suit)))
                .ToArray();

            foreach (var card in result)
                yield return card;
        }
    }
}
