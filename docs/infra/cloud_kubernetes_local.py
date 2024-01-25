from diagrams import Cluster, Diagram, Edge
from diagrams.k8s.compute import Deploy as Deployment, Pod
from diagrams.k8s.network import Service
from diagrams.k8s.storage import PV, PVC, StorageClass
from diagrams.k8s.clusterconfig import HPA
from diagrams.k8s.podconfig import ConfigMap, Secret
from diagrams.k8s.infra import Node

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

with Diagram("Cloud Kubernetes Local", show=False, graph_attr=diagram_attr, direction="LR"):
    node = Node("docker-desktop", **node_attr)        

    with Cluster("API", graph_attr=cluster_attr):
        deployment = Deployment("ctrl-eat-api", **item_attr)

        api_service = Service("ctrl-eat-api", **item_attr)
        pv = PV("pv", **item_attr)
        sc = StorageClass("sc", **item_attr)
        
        config = ConfigMap("config", **item_attr)
        secret = Secret("secret", **item_attr)
        hpa = HPA("hpa", **item_attr)

        deployment >> Edge(label="") << api_service
        deployment << config
        deployment << secret
        deployment << hpa

        node >> api_service

        with Cluster("Pods (up to 5 pods)", graph_attr=cluster_attr):
            pod = Pod("api", **item_attr)
            pvc = PVC("pvc", **item_attr)

            pod << pv << sc
            api_service >> pod >> pvc
               

    with Cluster("Database", graph_attr=cluster_attr):
        deployment = Deployment("ctrl-eat-db", **item_attr)
        
        db_service = Service("ctrl-eat-db", **item_attr)
        pv = PV("pv", **item_attr)
        sc = StorageClass("sc", **item_attr)

        config = ConfigMap("config", **item_attr)
        secret = Secret("secret", **item_attr)

        api_service >> Edge(label="") << db_service

        deployment >> Edge(label="") << db_service
        deployment << config
        deployment << secret

        node >> db_service

        with Cluster("Pods (up to 1 pod)", graph_attr=cluster_attr):
            pod = Pod("db", **item_attr)
            pvc = PVC("pvc", **item_attr)
        
            pod << pv << sc
            db_service >> pod >> pvc
