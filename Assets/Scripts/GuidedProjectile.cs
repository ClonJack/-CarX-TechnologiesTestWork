using UnityEngine;
using System.Collections;

public class GuidedProjectile : UpdateManager
{/// <summary>
/// Лучше использовать встроенную проверку TryGetComponent потому что может выдать исключение если не будет нужного компонента 
/// </summary>
    public GameObject m_target;
    [SerializeField] private float m_speed = 0.2f;
    [SerializeField] private int m_damage = 10;
    private void BulletMove()
    {
        if (m_target != null)
        {
            var translation = m_target.transform.position - transform.position;
            if (translation.magnitude > m_speed)
            {
                translation = translation.normalized * m_speed;
            }
            transform.Translate(translation);
        }
    }

    public override void GameUpdate()
    {
        if (m_target == null)
            Destroy(gameObject);

        BulletMove();
    }
 

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Monster monster)) { 
            
            

            monster.m_hp -= m_damage;

            if (monster.m_hp <= 0)
                Destroy(monster.gameObject);
            
            Destroy(gameObject);
        }
    }
}
