using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] Image cardImage;


        public void Initialize(CardProfile cardProfile)
        {
            cardImage.sprite = cardProfile.FaceSprite;
            cardImage.SetNativeSize();
        }
    }
}
