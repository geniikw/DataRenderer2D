# DataRenderer2D 

DataRenderer2D is a simple drawing tool. you can make mesh from data and control it using animator.
all of PR, bug report, comment, suggestion are vary grateful.

[PatchNote](https://github.com/geniikw/SplineMeshDrawer-PatchNote/blob/master/PatchNode.md)

[AssetStore(free)](https://assetstore.unity.com/packages/tools/modeling/data-renderer-2d-102377)

## Script Edit
If you want to control by script, you have to call GeometryUpdateFlagup() after adjust value.
```csharp
public UILine line;
public float time;

IEnumerator Coroutine(){
   var t = 0f;
   while(t < 1f)
   {
      t+=Time.deltaTime/time;
      line.line.option.endRatio = t;
      line.UpdateGeometryFlagUp();
      yield return null;
   }
}
```

## Bezier line
- Each node has control point and width.
- set line drawing rate using start rate and end rate.

![bezier](https://github.com/geniikw/SplineMeshDrawer-PatchNote/raw/master/bezier.gif)

<details>
<summary>
Explain(Korean)
</summary>
 이게 사실 메인입니다. 아래 것(?)들은 그냥 사은품정도로 생각하면 됩니다. 원래 이 에셋의 이름은 Spline mesh Drawer였습니다. 이 이름일 때가 훨씬 잘팔렸구요(...). 아래 다각형과 잡것들은 다른 에셋으로 하려다가 그냥 합쳐서 팔자 해서 DataRenderer2D로 바꾸고 합쳐버렸습니다. 그리고 판매량은 반토막
이름의 중요성을 깨닫는 순간입니다.</br>
 2D로 구성된 선을 생각하는 데로 그리는것이 목적입니다. 시작비율과 끝나는 비율, 각점에서 넓이 등을 커스터마이징 할 수 있습니다. 각 요소들을 Animator에서 조절하여 시각적으로 선을 그리는 효과를 보여주는게 목적이였습니다. 유니티에서 Animator로 움직이기 위해선 struct를 사용해야 합니다.
그래서 여러 문제들이 생겼는데 덕분에 코드가 개판(...). 뭐, 여러가지 경험을 하면 좋은거죠.</br>
 제일 놀랐던건 사용자분들중 여기에 텍스쳐를 입혀서 사용한 분입니다. sprite로 텍스쳐를 입히기위해 짱구를 굴려봤는데 아무리해도 uv잡는게 힘들어서 그냥 0~1로 만들었기 때문에 아틀라스로 표현하긴 불가능합니다. 그래도 여기에 텍스쳐를 입히고 광원을 줘서 나무를 그리고 있는 프로젝트를 봤는데
정말 멋지더군요.</br>
<img src="https://github.com/geniikw/SplineMeshDrawer-PatchNote/blob/master/textureline.png?raw=true" width="400" height="400">
</details>

## Polygon
- various method to draw polygon.
- count, scale, inner ratio.

![polygon](https://github.com/geniikw/SplineMeshDrawer-PatchNote/raw/master/polygon.gif)

<details>
<summary>
Explain(Korean)
</summary>
<p>
 기본적인 다각형을 그리는 녀석입니다. 지그재그로 다각형을 그리는 알고리즘엔 제법 짱구를 굴려서 만들었습니다.</br>
사실 Hole형식으로 한점에 저렇게 빡빡하게(?) 매쉬가 모이는 경우 어떤 디메리트가 있을 것 같아서 지그재그로 그린건데
지그재그의 경우 그라데이션을 적용하면 좀 이상하게 나오는 걸 확인해서 그냥 옵션으로 빼버리자 해서 이렇게 됬습니다.
뭐 어떤 방식이던 장단이 있겠죠.</br>
 다각형을 그릴때 시계방향으로 나오거나 사라지는 효과를 만들고 싶었습니다.(이유는 없습니다. 그저 만들고 싶었을 뿐). 처음에는 원을 기준으로 그렸는데 그리는 도중 다각형이 찌부러지는(...) 것을 확인했습니다. 지금은 잘 나오는데 다음 점으로 방향벡터를 구해서 영점에서 시작 각도와 끝나는 각도로의
방향벡터와 겹치는 점을 기준으로 그리고 있습니다. 이걸 쓰고 있는 저도 무슨말을 하는지 잘 모르겠으니 그냥 넘가셔도 됩니다. 아무튼 자연스럽게 없어지게 만드는건 성공했는데 이걸 뭐 어따 써야될지는 잘 모르겠습니다.
 이건 떨어진 면접에서 나온 이야기인데, 곧 각 변에 대하여 길이나 색상을 커스텀할 수 있게 하도록 옵션하나를 추가할 것 같습니다.
말이 좀 이상해서 이해하기 힘든데 예를들어 게임에서 보면 5각형으로 스텟을 보여주는 방식에 쓸 수 있도록 만들 예정입니다.
뭐, SKT에서 뱅만 KDA가 높아서 오각형을 뚫고 나오잖아요? 그런거 말하는 겁니다.
</p>
</details>

## Sinwave
![sin](https://github.com/geniikw/SplineMeshDrawer-PatchNote/raw/master/sin.gif)

<details>
<summary>
Explain(Korean)
</summary>
<p>
 네이버 유니티카페에서 질문을 받고 만든 것입니다. 간단한 모델이라 만드는데 1시간쯤 걸린 것 같습니다.
사실 그리 사용할 데가 애매한 녀석입니다. 물을 표현한다거나 할 때 쓸 수 있으나. 텍스쳐 같은건 꿈도 못꾸고...
만들면서 신호처리 때 배웠던 톱니파나 지그재그 등등 각동 시그널을 표현하도록 만들려고 했는데
수직으로 올라가는 패턴의 경우 매쉬를 다르게 해줘야 된다는 것 깨달은 동시에 포기했습니다. 어설프게
결국 sin파밖에 없는 애매한 녀석이 되었습니다.
</p>
</details>

## Hole
![hole](https://github.com/geniikw/SplineMeshDrawer-PatchNote/raw/master/hole2.gif)

<details>
<summary>
Explain(Korean)
</summary>
<p>
 그냥 만들고 싶어져서 만든 형식입니다. 사실 폴리곤에 반전형식으로 넣을까 했는데 따로 분리했습니다.
그냥보면 뻥뚫린 원입니다. 와이어프레임이 어떻게 되어 있나 볼 수 있는 gif입니다.</br>
<img src="https://github.com/geniikw/SplineMeshDrawer-PatchNote/blob/master/holeexplain.gif?raw=true" width="400" height="400">
</br>보시면 안에있는 다각형의 각 꼭지점과 외부의 정사각형에 대하여 폴리곤을 그리고 있습니다.
대충 다음과 같은 식으로 폴리곤을 만듦니다. </br></br>
1. 외부 4변에서 다각형의 가장 가까운 점으로 세모를 그린다.</br>
2. 내부 각변에서 가장 가깝게 바라보고 있는 외부 4점중 하나와 세모를 그린다.</br></br>

2의 경우 정확하게 가운데에서 그릴경우 내부변에서 어디로 세모를 그릴지 몰라서 버그가 발생하는데
언젠간 고치겠습니다. 
</p>
</details>
