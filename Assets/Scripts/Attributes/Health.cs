using Kp4wsGames.EventManagement;
using UnityEngine;

namespace Kp4wsGames.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f; //TODO
        [SerializeField] GameEvent GameoverEvent;
        [SerializeField] GameEvent EnemyDestroyedEvent;
        private bool isDead;

        public void TakeDamage(GameObject sender, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if(healthPoints == 0)
            {
                Die();
            }
            else
            {
                //On take damage
            }
        }

        public bool IsDead()
        {
            return isDead;
        }

        public float getHealth()
        {
            return healthPoints;
        }

        private void Die()
        {
            if (isDead)
                return;

            isDead = true;
            Destroy(gameObject); //TODO

            if(gameObject.tag == "Player")
            {
                GameoverEvent.Raise();
            }
            else
            {
                EnemyDestroyedEvent.Raise();
            }
        }
    }
}