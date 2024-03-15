.PHONY:
	test \
	coverage \
	up \
	up-build \
	down \
	seed-local \
	seed-kube \
	docker-build-api \
	docker-push-api \
	docker-build-db \
	docker-push-db \
	kube-api-up \
	kube-api-down \
	kube-db-up \
	kube-db-down \

# variables
api_image_version=1.8
db_image_version=1.2

test:
	dotnet test ./api/CtrlEat/CtrlEat.sln --collect:"XPlat Code Coverage;Format=json,lcov,cobertura"

coverage:
	reportgenerator \
		-reports:"./api/CtrlEat/tests/**/TestResults/**/coverage.cobertura.xml" \
		-targetdir:"coveragereport" \
		-historydir:"coveragereport-hist" \
		-reporttypes:"Html;SvgChart;MarkdownSummaryGithub"

up:
	docker compose -f ./api/CtrlEat/docker-compose.yml up -d

up-build:
	docker compose -f ./api/CtrlEat/docker-compose.yml up -d --build

down:
	docker compose -f ./api/CtrlEat/docker-compose.yml down

gen-cloud-diagrams:
	cd docs/infra && \
	python3 cloud_kubernetes_local.py && \
	python3 cloud_kubernetes_aws.py && \
	python3 cloud_aws_authorizer.py

seed-local:
	sh ./api/CtrlEat/scripts/api/seed_lanches.sh 5001 && \
	sh ./api/CtrlEat/scripts/api/seed_acompanhamentos.sh 5001 && \
	sh ./api/CtrlEat/scripts/api/seed_bebidas.sh 5001 && \
	sh ./api/CtrlEat/scripts/api/seed_sobremesas.sh 5001

seed-kube:
	sh ./api/CtrlEat/scripts/api/seed_lanches.sh 30002 && \
	sh ./api/CtrlEat/scripts/api/seed_acompanhamentos.sh 30002 && \
	sh ./api/CtrlEat/scripts/api/seed_bebidas.sh 30002 && \
	sh ./api/CtrlEat/scripts/api/seed_sobremesas.sh 30002

docker-build-api:
	cd api/CtrlEat && docker build -t jsfelipearaujo/ctrl-eat-api:v$(api_image_version) .

docker-push-api:
	docker push jsfelipearaujo/ctrl-eat-api:v$(api_image_version)

docker-build-db:
	cd api/CtrlEat/scripts/database && docker build -t jsfelipearaujo/ctrl-eat-db:v$(db_image_version) .

docker-push-db:
	docker push jsfelipearaujo/ctrl-eat-db:v$(db_image_version)

kube-db-up:
	kubectl apply \
		-f ./infra/metrics.yaml \
		-f ./infra/db-pv.yaml \
		-f ./infra/db-pvc.yaml \
		-f ./infra/db-configmap.yaml \
		-f ./infra/db-secret.yaml \
		-f ./infra/db-deployment.yaml \
		-f ./infra/db-service.yaml

kube-db-down:
	kubectl delete \
		-f ./infra/metrics.yaml \
		-f ./infra/db-pv.yaml \
		-f ./infra/db-pvc.yaml \
		-f ./infra/db-configmap.yaml \
		-f ./infra/db-secret.yaml \
		-f ./infra/db-deployment.yaml \
		-f ./infra/db-service.yaml

kube-api-up:
	kubectl apply \
		-f ./infra/api-pv.yaml \
		-f ./infra/api-pvc.yaml \
		-f ./infra/api-configmap.yaml \
		-f ./infra/api-secret.yaml \
		-f ./infra/api-deployment.yaml \
		-f ./infra/api-hpa.yaml \
		-f ./infra/api-service.yaml

kube-api-down:
	kubectl delete \
		-f ./infra/api-pv.yaml \
		-f ./infra/api-pvc.yaml \
		-f ./infra/api-configmap.yaml \
		-f ./infra/api-secret.yaml \
		-f ./infra/api-deployment.yaml \
		-f ./infra/api-hpa.yaml \
		-f ./infra/api-service.yaml

k6-run:
	k6 run k6/index.js