# OrderManager

## Descrição
A API Order Manager é um microserviço desenvolvido para gerenciar pedidos, clientes e itens de pedidos. Ele consome dados de uma fila RabbitMQ e permite a consulta de informações, como o valor total do pedido, a quantidade de pedidos por cliente e a lista de pedidos realizados por cliente.

## Tecnologias Utilizadas
- .NET 8
- Entity Framework Core
- SQL Server
- RabbitMQ
- Docker
- Newtonsoft.Json para manipulação de JSON

## Funcionalidades
- CRUD para pedidos, clientes e itens de pedidos
- Consumo de mensagens de uma fila RabbitMQ
- Cálculo do valor total do pedido
- Consulta de pedidos por cliente
- Retorno de informações formatadas em JSON
