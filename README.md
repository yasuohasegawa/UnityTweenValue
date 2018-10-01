# UnityTweenValue
The UnityTweenValue is a simple utility that only returns one easing value.<br>

### exmaple code

no timescale

```
Vector3 pos = obj.transform.position;
tw = TweenValue.Instance.AddTween(pos.x, 0f, 0.3f, 1.0f, false, (float val) => {
    // get tween value from update callback
    pos.x = val;
    obj.transform.position = pos;
}, (float val) => {
    // complete callback
}, Easing.easeInOutCubic);
```

use timescale

```
Vector3 pos = obj.transform.position;
Time.timeScale = 0.5f;
tw = TweenValue.Instance.AddTween(pos.x, 0f, 0.3f, 1.0f, true, (float val) => {
    // get tween value from update callback
    pos.x = val;
    obj.transform.position = pos;
}, (float val) => {
    // complete callback
}, Easing.easeInOutCubic);
```

It only tested on Windows 10 with Unity 2018.2.4f1.
