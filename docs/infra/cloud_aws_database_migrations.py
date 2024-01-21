from diagrams import Cluster, Diagram, Edge
from diagrams.aws.storage import S3
from diagrams.aws.database import RDSPostgresqlInstance as RDS
from diagrams.onprem.ci import GithubActions
from diagrams.aws.compute import LambdaFunction as Lambda

from functools import partial

Edge = partial(Edge, fontsize="15")

graph_attr = {
    "fontsize": "25",
    "bgcolor": "white"
}

last_step_label = """
4. descompact .zip files
5. apply migrations
6. delete .zip files
"""

diagram_label = """

Cloud AWS Database Migrations

"""

with Diagram(diagram_label, show=False, graph_attr=graph_attr, direction="LR"):
    github = GithubActions("Github Actions")

    with Cluster("AWS"):
        s3 = S3("Bucket /migrations")
        lambda_function = Lambda("Lambda Migrator")
        rds = RDS("RDS")

        github >> Edge(label="1. upload .zip files to") >> s3
        s3 >> Edge(label="2. trigger the lambda") >> lambda_function
        
        lambda_function << Edge(label="3. download .zip files") >> s3
        lambda_function >> Edge(label=last_step_label) >> rds


