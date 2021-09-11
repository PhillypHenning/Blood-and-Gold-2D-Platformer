using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _Speed = 20;
    [SerializeField] private float _Acceleration = 0f;
    [SerializeField] private float _BulletDamage = 10f;
    private Rigidbody2D _RigidBody2D;
    private SpriteRenderer _SpriteRenderer;
    private Vector2 _Movement;
    private ReturnToPool _ProjectileReturnToPool;

    public Vector2 Direction { get; set; }
    public bool FacingRight { get; set; }
    public float Speed { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        _RigidBody2D = GetComponent<Rigidbody2D>();
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _ProjectileReturnToPool = GetComponent<ReturnToPool>();

        FacingRight = true;
        Speed = _Speed;
    }

    void Start()
    {
        Physics2D.IgnoreLayerCollision(10, 11); // <-- Projectile ignore collision with "Not Hitables"
    }

    void FixedUpdate()
    {
        MoveProjectile();
    }

    public void MoveProjectile(){
        _Movement = Direction * Speed *Time.deltaTime;
        _RigidBody2D.MovePosition(_RigidBody2D.position + _Movement);

        // This will increase bullet speed over time.. 
        Speed += _Acceleration * Time.deltaTime;
    }

    public void FlipProjectile(){
        // Flips to the opposite of what it currently is. 
        _SpriteRenderer.flipX = !_SpriteRenderer.flipX;
    }

    public void SetDirection(Vector2 newDirection, Quaternion newRotation, bool isFacingRight=true){
        Direction = newDirection;
        if(!isFacingRight){
            FlipProjectile();
        }
        transform.rotation = newRotation;
    }

    public void Reset(){
        _SpriteRenderer.flipX = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log(other.tag);
        if(other.tag == "Enemy" || other.tag == "Player" || other.tag == "Shield"){
            _ProjectileReturnToPool.DestroyObject();
            CharacterHealth characterHealth = other.GetComponent<CharacterHealth>();
            if(characterHealth._Damagable){
                characterHealth.Damage(_BulletDamage);
            }
        }
        if(other.tag == "Interactable"){
            Interactable interactable = other.GetComponent<Interactable>();
            if(interactable._Breakable){
                interactable.DamageInteractable(_BulletDamage);
            }
        }

        if(other.tag == "Enemy Wall"){
            return;
        }
        _ProjectileReturnToPool.DestroyObject();
        
    }
}
