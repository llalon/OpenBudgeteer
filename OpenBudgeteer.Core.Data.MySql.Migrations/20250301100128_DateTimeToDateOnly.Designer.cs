﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OpenBudgeteer.Core.Data.Entities;

#nullable disable

namespace OpenBudgeteer.Core.Data.MySql.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20250301100128_DateTimeToDateOnly")]
    partial class DateTimeToDateOnly
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("AccountId");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.BankTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("TransactionId");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65, 2)");

                    b.Property<string>("Memo")
                        .HasColumnType("longtext");

                    b.Property<string>("Payee")
                        .HasColumnType("longtext");

                    b.Property<DateOnly>("TransactionDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("BankTransaction");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.Bucket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("BucketId");

                    b.Property<Guid>("BucketGroupId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ColorCode")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsInactive")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateOnly>("IsInactiveFrom")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("TextColorCode")
                        .HasColumnType("longtext");

                    b.Property<DateOnly>("ValidFrom")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("BucketGroupId");

                    b.ToTable("Bucket");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.BucketGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("BucketGroupId");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BucketGroup");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.BucketMovement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("BucketMovementId");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65, 2)");

                    b.Property<Guid>("BucketId")
                        .HasColumnType("char(36)");

                    b.Property<DateOnly>("MovementDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("BucketId");

                    b.ToTable("BucketMovement");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.BucketRuleSet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("BucketRuleSetId");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<Guid>("TargetBucketId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("TargetBucketId");

                    b.ToTable("BucketRuleSet");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.BucketVersion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("BucketVersionId");

                    b.Property<Guid>("BucketId")
                        .HasColumnType("char(36)");

                    b.Property<int>("BucketType")
                        .HasColumnType("int");

                    b.Property<int>("BucketTypeXParam")
                        .HasColumnType("int");

                    b.Property<decimal>("BucketTypeYParam")
                        .HasColumnType("decimal(65, 2)");

                    b.Property<DateOnly>("BucketTypeZParam")
                        .HasColumnType("date");

                    b.Property<string>("Notes")
                        .HasColumnType("longtext");

                    b.Property<DateOnly>("ValidFrom")
                        .HasColumnType("date");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BucketId");

                    b.ToTable("BucketVersion");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.BudgetedTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("BudgetedTransactionId");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65, 2)");

                    b.Property<Guid>("BucketId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("BucketId");

                    b.HasIndex("TransactionId");

                    b.ToTable("BudgetedTransaction");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.ImportProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("ImportProfileId");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("AdditionalSettingAmountCleanup")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("AdditionalSettingAmountCleanupValue")
                        .HasColumnType("longtext");

                    b.Property<int>("AdditionalSettingCreditValue")
                        .HasColumnType("int");

                    b.Property<string>("AmountColumnName")
                        .HasColumnType("longtext");

                    b.Property<string>("CreditColumnIdentifierColumnName")
                        .HasColumnType("longtext");

                    b.Property<string>("CreditColumnIdentifierValue")
                        .HasColumnType("longtext");

                    b.Property<string>("CreditColumnName")
                        .HasColumnType("longtext");

                    b.Property<string>("DateFormat")
                        .HasColumnType("longtext");

                    b.Property<string>("Delimiter")
                        .IsRequired()
                        .HasColumnType("varchar(1)");

                    b.Property<int>("HeaderRow")
                        .HasColumnType("int");

                    b.Property<string>("MemoColumnName")
                        .HasColumnType("longtext");

                    b.Property<string>("NumberFormat")
                        .HasColumnType("longtext");

                    b.Property<string>("PayeeColumnName")
                        .HasColumnType("longtext");

                    b.Property<string>("ProfileName")
                        .HasColumnType("longtext");

                    b.Property<string>("TextQualifier")
                        .IsRequired()
                        .HasColumnType("varchar(1)");

                    b.Property<string>("TransactionDateColumnName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("ImportProfile");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.MappingRule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("MappingRuleId");

                    b.Property<Guid>("BucketRuleSetId")
                        .HasColumnType("char(36)");

                    b.Property<int>("ComparisonField")
                        .HasColumnType("int");

                    b.Property<int>("ComparisonType")
                        .HasColumnType("int");

                    b.Property<string>("ComparisonValue")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("BucketRuleSetId");

                    b.ToTable("MappingRule");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.RecurringBankTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("TransactionId");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65, 2)");

                    b.Property<DateOnly>("FirstOccurrenceDate")
                        .HasColumnType("date");

                    b.Property<string>("Memo")
                        .HasColumnType("longtext");

                    b.Property<string>("Payee")
                        .HasColumnType("longtext");

                    b.Property<int>("RecurrenceAmount")
                        .HasColumnType("int");

                    b.Property<int>("RecurrenceType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("RecurringBankTransaction");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.BankTransaction", b =>
                {
                    b.HasOne("OpenBudgeteer.Core.Data.Entities.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.Bucket", b =>
                {
                    b.HasOne("OpenBudgeteer.Core.Data.Entities.Models.BucketGroup", "BucketGroup")
                        .WithMany("Buckets")
                        .HasForeignKey("BucketGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BucketGroup");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.BucketMovement", b =>
                {
                    b.HasOne("OpenBudgeteer.Core.Data.Entities.Models.Bucket", "Bucket")
                        .WithMany("BucketMovements")
                        .HasForeignKey("BucketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bucket");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.BucketRuleSet", b =>
                {
                    b.HasOne("OpenBudgeteer.Core.Data.Entities.Models.Bucket", "TargetBucket")
                        .WithMany()
                        .HasForeignKey("TargetBucketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TargetBucket");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.BucketVersion", b =>
                {
                    b.HasOne("OpenBudgeteer.Core.Data.Entities.Models.Bucket", "Bucket")
                        .WithMany("BucketVersions")
                        .HasForeignKey("BucketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bucket");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.BudgetedTransaction", b =>
                {
                    b.HasOne("OpenBudgeteer.Core.Data.Entities.Models.Bucket", "Bucket")
                        .WithMany("BudgetedTransactions")
                        .HasForeignKey("BucketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OpenBudgeteer.Core.Data.Entities.Models.BankTransaction", "Transaction")
                        .WithMany("BudgetedTransactions")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bucket");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.ImportProfile", b =>
                {
                    b.HasOne("OpenBudgeteer.Core.Data.Entities.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.MappingRule", b =>
                {
                    b.HasOne("OpenBudgeteer.Core.Data.Entities.Models.BucketRuleSet", "BucketRuleSet")
                        .WithMany("MappingRules")
                        .HasForeignKey("BucketRuleSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BucketRuleSet");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.RecurringBankTransaction", b =>
                {
                    b.HasOne("OpenBudgeteer.Core.Data.Entities.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.BankTransaction", b =>
                {
                    b.Navigation("BudgetedTransactions");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.Bucket", b =>
                {
                    b.Navigation("BucketMovements");

                    b.Navigation("BucketVersions");

                    b.Navigation("BudgetedTransactions");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.BucketGroup", b =>
                {
                    b.Navigation("Buckets");
                });

            modelBuilder.Entity("OpenBudgeteer.Core.Data.Entities.Models.BucketRuleSet", b =>
                {
                    b.Navigation("MappingRules");
                });
#pragma warning restore 612, 618
        }
    }
}
