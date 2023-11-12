using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private float roamingChangeDirection = 2f;
        private enum State
        {
            Roaming
        }

        private State state;
        private EnemyPathfinding enemyPathfinding;

        private void Awake()
        {
            enemyPathfinding = GetComponent<EnemyPathfinding>();
            state = State.Roaming;
        }

        private void Start()
        {
            StartCoroutine(RoamingRoutine());
        }

        private IEnumerator RoamingRoutine()
        {
            while (state == State.Roaming)
            {
                Vector2 roamingPosition = GetRoamingPosition();
                enemyPathfinding.MoveTo(roamingPosition);
                yield return new WaitForSeconds(roamingChangeDirection);
            }
        }

        private Vector2 GetRoamingPosition()
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
    }
}
