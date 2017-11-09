# FaceScraperUnivale
Programa loga no Facebook dadas as credenciais, captura cidade que mora atualmente, verifica se estudou na Univale e grava em um arquivo pra ser lido após execução do scraper. Listbox exibe nome e dois cliques carrega foto e local no mapa onde a pessoa reside atualmente.

![alt text](https://i.imgur.com/elNFs8f.png "Exemplo")

# Dependências
- **Instalar os pacotes do NuGet**

Já estão acompanhados nesse git

Selenium.WebDriver (*Necessário para rodar o bot*)

Newtonsoft.Json (*Necessário para analisar a resposta em formato JSON*)

- **Extrair arquivo chromedriver.exe na pasta C:/Program Files**

https://chromedriver.storage.googleapis.com/index.html?path=2.33/ (*Arquivo necessário para executar o bot via janelas do Chrome*)

- **Resposta da API do Facebook**

O programa depende que exista um arquivo json.txt com a resposta da API do Facebook. 

Foi usado {group-id}/members?limit=25&pretty=0, onde dado um id de grupo (ex. "Alunos da Univale" tem id=261065417267565) ele retorna a lista dos 25 primeiros membros listados no grupo, seja este grupo aberto ou fechado. Grupos secretos necessitam de permissão de um usuário que faça parte do grupo.

# Bloqueio do Facebook
Após cerca de 150-200 visualizadas em perfil via id, o Facebook bloqueia a leitura da página por um tempo de horas.
[Referir a essa resposta do StackOverflow](https://stackoverflow.com/a/41220267) para compilação de uma versão customizada do chromedriver.exe que tenta mascarar a atividade do bot.

# To-do list
- Automatizar a API do Facebook para salvar o json.txt dada um id de grupo;
- Adaptar para outras universidades;
- Adicionar LinkedIn;


