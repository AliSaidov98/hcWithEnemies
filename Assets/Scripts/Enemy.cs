using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Animator _enemyAnim;
    
    private Transform _player;
    private NavMeshAgent _navMeshAgent;
    private static readonly int Run = Animator.StringToHash("run");
    private GameEvents _gameEvents;


    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _gameEvents = FindObjectOfType<GameEvents>();
    }

    void Update()
    {
        DetectPlayer();
        
        _enemyAnim.SetBool(Run, _navMeshAgent.velocity.magnitude > 0);
    }

    private void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius, _playerLayer);

        if (hitColliders.Length > 0)
        {
            foreach (var hitCollider in hitColliders)
            {
                _player = hitCollider.transform;
                var direction = _player.position - transform.position;
                
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction, out hit, _radius))
                {
                    if(!hit.transform.CompareTag("Player")) return;
                    
                    Debug.DrawRay(transform.position, direction, Color.red);
                    MoveTowards(_player.position);
                }
            }
        }
    }

    private void MoveTowards(Vector3 target)
    {
        _navMeshAgent.SetDestination(target);
                                                     
        if(Vector3.Distance(_player.position, transform.position) <= _navMeshAgent.stoppingDistance)
            Kill();        
    }

    private void Kill()
    {
        if(!_gameEvents.gameOver)
            _gameEvents.SetGameOver();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
