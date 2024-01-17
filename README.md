# vertem-news-api

Projeto utilizado em um processo seletivo cujo desafio foi o seguinte:
O candidato deve criar uma API RESTful que utiliza uma API pública de notícias como, por exemplo, a NewsAPI. A API criada pelo candidato deve coletar notícias de várias fontes e armazená-las em um banco de dados. Os usuários devem ser capazes de buscar notícias por categoria, fonte ou palavras-chave.

Implemente os seguintes endpoints:
GET /news: Este endpoint deve retornar todas as notícias no banco de dados.
GET /news/{id}: Este endpoint deve retornar uma única notícia baseada em seu ID.
GET /news/category/{category}: Este endpoint deve retornar todas as notícias de uma determinada categoria.
GET /news/source/{source}: Este endpoint deve retornar todas as notícias de uma determinada fonte.
GET /news/search/{keyword}: Este endpoint deve retornar todas as notícias que contêm a palavra-chave fornecida.

Link do pipeline: https://github.com/samuelfsantos/vertem-news-api/actions/runs/5115671438
Link do projeto já com o deploy realizado na digital ocean: http://104.248.228.214:5008/swagger/index.html

-> Estilos e Padrões de arquitetura utilizados
* Domain-Driven Design (DDD)
* SOLID
* Repository Pattern
* Unit Of Work Pattern

-> Infra
* Imagem docker (Net Core 7.0)
* Docker Compose
* Banco de dados SQLite
  


