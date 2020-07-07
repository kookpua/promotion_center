# 迁移
[参考微软文档](https://docs.microsoft.com/zh-cn/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)


## 通过 .NET Core CLI创建迁移:
1. 生成迁移文件,在src\Servers\Promotion下执行:
>dotnet ef migrations add InitialCreate  -s Huatek.Torch.Promotions.API -p Huatek.Torch.Promotions.Infrastructure

如果缺少ef执行:
> dotnet tool install --global dotnet-ef

如果缺少Microsoft.EntityFrameworkCore.Design执行:

> dotnet add package Microsoft.EntityFrameworkCore.Design

2. 更新数据库
> dotnet ef database update -s Huatek.Torch.Promotions.API -p Huatek.Torch.Promotions.Infrastructure

3. 运行(进入 Huatek.Torch.Promotions.API 目录)
>dotnet run

4. 删除迁移
>dotnet ef migrations remove -s Huatek.Torch.Promotions.API -p Huatek.Torch.Promotions.Infrastructure

5.生成SQL脚本
>dotnet ef migrations script -s Huatek.Torch.Promotions.API -p Huatek.Torch.Promotions.Infrastructure

## 通过VS2019创建迁移

1. 搜索程序包 或 package manage

2. 生成Migrations命令

> Add-Migration InitialCreate

3. 生成数据的脚本的命令（正式环境)

> script-migration

4. 成&更新数据库的命令（开发环境）

> update-database

5. 移除Migration 或删除数据库

>Remove-Migration
 Drop-Database

## Run 
[linux-docker-run .net core](https://www.cnblogs.com/nickchou/p/11810938.html)
[linux-docker-run .net core](https://github.com/kookpua/DockerDemo)
[vagrant](https://vagrant.ninghao.net/synced-folder--basic.html)
