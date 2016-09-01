using System.Collections;
using UnityEngine;


public class GameObjectBehaviour : MonoBehaviour
{
    protected Rigidbody2D body;
    protected void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
}
