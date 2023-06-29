using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    // ������Ʈ�� ó���� ����
    private Transform tr;

    // Animation ������Ʈ�� ������ ����
    private Animation anim;

    // �ʱ� ���� ��
    private readonly float initHp = 100.0f;

    // �̵� �ӷ� ���� (public���� ����Ǿ� �ν����� �信 �����)
    public float moveSpeed = 10.0f;

    // ȸ�� �ӵ� ����
    public float turnSpeed = 80.0f;

    //���� ���� ��
    public float currHp;

    // Hpbar ������ ����
    private Image hpBar;

    // ��������Ʈ ����
    public delegate void PlayerDieHandler();

    // �̺�Ʈ ����
    public static event PlayerDieHandler OnPlayerDie;

    // Start is called before the first frame update
    void Start()
    {
        // Transform ������Ʈ�� ������ ������ ����
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        anim.Play("Idle");

        //HP �ʱ�ȭ
        currHp = initHp;

        // Hpbar ����
        hpBar = GameObject.FindGameObjectWithTag("HP_BAR")?.GetComponent<Image>();
        DisplayHealth();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");      // -1.0f ~ 0.0f ~ +1.0f
        float v = Input.GetAxis("Vertical");        // -1.0f ~ 0.0f ~ +1.0f
        float r = Input.GetAxis("Mouse X");

        // �����¿� �̵� ���� ���� ���
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        // Translate(�̵� ���� * �ӷ� * Time.deltaTime)
        tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);

        // Vector3.up ���� �������� turnSpeed��ŭ�� �ӵ��� ȸ��
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);

        // ���ΰ� ĳ������ �ִϸ��̼� ����
        PlayerAnim(h, v);
    }

    void PlayerAnim(float h, float v)
    {
        // Ű���� �Է°��� �������� ������ �ִϸ��̼� ����
        if (v >= 0.1f)
        {
            anim.CrossFade("RunF", 0.25f);  // ���� �ִϸ��̼� ����
        }
        else if (v <= -0.1f)
        {
            anim.CrossFade("RunB", 0.25f);  // ���� �ִϸ��̼� ����
        }
        else if (h >= 0.1f)
        {
            anim.CrossFade("RunR", 0.25f);  // ������ �̵� �ִϸ��̼� ����
        }
        else if (h <= -0.1f)
        {
            anim.CrossFade("RunL", 0.25f);  // ���� �̵� �ִϸ��̼� ����
        }
        else
        {
            anim.CrossFade("Idle", 0.25f);   // ���� �� Idle �ִϸ��̼� ����
        }
    }

    // �浹�� Collider�� IsTrigger �ɼ��� üũ���� �� �߻�
    void OnTriggerEnter(Collider coll)
    {
        // �浹�� Collider�� ������ PUNCH�̸� Player�� HP ����
        if (currHp >= 0.0f && coll.CompareTag("PUNCH"))
        {
            currHp -= 10.0f;

            DisplayHealth();

            Debug.Log($"Player hp = {currHp / initHp}");
            // Player�� ������ 0 �����̸� ��� ó��
            if (currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    // Player�� ��� ó��
    // Player�� ��� ó��
    void PlayerDie()
    {
        Debug.Log("Player Die !");
        // // MONSTER �±׸� ���� ��� ���ӿ�����Ʈ�� ã�ƿ�
        // GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");
        // // ��� ������ OnPlayerDie �Լ��� ���������� ȣ��
        // foreach (GameObject monster in monsters)
        // {
        // monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        // }
        // ���ΰ� ��� �̺�Ʈ ȣ��(�߻�)
        OnPlayerDie();
    }

    void DisplayHealth() 
    {
        hpBar.fillAmount = currHp / initHp;
    }

}
