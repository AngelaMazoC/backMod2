#!/bin/bash
cd ../..

current_branch=$(git rev-parse --abbrev-ref HEAD)

read -r -p "ingrese que avance realizo en la tarea corto y descriptivo: " message

git add .

git commit -m"feature/$current_branch/$message"

git push origin "$current_branch"

sleep 10s
