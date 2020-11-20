
# <center>促销服务</center>
## 促销服务
促销服务维护着所有促销信息,核心业务为:
- 促销基础信息的维护
- 设置参与促销的商品

## 架构模式
![avatar](/img/促销中心架构图.png)

本微服务采用简单的数据驱动的CRUD微服务架构，来执行促销活动的创建、读取、更新和删除（CRUD）操作。这种类型的服务在单个 ASP.NET Core Web API 项目中即可实现所有功能，该项目包括数据模型类、业务逻辑类及其数据访问类。
![avatar](/img/solution.png)


## 核心技术选型：
- ☑ Net Core3.1
- ☑ ASP.NET Core Web API
- ☑ Entity Framework Core
- ☑ Autofac
- ☑ MSSQL
- ☑ MySQL（可选）
- ☑ XUnitTest
- ☑ Serilog
- ☑ Swagger & Swashbuckle(Swagger API文档)
- ☑ Rest架构风格API

## 实体建模&数据库结构

### 实体建模
![avatar](/img/class.png)

### 数据库表结构

![avatar](/img/sql.png)
Promotion(活动基础表)

字段 | 类型 | 说明|
--- | --- | --- |
Id | int|活动id,自增id|
Title | nvarchar(100) not null|活动标题|
**_Description_** | **_nvarchar(max)_**|**_活动说明_**|
PromotionTypeId | int not null|活动类型,详见PromotionType|
CreatedOnUtc|datetime not null|活动创建时间|
CreatedCustomerId|id not null|活动创建人|
UpdatedOnUtc|datetime not null|活动更新时间,默认为创建时间|
UpdatedCustomerId|id not null|活动更新人,默认为创建人|
StartDate | datetime not null|活动生效时间|
EndDate | datetime not null|活动结束时间|
PromotionStateId|int not null|活动状态,详见PromotionState|
Deleted | bit NOT NULL|活动是否删除|
**_PromotionProductTypeId_** | **_int not null_**|**_活动商品类型,详见PromotionProductType_**|



PromotionProduct(活动商品)

字段 | 类型 | 说明|
--- | --- | --- |
Id | int|自增id|
PromotionId | int not null|活id|
ProductId | int not null|商品id|
**~~_DiscountTypeId_~~**| **~~_int not null_~~** |**~~_折扣类型,详见DiscountType_~~**|
**~~_DiscountAmount_~~** | **~~_[decimal](18, 4) null_~~**|**~~_折抵或折减的值_~~**|
Price | [decimal](18, 4) null|活动价|
StockQuantity | int null|活动库存|
Deleted | bit NOT NULL|活动商品是否删除|



PromotionType(活动类型-枚举)

字段 | 类型 | 
--- | --- | 
Id | int|
Type | nvarchar(100)|

eg:

id|type|
---|---|
1|限时折扣|
2|新用户专享|
3|满额减|
4|满额折|
5|满件减|
6|满件折|

PromotionState(活动状态-枚举)

字段 | 类型 | 
--- | --- | 
Id | int|
State | nvarchar(20)|

eg:

id|state|
---|---|
1|已创建|
2|已发布|
3|已结束|
4|已到期|


**~~_DiscountType(折减类型-枚举)_~~**
>感觉这里使用不合适，应该另一个维度的，之后的版本再考虑

字段 | 类型 | 
--- | --- | 
Id | int|
Type | nvarchar(100)|

eg:

id|type|
---|---|
0|None无|
1|Discount折抵--DiscountAmount值为10代表1折|
2|CashDiscount折减--DiscountAmount值为10代表折减10块钱|

**_PromotionProductType(活动商品类型-枚举)_**
> 多个商品/一个商品/全馆

字段 | 类型 | 
--- | --- | 
Id | int|
Type | nvarchar(100)|

eg:

id|type|
---|---|
1|可以选多个商品|
2|可以选一个商品|
3|可以全部商品，此项不用选商品，第一版本不支援此项，后边在支持|


##  API接口
- ☑查询(all或ids)
- ☑修改
- ☑新增
- ☑查询 By Id
- ☑逻辑删除活动
- ☑发布活动
- ☑设置活动商品(商品Id,活动价,活动库存)
- ☑查询某个活动下的所有活动商品
- ☑删除商品
- ☑编辑活动商品(活动价,活动库存)
- ☑根据商品查询参加的活动

![avatar](/img/swagger.png)



