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

echo "Client ID: ${ARM_CLIENT_ID}"
echo "Client Secret: ${ARM_CLIENT_SECRET}"
echo "Subscription ID: ${ARM_SUBSCRIPTION_ID}"
echo "Tenant ID: ${ARM_TENANT_ID}"

terraform init
terraform plan -out=./tfplan.plan

popd