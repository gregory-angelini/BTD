using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public abstract class GameRuleProfile : ScriptableObject
    {
        public abstract RuleType Type { get; }
        public abstract IEnumerable<CardProfile> Apply(IEnumerable<CardProfile> cards);
    }
}
