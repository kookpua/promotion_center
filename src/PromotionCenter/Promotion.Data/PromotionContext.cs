using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Promotion.Domin;
using Microsoft.Extensions.Logging;

namespace Promotion.Data
{
    public class PromotionContext:DbContext
    {
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
        public PromotionContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLoggerFactory(ConsoleLoggerFactory);
        //}

        public  DbSet<Promotions> Promotions { get; set; }
        public DbSet<PromotionProduct> PromotionProducts { get; set; }

        //public static readonly ILoggerFactory ConsoleLoggerFactory = 
        //    LoggerFactory.Create(builder=> {
        //        builder.AddFilter((category,level)=>
        //        category==DbLoggerCategory.Database.Command.Name
        //        && level==LogLevel.Information)
        //        .AddConsole();
        //});
    }
}
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
