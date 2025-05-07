using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class M_FlyAroundMagicArrow: MagicBase, I_MagicEffect
{
    public float accelerate = 6;
    public float timeBeforeActive = 0.5f;
    public MagicBase M_MagicArrow ;
    public Bullet centerBullet;
   // public float radiusCorrection = 0.3f;
    private void Awake()
    {
        M_MagicArrow = GameObject.Find("M_MagicArrow").GetComponent<M_MagicArrow>();
    }
    public override void TriggerMagic(Vector3 position)
    {
        //����MagicBullt,����I_MagicEffect;
        centerBullet = BulletPoolManager.Instance().SpawnMagicBullet(position, M_MagicArrow,queueCount);
        PlayerAttack playerAttack = PlayerAttack.Instance();
        //�����Ч���Ž�List
        BulletPoolManager.Instance().AddMagicToList(this);
        //BulletPoolManager.Instance().AddEachMagicCount(3);
        //������Ч��+3
        if (playerAttack.magicQueues[queueCount].Count > 0)
        {
            if (playerAttack.magicQueues[queueCount].Peek().isActive) //isActive��ʾ�������ͷ�
            {
                 playerAttack.magicQueues[queueCount].Dequeue().TriggerMagic(position);
            }
            else
            {
                playerAttack.magicQueues[queueCount].Dequeue();
            }     
        }
      
    }
    
    public override void TriggerMagic(Vector3 position, Vector3 forward)
    {
        centerBullet = BulletPoolManager.Instance().SpawnMagicBullet(position,forward,M_MagicArrow, queueCount);
        PlayerAttack playerAttack = PlayerAttack.Instance();
        //�����Ч���Ž�List
        BulletPoolManager.Instance().AddMagicToList(this);

            if (playerAttack.magicQueues[queueCount].Count > 0)
            {
                if (playerAttack.magicQueues[queueCount].Peek().isActive)
                {
                    playerAttack.magicQueues[queueCount].Dequeue().TriggerMagic(position, forward);
                }
                else
                {
                    playerAttack.MagicQueue.Dequeue();
                }
            }
        
    }

    public void TriggerEffect(Bullet bullet)
    {
        Bullet tempBullet;
        bullet.SetSpeed(bullet.Speed * accelerate);
        //Debug.Log(bullet.GetComponent<Rigidbody>().velocity);
        bullet.AddComponent<FlyAroundPoint>();
        bullet.GetComponent<FlyAroundPoint>().timeBeforeActive = timeBeforeActive;
        bullet.GetComponent<FlyAroundPoint>().centerObejct = centerBullet.gameObject;

            tempBullet = GameObject.Instantiate(bullet);
            tempBullet.GetComponent<Rigidbody>().velocity = bullet.GetComponent<Rigidbody>().velocity;
            tempBullet.SetSpeed(tempBullet.Speed * accelerate);
            tempBullet.transform.position += Vector3.forward/4;
            

            tempBullet = GameObject.Instantiate(bullet);
            tempBullet.GetComponent<Rigidbody>().velocity = bullet.GetComponent<Rigidbody>().velocity;
            tempBullet.SetSpeed(tempBullet.Speed * accelerate);
            tempBullet.transform.position += Vector3.left/4  ;

            tempBullet = GameObject.Instantiate(bullet);
            tempBullet.GetComponent<Rigidbody>().velocity = bullet.GetComponent<Rigidbody>().velocity;
            tempBullet.SetSpeed(tempBullet.Speed * accelerate);
            tempBullet.transform.position += Vector3.right/4  ;

            bullet.transform.position += Vector3.back/4  ;


    }


}
