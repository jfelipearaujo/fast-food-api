#!/bin/sh

# seed product categories
echo "Lanches - Seeding product categories..."

categoryId=$(curl -X 'POST' \
  'http://localhost:5001/api/v1/products/categories' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "description": "Lanches"
}' 2>/dev/null | jq --raw-output '"\(.id)"')

echo "Lanches - Seeding products..."

productId_1=$(curl -X 'POST' \
  'http://localhost:5001/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Hambúrguer Clássico",
  "amount": 16.9,
  "currency": "BRL"
}' 2>/dev/null | jq --raw-output '"\(.id)"')

productId_2=$(curl -X 'POST' \
  'http://localhost:5001/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Hamburguer de Frango Grelhado",
  "amount": 14.5,
  "currency": "BRL"
}' 2>/dev/null | jq --raw-output '"\(.id)"')

productId_3=$(curl -X 'POST' \
  'http://localhost:5001/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Hamburguer Vegetariano",
  "amount": 12.99,
  "currency": "BRL"
}' 2>/dev/null | jq --raw-output '"\(.id)"')

productId_4=$(curl -X 'POST' \
  'http://localhost:5001/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Hamburguer Duplo com Queijo",
  "amount": 15.5,
  "currency": "BRL"
}' 2>/dev/null | jq --raw-output '"\(.id)"')

productId_5=$(curl -X 'POST' \
  'http://localhost:5001/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Hamburguer Picante com Jalapeños",
  "amount": 13.75,
  "currency": "BRL"
}' 2>/dev/null | jq --raw-output '"\(.id)"')

echo "Lanches - Seeding product images..."

curl -X "POST" \
  "http://localhost:5001/api/v1/products/"$productId_1"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/lanches/product_01.jpg;type=image/jpeg" 2>/dev/null

curl -X "POST" \
  "http://localhost:5001/api/v1/products/"$productId_2"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/lanches/product_02.jpg;type=image/jpeg" 2>/dev/null

curl -X "POST" \
  "http://localhost:5001/api/v1/products/"$productId_3"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/lanches/product_03.jpg;type=image/jpeg" 2>/dev/null

curl -X "POST" \
  "http://localhost:5001/api/v1/products/"$productId_4"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/lanches/product_04.jpg;type=image/jpeg" 2>/dev/null

curl -X "POST" \
  "http://localhost:5001/api/v1/products/"$productId_5"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/lanches/product_05.jpg;type=image/jpeg" 2>/dev/null

echo "Lanches - Done"