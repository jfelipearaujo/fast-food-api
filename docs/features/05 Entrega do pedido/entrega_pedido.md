# Preparação do Pedido

## Entidades

```
Cliente

> Entidade responsável por realizar os pedidos no sistema

CPF
Nome completo
E-mail
```

```
Pedido

> Entidade responsável por agrupar um conjunto de itens selecionados pelo cliente

Cliente
Status do pedido
Lista dos produtos
```

## Fluxo

1. Cliente é notificado sobre seu pedido **pronto**
2. Cliente retira o pedido com um funcionário
3. Funcionário atualiza o status do pedido para **finalizado**

## Storytelling

![fluxo_storytelling](./entrega_pedido.png)