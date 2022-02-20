using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class snakemove : MonoBehaviour
{
    private Vector2 AreaLimit = new Vector2(42, 23);
    
    [SerializeField] private GameObject food;
    [SerializeField] private GameObject TailPrefabs;
    [SerializeField] private float speed = 1;
   
   
    private Vector2 _direction = Vector2.down;
    
    private List<Transform> _snake = new List<Transform>();

    public scores objec;
    public GameObject deathscreen;
    

    private bool _grow;

    private void Start()
    {
        ChangePositionFood();
        StartCoroutine(Move());
        _snake.Add(transform);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && _direction != Vector2.right)
            _direction = Vector2.left;
        if (Input.GetKeyDown(KeyCode.D) && _direction != Vector2.left)
            _direction = Vector2.right;
        if (Input.GetKeyDown(KeyCode.W) && _direction != Vector2.down)
            _direction = Vector2.up;
        if (Input.GetKeyDown(KeyCode.S) && _direction != Vector2.up)
            _direction = Vector2.down;
    }
   

    private IEnumerator Move()
    {
        while(true)
        {
            if (_grow)
            {
                _grow = false;
                Grow();
            }
            for (int i=_snake.Count-1;i>0;i--)
            {
                _snake[i].position = _snake[i - 1].position;
            }
            var position = transform.position;
            position += (Vector3)_direction;
            position.x = Mathf.RoundToInt(position.x);
            position.y = Mathf.RoundToInt(position.y);

            transform.position = position;

            yield return new WaitForSeconds(speed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("food"))
        {
            //Grow();
            _grow = true;
            objec.Updatescore();
        }
        if(collision.CompareTag("wall"))
        {
            Dead();
        }
        if(collision.CompareTag("fast"))
        {
            speed = speed / 1.2f;
        }
        if (collision.CompareTag("poison"))
        {
            Dead();
        }
    }
    private void Grow()
    {
        var tail = Instantiate(TailPrefabs);
        _snake.Add(tail.transform);
        _snake[_snake.Count - 1].position = _snake[_snake.Count - 2].position;
        ChangePositionFood();
    }
    private void ChangePositionFood()
    {
        Vector2 newFoodPosition;
        do
        {
            var x = (int)Random.Range(1, AreaLimit.x);
            var y = (int)Random.Range(1, AreaLimit.y);
            newFoodPosition = new Vector2(x, y);
        } while (!CanSpawnFood(newFoodPosition));

        food.transform.position = newFoodPosition;
    }
    private bool CanSpawnFood(Vector2 newposition)
    {
        foreach(var item in _snake)
        {
            var x = Mathf.RoundToInt(item.position.x);
            var y = Mathf.RoundToInt(item.position.y);
            if (new Vector2(x, y) == newposition)
                return false;
        }
        return true;

    }
    private void Dead()
    {
        deathscreen.SetActive(true);
        StopAllCoroutines();
    }
}
