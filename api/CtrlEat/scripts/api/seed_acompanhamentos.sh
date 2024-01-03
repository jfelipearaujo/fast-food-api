#!/bin/sh

# seed product categories
echo "Acompanhamentos - Seeding product categories..."

host="http://127.0.0.1:30002"

categoryId=$(curl -X 'POST' \
  $host'/api/v1/products/categories' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "description": "Acompanhamentos"
}' 2>/dev/null | jq --raw-output '"\(.id)"')

echo "Acompanhamentos - Seeding products..."

productId_1=$(curl -X 'POST' \
  $host'/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Batata Frita Crocante",
  "amount": 3.99,
  "currency": "BRL"
}' 2>/dev/null | jq --raw-output '"\(.id)"')

productId_2=$(curl -X 'POST' \
  $host'/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Anéis de Cebola Fritos",
  "amount": 4.5,
  "currency": "BRL"
}' 2>/dev/null | jq --raw-output '"\(.id)"')

productId_3=$(curl -X 'POST' \
  $host'/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Nachos com Queijo e Guacamole",
  "amount": 5.25,
  "currency": "BRL"
}' 2>/dev/null | jq --raw-output '"\(.id)"')

productId_4=$(curl -X 'POST' \
  $host'/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Molho de Queijo com Bacon",
  "amount": 4.75,
  "currency": "BRL"
}' 2>/dev/null | jq --raw-output '"\(.id)"')

productId_5=$(curl -X 'POST' \
  $host'/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Salada de Repolho Coleslaw",
  "amount": 2.25,
  "currency": "BRL"
}' 2>/dev/null | jq --raw-output '"\(.id)"')

echo "Acompanhamentos - Seeding product images..."

curl -X "POST" \
  $host"/api/v1/products/"$productId_1"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/acompanhamentos/product_01.jpg;type=image/jpeg" 2>/dev/null

curl -X "POST" \
  $host"/api/v1/products/"$productId_2"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/acompanhamentos/product_02.jpg;type=image/jpeg" 2>/dev/null

curl -X "POST" \
  $host"/api/v1/products/"$productId_3"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/acompanhamentos/product_03.jpg;type=image/jpeg" 2>/dev/null

curl -X "POST" \
  $host"/api/v1/products/"$productId_4"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/acompanhamentos/product_04.jpg;type=image/jpeg" 2>/dev/null

curl -X "POST" \
  $host"/api/v1/products/"$productId_5"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/acompanhamentos/product_05.jpg;type=image/jpeg" 2>/dev/null

echo "Acompanhamentos - Done"