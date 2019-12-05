#!/usr/bin/env bash
set -ex

TERRAFORM_VERSION="0.12.17"
TERRAFORM_URL="https://releases.hashicorp.com/terraform/${TERRAFORM_VERSION}/terraform_${TERRAFORM_VERSION}_linux_amd64.zip"

sudo apt-get update

sudo apt-get install wget unzip

sudo wget ${TERRAFORM_URL}

unzip terraform_${TERRAFORM_VERSION}_linux_amd64.zip

sudo mv terraform /usr/local/bin/

pushd ./prod

az login --service-principal --username $ARM_CLIENT_ID --password $ARM_CLIENT_SECRET --tenant $ARM_TENANT_ID
az account set --subscription $ARM_SUBSCRIPTION_ID
terraform init
terraform plan -out=./tfplan.plan

popd