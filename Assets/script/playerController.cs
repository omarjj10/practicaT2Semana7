using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class playerController : MonoBehaviour
{
    public float JumpForce = 10;
    public float velocity = 10;
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sr;
    private GameObject cameraGO;
    private static readonly string ANIMATOR_STATE = "Estado";
    private static readonly int saltar = 2;
    private static readonly int correr = 1;
    private static readonly int quieto = 0;
    private static readonly int deslizarse = 3;
    private static readonly int morir = 4;
    private static readonly int disparar = 5;
    private static readonly int RIGHT = 1;
    private static readonly int LEFT = -1;
    private vidaController vidaManager;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        cameraGO = GameObject.Find("Main Camera");
        vidaManager = GameObject.Find("vidaManager").GetComponent<vidaController>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        ChangeAnimation(quieto);
        if (Input.GetKey(KeyCode.RightArrow)) //si presiona la flecha a la derecha
        {
            Desplazarse(RIGHT);
            
        }
        if (Input.GetKey(KeyCode.LeftArrow)) //si presiona la flecha a la derecha
        {
            Desplazarse(LEFT);
        }
        if (Input.GetKey(KeyCode.C))
        {
            Deslizarse();
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            Disparar();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            Disparar2();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            ChangeAnimation(saltar);
        }
        if (vidaManager.vida == 0)
        {
            ChangeAnimation(morir);
        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Enemy")
        {
            Debug.Log("Entrar en colision: "+other.gameObject.name);
            vidaManager.desminuirVida(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "llave")
        {
            //cambio de escena
            SceneManager.LoadScene("Escena2");
        }
    }
    private void Disparar()
    {
        var x = this.transform.position.x;
        var y = this.transform.position.y;
        var bulletGo=Instantiate(bulletPrefab,new Vector2(x,y), Quaternion.identity) as GameObject;
        var controller=bulletGo.GetComponent<bulletController>();
        if (_sr.flipX){
            controller.velocidad *= -1;
        }
        ChangeAnimation(disparar);
    }
    private void Disparar2()
    {
        var x = this.transform.position.x;
        var y = this.transform.position.y;
        var bulletGo=Instantiate(bulletPrefab2,new Vector2(x,y), Quaternion.identity) as GameObject;
        var controller=bulletGo.GetComponent<bullet>();
        if (_sr.flipX){
            controller.velocidad *= -1;
        }
        ChangeAnimation(disparar);
    }
    private void Deslizarse()
    {
        ChangeAnimation(deslizarse);
    }

    private void Desplazarse(int position)
    {
        _rb.velocity = new Vector2(velocity * position, _rb.velocity.y);
        _sr.flipX = position == LEFT;
        ChangeAnimation(correr);
    }
    private void ChangeAnimation(int animation)
    {
        _animator.SetInteger(ANIMATOR_STATE,animation);
    }
}
