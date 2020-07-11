using UnityEngine;

public class PlayerController : MovingObject
{
    private Animator _animator;
    private LevelManager _level;

    protected override void Start()
    {
        base.Start();
        _animator.GetComponent<Animator>();
        _level = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    private void Update() 
    {
        if (_level.PlayerTurn && _level.InputsAmount > 0)
        {
            // Get each input
        }
    }

}