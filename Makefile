.PHONY:
	up \
	down \
	seed-all \
	seed-lanches \
	seed-acompanhamentos \
	seed-bebidas \
	seed-sobremesas \

up:
	docker compose -f ./api/CtrlEat/docker-compose.yml up -d

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