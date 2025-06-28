using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    [CreateAssetMenu(fileName = "GameProfile", menuName = "Game/GameProfile")]
    public class GameProfile : ScriptableObject
    {
        public GameRuleProfile[] Rules;
    }
}
