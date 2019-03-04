# RushDevelopFrameWork

A FrameWork for Development of Unity3D，and which is [Progressive].

一套用于辅助Unity3D开发的渐进式框架，旨在帮助开发者快捷高效的完成开发任务。[RushDevelopFrameWork]

本框架使用了MVC的设计思想，其中Model层的实现与否，视具体业务逻辑而定。

主要贯穿逻辑：
![逻辑关系图](https://raw.githubusercontent.com/Laughing111/RushDevelopFrameWork/image/image/%E9%80%BB%E8%BE%91%E5%9B%BE.jpg)

框架提供的方法：

（1）序列化Unity-Hierarchy下，UI面板下的物体为枚举变量，供编程调用。

	 选中Hierarchy下的UI节点，按住Shift的同时，右键，在弹出的菜单面板中，选择Serialized2Sys，即
可为该UI面板创建对应的子物体的枚举变量。实际调用过程中，可以通过 " PanelAssets_[面板名字].子物体名字"进行调用。

（2）自动添加UI面板(View)根节点类的脚本，类名为面板名，并继承于UIBase,UIBase继承于MonoBehaviour。

	选中Hierarchy下的UI节点，按住Shift的同时，右键，在弹出的菜单面板中，选择CreatePanelScripts,
框架会自动在Assets/Scripts下，添加[面板名.cs]文件。

（3）根据UI资源文件，自动创建并排版UI面板。（此功能需要与本框架指定的UI命名规定，和PhotoShop的AutoCutPng.javascripts
一起使用，需要请联系作者邮箱：244215913@qq.com）

     在顶部菜单栏的R.D.Tools中，选择UIPageLayOut，将弹出相应的UI自动布局菜单，指定好正确的UIRoot(Canvas节点)，
和UIAssetsUrl(UI资源所在的文件夹)，点击加载UI信息，提示成功加载后，点击自动布局，即可完成布局。

（4）本框架将在编辑器内不断更新。

	选择R.D.Tools/VersionManager/CheckUpdate即可检查更新，更新完成后，将自动覆盖旧版本。
更多功能，将在ReadMe.md中保持更新，敬请关注...

脚本使用范例：

UI面板类提供的功能：

（1）提供注册方法，对本面板下的子物体，进行用户交互事件的注册。

（2）提供添加信件方法，对本面板下的子物体和本面板，定义供其他面板或其他类调用的方法。

（3）提供面板的生命周期，唤醒,初始化，隐藏等，部分生命周期，暴露于外，可供用户于Inspector上进行修改。

（4）提供面板的无人操作倒计时功能。

（5）提供面板间的信件传阅方法（调用其他面板中的方法），无需获得其他面板的引用实例，即可完成其他面板方法的调用。




