﻿// <auto-generated />
using System;
using IFrameworkDemo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IFrameworkDemo.Migrations
{
    [DbContext(typeof(DemoDbContext))]
    [Migration("20180830071720_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IFramework.MessageStores.Relational.Command", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CorrelationId")
                        .HasMaxLength(200);

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("IP");

                    b.Property<string>("MessageBody")
                        .HasColumnType("ntext");

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<string>("Producer");

                    b.Property<string>("Result");

                    b.Property<string>("ResultType");

                    b.Property<int>("Status");

                    b.Property<string>("Topic")
                        .HasMaxLength(200);

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("msgs_Commands");
                });

            modelBuilder.Entity("IFramework.MessageStores.Relational.Event", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AggregateRootId")
                        .HasMaxLength(200);

                    b.Property<string>("AggregateRootType");

                    b.Property<string>("CorrelationId")
                        .HasMaxLength(200);

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("IP");

                    b.Property<string>("MessageBody");

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<string>("Producer");

                    b.Property<string>("Topic")
                        .HasMaxLength(200);

                    b.Property<string>("Type");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("AggregateRootId");

                    b.HasIndex("CorrelationId");

                    b.HasIndex("Name");

                    b.ToTable("msgs_Events");
                });

            modelBuilder.Entity("IFramework.MessageStores.Relational.HandledEvent", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("SubscriptionName")
                        .HasMaxLength(322);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("HandledTime");

                    b.HasKey("Id", "SubscriptionName");

                    b.ToTable("msgs_HandledEvents");

                    b.HasDiscriminator<string>("Discriminator").HasValue("HandledEvent");
                });

            modelBuilder.Entity("IFramework.MessageStores.Relational.UnPublishedEvent", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CorrelationId");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Ip");

                    b.Property<string>("MessageBody")
                        .HasColumnType("ntext");

                    b.Property<string>("Name");

                    b.Property<string>("Producer");

                    b.Property<string>("ReplyToEndPoint");

                    b.Property<string>("Topic");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("msgs_UnPublishedEvents");
                });

            modelBuilder.Entity("IFramework.MessageStores.Relational.UnSentCommand", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CorrelationId");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Ip");

                    b.Property<string>("MessageBody")
                        .HasColumnType("ntext");

                    b.Property<string>("Name");

                    b.Property<string>("Producer");

                    b.Property<string>("ReplyToEndPoint");

                    b.Property<string>("Topic");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("msgs_UnSentCommands");
                });

            modelBuilder.Entity("IFrameworkDemo.Models.Blog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("Title")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("IFramework.MessageStores.Relational.FailHandledEvent", b =>
                {
                    b.HasBaseType("IFramework.MessageStores.Relational.HandledEvent");

                    b.Property<string>("Error");

                    b.Property<string>("StackTrace");

                    b.ToTable("FailHandledEvent");

                    b.HasDiscriminator().HasValue("FailHandledEvent");
                });

            modelBuilder.Entity("IFramework.MessageStores.Relational.Command", b =>
                {
                    b.OwnsOne("IFramework.Message.Impl.SagaInfo", "SagaInfo", b1 =>
                        {
                            b1.Property<string>("CommandId");

                            b1.Property<string>("ReplyEndPoint");

                            b1.Property<string>("SagaId");

                            b1.ToTable("msgs_Commands");

                            b1.HasOne("IFramework.MessageStores.Relational.Command")
                                .WithOne("SagaInfo")
                                .HasForeignKey("IFramework.Message.Impl.SagaInfo", "CommandId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("IFramework.MessageStores.Relational.Event", b =>
                {
                    b.OwnsOne("IFramework.Message.Impl.SagaInfo", "SagaInfo", b1 =>
                        {
                            b1.Property<string>("EventId");

                            b1.Property<string>("ReplyEndPoint");

                            b1.Property<string>("SagaId");

                            b1.ToTable("msgs_Events");

                            b1.HasOne("IFramework.MessageStores.Relational.Event")
                                .WithOne("SagaInfo")
                                .HasForeignKey("IFramework.Message.Impl.SagaInfo", "EventId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("IFramework.MessageStores.Relational.UnPublishedEvent", b =>
                {
                    b.OwnsOne("IFramework.Message.Impl.SagaInfo", "SagaInfo", b1 =>
                        {
                            b1.Property<string>("UnPublishedEventId");

                            b1.Property<string>("ReplyEndPoint");

                            b1.Property<string>("SagaId");

                            b1.ToTable("msgs_UnPublishedEvents");

                            b1.HasOne("IFramework.MessageStores.Relational.UnPublishedEvent")
                                .WithOne("SagaInfo")
                                .HasForeignKey("IFramework.Message.Impl.SagaInfo", "UnPublishedEventId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("IFramework.MessageStores.Relational.UnSentCommand", b =>
                {
                    b.OwnsOne("IFramework.Message.Impl.SagaInfo", "SagaInfo", b1 =>
                        {
                            b1.Property<string>("UnSentCommandId");

                            b1.Property<string>("ReplyEndPoint");

                            b1.Property<string>("SagaId");

                            b1.ToTable("msgs_UnSentCommands");

                            b1.HasOne("IFramework.MessageStores.Relational.UnSentCommand")
                                .WithOne("SagaInfo")
                                .HasForeignKey("IFramework.Message.Impl.SagaInfo", "UnSentCommandId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
