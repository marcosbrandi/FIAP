<!--# Título e Imagem de capa-->
<h1 align="center">Tech Challenge Fase #3 - Grupo 60 FIAP ©2024</h1> 
<!--  
![GitHub Org's stars](https://img.shields.io/github/stars/marcosbrandi/fiap?style=social)
![Badge em Desenvolvimento](http://img.shields.io/static/v1?label=STATUS&message=EM%20DESENVOLVIMENTO&color=GREEN&style=for-the-badge)
-->
<p align="center">
<img loading="lazy" src="https://img.shields.io/github/stars/marcosbrandi/fiap?style=social"/>
<img loading="lazy" src="http://img.shields.io/static/v1?label=STATUS&message=EM%20DESENVOLVIMENTO&color=GREEN&style=for-the-badge"/>
</p>
<!--
:construction: Projeto em construção :construction:
-->

# Índice 

<!--* [Título e Imagem de capa](#Título-e-Imagem-de-capa)-->
<!--* [Acesso ao Projeto](#acesso-ao-projeto)-->
<!--* [Badges](#badges)-->
<!--* [Status do Projeto](#status-do-Projeto)-->
<!--* [Licença](#licença)-->
<!--* [Conclusão](#conclusão)-->
<!--* [Pessoas Contribuidoras](#pessoas-contribuidoras)-->

* [Descrição do Projeto](#descrição-do-projeto)
* [Funcionalidades e Demonstração da Aplicação](#funcionalidades-e-demonstração-da-aplicação)
* [Tecnologias utilizadas](#tecnologias-utilizadas)
* [Link do Projeto no YouTube](#link-do-projeto-no-youtube)
* [Pessoas Desenvolvedoras do Projeto](#pessoas-desenvolvedoras-do-projeto)

<!--
# Badges
![Badge em Desenvolvimento](http://img.shields.io/static/v1?label=STATUS&message=EM%20DESENVOLVIMENTO&color=GREEN&style=for-the-badge)
-->

# Descrição do Projeto

Nesta etapa, a iniciativa evolui para incorporar conceitos mais avançados, como a arquitetura de microsserviços e a comunicação assíncrona. O objetivo principal é refatorar um aplicativo .NET previamente desenvolvido para gerenciar contatos regionais, aprimorando sua estrutura e funcionalidades.

* Objetivos Principais

Arquitetura de Microsserviços: Transformar o aplicativo monolítico em um conjunto de microsserviços independentes, cada um encarregado de uma funcionalidade específica (como cadastro, consulta, atualização e exclusão de contatos).
Comunicação Assíncrona: Implementar o RabbitMQ para facilitar a comunicação entre os microsserviços, permitindo o envio de dados por meio de filas. Isso envolve a criação de um microsserviço que coleta dados e os envia para uma fila, onde um microsserviço consumidor irá processar essas informações e persistir no banco de dados.
Criar testes unitário e de integração

* Requisitos Técnicos

Reestruturar o aplicativo em microsserviços menores, seguindo padrões de design, como Circuit Breaker, quando necessário.
Configurar o RabbitMQ para gerenciar a fila de mensagens, desenvolvendo tanto produtores quanto consumidores para as operações de criação, atualização e exclusão de contatos.

* Critérios de Aceite

Funcionamento da pipeline com testes unitários e de integração.
Monitoramento utilizando Prometheus e Grafana, conforme os requisitos técnicos da fase anterior.
Demonstração da comunicação entre microsserviços via RabbitMQ, ilustrando a inserção de dados na fila e o processamento pelo consumidor.

# Funcionalidades e Demonstração da Aplicação

<!--
:hammer: 
![Domain Story Telling](https://github.com/marcosbrandi/FIAP/assets/7784571/b05b863c-ca48-4bfd-830c-8ac9ff26bdf9)
![Domain Story Telling](https://github.com/marcosbrandi/FIAP/blob/master/Docs/Domain%20Storytelling/Domain%20Story%20Telling.jpg)
![Domain Story Telling]
-->
- `Domain Story Telling`: 
<img loading="lazy" width="50%" height="50%" src="https://github.com/marcosbrandi/FIAP/blob/master/Docs/Domain%20Storytelling/Domain%20Story%20Telling.jpg"/>

- `Schemas`: 
<img loading="lazy" width="40%" height="40%" src="https://github.com/marcosbrandi/FIAP/blob/master/Docs/Domain%20Storytelling/Schemas.PNG"/>

- `Endpoints`:
<!--
<img loading="lazy" width="100%" height="100%" src="https://github.com/marcosbrandi/FIAP/blob/master/Docs/Domain%20Storytelling/Endpoints.PNG"/>
-->
<img loading="lazy" width="100%" height="100%" src="https://github.com/marcosbrandi/FIAP/blob/master/Docs/Domain%20Storytelling/Endpoint-Producer.jpg"/>

- `Inserir Contato`: Cria um novo contato
    - Os parâmetros devem corresponder ao body do json, há validações para Id e E-mail repetido
- `Atualizar Contato`: Atualiza um contato existente
    - Os parâmetros devem corresponder ao body do json, há validações para Id e E-mail repetido
- `Deletar Contato`: Exclui um contato existente com i Id informado como parâmetro

<img loading="lazy" width="100%" height="100%" src="https://github.com/marcosbrandi/FIAP/blob/master/Docs/Domain%20Storytelling/Endpoint-Consumer.jpg"/>

- `Consulta por Nome`: Retorna um contato passando o Nome como parâmetro de busca
- `Consulta por ID`: Retorna um contato passando Id de registro como parâmetro de busca
- `Consulta por DDD`: Retorna os contatos correspondentes ao DDD recebido como parâmetro de busca
    - Se o DDD nâo for informado, retorna todos os contatos cadastrados
- `Consulta de UF por DDD`: Retorna a UF correspondente ao DDD recebido como parâmetro de busca

# Testes

- `Teste Unitário`:
Este teste verifica a funcionalidade dos métodos CRUD do controlador. Um mock do IMessageBus é utilizado para simular a comunicação com o barramento de mensagens, permitindo que o controlador seja testado de forma isolada e assegurando que as lógicas internas funcionem corretamente sem depender de componentes externos.

- `Testes de Integração`:
Este teste cobre a publicação e o consumo de mensagens no RabbitMQ. O que o teste de integração realiza:

- Cria e declara uma fila de teste no RabbitMQ para simular o ambiente de produção.
- Publica um objeto NovoContato na fila, simulando a operação de criação de um novo contato.
- Consome a mensagem da fila e verifica se ela foi recebida corretamente.
- Assegura que a mensagem recebida não é nula e que os dados correspondem exatamente ao que foi enviado, garantindo a integridade da comunicação entre os serviços.

Além disso, o teste também valida a funcionalidade do endpoint de criação de contatos da API, garantindo que a requisição para adicionar um novo contato seja bem-sucedida e retorne o status esperado.

# Docker
<img loading="lazy" width="100%" height="100%" src="https://github.com/marcosbrandi/FIAP/blob/master/Docs/Domain%20Storytelling/Docker.jpg"/>


# Tecnologias utilizadas
- C#, .Net 8, Minimal API, Postgres, EF Core 8, OpenAPI, RabbitMQ, Docker, Grafana, Prometheus

# Link do Projeto no YouTube
- https://www.youtube.com/watch?v=gmRAX21eDeo
<!--# Pessoas Contribuidoras-->

# Pessoas Desenvolvedoras do Projeto

- Gustavo Amaral (gustavo-amaral@hotmail.com)
- Marcos Brandi Torres (marcosbrandi@hotmail.com)
- Jhonas Nobre (jhonas_nobre@hotmail.com)
- Júlio Valle (juliodovale2012@gmail.com)

<!--# Licença-->

<!--# Conclusão-->

<!--* [Índice](#índice)-->

