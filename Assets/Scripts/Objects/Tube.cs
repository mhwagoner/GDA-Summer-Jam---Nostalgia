using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Tube : MonoBehaviour
{
    [SerializeField] private ScoreCollider scoreCollider;
    [SerializeField] private GameObject entryCollider;
    public int numBalls;
    private List<Object> _balls = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreCollider.OnObjectEnter += AddBall; 
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AddBall(Object obj)
    {
        //Debug.Log("Added ball");
        _balls.Add(obj);
        obj.canScore = false;
        if (_balls.Count >= numBalls)
        {
            StartCoroutine(WaitAndResetBalls());
        }
    }

    private IEnumerator WaitAndResetBalls()
    {
        yield return new WaitForSeconds(4);
        ResetBalls();
    }

    private void ResetBalls()
    {
        foreach(Object ball in _balls)
        {
            LayerMask layerToBlock = (1 << entryCollider.layer);
            ball.GetComponent<CircleCollider2D>().excludeLayers |= layerToBlock;
            ball.ResetObject();
        }
        _balls.Clear();
    }
}
