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
    "height": "2.1"
}

with Diagram("Cloud Kubernetes Local", show=False, graph_attr=diagram_attr):
    node = Node("docker-desktop", **node_attr)        

    with Cluster("API", graph_attr=cluster_attr):
        api_service = Service("ctrl-eat-api", **item_attr)
        pv = PV("pv", **item_attr)
        sc = StorageClass("sc", **item_attr)

        node >> api_service

        pods = []

        with Cluster("Pods (up to 5 pods)", graph_attr=cluster_attr):
            for _ in range(1):
                pod = Pod("api", **item_attr)
                pvc = PVC("pvc", **item_attr)
                pods.append(api_service >> pod >> pvc)
        
        pods << pv << sc

        deployment = Deployment("ctrl-eat-api", **item_attr)
        config = ConfigMap("config", **item_attr)
        secret = Secret("secret", **item_attr)
        hpa = HPA("hpa", **item_attr)

        deployment << config
        deployment << secret

        deployment << hpa

        pods >> deployment

    with Cluster("Database", graph_attr=cluster_attr):
        db_service = Service("ctrl-eat-db", **item_attr)
        pv = PV("pv", **item_attr)
        sc = StorageClass("sc", **item_attr)

        api_service >> Edge(label="") << db_service

        node >> db_service

        pods = []

        with Cluster("Pods (up to 1 pods)", graph_attr=cluster_attr):
            for _ in range(1):
                pod = Pod("db", **item_attr)
                pvc = PVC("pvc", **item_attr)
                pods.append(db_service >> pod >> pvc)
        
        pods << pv << sc

        deployment = Deployment("ctrl-eat-db", **item_attr)
        config = ConfigMap("config", **item_attr)
        secret = Secret("secret", **item_attr)

        deployment << config
        deployment << secret

        pods >> Edge(label="") << deployment
