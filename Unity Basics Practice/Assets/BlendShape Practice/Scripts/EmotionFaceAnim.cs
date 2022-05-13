using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionFaceAnim : MonoBehaviour
{
    public Animator emotion;
    public Text text;

    private void Awake()
    {
        emotion.GetComponent<Animator>();
    }

    public void Anim_Angry()
    {
        emotion.SetBool("Angry", true);
        emotion.SetBool("Happy", false);
        text.text = "Emotion State : Angry";
    }

    public void Anim_Happy()
    {
        emotion.SetBool("Angry", false);
        emotion.SetBool("Happy", true);
        text.text = "Emotion State : Happy";
    }
}
