using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tweenvalue;

public class TweenTest : MonoBehaviour {

    public GameObject obj;
    private Tween tw;

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            DoTweenTest();
            //tw.CancelAndStop();
        }
    }

    private void DoTweenTest()
    {
        //Time.timeScale = 0.5f;
        Vector3 pos = obj.transform.position;
        tw = TweenValue.Instance.AddTween(pos.x, 0f, 0.3f, 1.0f, false, (float val) => {
            // get tween value from update callback
            pos.x = val;
            obj.transform.position = pos;
        }, (float val) => {
            // complete callback
        }, Easing.easeInOutCubic);
    }
}
