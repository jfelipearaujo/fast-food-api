from diagrams import Cluster, Diagram, Edge
from diagrams.aws.storage import S3
from diagrams.aws.database import RDSPostgresqlInstance as RDS
from diagrams.onprem.ci import GithubActions
from diagrams.aws.compute import LambdaFunction as Lambda

from functools import partial

Edge = partial(Edge, fontsize="15")

diagram_attr = {
    "fontsize": "25",
    "bgcolor": "white",
    "pad": "0.5",
    "splines": "spline",
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

last_step_label = """
4. descompact .zip files
5. apply migrations
6. delete .zip files
"""

diagram_label = """

Cloud AWS Database Migrations

"""

with Diagram(diagram_label, show=False, graph_attr=diagram_attr, direction="LR"):
    github = GithubActions("Github Actions", **item_attr)

    with Cluster("AWS", graph_attr=cluster_attr):
        s3 = S3("Bucket /migrations", **item_attr)
        lambda_function = Lambda("Lambda Migrator", **item_attr)
        rds = RDS("RDS", **item_attr)

        github >> Edge(label="1. upload .zip files to") >> s3
        s3 >> Edge(label="2. trigger the lambda") >> lambda_function
        
        lambda_function << Edge(label="3. download .zip files") >> s3
        lambda_function >> Edge(label=last_step_label) >> rds


