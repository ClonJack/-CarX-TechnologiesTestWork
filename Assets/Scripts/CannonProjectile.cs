using UnityEngine;
using System.Collections;

public class CannonProjectile : UpdateManager
{

    [SerializeField] private float m_speed = 0.2f;
    [SerializeField] public int m_damage = 10;
    public GameObject m_target;

    private void BulletMove()
    {
        var translation = transform.forward * m_speed;
        transform.Translate(translation, Space.World);
    }

    public override void GameUpdate()
    {
        if (m_target == null)
            Destroy(gameObject);

        BulletMove();
    }
  
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Monster monster))
        {
            monster.m_hp -= m_damage;
            if (monster.m_hp <= 0)
            {
                Destroy(monster.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
