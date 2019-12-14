terraform {
  required_version = "0.12.17"
}

provider "azurerm" {
  version = "=1.36.0"
}

provider "aws" {
  version = "~> 2.0"
  region = "us-east-1"
}