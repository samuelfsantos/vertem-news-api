# vertem-news-api

Projeto contruido para atender um processo seletivo cujo desafio foi o seguinte:
O candidato deve criar uma API RESTful que utiliza uma API pública de notícias como, por exemplo, a NewsAPI. A API criada pelo candidato deve coletar notícias de várias fontes e armazená-las em um banco de dados. Os usuários devem ser capazes de buscar notícias por categoria, fonte ou palavras-chave.

-> Implementação dos seguintes endpoints:
* POST /news: Este endpoint inclui uma nova notícia.
* PUT /news/{id}: Este endpoint atualiza uma notícia baseada em seu ID.
* DELETE /news/{id}: Este endpoint exclui uma notícia baseada em seu ID.
* GET /news: Este endpoint deve retornar todas as notícias no banco de dados.
* GET /news/{id}: Este endpoint deve retornar uma única notícia baseada em seu ID.
* GET /news/category/{category}: Este endpoint deve retornar todas as notícias de uma determinada categoria.
* GET /news/source/{source}: Este endpoint deve retornar todas as notícias de uma determinada fonte.
* GET /news/search/{keyword}: Este endpoint deve retornar todas as notícias que contêm a palavra-chave fornecida.
* POST /news/integracao-news-api-org: Este endpoint inicia a sincronização da integração com o Web Service externo para atualizar o banco de dados local com as notícias obtidas.

-> Estilos e Padrões de arquitetura utilizados
* Domain-Driven Design (DDD)
* Clean Architecture
* SOLID
* Repository Pattern
* Unit Of Work Pattern
* CQRS Pattern
* Anti Corruption Layer Pattern (Services)
* Documentação com Swagger

-> DevOps
* CI/CD com GitHub Actions
* Imagem docker (Net Core 8.0)
* Docker Compose
* Hospedagem no Servidor Digital Ocean

-> Banco de dados
* NoSQL: Redis para cache
* Relacional: SQLite para persistência

-> Links úteis
* Pipeline GitHub Actions: https://github.com/samuelfsantos/vertem-news-api/actions
* Link do projeto já com o deploy realizado na digital ocean: http://104.248.228.214:5008/swagger/index.html
  


