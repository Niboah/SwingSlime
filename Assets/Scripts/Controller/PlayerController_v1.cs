using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerBodyBehaviour _playerBody;
    private CameraHolderBehaviour _cameraHolder;
    private Slider _healthSlider;

    void Start()
    {
        _inputReader = GetComponent<InputReader>();
        _playerBody = GetComponentInChildren<PlayerBodyBehaviour>();
        _cameraHolder = GetComponentInChildren<CameraHolderBehaviour>();
        _healthSlider = GetComponentInChildren<Slider>();
    }

    void Update()
    {
        _cameraHolder.UpdateCamera(_inputReader.rotation);
        updateGameObjectPosition();
        updateHealth();
        if (gameObject.transform.position.y < 0) GameController.GameOver();
    }

    private void updateGameObjectPosition() 
    {
        gameObject.transform.position = _playerBody.transform.position;
        _playerBody.transform.localPosition = Vector3.zero;
    }

    private void updateHealth() 
    {
        _healthSlider.value = ((float)_playerBody.health) / ((float)_playerBody.maxHealth);
        if (_healthSlider.value <= 0) GameController.GameOver();
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (!_playerBody.onMove && _cameraHolder.GetRaycastHit(out hit)) 
            _playerBody.StartMove(hit.point, _cameraHolder.GetDirection());
    }

    public void CancelShoot()
    {
        _playerBody.StopMove();
    }

    public void Restart()
    {
        GameController.Restart();
    }
}
