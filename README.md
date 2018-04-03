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

| 事件         | 条件           |
| ------------ |:-------------:|
| 开船         | 船在开始岸，船在结束岸，船上至少有一个人 |
| 船的左方下船     | 船靠岸且船左方有对象      |
| 船的右方下船| 船靠岸且船右方有对象      |
|开始岸的牧师上船| 船在开始岸，船有空位，开始岸有牧师 |
|开始岸的魔鬼上船| 船在开始岸，船有空位，开始岸有魔鬼 |
|结束岸的牧师上船| 船在结束岸，船有空位，结束岸有牧师 |
|结束岸的魔鬼上船| 船在结束岸，船有空位，结束岸有魔鬼 |

相关类说明：

SSDirector利用单例模式创建导演，这里继承于System.Object，保持导演类一直存在，不被Unity内存管理而管理

ISceneController 
这是一个场景的控制器的接口，场景之间的切换，以及暂停与恢复都靠它操作，能操作的就是场景本身，不能对场景中具体对象进行操作

ISIUserAction 
用户动作的一个接口，用户通过键盘、鼠标等对游戏发出指令，这个指令会触发游戏中的一些行为，(比如在这个游戏中，点击角色让角色移动，这个角色移动就是用户动作后触发的行为)，由IUserAction来声明SActionCallback为动作事件接口，所有事件管理者都必须实现这个接口，来实现事件调度。所以，组合事件需要实现它，事件管理器也必须实现它

GenGameObjects
这是一个控制器，对场景中的具体对象进行操作（其实也是使用GameModels给出的函数进行控制），可以看到这个控制器继承了两个接口类并实现了它们的方法，控制器是场景中各游戏对象行为改变的核心

UserGUI 
建立用户的交互界面，比如按钮和标签

**为实现MVC的分离，我们在Assets文件夹下面新建三个文件夹，分别为：Materials（放置图片等一些游戏素材），Resources（放置游戏对象预制，同样在该文件夹下新建文件夹Prefabs，放置预制，方便脚本对游戏对象预制资源的加载），Scripts（放置游戏脚本）**

**GameModels**
游戏场景中可交互的游戏模型，在这个游戏中我建立了角色模型和船模型以及陆地模型给控制器留出函数供其调用

这里先讲一下关于Tag： 
Tag可以用来识别游戏对象,在Inspector中名字的下面可以看到Tag,官方文档中对于Tag的好处描述得很清楚，这里我是用每个GameObject的name来区分不同的对象的，因为GameObject比较少，如果比较多的话建议还是使用Tag。

**land(陆地模型)** 
1.先来想一下陆地模型应该具备什么属性吧，首先陆地有两块，所以我们需要一个标志位来记录是开始的陆地还是结束陆地，然后陆地的位置(因为陆地被创建后就不动了只用一次，所以我在创建时直接赋值给陆地的position所以没有记录)，以及陆地上的角色(用角色模型的队列来记录)，每个角色的位置(Queue的数组来记录)。

2.陆地应该有的函数：应该知道在自己陆地上牧师和魔鬼各自的数量，从陆地上移除一个角色，添加一个角色，知道陆地上哪一个是第一个空的位置(角色登陆的时候放上去)，游戏结束的重置等。

因为陆地的摆放是按照z轴对称的，所以x坐标是相反数，得到陆地空位置的时候就可以乘陆地的标志就行。

**ship(船模型)**

1.那再来想一下船模型应该具有什么属性：船在开始/结束陆地旁的位置，在开始/结束陆地旁船上可以载客的两个位置(用Vector3的数组表示)，船上载有的角色(用角色模型的数组来记录)，标记船在开始陆地还是结束陆地的旁边。

2.船应该有的函数：与陆地拥有的函数类似，增加了一个移动船的函数，让船到设定的位置

**RoleModel(角色模型)** 

1.一个角色的属性：标志角色是牧师还是恶魔，标志是否在船上 

2.角色模型的函数：去到陆地/船上(其实就是把哪个作为父节点，并且修改是否在船上标志)，其他就是基本的get/set函数。

3.如何加载预制？ 
首先用Resources.Load函数，第一个参数是你的预制体的路径（注意：路径名使用正斜杠“/”，如果使用反斜杠“\”会不正常运行），上面我们把预制全放在Prefabs文件夹里面，所以路径是“Prefabs/。。。”，第二个参数是要返回的对象类型，当然你也可以只写第一个参数但是需要强制转换成为其他对象类型。Resources.Load函数让我们用一个对象来保存我们加载好的预制体，但预制体没有载入到场景中，还未进行实例化。
然后使用Object.Instantiate函数，它传入一个你想要初始化的对象，可以设置位置，角度，和父节点的Transform等，这样一个实例化的预制体就出现在场景中了

***至于完整代码，请看GitHub***
