using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game
{
    public class BetResultPopup : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] TextMeshProUGUI resultLabel;
        public string Text
        {
            get => resultLabel.text;
            set
            {
                resultLabel.text = value;
            }
        }

        public void Play()
        {
            animator.SetTrigger("start");
        }
    }
}