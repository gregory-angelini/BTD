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

        public override RuleType Type => RuleType.Modify_Value;

        public override IEnumerable<CardProfile> Apply(IEnumerable<CardProfile> cards)
        {
            return cards
                .Where(card =>
                (suits.Length == 0 || suits.Contains(card.Suit)) &&
                (ranks.Length == 0 || ranks.Contains(card.Rank)));
        }
    }
}
