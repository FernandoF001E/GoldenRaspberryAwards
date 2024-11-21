Desenvolvimento de uma API RESTful para possibilitar a leitura da lista de indicados e vencedores
da categoria Pior Filme do Golden Raspberry Awards.

## Pré-requisitos

- [.NETCore 8.0]

### Banco de dados
- Banco de dados em memória

### Execução do projeto
- Abrir a solution GoldenRaspberryAwards e setar como projeto principal(Set as Startup Project) o projeto WebApi.
- Na aba superior do visual studio clique em IIS Express para executar a aplicação para abrir o swagger.
- No swagger irá conter o endpoint get /api/awards/intervals.
- Em seguida clique em TryOut -> Execute para retornar o resultado da planilha com os dados em memória.
- Ao executar a aplicação, será gravado no banco em memória os dados do arquivo CSV Movielist.csv que está dentro da pasta CSV do projeto WebAPI.

- Ou via postman, consumir o seguinte endpoit --> https://localhost:44354/api/awards/intervals.

- Obs: **NÃO** foi pedido nenhuma autenticação para consumir a API.

### Execução do teste unitário
- No menu superior do Visual Studio clique em View -> Test Explorer. Vai listar todos os testes pendendes de execução.
- Ou, clique com o botão direito do mouse no projeto UnitTest -> RunTest.






