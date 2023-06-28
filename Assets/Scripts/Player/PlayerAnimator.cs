using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Player _player;
    Animator _animator;
    PlayerState _state;

    void Start()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _state = _player.State;
    }

    void Update()
    {
        if (_state == _player.State)
            return;

        if (_player.State != PlayerState.Idle)
            _animator.SetBool(_player.State.ToString(), true);

        if (_state != PlayerState.Idle)
            _animator.SetBool(_state.ToString(), false);

        _state = _player.State;
    }
}
