using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBodyBehaviour : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public float speed;

    public AudioSource linkedSound;
    public AudioSource takePointSound;
    public AudioSource badRockSound;
    public AudioSource pastedSound;

    public GameObject outfit;

    public bool onMove;
    public bool pasted;

    private Rigidbody _rb;
    private LineRenderer _rope;

    private Vector3 _moveDestination;
    private Vector3 _moveDirection;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rope = GetComponent<LineRenderer>();
        onMove =false;
        pasted = false;
    }

    void Update()
    {
        if (onMove && !pasted)
        {
            _rb.velocity += _moveDirection * speed;
            gameObject.transform.right = _moveDirection.normalized;
            DrawRope(gameObject.transform.position, _moveDestination);
        }
        else DrawRope();

    }
   
    public void StartMove(Vector3 to, Vector3 direction)
    {
        linkedSound.Play();
        onMove = true;
        pasted = false;
        _moveDestination = to;
        _moveDirection = direction;
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;
        gameObject.transform.localScale = new Vector3(3f, 1f, 1f);
        outfit.SetActive(false);
    }

    public void StopMove()
    {
        onMove = false;
        if (pasted)  
            pasted=false;
        _moveDestination = Vector3.zero;
        _moveDirection = Vector3.zero;
        _rb.useGravity = true;   
        gameObject.transform.localScale = new Vector3(3f, 3f, 3f);
        outfit.SetActive(true);

    }

    public void StopMovePasted()
    {
        pastedSound.Play();
        onMove = false;
        pasted = true;
        _moveDestination = Vector3.zero;
        _moveDirection = Vector3.zero;
        _rb.velocity = Vector3.zero;
        gameObject.transform.localScale = new Vector3(3f, 3f, 3f);
        outfit.SetActive(true);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<HealthBehaviour>(out var target))
        {
            int targetValue = target.gameObject.GetComponent<HealthBehaviour>().OnCollision();

            if (targetValue > 0) takePointSound.Play();
            else badRockSound.Play();

            health += targetValue;

            StopMove();
        }
        else if (collision.collider.TryGetComponent<SphereCollider>(out _)) StopMovePasted();
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.CompareTag("Level Rock") ) 
        {
            if(health >= maxHealth) 
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            StopMovePasted();
        }
    }


    public void DrawRope(Vector3 init, Vector3 other)
    {
        _rope.enabled = true;
        _rope.SetPosition(0, init- new Vector3(0,0.2f,0)) ;
        _rope.SetPosition(1, other);
        
    }
    public void DrawRope()
    {
        _rope.enabled = false;
    }
}
