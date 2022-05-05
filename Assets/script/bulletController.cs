using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    public float velocidad=10;
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject,5);
    }
    void Update()
    {
        _rb.velocity = new Vector2(velocidad, _rb.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Enemy")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
