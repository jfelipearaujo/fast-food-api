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

kube-db-up:
	kubectl apply \
		-f ./infra/db-pv.yaml \
		-f ./infra/db-pvc.yaml \
		-f ./infra/db-configmap.yaml \
		-f ./infra/db-deployment.yaml \
		-f ./infra/db-service.yaml

kube-db-down:
	kubectl delete \
		-f ./infra/db-pv.yaml \
		-f ./infra/db-pvc.yaml \
		-f ./infra/db-configmap.yaml \
		-f ./infra/db-deployment.yaml \
		-f ./infra/db-service.yaml