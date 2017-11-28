﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Data;

namespace ToDo_App.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20171127190120_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Data.Todo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAssigned");

                    b.Property<bool>("HasReminder");

                    b.Property<string>("Note");

                    b.Property<bool>("Recycled");

                    b.Property<DateTime>("Reminder");

                    b.HasKey("Id");

                    b.ToTable("Todos");
                });
        }
    }
}
