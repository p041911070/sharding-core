﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Sample.Migrations.EFCores;
using ShardingCore;
using ShardingCore.Bootstrappers;
using ShardingCore.TableExists;
using ShardingCore.TableExists.Abstractions;

namespace Sample.Migrations
{
    public class DefaultDesignTimeDbContextFactory: IDesignTimeDbContextFactory<DefaultShardingTableDbContext>
    {
        private static IServiceProvider _serviceProvider;
        static DefaultDesignTimeDbContextFactory()
        {
            var services = new ServiceCollection();
            services.AddShardingDbContext<DefaultShardingTableDbContext>()
                .AddEntityConfig(o =>
                {
                    o.AddShardingTableRoute<ShardingWithModVirtualTableRoute>();
                    o.AddShardingTableRoute<ShardingWithDateTimeVirtualTableRoute>();
                })
                .AddConfig(op =>
                {
                    op.UseShardingQuery((conStr, builder) =>
                    {
                        builder.UseSqlServer(conStr)
                            .ReplaceService<IMigrationsSqlGenerator, ShardingSqlServerMigrationsSqlGenerator<DefaultShardingTableDbContext>>()
                            .ReplaceService<IMigrationsModelDiffer, RemoveForeignKeyMigrationsModelDiffer>();
                    });
                    op.UseShardingTransaction((connection, builder) =>
                    {
                        builder.UseSqlServer(connection);
                    });
                    op.AddDefaultDataSource("ds0", "Data Source=localhost;Initial Catalog=ShardingCoreDBMigration;Integrated Security=True;");
                   
                }).ReplaceService<ITableEnsureManager,SqlServerTableEnsureManager>().EnsureConfig();
            _serviceProvider = services.BuildServiceProvider();
        }

        public DefaultShardingTableDbContext CreateDbContext(string[] args)
        {
            return _serviceProvider.GetService<DefaultShardingTableDbContext>();
        }
    }
}
