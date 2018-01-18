using System.Collections;
using UnityEngine.Events;
using UnityEngine;
public class MyButton : MonoBehaviour
{
    public UnityEvent signalOnClick = new UnityEvent();
    public int cl = 1;
    public void _onClick()
    {
        this.signalOnClick.Invoke();
        cl = 2;
    }

   
}