from diagrams import Cluster, Diagram
from diagrams.k8s.compute import Deploy as Deployment, Pod, ReplicaSet
from diagrams.k8s.network import Service
from diagrams.k8s.storage import PV, PVC, StorageClass
from diagrams.k8s.clusterconfig import HPA
from diagrams.k8s.podconfig import ConfigMap, Secret
from diagrams.k8s.infra import Node

graph_attr = {
    "fontsize": "25",
    "bgcolor": "white"
}

with Diagram("Cloud Kubernetes Local", show=False, graph_attr=graph_attr):
    node = Node("docker-desktop")        

    with Cluster("API"):
        service = Service("ctrl-eat-api")
        pv = PV("pv")
        sc = StorageClass("sc")

        node >> service

        pods = []

        with Cluster("Pods (up to 5 pods)"):
            for _ in range(1):
                pod = Pod("api")
                pvc = PVC("pvc")
                pods.append(service >> pod >> pvc)
        
        pods << pv << sc

        deployment = Deployment("ctrl-eat-api")
        config = ConfigMap("config")
        secret = Secret("secret")
        hpa = HPA("hpa")
        rs = ReplicaSet("rs")

        deployment << config
        deployment << secret

        rs << deployment << hpa

        pods >> rs

    with Cluster("Database"):
        service = Service("ctrl-eat-db")
        pv = PV("pv")
        sc = StorageClass("sc")

        node >> service

        pods = []

        with Cluster("Pods (up to 1 pods)"):
            for _ in range(1):
                pod = Pod("db")
                pvc = PVC("pvc")
                pods.append(service >> pod >> pvc)
        
        pods << pv << sc

        deployment = Deployment("ctrl-eat-db")
        config = ConfigMap("config")
        secret = Secret("secret")
        rs = ReplicaSet("rs")

        deployment << config
        deployment << secret

        rs << deployment

        pods >> rs
