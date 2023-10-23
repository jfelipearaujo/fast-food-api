#!/bin/sh

# seed product categories
echo "Sobremesas - Seeding product categories..."

categoryId=$(curl -X 'POST' \
  'http://localhost:5001/api/v1/products/categories' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "description": "Sobremesas"
}' 2>/dev/null | jq --raw-output '"\(.id)"')

echo "Sobremesas - Seeding products..."

productId_1=$(curl -X 'POST' \
  'http://localhost:5001/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Sundae de Brownie",
  "amount": 6.99,
  "currency": "BRL",
  "ImageUrl": ""
}' 2>/dev/null | jq --raw-output '"\(.id)"')

productId_2=$(curl -X 'POST' \
  'http://localhost:5001/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Milkshake de Morango",
  "amount": 4.99,
  "currency": "BRL",
  "ImageUrl": ""
}' 2>/dev/null | jq --raw-output '"\(.id)"')

productId_3=$(curl -X 'POST' \
  'http://localhost:5001/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Torta de Maçã Quente",
  "amount": 5.75,
  "currency": "BRL",
  "ImageUrl": ""
}' 2>/dev/null | jq --raw-output '"\(.id)"')

productId_4=$(curl -X 'POST' \
  'http://localhost:5001/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Banana Split",
  "amount": 7.5,
  "currency": "BRL",
  "ImageUrl": ""
}' 2>/dev/null | jq --raw-output '"\(.id)"')

productId_5=$(curl -X 'POST' \
  'http://localhost:5001/api/v1/products' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "productCategoryId": "'"$categoryId"'",
  "description": "Torta de Limão Merengue",
  "amount": 6.25,
  "currency": "BRL",
  "ImageUrl": ""
}' 2>/dev/null | jq --raw-output '"\(.id)"')

echo "Sobremesas - Seeding product images..."

curl -X "POST" \
  "http://localhost:5001/api/v1/products/"$productId_1"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/sobremesas/product_01.jpg;type=image/jpeg" 2>/dev/null

curl -X "POST" \
  "http://localhost:5001/api/v1/products/"$productId_2"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/sobremesas/product_02.jpg;type=image/jpeg" 2>/dev/null

curl -X "POST" \
  "http://localhost:5001/api/v1/products/"$productId_3"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/sobremesas/product_03.jpg;type=image/jpeg" 2>/dev/null

curl -X "POST" \
  "http://localhost:5001/api/v1/products/"$productId_4"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/sobremesas/product_04.jpg;type=image/jpeg" 2>/dev/null

curl -X "POST" \
  "http://localhost:5001/api/v1/products/"$productId_5"/image" \
  -H "accept: */*" \
  -H "Content-Type: multipart/form-data" \
  -F "file=@./api/CtrlEat/scripts/api/images/sobremesas/product_05.jpg;type=image/jpeg" 2>/dev/null

echo "Sobremesas - Done"