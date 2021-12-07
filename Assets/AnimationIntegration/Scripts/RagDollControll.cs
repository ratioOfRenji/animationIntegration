using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollControll : MonoBehaviour
{
    [SerializeField]
    private Rigidbody[] allRigidbodies;
   
    private void Awake()
    {
        //������ rigidbody �� ������ kinematic, ����� ��� ����� �� ������ �� ������, ���� �� ����� ��������
        for (int i = 0; i < allRigidbodies.Length; i++)
        {
            allRigidbodies[i].isKinematic = true;
        }
    }
    private void OnEnable()
    {
        FinishEnemy._AttackEnemy += MakePhisical;
    }
    public void MakePhisical() //����� ���������� RagDoll
    {
        //������ ��� ������� �����, ����� �� ���� ��������������� ��� ������ ������ ����������
        gameObject.tag = "DeadEnemy";
        // ����� ������ ������ ���������� ( � ������ ������ ������� null, ��� ������� ui � �� ���� ��� ��� ��������� �������� ��������� �� ��� ������� �����)
        FinishEnemy.Instance.findEnemy();
        StartCoroutine(waitBeforeEnable());
        //��������, ����������� ��������, � ��������� �������. ��� ������ ���� ������
        IEnumerator waitBeforeEnable()
        {
            yield return new WaitForSeconds(.5f);
            Animator animator = GetComponentInChildren<Animator>();
            animator.enabled = false;
            //�������� ����������� ����� ������� � ������, ����� ������� ��������� ����� ����, � �� ���� �� ������ ���� � �������������
            Vector3 forceDirection = (FinishEnemy.Instance.transform.position - this.transform.position).normalized;
            for (int i = 0; i < allRigidbodies.Length; i++)
            {
                allRigidbodies[i].isKinematic = false;
                allRigidbodies[i].AddForce(-forceDirection * 1000);
            }

            StartCoroutine(delayBeforeSpawn());
        }

    }
    private void OnDisable()
    {
        FinishEnemy._AttackEnemy -= MakePhisical;
    }
    IEnumerator delayBeforeSpawn() //��������, ������������ ������� ����� � ��������� ������
    {
        yield return new WaitForSeconds(5f);


        EnemySpawner.Instance.SpawnEnemy();
        Destroy(this.gameObject);
    }
   
}
