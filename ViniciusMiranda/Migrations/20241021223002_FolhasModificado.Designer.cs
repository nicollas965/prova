﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ViniciusMiranda.Data;

#nullable disable

namespace ViniciusMiranda.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241021223002_FolhasModificado")]
    partial class FolhasModificado
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("ViniciusMiranda.Models.FolhaPagamento", b =>
                {
                    b.Property<long>("FolhaPagamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Ano")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FuncionarioId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("FuncionarioId1")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("ImportFgts")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ImportInss")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ImpostoIrrf")
                        .HasColumnType("TEXT");

                    b.Property<int>("Mes")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantidade")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("SalarioBruto")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("SalarioLiquido")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Valor")
                        .HasColumnType("TEXT");

                    b.HasKey("FolhaPagamentoId");

                    b.HasIndex("FuncionarioId1");

                    b.ToTable("FolhasPagamentos");
                });

            modelBuilder.Entity("ViniciusMiranda.Models.Funcionario", b =>
                {
                    b.Property<long>("FuncionarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("FuncionarioId");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("ViniciusMiranda.Models.FolhaPagamento", b =>
                {
                    b.HasOne("ViniciusMiranda.Models.Funcionario", "Funcionario")
                        .WithMany()
                        .HasForeignKey("FuncionarioId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Funcionario");
                });
#pragma warning restore 612, 618
        }
    }
}