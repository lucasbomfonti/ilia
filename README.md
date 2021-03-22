# ilia

## WebApi .NET Core desenvolvida na versão 3.1 do framework.

### Algumas informações sobre o desenvolvimento da API:
* Foi adicionado uma método Get onde espera por parâmetro um modelo de filtro, assim possibilitando a visualização de todos os clientes que dividem a mesma Rua, Cidade, Estado, CEP e País
* A arquitetura escolhida foi Onion Architecture.
* Foi implementado um modelo de entidade anêmica, ou seja, as entidades são apenas representações do banco de dados, dessa forma, a regra de negócio ficava na responsabilidade apenas da camana de serviço.
* Para a camada de acesso a dados foi implementado Repository Pattern.
* Para autenticação da API foi usado um modelo simplificado de JWT.
* Foi adicionado suporte a docker para a aplicação, sendo assim, é possível executa-la em um ambiente isolado do IIS.
* O ORM utilizado para manipulação de dados foi EntityFrameworkCore.
* A Ênfase do desenvolvimento foi torna-lo o mais enxuto possível para o front-end, nesse caso, foi criado vários objetos de transferência de dados (DTO) visando apresentar para o front-end apenas o necessário. Outra ênfase do desenvolvimento foi torna-lo mais genérico possível, sendo assim, adaptável para possíveis alterações e implementações futuras.

### Setup Local:
* Clone o projeto.
* Caso queira apontar em uma instância do SQL Server basta alterar a propriedade ConnectionString dentro do arquivo EnvironmentProperties no projeto Ilia.CrossCutting. Caso contrário, se não deseja apontar para sua instância local basta ignorar esse passo, a aplicação irá subir um banco de dados em memória para teste.
* Inicie a Aplicação e acesse a rota {Localhost}:{port}/swagger.

### Setup Docker:
* Clone o projeto.
* Abra o Windows PowerShell, navegue até a pasta local de seu projeto, execute 'docker-compose build' seguido de 'docker-compose up -d'
* Acesse a rota {Localhost}:{port}/swagger.

### Obs.
  * É necessário estar logado no sistema para realizar algumas requisições. Para isso cadastre um usuário, realize o login e envie o token de autorização na request.
