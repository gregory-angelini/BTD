using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Game
{
    public class PlayerArea : MonoBehaviour
    {
        [SerializeField] string playerName;
        [SerializeField] TextMeshProUGUI scoreLabel;
        [SerializeField] Transform cardSlot;


        int score = 0;
        public int Score 
        {
            get => score;
            set
            {
                score = value;
                scoreLabel.text = $"{playerName}: {score}";
            }
        }

        public Vector2 CardSlotPositon()
        {
            return cardSlot.localPosition;
        }
    }
}