
# <center>促销服务</center>
## 促销服务
促销服务维护着所有促销信息,核心业务为:
- 促销基础信息的维护
- 设置参与促销的商品

## 架构模式
![avatar](/img/促销中心架构图.png)

本微服务采用简单的数据驱动的CRUD微服务架构，来执行促销活动的创建、读取、更新和删除（CRUD）操作。这种类型的服务在单个 ASP.NET Core Web API 项目中即可实现所有功能，该项目包括数据模型类、业务逻辑类及其数据访问类。[待补充项目结构图]

## 核心技术选型：
- Net Core3.1
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server(**主从**)
- Autofac
- Serilog
- Swagger
- Docker(Linux X86&**ARM**)
- Swashbuckle(Swagger API文档)(可选)
- Dapper（可选）
- Redis（可选）
- UnitTest（可选）

## 实体建模&数据库结构

Promotion(活动基础表)

字段 | 类型 | 说明
--- | --- | --- |
Id | int|活动id,自增id
Title | nvarchar(100) notnull|活动标题
Description | nvarchar(max)|活动详情
PromotionTypeId | int notnull|活动类型,详见PromotionType
CreatedOnUtc|datetime notnull|活动创建时间
CreatedCustomerId|id notnull|活动创建人
UpdatedOnUtc|datetime notnull|活动更新时间,默认为创建时间
UpdatedCustomerId|id notnull|活动更新人,默认为创建人
StartDate | datetime notnull|活动生效时间
EndDate | datetime notnull|活动结束时间
PromotionStateId|int not null|活动状态,详见PromotionState
Deleted | bit NOT NULL|活动是否删除


PromotionProduct(活动商品)
字段 | 类型 | 说明
--- | --- | --- |
Id | int|自增id
PromotionId | int notnull|活id
ProductId | int notnull|商品id
Price | [decimal](18, 4) null|活动价
StockQuantity | int null|活动库存
Deleted | bit NOT NULL|活动商品是否删除



PromotionType(活动类型-枚举)
字段 | 类型 | 
--- | --- | 
Id | int|
Type | nvarchar(100)|

eg:
id|type
---|---|
1|限时折扣
2|新用户专享
3|满额减
4|满额折
5|满件减
6|满件折

PromotionState(活动状态-枚举)
字段 | 类型 | 
--- | --- | 
Id | int|
State | nvarchar(20)|

eg:
id|state
---|---|
1|已创建
2|已发布
3|已结束
4|已到期



##  API接口
- 新增
- 修改
- 删除
- 查询 By Id
- 查询 All
- 活动发布
- 设置活动商品(商品Id,活动价,活动库存)



