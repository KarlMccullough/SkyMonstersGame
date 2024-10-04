using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;


    [SerializeField]
    private float xLimit = 2.6f;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private GameObject titleText;
   

    private bool movingRight = true;
    private int direction = 1;
    private bool startMoving = false;

    public bool StartMoving { get { return startMoving; } set { startMoving = value; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        titleText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && movingRight == true)
        {
            titleText.SetActive(false);
            direction = -direction;
            transform.localScale = new Vector3(direction, 1, 1);
        }
       
        if (transform.position.x >= 2.5f || Input.GetMouseButtonDown(0))
        {
            startMoving = true;
            direction = -direction;
            transform.localScale = new Vector3(direction, 1, 1);
        }
        if (transform.position.x <= -2.5f || Input.GetMouseButtonDown(0))
        {
            
            direction = -direction;
            transform.localScale = new Vector3(direction, 1, 1);
        }

        if (startMoving == false)
        {
            return;
        }

        ChangeDirection();

        transform.position += Vector3.right * moveSpeed * Time.deltaTime * direction;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xLimit, xLimit), transform.position.y, transform.position.z);

        anim.SetBool("Start", startMoving);
    }

    void ChangeDirection()
    {
        if (movingRight && transform.position.x >= xLimit)
        {
            movingRight = false;
            direction = -1;
            transform.localScale = new Vector3(direction, 1, 1);
        }
        if (movingRight == false && transform.position.x <= xLimit)
        {
            movingRight = true;
            direction = 1;
            transform.localScale = new Vector3(direction, 1, 1);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            SoundManager.instance.PlayCoin();
            MenuManager.instance.IncreaseCoin();
           

            GameObject coinEffect = ObjectPooling.instance.GetPooledObject("CoinEffect");
            coinEffect.transform.position = transform.position;
            coinEffect.SetActive(true);
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Enemy"))
        {
            MenuManager.instance.GameOver();
            gameObject.SetActive(false);
            
        }
    }
}
