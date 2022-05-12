using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part_09_Animator_Move : MonoBehaviour
{   
    // SerializeField 사용 시 private 변수를 inspector에 띄움
    private Animator anim;
    private Rigidbody rigid;
    private BoxCollider col;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask layerMask;

    private bool isMove;
    private bool isJump;
    private bool isFall;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
    }

    void Update()
    {
        TryWalk();
        TryJump();
    }

    private void TryJump()
    {
        if (isJump)
        {
            if (rigid.velocity.y <= -0.1f && !isFall)
            {
                isFall = true;
                anim.SetTrigger("Fall");
            }

            // 아래 방향으로 레이를 쏴 정보를 hitIfo에 저장
            RaycastHit hitInfo;
            // 자기 자신의 위치에서 아래 방향으로 박스 콜라이더의 y값의 2분의 1만큼, 특정 레이어에만 반응하도록 레이를 쏘아 hitInfo에 저장함
            if (Physics.Raycast(transform.position, -transform.up, out hitInfo, col.bounds.extents.y + 0.5f, layerMask))
            {
                anim.SetTrigger("Land");
                isJump = false;
                isFall = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            rigid.AddForce(Vector3.up * jumpForce);
            anim.SetTrigger("Jump");
        }
    }

    private void TryWalk()
    {
        float _dirX = Input.GetAxisRaw("Horizontal"); // 수평 이동(A, D, L Arrow, R Arrow) 왼쪽 : -1, 기본 : 0, 오른쪽 : 1
        float _dirZ = Input.GetAxisRaw("Vertical"); // 수직 이동(W, S, U Arrow, D Arrow) 아래 : -1, 기본 : 0, 위 : 1

        Vector3 direction = new Vector3(_dirX, 0, _dirZ); // 키보드 입력에 따라 Direction 값이 유동적으로 변함
        isMove = false;

        // 키보드 입력이 있을 경우
        if (direction != Vector3.zero) // Vector3.zero : (0, 0, 0)
        {
            isMove = true;

            // direction의 값이 (1, 0, 1)이 되면 더해서 2가 되는데 Normalized를 통해 합이 1이 되도록 보간함
            // 대각선 움직임도 수평, 수직 움직임과 똑같이 만들기 위해 Normalized를 하며 그렇지 않을 경우 대각선 속도만 유독 빨라지게 됨
            // 즉, Normalized는 Vector3 값들의 총 합을 1로 보간하기 위함
            this.transform.Translate(direction.normalized * moveSpeed * Time.deltaTime); // 1초당 moveSpeed만큼 움직임
        }

        anim.SetBool("Move", isMove); // isMove 변수를 통해 키보드 입력 true, false 반환

        // 키보드 입력을 통한 벡터의 값을 파라미터로 넘겨줌
        anim.SetFloat("DirX", direction.x);
        anim.SetFloat("DirZ", direction.z);
    }
}
