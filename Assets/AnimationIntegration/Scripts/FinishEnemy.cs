using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishEnemy : MonoBehaviour
{
    private GameObject enemy;
    private bool startAttackEnemy;
   
    [SerializeField] private float finishRunSpeed = 1.5f;
    [SerializeField] private float distanceToFinishEnemy = 9f;
    [SerializeField] private GameObject finishEnemyUi;
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject riffle;
    [SerializeField] private GameObject knife;
    private LegsRotation legsRotation;
    private TorsoRotation torsoRotation;
    private PlayerController playerController;
    private RagDollControll ragDoll;

    public delegate void AttackEnemy();
    public static event AttackEnemy _AttackEnemy;
    public static FinishEnemy Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void OnEnable()
    {
        spawner._findEnemy += findEnemy;
    }
    void Start()
    {


        legsRotation = GetComponent<LegsRotation>();
        torsoRotation = GetComponent<TorsoRotation>();
        playerController = GetComponent<PlayerController>();
    }


    void Update()
    {

        if (enemy != null)
        {
            if (Vector3.Distance(this.transform.position, enemy.transform.position) < distanceToFinishEnemy && Input.GetKeyDown("space"))
            {
                startAttackEnemy = true;



            }
            //������ ��������� ui ��� ���������
            if (Vector3.Distance(this.transform.position, enemy.transform.position) < distanceToFinishEnemy && startAttackEnemy == false)
            {
                finishEnemyUi.SetActive(true);
            }
            else
            {
                finishEnemyUi.SetActive(false);
            }
            if (startAttackEnemy == true)
            {
                finishEnemy();


            }
        }
        else
        {
            finishEnemyUi.SetActive(false);
        }


    }
    public void finishEnemy()
    {
        //��������� ���������� ���������� �� �������, �� ����� ���������
        legsRotation.enabled = false;
        torsoRotation.enabled = false;
        playerController.enabled = false;
        // �������� �������� ����, ���������� ������ ������ �� �����, ������������ ������ � �����
        animator.Play("Run_Rifle");
        transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, finishRunSpeed * Time.deltaTime);
        transform.LookAt(enemy.transform.position);

        if (Vector3.Distance(this.transform.position, enemy.transform.position) <= 1f) //����� ����� ������� �� �����, �������� �������� ���������, �������� ���, ��������� ��������
        {
            startAttackEnemy = false;
            riffle.SetActive(false);
            knife.SetActive(true);
            animator.Play("Finishing");
            _AttackEnemy(); 
            StartCoroutine(waitBeforeEnable()); // ��������� ��������, ������� ������� ��������� ���������� �� ����������, ����� ��������� ����������
        }
        IEnumerator waitBeforeEnable() // ��������, ������� ������� ��������� ���������� �� ����������, ����� ��������� ����������
        {
            yield return new WaitForSeconds(3.6f);
            riffle.SetActive(true);
            knife.SetActive(false);
            legsRotation.enabled = true;
            torsoRotation.enabled = true;
            playerController.enabled = true;
        }
    }
    public void findEnemy() // ������� ������ �����. ���������� �� ������ � �� ������(��� ������ ������ �����)
    {
        enemy = GameObject.FindWithTag("Enemy");

        if (enemy != null)
        {
            ragDoll = enemy.GetComponent<RagDollControll>();
            Debug.Log(enemy.name);
        }

    }
    private void OnDisable()
    {
        spawner._findEnemy -= findEnemy;
    }
    public void buttonFinishEnemy()
    {
        startAttackEnemy = true;
    }
}
