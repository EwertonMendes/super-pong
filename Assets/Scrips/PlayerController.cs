using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    public int speed = 12;
    public double limitUpPosition;
    public double limitDownPosition;
    public int score = 0;

    public string upKey;
    public string downKey;

    private Rigidbody2D _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(upKey) && this.transform.position.y < limitUpPosition)
        {
            _rigidbody.velocity = new Vector2(0, speed);
        }
        else if (Input.GetKey(downKey) && this.transform.position.y > limitDownPosition)
        {
            _rigidbody.velocity = new Vector2(0, speed * -1);
        }
        else
        {
            _rigidbody.velocity = new Vector2(0, 0);
        }
    }
}
