using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    [CreateAssetMenu(fileName = "VisualSettingsProfile", menuName = "Game/VisualSettingsProfile")]
    public class VisualSettingsProfile : ScriptableObject
    {
        public float CardMoveSpeed;
        public float CardScale;
        public Ease CardMoveEase;
        public float CardFlipDuration;
        public Ease CardFlipEase;
    }
}
