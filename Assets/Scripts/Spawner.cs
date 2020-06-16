using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Spawner : UpdateManager
{
    /// <summary>
    //код слишком сложно читается и постоянно вызввается в Update
    //также лучше спавнить префабы чем создавать примитиву и на нее навешивать компоненты через кода 
    //мой подход будет хорош для геймдизайнеров , которые смогут играться с цветом  материала для монстров и.т.п
    /// </summary>
    [SerializeField] private float m_interval = 3;
    [SerializeField] private GameObject m_moveTarget;
    [Header("Персонаж,который будет спавниться")]
    [SerializeField] private GameObject prefab;
    public static List<Monster> monsters = new List<Monster>();
    private IEnumerator ISpawn()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(m_interval);
        while (true)
        {
            yield return waitForSeconds;
            var monster = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<Monster>();
            monsters.Add(monster);
            monster.m_moveTarget = m_moveTarget;
        }
    }
   public override void StartUpdate() => StartCoroutine(ISpawn());
}
