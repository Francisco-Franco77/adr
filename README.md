# Antecipação de Recebíveis

Teste de criação de uma API .NET Core para calcular Antecipação de Recebíveis para notas fiscais. Baseado no enunciado em https://github.com/pratesy/size-tecnico.

API interage com um banco SQL server. O backup do banco de teste, bem como o script utilizado para criação do banco, estão na pasta AdR/SQL.<br>
Foi implementado o Swagger para simular um front interagindo com a API.

### Configurações
Em AdR/AdR/appsettings.json é necessário redefinir o valor da variável DatabasePath com o caminho da instância SQL Server local. 
