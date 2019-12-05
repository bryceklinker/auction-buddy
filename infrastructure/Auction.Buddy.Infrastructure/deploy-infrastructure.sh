#!/usr/bin/env bash
set -ex

TERRAFORM_URL="https://releases.hashicorp.com/terraform/0.12.17/terraform_0.12.17_linux_amd64.zip"

sudo apt-get update

sudo apt-get install wget unzip

sudo wget ${TERRAFORM_URL}

unzip terraform_0.12.7_linux_amd64.zip

sudo mv terraform /usr/local/bin/

pushd ./prod

terraform init
terraform plan -out=./tfplan.plan

popd