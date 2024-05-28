<!--# Título e Imagem de capa-->
<h1 align="center">Tech Challenge Fase #1 - Grupo 60 FIAP ©2024</h1> 
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

* O Problema
  - O Tech Challenge desta fase será desenvolver um aplicativo utilizando a plataforma .NET 8 para cadastro
de contatos regionais, considerando a persistência de dados e a qualidade do software.

* Requisitos Funcionais
  - Cadastro de contatos: permitir o cadastro de novos contatos, incluindo nome, telefone e e-mail. As‐
socie cada contato a um DDD correspondente à região.
  - Consulta de contatos: implementar uma funcionalidade para consultar e visualizar os contatos ca‐ dastrados, os quais podem ser filtrados pelo DDD da região.
  - Atualização e exclusão: possibilitar a atualização e exclusão de contatos previamente cadastrados.

* Requisitos Técnicos
  - Persistência de Dados: utilizar um banco de dados para armazenar as informações dos contatos. Escolha entre Entity Framework Core ou Dapper para a camada de acesso a dados.
  - Validações: implementar validações para garantir dados consistentes (por exemplo: validação de
formato de e-mail, telefone, campos obrigatórios).
  - Testes Unitários: desenvolver testes unitários utilizando xUnit ou NUnit.


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
<img loading="lazy" width="100%" height="100%" src="https://github.com/marcosbrandi/FIAP/blob/master/Docs/Domain%20Storytelling/Endpoints.PNG"/>


- `Consulta por Nome`: Retorna um contato passando o Nome como parâmetro de busca
- `Consulta por ID`: Retorna um contato passando Id de registro como parâmetro de busca
- `Consulta por DDD`: Retorna os contatos correspondentes ao DDD recebido como parâmetro de busca
    - Se o DDD nâo for informado, retorna todos os contatos cadastrados
- `Consulta de UF por DDD`: Retorna a UF correspondente ao DDD recebido como parâmetro de busca
- `Inserir Contato`: Cria um novo contato
    - Os parâmetros devem corresponder ao body do json, há validações para Id e E-mail repetido
- `Atualizar Contato`: Atualiza um contato existente
    - Os parâmetros devem corresponder ao body do json, há validações para Id e E-mail repetido
- `Deletar Contato`: Exclui um contato existente com i Id informado como parâmetro



# Tecnologias utilizadas
- C#, .Net 8, Minimal API, InMemory Database, EF Core 8, OpenAPI

# Link do Projeto no YouTube
- https://www.youtube.com/watch?v=gmRAX21eDeo
<!--# Pessoas Contribuidoras-->

# Pessoas Desenvolvedoras do Projeto
- Júlio Valle (juliodovale2012@gmail.com)
- Gustavo Amaral (gustavo-amaral@hotmail.com)
- Marcos Brandi Torres (marcosbrandi@hotmail.com)
- Valterlei Viana (valterlei.viana@gmail.com)
- Jhonas Nobre (jhonas_nobre@hotmail.com)

<!--# Licença-->

<!--# Conclusão-->

<!--* [Índice](#índice)-->

