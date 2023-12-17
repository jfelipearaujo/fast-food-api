#!/bin/sh

# seed product categories
echo "Bebidas - Seeding product categories..."

host="http://127.0.0.1:30002"

categoryId=$(curl -X 'POST' \
  $host'/api/v1/products/categories' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "description": "Bebidas"
}' 2>/dev/null | jq --raw-output '"\(.id)"')

echo "Bebidas - Seeding products..."

productId_1=$(curl -X 'POST' \
  $host'/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Refrigerante de 500ml",
  "amount": 2.5,
  "currency": "BRL"
}' 2>/dev/null | jq --raw-output '"\(.id)"')

productId_2=$(curl -X 'POST' \
  $host'/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Milkshake de Chocolate",
  "amount": 4.99,
  "currency": "BRL"
}' 2>/dev/null | jq --raw-output '"\(.id)"')

productId_3=$(curl -X 'POST' \
  $host'/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Cerveja Artesanal",
  "amount": 6.5,
  "currency": "BRL"
}' 2>/dev/null | jq --raw-output '"\(.id)"')

productId_4=$(curl -X 'POST' \
  $host'/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Água Mineral",
  "amount": 1.5,
  "currency": "BRL"
}' 2>/dev/null | jq --raw-output '"\(.id)"')

productId_5=$(curl -X 'POST' \
  $host'/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Café Preto",
  "amount": 1.75,
  "currency": "BRL"
}' 2>/dev/null | jq --raw-output '"\(.id)"')

echo "Bebidas - Seeding product images..."

curl -X "POST" \
  $host"/api/v1/products/"$productId_1"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/bebidas/product_01.jpg;type=image/jpeg" 2>/dev/null

curl -X "POST" \
  $host"/api/v1/products/"$productId_2"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/bebidas/product_02.jpg;type=image/jpeg" 2>/dev/null

curl -X "POST" \
  $host"/api/v1/products/"$productId_3"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/bebidas/product_03.jpg;type=image/jpeg" 2>/dev/null

curl -X "POST" \
  $host"/api/v1/products/"$productId_4"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/bebidas/product_04.jpg;type=image/jpeg" 2>/dev/null

curl -X "POST" \
  $host"/api/v1/products/"$productId_5"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/bebidas/product_05.jpg;type=image/jpeg" 2>/dev/null

echo "Bebidas - Done"