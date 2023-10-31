# FiapTechChallenge
Espaço destinado para tech challenges referente ao curso de pós-graduação.

## 🤝 Colaboradores
* Hugo Souza - rm351477 
* Leandro Campos - rm350757 
* Lucas Silva - rm351454

# Documentação 📜

Documentação: [https://docs.google.com/document/d/1njXBAbwb_Z5NDJMHJjPWvT2fH1nOTIHqpGJ67utbi38/edit?usp=sharing](https://github.com/hugorsouza/FiapTechChallenge/commit/d8c9de3ffbbf26429eed97a011f3bb27bfb6489a)

## 💻 Pré-requisitos

Para executar o projeto será necessário:

* Instalar o SDK .NET 7
* Ter uma instância do SQL Sever local
* Executar o script FiapTechChallenge/Scripts/Criar database.sql
* Definir a ConnectionString "ConnectionStrings:Ecommerce" do SQL Server no appsettings.json
* Definir a ConnectionString "BlobStorage:ConectionString" do Azure Blob Storage no appsettings.json
* Utilizar uma IDE como o VS 2022, Rider ou VSCode para executar o projeto

## 💻 Executando o projeto
Após realizar o setup inicial você poderá executar o projeto. Caso esteja executando em modo Development, uma carga de dados inicial será inserida no banco de dados para facilitar o acesso inicial a API.
Nessa carga inicial serão criados:
* Clientes e seus respectivos Usuários de acesso
* Funcionários e seus respectivos Usuários de acesso

Após executar o projeto, as rotas de documentação estarão disponíveis. Elas poderão ser consultadas em:
* Swagger: localhost:{porta}/swagger/index.html
* Redoc: localhost:{porta}/api-docs/index.html

## 💻 Realizando testes
Será possível efetuar os testes na API pelo Swagger. Para isso, recomendamos o uso dos usuários cadastrados na carga inicial dados de desenvolvimento.
Usuários disponíveis:
* Perfil Cliente: cliente@hotmail.com
* Perfil Funcionário (Admin): admin@hotmail.com
* Perfil Funcionário (Padrão): funcionario@hotmail.com
  
A senha padrão de todos os usuários criados durante essa carga inicial será "123456". Existirão outros usuários com dados aleatórios, eles foram criados utilizando a biblioteca Bogus.

Para se autenticar, utilize a rota /Autenticacao/login e utilize uma das credenciais disponibilizadas. Após obter o token de acesso, será necessário informar ele no botão Authorize para que assim seja possível acessar as rotas protegidas.

