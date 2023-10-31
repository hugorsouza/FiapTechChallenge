USE [master]
GO
/****** Object:  Database [ECommerce]    Script Date: 10/30/2023 11:02:15 PM ******/
CREATE DATABASE [ECommerce] 
GO
USE [ECommerce]
GO
CREATE TABLE [dbo].[Categoria](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NULL,
	[Ativo] [bit] NULL,
	[Descricao] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 10/30/2023 11:02:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[Id] [int] NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[Sobrenome] [varchar](100) NOT NULL,
	[Cpf] [varchar](11) NOT NULL,
	[DataNascimento] [datetime2](7) NOT NULL,
	[DataCadastro] [datetime2](7) NOT NULL,
	[DataAlteracao] [datetime2](7) NULL,
	[RecebeNewsletterEmail] [bit] NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Endereco]    Script Date: 10/30/2023 11:02:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Endereco](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Logradouro] [varchar](50) NULL,
	[Numero] [varchar](8) NULL,
	[CEP] [varchar](8) NULL,
	[Bairro] [varchar](50) NULL,
	[Cidade] [varchar](50) NULL,
	[Estado] [varchar](50) NULL,
	[EntidadeId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estoque]    Script Date: 10/30/2023 11:02:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estoque](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](250) NOT NULL,
	[UsuarioDocumento] [varchar](14) NOT NULL,
	[Produto] [varchar](250) NOT NULL,
	[QuantidadeAtual] [int] NOT NULL,
	[DataUltimaMovimentacao] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fabricante]    Script Date: 10/30/2023 11:02:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fabricante](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NULL,
	[Ativo] [bit] NULL,
	[CNPJ] [varchar](14) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Funcionario]    Script Date: 10/30/2023 11:02:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Funcionario](
	[Id] [int] NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[Sobrenome] [varchar](100) NOT NULL,
	[Cpf] [varchar](11) NOT NULL,
	[DataNascimento] [datetime2](7) NOT NULL,
	[DataCadastro] [datetime2](7) NOT NULL,
	[DataAlteracao] [datetime2](7) NULL,
	[Cargo] [varchar](256) NULL,
	[Administrador] [bit] NOT NULL,
 CONSTRAINT [PK_Funcionario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 10/30/2023 11:02:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](250) NOT NULL,
	[UsuarioDocumento] [varchar](14) NOT NULL,
	[Descricao] [varchar](250) NOT NULL,
	[Quantidade] [int] NOT NULL,
	[ValorUnitario] [decimal](18, 0) NOT NULL,
	[ValorTotal] [decimal](18, 0) NOT NULL,
	[DataPedido] [datetime] NOT NULL,
	[TipoPedido] [int] NOT NULL,
	[TipoPedidoDescricao] [varchar](250) NOT NULL,
	[Status] [int] NOT NULL,
	[StatusDescricao] [varchar](250) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produto]    Script Date: 10/30/2023 11:02:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NULL,
	[Ativo] [bit] NULL,
	[Preco] [decimal](10, 2) NULL,
	[Descricao] [varchar](50) NULL,
	[FabricanteId] [int] NULL,
	[UrlImagem] [varchar](100) NULL,
	[CategoriaId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 10/30/2023 11:02:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NomeExibicao] [varchar](256) NOT NULL,
	[Email] [varchar](256) NOT NULL,
	[EmailNormalizado]  AS (Trim(upper([Email]))),
	[Senha] [varchar](256) NOT NULL,
	[DataCadastro] [datetime2](7) NOT NULL,
	[DataAlteracao] [datetime2](7) NULL,
	[Perfil] [int] NOT NULL,
	[EmailConfirmado] [bit] NOT NULL,
	[Ativo] [bit] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cliente]  WITH CHECK ADD  CONSTRAINT [FK_Cliente_Usuario] FOREIGN KEY([Id])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Cliente] CHECK CONSTRAINT [FK_Cliente_Usuario]
GO
ALTER TABLE [dbo].[Funcionario]  WITH CHECK ADD  CONSTRAINT [FK_Funcionario_Usuario] FOREIGN KEY([Id])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Funcionario] CHECK CONSTRAINT [FK_Funcionario_Usuario]
GO
ALTER DATABASE [ECommerce] SET  READ_WRITE 
GO
alter table Cliente add constraint UQ_Cliente_Cpf unique(Cpf)
GO
alter table Funcionario add constraint UQ_Funcionario_Cpf unique(Cpf)
GO
alter table Usuario add constraint UQ_Usuario_EmailNormalizado unique(EmailNormalizado)