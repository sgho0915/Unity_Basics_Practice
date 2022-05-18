using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPractice_Call : MonoBehaviour
{
    void Start()
    {
        // SingletonPractice 클래스에 점수를 설정한다
        SingletonPractice.Instance.highScore = 100;
    }
}
