using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Vector3 _start;
    private Vector3 _end;
    private PlayerState _state;
    private Camera _camera;
    private Rigidbody2D rigid;
    [SerializeField] private float _playerSizeRadius;
    private Animator _animator;
    private AudioSource jumpAudio;
    private PlayerTypeController _playerTypeController;

    private float _jumpTimer;
    [SerializeField] private float _jumpTime;
    [SerializeField] private float _jumpForce;

    [SerializeField] private Sprite[] _playerSprites;
    [SerializeField] private Transform _spriteTransform;
    [SerializeField] private Transform _frogShadowTransform;
    [SerializeField] private Transform _tankShadowTransform;

    void Start()
    {
        _frogShadowTransform.gameObject.SetActive(false);
        _tankShadowTransform.gameObject.SetActive(false);
        _camera = Camera.main;
        _animator = GetComponent<Animator>();
        jumpAudio = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody2D>();
        _playerTypeController = GetComponent<PlayerTypeController>();
    }

    void Update()
    {
        if (GameManager.instance.gameState != GameState.Main) return;
        
        if (_state != PlayerState.Jumping && !IsOnFloor())
        {
            //Died
            GameManager.instance.PlayerDied();
            _animator.SetTrigger("Fall");
            return;
        }
        

        if (Input.GetMouseButtonDown(0) && _state == PlayerState.Idle)
        {
            _start = _camera.ScreenToWorldPoint(Input.mousePosition);
            SetPlayerState(PlayerState.AboutToJump);
        }
        else if (_state == PlayerState.AboutToJump)
        {
            _end = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dir = (_start - _end);
            float rotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);

            _spriteTransform.localPosition = new Vector2(0, -Vector2.Distance(_start, _end) / 100);

            Debug.DrawLine(transform.position, dir, Color.red, 1, false);
            if (Input.GetMouseButtonUp(0))
            {
                Jump();
            }
        }
        else if (_state == PlayerState.Jumping)
        {
            RunTimer();
        }
    }

    private bool IsOnFloor()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(this.transform.position, _playerSizeRadius);
        foreach (Collider2D col in colls)
        {
            if (col.tag == "Tile")
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _playerSizeRadius);
    }

    private void RunTimer()
    {
        if (_jumpTimer < _jumpTime)
        {
            _jumpTimer += Time.deltaTime;
        }
        else
        {
            OnLanding();
        }
    }


    private void Jump()
    {
        SetPlayerState(PlayerState.Jumping);
        rigid.AddRelativeForce(Vector2.up * _jumpForce);
       
        jumpAudio.pitch += Random.Range(0.05f, 0.1f);
        if (jumpAudio.pitch > 1.3f) jumpAudio.pitch = 0.5f;
        jumpAudio.Play();
    }
    private void OnLanding()
    {
        SetPlayerState(PlayerState.Idle);
        _jumpTimer = 0;
        rigid.velocity = Vector2.zero;
    }

    private void SetPlayerState(PlayerState state)
    {
        this._state = state;
        var playerType = _playerTypeController.PlayerType;
        
        switch (state)
        {
            case PlayerState.Idle:
                _frogShadowTransform.gameObject.SetActive(false);
                SetPlayerSprite(_playerSprites[0]);
                _tankShadowTransform.gameObject.SetActive(false);
                transform.localScale = new Vector3(1.0f, 1.0f, 1);
                break;
            case PlayerState.Jumping:
                switch (playerType)
                {
                    case PlayerType.Frog:
                        _spriteTransform.localPosition = Vector2.zero;
                        _frogShadowTransform.gameObject.SetActive(true);
                        SetPlayerSprite(_playerSprites[1]);
                        break;
                    case PlayerType.Tank:
                        _tankShadowTransform.gameObject.SetActive(true);
                        break;
                }
                transform.localScale = new Vector3(1.1f, 1.1f, 1);
                break;
        }
        
    }

    private void SetPlayerSprite(Sprite sprite)
    {
        _spriteTransform.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    
}

public enum PlayerState
{
    Idle,
    AboutToJump,
    Jumping
}
