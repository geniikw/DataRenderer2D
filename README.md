# DataRenderer2D 

DataRenderer2D is a simple drawing tool. you can make mesh from data and control it using animator.
all of PR, bug report, comment, sugestion are vary grateful.

[PatchNote](https://github.com/geniikw/SplineMeshDrawer-PatchNote/blob/master/PatchNode.md)

[AssetStore(free)](https://assetstore.unity.com/packages/tools/modeling/data-renderer-2d-102377)

## Bezier line
- Each node has control point and width.
- set line drawing rate using start rate and end rate.

![bezier](https://github.com/geniikw/SplineMeshDrawer-PatchNote/raw/master/bezier.gif)

## Polygon
- various method to draw polygon.
- count, scale, inner ratio.

![polygon](https://github.com/geniikw/SplineMeshDrawer-PatchNote/raw/master/polygon.gif)

## Sinwave
![sin](https://github.com/geniikw/SplineMeshDrawer-PatchNote/raw/master/sin.gif)

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
알고리즘상 고칠수가 없네요...
</p>
