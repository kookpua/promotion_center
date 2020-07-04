using DotNetCore.CAP;
using Huatek.Torch.Infrastructure.Core;
using Huatek.Torch.Promotion.Domain.PromotionAggregate;
using Huatek.Torch.Promotion.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Huatek.Torch.Promotion.Infrastructure
{
    public class PromotionContext : EFContext
    {
        public PromotionContext(DbContextOptions options, IMediator mediator=null,
            ICapPublisher capBus = null) : base(options, mediator, capBus)
        {

        }

        public DbSet<Promotions> Promotions { get; set; }
        public DbSet<PromotionProduct> PromotionProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 注册领域模型与数据库的映射关系
            modelBuilder.ApplyConfiguration(new PromotionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PromotionProductEntityTypeConfiguration());
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}


#region  code first
/*
 * 一、code first
 * 数据迁移，创建和更新功能：
  1. 搜索程序包 或 package manage
  2. 生成Migrations命令：add-migration initial || add-migration changesomething
  3. 生成数据的脚本的命令（正式环境）：script-migration
    PS：通常在生成环境使用
  4. 生成&更新数据库的命令（开发环境）：update-database -verbose
    PS：-verbose 生成过程中执行的一些明细
  5.Remove-Migration&Drop-Database 移除Migration和删除数据库
二、现成的数据库生成Code
。。。。。
 */
#endregion


#region  get-help entityframe
/*
 get-help entityframe

                     _/\__
               ---==/    \\
         ___  ___   |.    \|\
        | __|| __|  |  )   \\\
        | _| | _|   \_/ |  //|\\
        |___||_|       /   \\\/\\

TOPIC
    about_EntityFrameworkCore

SHORT DESCRIPTION
    Provides information about the Entity Framework Core Package Manager Console Tools.

LONG DESCRIPTION
    This topic describes the Entity Framework Core Package Manager Console Tools. See https://docs.efproject.net for
    information on Entity Framework Core.

    The following Entity Framework Core commands are available.

        Cmdlet                      Description
        --------------------------  ---------------------------------------------------
        Add-Migration               Adds a new migration.

        Drop-Database               Drops the database.

        Get-DbContext               Gets information about a DbContext type.

        Remove-Migration            Removes the last migration.

        Scaffold-DbContext          Scaffolds a DbContext and entity types for a database.

        Script-DbContext            Generates a SQL script from the current DbContext. 

        Script-Migration            Generates a SQL script from migrations.

        Update-Database             Updates the database to a specified migration.

SEE ALSO
    Add-Migration
    Drop-Database
    Get-DbContext
    Remove-Migration
    Scaffold-DbContext
    Script-DbContext
    Script-Migration
    Update-Database
 */
#endregion