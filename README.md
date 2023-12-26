# 2023/12/26 我重新打开了这个仓库，这个你拿来应付作业可以，想要正经能看的请在github搜索abp 很多个star和fork那个


# RepositorySystem

这是我的课程大作业《仓库管理系统》

基于.NetFramework + [layuimini](https://github.com/zhongshaofa/layuimini#layuimini%E5%90%8E%E5%8F%B0%E6%A8%A1%E6%9D%BF)（前端）后台模板开发。[我还使用.net core移植了一份](https://github.com/DearIcer/RepositorySystemDotNetCore)。

# 如何开始
先初始化菜单接口

在\Views\Home\Index.cshtml 把第一行初始化接口解除注释，把第二个后端接口注释

```
var options = {
                iniUrl: "../layuimini/api/init.json",    // 初始化接口
                /*iniUrl: "/MenuInfo/GetMenus",    // 初始化接口*/
                clearUrl: "../layuimini/api/clear.json", // 缓存清理接口
                urlHashLocation: true,      // 是否打开hash定位
                bgColorDefault: false,      // 主题默认配置
                multiModule: true,          // 是否开启多模块
                menuChildOpen: false,       // 是否默认展开菜单
                loadingTime: 0,             // 初始化加载时间
                pageAnim: true,             // iframe窗口动画
                maxTabNum: 20,              // 最大的tab打开数量
            };
            miniAdmin.render(options);
```
在Global.asax打开初始化数据这个函数，添加数据库用完务必删除这个函数，在正式发布的版本中不允许存在这个函数
```
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //只需要执行一次，用完记得注释！
            InitDB();
        }
```
有了初始用户后即可登录后台，添加后端接口菜单的数据
![菜单接口数据][菜单页面]

[菜单页面]: /docs/img/QQ截图20230730155657.png

参考数据源
```
0c9ff2cb-10fb-4f42-ab7b-fa744fd2c631	步骤管理	-	1	1	/WorkFlow_InstanceStep/ListView	NULL	fa fa-street-view	_self	False	NULL	2023-07-20 00:50:41.063
1f361caf-7f00-4ae7-93c6-9429c63140e8	耗材记录日志	NULL	1	1	/ConsumableRecord/ListView	NULL	NULL	_self	False	NULL	2023-07-12 13:24:12.683
24ec9998-cdfc-4e05-8e11-396e73cff0ab	工作流模板管理	NULL	1	1	/WorkFlow_Model/ListView	NULL	fa fa-street-view	_self	False	NULL	2023-07-17 15:13:11.443
2efa6283-37bd-498b-bf46-fa6c7113b3a5	系统管理	系统管理	1	1		-	fa fa-cog		False	NULL	2023-07-10 14:48:29.477
4e833a96-97f8-4fd8-8dbb-08d6b10be11f	部门管理	-	1	1	/DepartmentInfo/ListView	-	fa fa-sitemap	_self	False	NULL	2023-07-10 18:55:41.430
5e8be128-8d29-4de1-9842-9b36f02aeba2	工作流实例	NULL	1	1	/WorkFlow_Instance/ListView	NULL	fa fa-street-view	_self	False	NULL	2023-07-17 16:37:38.657
7ee01a0a-4d3f-41cd-a5cf-4284dc3f962d	角色管理	----	2	2	/RoleInfo/ListView	2efa6283-37bd-498b-bf46-fa6c7113b3a5	fa fa-street-view	_self	False	2023-07-10 18:53:16.643	2023-07-10 14:51:16.183
87ee527b-8f02-4aad-8f39-1e8214092b9a	用户管理	-	1	1	/UserInfo/ListView	-	fa fa-user	_self	False	2023-07-10 18:53:13.847	2023-07-10 14:49:20.437
8fdc1456-c433-402d-aec1-8dcad8fc2c2e	耗材类别管理	NULL	1	1	/Category/ListView	NULL	NULL	_self	False	NULL	2023-07-11 18:42:17.330
bc54325a-39cf-4020-bff2-f48df7e1389b	耗材管理	-	1	1	/ConsumableInfo/ListView	NULL	 -	_self	False	NULL	2023-07-12 15:07:56.197
cff5de54-2d6d-4d06-8ff1-51e1c8e67f6d	菜单管理	-	2	2	/MenuInfo/ListView	2efa6283-37bd-498b-bf46-fa6c7113b3a5	fa fa-street-view	_self	False	2023-07-10 18:53:20.807	2023-07-10 14:51:52.320
```
然后新建个角色并绑定上用户和菜单
![角色接口数据][角色页面]

[角色页面]: /docs/img/QQ截图20230730160219.png

回到菜单接口

在\Views\Home\Index.cshtml 把第一行删除，把第二行解除注释，更换成后端的数据接口

```
var options = {
                iniUrl: "/MenuInfo/GetMenus",    // 初始化接口
                clearUrl: "../layuimini/api/clear.json", // 缓存清理接口
                urlHashLocation: true,      // 是否打开hash定位
                bgColorDefault: false,      // 主题默认配置
                multiModule: true,          // 是否开启多模块
                menuChildOpen: false,       // 是否默认展开菜单
                loadingTime: 0,             // 初始化加载时间
                pageAnim: true,             // iframe窗口动画
                maxTabNum: 20,              // 最大的tab打开数量
            };
            miniAdmin.render(options);
```
到此基础使用配置完成

我现在在找实习，有大佬看上我的话可以联系邮箱3508498289@qq.com 不限城市！
# 大纲

# 物资管理系统

## 登录模块

### 前端页面

### 后端登录接口

## 首页模块

## 菜单模块

### 菜单管理前端页面

- 菜单添加前端页面
- 删除菜单

### 菜单后端数据接口

## 角色模块

### 角色菜单前端页面

- 角色菜单添加页面
- 删除角色

### 菜单后端数据接口

## 耗材记录模块

### 耗材记录前端页面

### 耗材记录后端接口数据返回

## 耗材分类模块

### 耗材分类前端页面

- 添加分类前端页面
- 删除分类

### 耗材分类后端数据接口

## 工作步骤模块

### 工作步骤前端页面

- 工作步骤申请前端页面
- 审核步骤

### 工作步骤后端数据接口

## 工作模板模块

### 工作模板前端页面

- 添加工作模板前端页面
- 删除工作模板

### 工作模板后端接口

## 工作实例模块

### 工作实例前端页面

- 申请耗材前端页面
- 作废

### 工作实例后端数据接口

## 耗材申请流程

### 由普通员工发起

- 员工部门领导审核

  - 仓库管理员审核

    - 出库
    - 仓管驳回

  - 领导驳回

### 领导发起

- 领导审核

  - 仓库管理员审核

    - 出库
    - 仓管驳回

## 首页菜单加载流程

### 验证登录账号

- 查询菜单接口

  - 查询数据库，根据用户所绑定的菜单，返回对应的数据

    - 构建菜单树

      - 返回前端页面进行加载
