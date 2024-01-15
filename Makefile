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

# variables
api_port=5001
api_image_version=1.5
db_image_version=1.1

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

seed-all-local: $(eval api_port = 5001) seed-all

seed-all-kube: $(eval api_port = 30002) seed-all

seed-lanches:
	sh ./api/CtrlEat/scripts/api/seed_lanches.sh $(api_port)

seed-acompanhamentos:
	sh ./api/CtrlEat/scripts/api/seed_acompanhamentos.sh $(api_port)

seed-bebidas:
	sh ./api/CtrlEat/scripts/api/seed_bebidas.sh $(api_port)

seed-sobremesas:
	sh ./api/CtrlEat/scripts/api/seed_sobremesas.sh $(api_port)

docker-build-api:
	cd api/CtrlEat && docker build -t jsfelipearaujo/ctrl-eat-api:v$(api_image_version) .

docker-push-api:
	docker push jsfelipearaujo/ctrl-eat-api:v$(api_image_version)

docker-build-db:
	cd api/CtrlEat/scripts/database && docker build -t jsfelipearaujo/ctrl-eat-db:v$(db_image_version) .

docker-push-db:
	docker push jsfelipearaujo/ctrl-eat-db:v$(db_image_version)

kube-metrics:
	kubectl apply -f ./infra/metrics.yaml

kube-up: kube-db-up kube-api-up

kube-down: kube-db-down kube-api-down

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