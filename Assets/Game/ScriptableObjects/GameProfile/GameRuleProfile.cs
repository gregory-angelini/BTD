using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public abstract class GameRuleProfile : ScriptableObject
    {
        public abstract IEnumerable<CardProfile> Apply(CardProfile[] cards);
    }
}
