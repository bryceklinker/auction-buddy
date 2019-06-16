#!/bin/sh -x

SQL_SERVER_PASSWORD="ThisIsAStrongPassword!"
CONTAINER_IMAGE_VERSION="latest"
CONTAINER_IMAGE_NAME="microsoft/mssql-server-linux"
CONTAINER_NAME="auction_buddy_database"
CONTAINER_IMAGE_FULL_NAME="${CONTAINER_IMAGE_NAME}:${CONTAINER_IMAGE_VERSION}"

docker image ls | grep -i "${CONTAINER_IMAGE_NAME}"
if [[ "$?" == "1" ]]; then
    echo "Pulling MSSQL Server Linux container image" 
    docker pull "${CONTAINER_IMAGE_FULL_NAME}"
fi

docker container ls | grep -i "${CONTAINER_NAME}"
if [[ "$?" == "0" ]]; then
    echo "Stopping ${CONTAINER_NAME} container"
    docker stop ${CONTAINER_NAME}
    
    echo "Removing ${CONTAINER_NAME} container"
    docker rm ${CONTAINER_NAME}
fi

echo "Starting MSSQL Server..."
docker run -e 'ACCEPT_EULA=Y' -e "MSSQL_SA_PASSWORD=${SQL_SERVER_PASSWORD}" -p 1401:1433 --name ${CONTAINER_NAME} -d ${CONTAINER_IMAGE_FULL_NAME}

