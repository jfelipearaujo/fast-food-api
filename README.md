<p align="center">
    <img src="./docs/assets/Ctrl+Eat-logos_v2.jpeg" width="600"/>
</p>

# Sobre o restaurente

> Ctrl+Eat é mais do que uma simples restaurente fast food; é uma experiência gastronômica em constante evolução. Localizada no coração do bairro (e agora em grande expansão!), nosso restaurante combina o sabor autêntico dos clássicos do fast food com a inovação da tecnologia de autoatendimento. Aqui, os clientes podem personalizar seus pedidos, escolhendo entre uma variedade de ingredientes frescos e molhos exclusivos. Nossa equipe dedicada garante que cada pedido seja preparado com perfeição. Além disso, oferecemos um ambiente moderno e acolhedor, onde os clientes podem relaxar e desfrutar de suas refeições. Com o sistema construído exclusivamente para a Ctrl+Eat, prometemos uma experiência deliciosa, rápida e personalizada para todos os amantes de fast food. Venha saborear o futuro da alimentação rápida!

## Conteúdo

- [Event Storming](https://miro.com/app/board/uXjVMlp82Do=/?share_link_id=110707337684)
- [Dockerfile & docker-compose](./docs/features/06%20Docker/docker.md)
- [Mapa de Navegação e uso de APIs](https://miro.com/app/board/uXjVNXQyIeY=/?share_link_id=702397873101)
- [Domínios](./docs/features/00%20Domínios/dominios.md)
- [Mapa de Contexto](./docs/features/01%20Mapa%20de%20Contexto/mapa_contexto.md)
- [Funcionalidades](./docs/features/features.md)
- [Diagrama de Relacionamento de Entidades](./docs/database/database.md)
- [Diagrama de Arquitetura](./docs/features/08%20Arquitetura/architecture.md)

## Containers

Este projeto utiliza o Docker para executar os serviços necessários para a aplicação funcionar corretamente. Para facilitar a execução, foi criado um arquivo `docker-compose.yml` que contém todos os serviços necessários.

Ao executar o comando para inicializar os containers, o Docker irá baixar as imagens necessárias e executar os seguintes containers:

- PostgreSQL (16.0)
- pgAdmin (7.8)
- Web API (.NET 7)

## Dependências

Para executar esta aplicação são necessárias as seguintes dependências:

- [Docker](https://docs.docker.com/engine/install/)
- [Makefile](https://linuxhint.com/install-make-ubuntu/)
- [JQ](https://jqlang.github.io/jq/)

### WSL2

Sugiro utilizar o [WSL2](https://learn.microsoft.com/pt-br/windows/wsl/install) para executar esta aplicação.

### Docker

Primeiro, atualize as dependências do sistema:
```bash
sudo apt update && sudo apt upgrade
```

Remova as versões antigas:    
```bash
sudo apt remove docker docker-engine docker.io containerd runc
```

Instale os pré-requisitos do Docker:
```bash
sudo apt-get install \
    apt-transport-https \
    ca-certificates \
    curl \
    gnupg \
    lsb-release
```

Adicione o repositório do Docker nos sources do Ubuntu:
```bash
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg
echo \
  "deb [arch=amd64 signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu \
  $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
```

Instale o Docker:
```bash
sudo apt-get install docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin
```

Dê as permissões para rodar o Docker com o usuário atual:
```bash
sudo usermod -aG docker $USER
```

Execute o Docker:
```bash
sudo service docker start
```

### Makefile

Instale o Makefile:
```bash
sudo apt install make
```

### JQ

Instale o JQ:
```bash
sudo apt install jq
```

Uma vez com todas as dependências instaladas, podemos prosseguir!

## Manifestos Kubernetes

Este projeto utiliza o Kubernetes para executar os serviços necessários para a aplicação funcionar corretamente. Para facilitar a execução, foi criado uma pasta `infra` que contém todos os manifestos necessários.

Clique [aqui](./infra/kubernetes.md) para mais informações sobre o Kubernetes deste projeto.

## Inicializando a aplicação via Kubernetes

1 - Inicialize os containers do banco de dados:
```bash
make kube-db-up
```

Aguarde os containers do banco de dados subirem e execute o comando abaixo para verificar se os pods estão no estado **Running**:
```bash
kubectl get pods
```

2 - Inicialize os containers da aplicação:
```bash
make kube-api-up
```

Aguarde os containers da aplicação subirem e execute o comando abaixo para verificar se os pods estão no estado **Running**:
```bash
kubectl get pods
```

3 - Realize o seed dos dados:
```bash
make seed-kube
```

4 - Derrubando os containers da aplicação:
```bash
make kube-api-down
```

5 - Derrubando os containers do banco de dados:
```bash
make kube-db-down
```

## Testando a aplicação via Postman

1 - Executando as APIs via Postman
  1. Importar o arquivo da [collection](./docs/features/07%20Postman/postman.json).
  2. Importar o arquivo do environment [Kubernetes](./docs/features/07%20Postman/local-kubernetes.postman_environment.json).

2 - Criando um **Cliente**
  1. Com a collection e environment importados, execute a rota que está em **Clientes > Cadastrar Cliente**.
  2. A collection está configurada para pegar o id do cliente criado e salvar no environment, para que possa ser utilizado nas próximas requisições.
  3. Para verificar se o cliente foi criado, execute a rota que está em **Clientes > Obter todos os clientes**.

3 - Criando um **Pedido**
  1. Execute a rota que está em **Pedidos > Criar um pedido para um cliente**.
  2. A collection está configurada para pegar o id do pedido criado e salvar no environment, para que possa ser utilizado nas próximas requisições.
  3. Para verificar se o pedido foi criado, execute a rota que está em **Pedidos > Visualizar um pedido**.
  4. Uma vez que o pedido foi criado, o status do pedido é será **Created**.

4 - Adicionando itens ao **Pedido**
  1. Execute a rota que está em **Pedidos > Adicionar um lanche ao pedido**.
  2. Execute a rota que está em **Pedidos > Adicionar um acompanhamento ao pedido**.
  3. Execute a rota que está em **Pedidos > Adicionar uma bebida ao pedido**.
  4. Execute a rota que está em **Pedidos > Adicionar uma sobremesa ao pedido**.
  5. Para verificar se os itens foram adicionados ao pedido, execute a rota que está em **Pedidos > Visualizar um pedido**.
  6. ATENÇÃO: Os pedidos exibidos nessa rota devem estar nos seguintes status **Received**, **OnGoing** ou **Done**. Os pedidos **Created** estão aguardando o pagamento e os pedidos **Completed** já foram retirados pelo cliente.

5 - Realizando o checkout do **Pedido**
  1. Execute a rota que está em **Pedidos > Realizar o checkout do pedido**.
  2. Para verificar se o checkout foi realizado, execute a rota que está em **Pedidos > Visualizar um pedido**.

6 - Simulando o hook de **Pagamento** do **Pedido**
  1. Execute a rota que está em **Pedidos > Simular o hook de pagamento** para aprovar o pagamento.
  2. Para verificar se o pagamento foi realizado, execute a rota que está em **Pedidos > Visualizar um pedido**.
  3. Uma vez que o pagamento foi realizado, o pedido é disponibilizado para a cozinha, e o status do pedido é alterado para **Received**.

7 - Iniciando um pedido na **Cozinha**
  1. Execute a rota que está em **Pedidos > Iniciar preparo do pedido**.
  2. Para verificar se o pedido foi iniciado na cozinha, execute a rota que está em **Pedidos > Visualizar um pedido**.
  3. Uma vez que o pedido foi iniciado na cozinha, o status do pedido é alterado para **OnGoing**.

8 - Pronto para retirar um pedido na **Cozinha**
  1. Execute a rota que está em **Pedidos > Pedido pronto para retirada**.
  2. Para verificar se o pedido está pronto para retirar, execute a rota que está em **Pedidos > Visualizar um pedido**.
  3. Uma vez que o pedido está pronto para retirar, o status do pedido é alterado para **Done**.

9 - Pedido entregue ao **Cliente**
  1. Execute a rota que está em **Pedidos > Pedido entregue ao cliente**.
  2. Para verificar se o pedido foi entregue ao cliente, execute a rota que está em **Pedidos > Visualizar um pedido**.
  3. Uma vez que o pedido foi entregue ao cliente, o status do pedido é alterado para **Completed**.
