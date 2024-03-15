from diagrams import Cluster, Diagram, Edge
from diagrams.aws.database import RDSPostgresqlInstance as RDS
from diagrams.aws.compute import LambdaFunction as Lambda
from diagrams.onprem.client import User
from diagrams.aws.network import APIGateway
from diagrams.aws.compute import ElasticKubernetesService as EKS
from diagrams.aws.security import Cognito

from functools import partial

Edge = partial(Edge, fontsize="15")

diagram_attr = {
    "fontsize": "25",
    "bgcolor": "white",
    "pad": "0.5",
    "splines": "splines",
}

cluster_attr = {
    "fontsize": "15",
    "size": "5",
    "margin": "8",
    "pad": "2"
}

item_attr = {
    "fontsize": "15",
    "height": "2.2"
}

diagram_label = """

Cloud AWS Authorizer

"""

def draw_login_flow(gateway: APIGateway):
    authorizer = Lambda("Authorizer")
    cognito = Cognito("Cognito")

    gateway >> authorizer
    authorizer >> cognito
    cognito >> authorizer  
    authorizer >> gateway

def draw_register_flow(gateway: APIGateway):
    eks = EKS("EKS")
    cognito = Cognito("Cognito")

    gateway >> eks
    eks >> cognito
    cognito >> eks
    eks >> gateway

def draw_authenticated_flow(gateway: APIGateway):
    cognito = Cognito("Cognito")
    eks = EKS("EKS")
    rds = RDS("RDS")

    gateway >> cognito
    cognito >> gateway

    gateway >> eks
    eks >> rds
    rds >> eks
    eks >> gateway

with Diagram(diagram_label, show=False, graph_attr=diagram_attr, direction="LR"):    
    user_register = User("User to be registered")
    user_login = User("User to perform login")
    user_authenticated = User("User authenticated")

    with Cluster("AWS - Register Flow"):
        gateway = APIGateway("API Gateway")
        user_register >> Edge(label="POST /register", color="black") >> gateway
        draw_register_flow(gateway)
        gateway >> Edge(label="Return access_token", color="black") >> user_register

    with Cluster("AWS - Login Flow"):
        gateway = APIGateway("API Gateway")
        user_login >> Edge(label="POST /login", color="black") >> gateway
        draw_login_flow(gateway)
        gateway >> Edge(label="Return access_token", color="black") >> user_login

    with Cluster("AWS - Authenticated Flow"):
        gateway = APIGateway("API Gateway")
        user_authenticated >> Edge(label="GET /orders\nHeader: access_token", color="black") >> gateway
        draw_authenticated_flow(gateway)
        gateway >> Edge(label="Return orders", color="black") >> user_authenticated
