﻿// <auto-generated />
using System;
using Ecommerce.Infra.Dados.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ecommerce.Infra.Dados.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231019102613_Inicial")]
    partial class Inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ecommerce.Domain.Entities.Pessoas.Autenticacao.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataAlteracaoUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCadastroUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("Email");

                    b.Property<bool>("EmailConfirmado")
                        .HasColumnType("bit");

                    b.Property<string>("EmailNormalizado")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)")
                        .HasComputedColumnSql("TRIM(UPPER([Email]))");

                    b.Property<string>("NomeExibicao")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("Perfil")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.Pessoas.Fisica.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)")
                        .HasColumnOrder(3);

                    b.Property<DateTime>("DataAlteracaoUtc")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(6);

                    b.Property<DateTime>("DataCadastroUtc")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(5);

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(4);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnOrder(1);

                    b.Property<bool>("RecebeNewsletterEmail")
                        .HasColumnType("bit");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnOrder(2);

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.ToTable("Cliente", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.Pessoas.Fisica.Funcionario", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<string>("Cargo")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)")
                        .HasColumnOrder(3);

                    b.Property<DateTime>("DataAlteracaoUtc")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(6);

                    b.Property<DateTime>("DataCadastroUtc")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(5);

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(4);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnOrder(1);

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnOrder(2);

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.ToTable("Funcionario", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.Pessoas.Juridica.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cnpj")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("EmailContato")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NomeFantasia")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Cnpj")
                        .IsUnique()
                        .HasFilter("[Cnpj] IS NOT NULL");

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("Empresa", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.Pessoas.Fisica.Cliente", b =>
                {
                    b.HasOne("Ecommerce.Domain.Entities.Pessoas.Autenticacao.Usuario", "Usuario")
                        .WithOne("Cliente")
                        .HasForeignKey("Ecommerce.Domain.Entities.Pessoas.Fisica.Cliente", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.Pessoas.Fisica.Funcionario", b =>
                {
                    b.HasOne("Ecommerce.Domain.Entities.Pessoas.Autenticacao.Usuario", "Usuario")
                        .WithOne("Funcionario")
                        .HasForeignKey("Ecommerce.Domain.Entities.Pessoas.Fisica.Funcionario", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.Pessoas.Juridica.Empresa", b =>
                {
                    b.HasOne("Ecommerce.Domain.Entities.Pessoas.Autenticacao.Usuario", "Usuario")
                        .WithOne("Empresa")
                        .HasForeignKey("Ecommerce.Domain.Entities.Pessoas.Juridica.Empresa", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.Pessoas.Autenticacao.Usuario", b =>
                {
                    b.Navigation("Cliente");

                    b.Navigation("Empresa");

                    b.Navigation("Funcionario");
                });
#pragma warning restore 612, 618
        }
    }
}
