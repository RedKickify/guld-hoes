using System;
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

    private float _jumpTimer;
    [SerializeField] private float _jumpTime;
    [SerializeField] private float _jumpForce;

    [SerializeField] private Sprite[] _playerSprites;
    [SerializeField] private Transform _spriteTransform;
    [SerializeField] private Transform _shadowTransform;

    void Start()
    {
        _shadowTransform.gameObject.SetActive(false);
        _camera = Camera.main;
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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
        _spriteTransform.localPosition = Vector2.zero;
        _spriteTransform.localScale = new Vector2(1.1f, 1.1f);
        _shadowTransform.gameObject.SetActive(true);
        GetComponent<AudioSource>().Play();
    }
    private void OnLanding()
    {
        SetPlayerState(PlayerState.Idle);
        _jumpTimer = 0;
        rigid.velocity = Vector2.zero;
        _spriteTransform.localScale = new Vector2(1, 1);
        _shadowTransform.gameObject.SetActive(false);
    }

    private void SetPlayerState(PlayerState state)
    {
        this._state = state;
        switch (state)
        {
            case PlayerState.Idle:
                SetPlayerSprite(_playerSprites[0]);
                break;
            case PlayerState.Jumping:
                SetPlayerSprite(_playerSprites[1]);
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