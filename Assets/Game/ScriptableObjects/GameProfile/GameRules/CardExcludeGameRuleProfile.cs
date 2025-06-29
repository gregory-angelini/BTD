using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Game
{
    [CreateAssetMenu(fileName = "GameRuleProfile", menuName = "Game/Rules/CardExcludeGameRuleProfile")]
    public class CardExcludeGameRuleProfile : GameRuleProfile
    {
        public Suit[] suits;
        public Rank[] ranks;

        public override RuleType Type => RuleType.Modify_Set;

        public override IEnumerable<CardProfile> Apply(IEnumerable<CardProfile> input)
        {
            return input.Where(card =>
                (ranks.Length == 0 || !ranks.Contains(card.Rank)) &&
                (suits.Length == 0 || !suits.Contains(card.Suit)));
        }
    }
}
