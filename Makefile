.PHONY:
	up \
	down \

up:
	docker compose -f ./api/CtrlEat/docker-compose.yml up -d

down:
	docker compose -f ./api/CtrlEat/docker-compose.yml down