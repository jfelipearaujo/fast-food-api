from diagrams import Cluster, Diagram
from diagrams.aws.compute import ECS, EKS
from diagrams.aws.storage import S3
from diagrams.aws.security import SecretsManager
from diagrams.aws.management import SystemsManagerParameterStore as ParameterStore
from diagrams.aws.database import RDSPostgresqlInstance as RDS
from diagrams.aws.network import ElbApplicationLoadBalancer as ALB
from diagrams.aws.security import WAF
from diagrams.aws.general import Users

graph_attr = {
    "fontsize": "25",
    "bgcolor": "white"
}

with Diagram("Cloud Kubernetes AWS", show=False, graph_attr=graph_attr):    
    users = Users("Internet")

    with Cluster("AWS"):
        waf = WAF("WAF")

        users >> waf

        with Cluster("VPC"):
            with Cluster("Public Subnet"):
                alb = ALB("ALB")
                waf >> alb

            with Cluster("Private Subnet"):
                s3 = S3("Bucket /images")
                secrets_manager = SecretsManager("Secrets")
                parameter_store = ParameterStore("Parameters")
                rds = RDS("RDS")

                service = EKS("k8s Service")

                with Cluster("EKS Cluster"):
                    alb >> service

                    pods = []

                    for i in range(0, 1):
                        pod = ECS("Pods")
                        pod >> s3
                        pod >> secrets_manager
                        pod >> parameter_store
                        pod >> rds

                        pods.append(service >> pod)