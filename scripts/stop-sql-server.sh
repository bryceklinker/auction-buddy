#!/bin/sh -x

CONTAINER_NAME="auction_buddy_database"

docker container ls | grep -i "${CONTAINER_NAME}"
if [[ "$?" == "0" ]]; then
    echo "Stopping ${CONTAINER_NAME} container"
    docker stop ${CONTAINER_NAME}
    
    echo "Removing ${CONTAINER_NAME} container"
    docker rm ${CONTAINER_NAME}
fi

