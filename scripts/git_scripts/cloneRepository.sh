#!/bin/bash

read -r -p "Ingrese la URL del repositorio Git: " url

rutas_escritorio=(
    "$HOME/Desktop"
    "$HOME/Escritorio"
    "$HOME/OneDrive/Desktop"
    "$HOME/OneDrive/Escritorio"
)

# shellcheck disable=SC2034
ruta_valida=false
escritorio_encontrado=""

for ruta in "${rutas_escritorio[@]}"; do
    if [ -d "$ruta" ]; then
        echo "Accediendo al escritorio en '$ruta'..."
        escritorio_encontrado="$ruta"
        break
    fi
done

if [ -z "$escritorio_encontrado" ]; then
    echo "No se pudo acceder al escritorio en ninguna de las rutas configuradas."
    exit 1
fi

cd "$escritorio_encontrado" || { echo "No se pudo cambiar al directorio '$escritorio_encontrado'"; exit 1; }

if [ -d ".git" ]; then
    echo "El directorio ya es un repositorio Git."
else
    git init || { echo "Error al inicializar el repositorio Git."; exit 1; }
fi

git clone "$url" || { echo "Error al clonar el repositorio Git desde '$url'."; exit 1; }

echo "Repositorio clonado en el escritorio en '$escritorio_encontrado'."

sleep 10s
