
# <center>促销服务</center>
## 促销服务
促销服务维护着所有促销信息,核心业务为:
- 促销基础信息的维护
- 设置促销规则
- 设置参与促销的商品

## 架构模式
![avatar](/img/促销中心架构图.png)

本微服务采用简单的数据驱动的CRUD微服务架构，来执行促销活动的创建、读取、更新和删除（CRUD）操作。这种类型的服务在单个 ASP.NET Core Web API 项目中即可实现所有功能，该项目包括数据模型类、业务逻辑类及其数据访问类。

## 核心技术选型：
- Net Core3.1
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Autofac
- Serilog
- Swagger
- Swashbuckle(Swagger API文档)(可选)
- Dapper（可选）
- Redis（可选）
- UnitTest（可选）
- Docker（可选）

## 实体建模
- 还未开始

## 数据库结构
- 还未开始




##  API接口
### 买一送一接口
- 新增
- 修改
- 删除
- 查询 By Id
- 查询
- 上下架
- 设置活动商品 productids

### 满额送接口
- 新增
- 修改
- 删除
- 查询 By Id
- 查询
- 上下架
- 设置活动商品 productids
- 设置满额送规则

### 额外的商品查询接口
- 根据单个商品查询参与活动信息(商品详情)
- 根据多个商品查询参与活动信息(购物车/下单)



