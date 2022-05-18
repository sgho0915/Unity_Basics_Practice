using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 예제 싱글톤 오브젝트 클래스 : 유니티 C# 스크립팅 마스터하기 155p
public class SingletonPractice : MonoBehaviour
{
    // 싱글턴 인스턴스에 접근하기 위한 C# 프로퍼티
    // get 접근자만 가지는 읽기 전용의 프로퍼티
    // 전역의 정적 Instance 프로퍼티를 두면, 어느 스크립트 파일에서든 지역변수나 오브젝트 참조를 만들 필요 없이 직접 접근이 가능해진다.
    public static SingletonPractice Instance
    {
        // private 변수 instance의 참조를 반환한다
        // 이 변수는 get 멤버만을 가지고 있는 읽기 전용 프로퍼티인 Instance를 통해 외부에서 접근할 수 있다.
        get
        {
            return instance;
        }
    }

    //-----------------------------------------------
    // private 멤버인 instance를 클래스에 static으로 선언
    // => 여러 개의 인스턴스가 있을 때 변수가 각각의 인스턴스에서 특정 값을 가지는 대신 모든 인스턴스에 걸쳐서 공유되는 변수가 된다
    private static SingletonPractice instance = null;
    //-----------------------------------------------


    // 최고 점수
    public int highScore = 0;

    // 게임이 정지 되었는지
    public bool isPaused = false;

    // 플레이어의 입력이 허용되는지
    public bool inputAllowed = true;


    // 초기화에 사용
    private void Awake()
    {
        // 씬에 이미 인스턴스가 존재하는지 검사
        // 존재하는 경우 이 인스턴스는 소멸시킴
        if (instance)
        {
            // Destroy는 객체 오브젝트를 지금 파괴하거나 일정 시간 후 파괴
            // DestroyImmediate는 객체 오브젝트를 즉시 파괴, 영구적으로 Asset을 파괴시킴
            DestroyImmediate(gameObject);
            return;
        }

        // 이 인스턴스를 씬에서 유효한 유일 오브젝트로 만든다
        instance = this;

        // 게임 매니저가 지속되도록 한다
        DontDestroyOnLoad(gameObject);
    }
}
