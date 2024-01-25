from diagrams import Cluster, Diagram
from diagrams.aws.compute import EC2, EKS
from diagrams.aws.storage import S3
from diagrams.aws.security import SecretsManager
from diagrams.aws.management import SystemsManagerParameterStore as ParameterStore
from diagrams.aws.database import RDSPostgresqlInstance as RDS
from diagrams.aws.network import ElbApplicationLoadBalancer as ALB, APIGateway
from diagrams.aws.security import WAF
from diagrams.aws.general import Users

diagram_attr = {
    "fontsize": "25",
    "bgcolor": "white",
    "pad": "0.5",
    "splines": "spline",
}

node_attr = {
    "fontsize": "20",
    "size": "5",
    "bgcolor": "white",
    "margin": "0.5",
    "height": "2.1",
    "pad": "1"
}

cluster_attr = {
    "fontsize": "20",
    "size": "5",
    "margin": "8",
    "pad": "2"
}

item_attr = {
    "fontsize": "20",
    "height": "2.2"
}

with Diagram("Cloud Kubernetes AWS", show=False, graph_attr=diagram_attr):    
    users = Users("Users", **item_attr)

    with Cluster("AWS", graph_attr=cluster_attr):
        waf = WAF("WAF", **item_attr)

        users >> waf

        with Cluster("VPC", graph_attr=cluster_attr):
            with Cluster("Public Subnet", graph_attr=cluster_attr):
                api_gateway = APIGateway("API Gateway", **item_attr)
                waf >> api_gateway

            with Cluster("Private Subnet", graph_attr=cluster_attr):
                alb = ALB("ALB", **item_attr)
                api_gateway >> alb

                s3 = S3("Bucket /images", **item_attr)
                secrets_manager = SecretsManager("Secrets", **item_attr)
                parameter_store = ParameterStore("Parameters", **item_attr)
                rds = RDS("RDS", **item_attr)

                service = EKS("k8s Service", **item_attr)

                with Cluster("EKS Cluster", graph_attr=cluster_attr):
                    alb >> service

                    pods = []

                    labels = ["...", "Pod 1", "Pod 5"]

                    for i in range(0, 3):
                        pod = EC2(labels[i], **item_attr)
                        pod >> s3
                        pod >> secrets_manager
                        pod >> parameter_store
                        pod >> rds
                        pods.append(service >> pod)