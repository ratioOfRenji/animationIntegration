using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollControll : MonoBehaviour
{
    [SerializeField]
    private Rigidbody[] allRigidbodies;
   
    private void Awake()
    {
        //делаем rigidbody на костях kinematic, чтобы они никак не влияли на модель, пока не будут включены
        for (int i = 0; i < allRigidbodies.Length; i++)
        {
            allRigidbodies[i].isKinematic = true;
        }
    }
    private void OnEnable()
    {
        FinishEnemy._AttackEnemy += MakePhisical;
    }
    public void MakePhisical() //метод включающий RagDoll
    {
        //меняем тэг убитого врага, чтобы не было неоднозначности при вызове поиска противника
        gameObject.tag = "DeadEnemy";
        // вызов метода поиска противника ( в данном случае получим null, это откючит ui и не даст еще раз проиграть анимацию добивания на уже добитом враге)
        FinishEnemy.Instance.findEnemy();
        StartCoroutine(waitBeforeEnable());
        //корутина, отключающая аниматор, и вклчающая рэгдолл. при вызове враг падает
        IEnumerator waitBeforeEnable()
        {
            yield return new WaitForSeconds(.5f);
            Animator animator = GetComponentInChildren<Animator>();
            animator.enabled = false;
            //вычислем направление между игроком и врагом, чтобы придать падающему врагу силу, и он упал не просто вниз а снаправлением
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
    IEnumerator delayBeforeSpawn() //корутина, уничтожающая убитого врага и спавнящая нового
    {
        yield return new WaitForSeconds(5f);


        EnemySpawner.Instance.SpawnEnemy();
        Destroy(this.gameObject);
    }
   
}
