# Space-Motion
## 游戏对象运动的本质是什么？
游戏对象运动的本质，其实是游戏对象跟随每一帧的变化，空间地变化。这里的空间变化包括了游戏对象的transform属性中的position跟rotation两个属性。一个是绝对或者相对位置的改变，一个是所处位置的角度的旋转变化

## 请用三种方法以上方法，实现物体的抛物线运动。（如，修改Transform属性，使用向量Vector3的方法…
1.第一种方式是利用position的改变实现位置变化，定义一个初速度Vec3，以及一个受重力向下的速度Vector3，两个速度不同方向，在物体运动期间改变每一帧物体运动的速度方向

```C#
using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
  
public class move1 : MonoBehaviour {  
    public float Power = 10;//the power 
	  public float Angle = 60;//the angle
	  public float Gravity = -10;//g
  
    private Vector3 MoveSpeedStart;//vector of the start speed
	  private Vector3 GravitySpeed = Vector3.zero;//vector of g
    // Use this for initialization  
    void Start () {  
        Debug.Log("start!");  
        MoveSpeedStart = Quaternion.Euler (new Vector3 (0, 0, Angle)) * Vector3.right * Power;
    }  
      
    // Update is called once per frame  
    void Update () {  
      GravitySpeed.y = Gravity * (time += Time.fixedDeltaTime);
		  //Move
		  transform.position += (MoveSpeedStart + GravitySpeed) * Time.fixedDeltaTime; 
    }  
}
```

1.第二种方式是利用transform.Translate的改变实现位置变化，定义一个初速度Vec3，以及一个受重力向下的速度Vector3，两个速度不同方向，在物体运动期间改变每一帧物体运动的速度方向

```C#
using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
  
public class move1 : MonoBehaviour {  
    public float Power = 10;//the power 
	  public float Angle = 60;//the angle
	  public float Gravity = -10;//g
  
    private Vector3 MoveSpeedStart;//vector of the start speed
	  private Vector3 GravitySpeed = Vector3.zero;//vector of g
    // Use this for initialization  
    void Start () {  
        Debug.Log("start!");  
        MoveSpeedStart = Quaternion.Euler (new Vector3 (0, 0, Angle)) * Vector3.right * Power;
    }  
      
    // Update is called once per frame  
    void Update () {  
      GravitySpeed.y = Gravity * (time += Time.fixedDeltaTime);
		  //Move
		  transform.Translate(MoveSpeedStart * Time.fixedDeltaTime);
		  transform.Translate(GravitySpeed * Time.fixedDeltaTime);
    }  
}
```

第三种方法是直接声明创建一个Vector3变量，同时定义该变量的值，也是竖直方向上是一个均匀增加的数值，水平方向是一个保持不变的数值，然后将游戏对象原本的position属性与该向量相加即可实现抛物线运动

```C#
using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
  
public class move2 : MonoBehaviour {  
  
    public float speed = 1;  
    // Use this for initialization  
    void Start () {  
        Debug.Log("start!");  
    }  
      
    // Update is called once per frame  
    void Update () {  
  
        Vector3 change = new Vector3( Time.deltaTime*5, -Time.deltaTime*(speed/10), 0);  
        this.transform.position += change;  
        speed++;  
    }  
}
```

## 写一个程序，实现一个完整的太阳系， 其他星球围绕太阳的转速必须不一样，且不在一个法平面上。
将八大行星的运动写进同个脚本里面，挂在Sun对象上即可。首先利用GameObject.Find函数找到该游戏对象，再通过其transform函数中的RotationAround跟Rotation函数分别来实现公转跟自转。

RotationAround需要三个参数，第一个参数是旋转的中心，这个八大行星都是以太阳中心为旋转中心；第二个参数是旋转轴，也就是一个Vector3变量，通过改变旋转轴的属性。

Rotation函数则可以只需要一个参数，即旋转时的方向及速度，用Vector3.up代表该物体是沿着自己的Y轴进行旋转的，后面的参数则是代表旋转的角速度，因此即可实现自转。

## 编程实践 牧师与魔鬼

### 列出游戏中提及的事物（Objects）

魔鬼，牧师，船，河流，两边的陆地

### 用表格列出玩家动作表（规则表），注意，动作越少越好
