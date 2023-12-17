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
	sh ./api/CtrlEat/scripts/api/seed_lanches.sh

seed-acompanhamentos:
	sh ./api/CtrlEat/scripts/api/seed_acompanhamentos.sh

seed-bebidas:
	sh ./api/CtrlEat/scripts/api/seed_bebidas.sh

seed-sobremesas:
	sh ./api/CtrlEat/scripts/api/seed_sobremesas.sh

docker-build-api:
	cd api/CtrlEat && docker build -t jsfelipearaujo/ctrl-eat-api:v1.1 .

docker-push-api:
	docker push jsfelipearaujo/ctrl-eat-api:v1.1

docker-build-db:
	cd api/CtrlEat/scripts/database && docker build -t jsfelipearaujo/ctrl-eat-db:v1 .

docker-push-db:
	docker push jsfelipearaujo/ctrl-eat-db:v1

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