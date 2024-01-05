.PHONY:
	test \
	coverage \
	up \
	up-build \
	down \
	seed-all \
	seed-lanches \
	seed-acompanhamentos \
	seed-bebidas \
	seed-sobremesas \
	docker-build-api \
	docker-push-api \
	docker-build-db \
	docker-push-db \
	kube-up \
	kube-down \
	kube-api-up \
	kube-api-down \
	kube-db-up \
	kube-db-down \

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

seed-all: seed-lanches seed-acompanhamentos seed-bebidas seed-sobremesas

seed-lanches:
	sh ./api/CtrlEat/scripts/api/seed_lanches.sh 5001

seed-acompanhamentos:
	sh ./api/CtrlEat/scripts/api/seed_acompanhamentos.sh 5001

seed-bebidas:
	sh ./api/CtrlEat/scripts/api/seed_bebidas.sh 5001

seed-sobremesas:
	sh ./api/CtrlEat/scripts/api/seed_sobremesas.sh 5001

docker-build-api:
	cd api/CtrlEat && docker build -t jsfelipearaujo/ctrl-eat-api:v1.1 .

docker-push-api:
	docker push jsfelipearaujo/ctrl-eat-api:v1.1

docker-build-db:
	cd api/CtrlEat/scripts/database && docker build -t jsfelipearaujo/ctrl-eat-db:v1 .

docker-push-db:
	docker push jsfelipearaujo/ctrl-eat-db:v1

kube-up:
	kubectl apply \
		-f ./infra/db-pv.yaml \
		-f ./infra/db-pvc.yaml \
		-f ./infra/db-configmap.yaml \
		-f ./infra/db-secret.yaml \
		-f ./infra/db-deployment.yaml \
		-f ./infra/db-service.yaml \
		-f ./infra/api-pv.yaml \
		-f ./infra/api-pvc.yaml \
		-f ./infra/api-configmap.yaml \
		-f ./infra/api-secret.yaml \
		-f ./infra/api-deployment.yaml \
		-f ./infra/api-service.yaml

kube-down:
	kubectl delete \
		-f ./infra/db-pv.yaml \
		-f ./infra/db-pvc.yaml \
		-f ./infra/db-configmap.yaml \
		-f ./infra/db-secret.yaml \
		-f ./infra/db-deployment.yaml \
		-f ./infra/db-service.yaml \
		-f ./infra/api-pv.yaml \
		-f ./infra/api-pvc.yaml \
		-f ./infra/api-configmap.yaml \
		-f ./infra/api-secret.yaml \
		-f ./infra/api-deployment.yaml \
		-f ./infra/api-service.yaml

kube-db-up:
	kubectl apply \
		-f ./infra/db-pv.yaml \
		-f ./infra/db-pvc.yaml \
		-f ./infra/db-configmap.yaml \
		-f ./infra/db-secret.yaml \
		-f ./infra/db-deployment.yaml \
		-f ./infra/db-service.yaml

kube-db-down:
	kubectl delete \
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
		-f ./infra/api-service.yaml

kube-api-down:
	kubectl delete \
		-f ./infra/api-pv.yaml \
		-f ./infra/api-pvc.yaml \
		-f ./infra/api-configmap.yaml \
		-f ./infra/api-secret.yaml \
		-f ./infra/api-deployment.yaml \
		-f ./infra/api-service.yaml