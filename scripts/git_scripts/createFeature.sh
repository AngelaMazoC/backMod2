#!/bin/bash
cd ../..

opciones="main\ndev\n\n\n\n\n"

read -r -p "ingrese el nombre de la tarea: " task

echo -e "$opciones" | git flow init

git flow feature start "$task"

git push --set-upstream origin "feature/$task"

sleep 10s
