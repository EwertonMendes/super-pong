using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject goalTextPrefab;
    public GameObject canvas;

    private int baseSpeed = 6;
    public int speed = 6;
    private AudioSource ballKickSound;
    private Rigidbody2D _rigidbody;
    private TrailRenderer trailRenderer;
    private Direction ballDirection;
    private TextMeshProUGUI player1PointText;
    private TextMeshProUGUI player2PointText;
    private PlayerController player1Script;
    private PlayerController player2Script;
    private SpriteRenderer sprite;

    private int randomIntExcept(int min, int max, int except)
    {
        int result = Random.Range(min, max + 1);
        while(result == except)
        {
            result = Random.Range(min, max + 1);
        }
        return result;
    }

    // Start is called before the first frame update
    void Start()
    {
        player1Script = player1.GetComponent<PlayerController>();
        player1PointText = GameObject.FindGameObjectWithTag("Player1Points").GetComponent<TextMeshProUGUI>();

        player2Script = player2.GetComponent<PlayerController>();
        player2PointText = GameObject.FindGameObjectWithTag("Player2Points").GetComponent<TextMeshProUGUI>();

        ballKickSound = this.GetComponent<AudioSource>();

        _rigidbody = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        sprite = GetComponent<SpriteRenderer>();
        ballDirection = (Direction)Random.Range(0, 1);

        ChangeBallDirection();
    }

    private void Update()
    {
        RotateBall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayKickSound();
            ChangeBallDirection();
        }

        if (collision.gameObject.CompareTag("UpperField"))
        {
            PlayKickSound();
            ChangeBallDirectionForField(Direction.UP);
        }

        if (collision.gameObject.CompareTag("BottomField"))
        {
            PlayKickSound();
            ChangeBallDirectionForField(Direction.DOWN);
        }

        if (collision.gameObject.CompareTag("Player1Goal"))
        {
            player2Script.score++;
            player2PointText.text = player2Script.score.ToString();
            ShowGoalMessageOnScreen();
            StartCoroutine(ResetBall());
        }

        if (collision.gameObject.CompareTag("Player2Goal"))
        {
            player1Script.score++;
            player1PointText.text = player1Script.score.ToString();
            ShowGoalMessageOnScreen();
            StartCoroutine(ResetBall());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("PowerUp"))
        {
            speed += 3;
            sprite.color = new Color32(119, 119, 182, 255);
        }
    }

    private void ChangeBallDirection()
    {
        float ramdomYDirection = speed * randomIntExcept(-1, 1, 0);

        if (ballDirection == Direction.LEFT)
        {
            ballDirection = Direction.RIGHT;
            _rigidbody.velocity = new Vector2(speed * -1, ramdomYDirection);
            return;
        }

        if(ballDirection == Direction.RIGHT)
        {
            ballDirection = Direction.LEFT;
            _rigidbody.velocity = new Vector2(speed, ramdomYDirection);
            return;
        }
    }

    private void ChangeBallDirectionForField(Direction direction)
    {
        if (direction == Direction.UP)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, speed * -1);
            return;
        }

        if (direction == Direction.DOWN)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, speed);
            return;
        }
    }

    IEnumerator ResetBall()
    {
        trailRenderer.enabled = false;
        _rigidbody.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        speed = baseSpeed;
        sprite.color = new Color32(255, 255, 255, 255);
        ChangeBallDirection();
        yield return new WaitForSeconds(0.20f);
        trailRenderer.enabled = true;
    }

    private void RotateBall()
    {
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z++;
        transform.rotation = Quaternion.Euler(rotationVector);
    }

    private void PlayKickSound()
    {
        ballKickSound.Play();
    }

    private void ShowGoalMessageOnScreen()
    {
        var instance = Instantiate(goalTextPrefab, canvas.transform);
        LeanTween.moveLocalX(instance, instance.transform.position.x * -100, 1f)
            .setEaseLinear().setDestroyOnComplete(true);
    }
}

public enum Direction
{
    RIGHT,
    LEFT,
    UP,
    DOWN
}
