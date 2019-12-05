variable "client_id" {
  type = string
}

variable "client_secret" {
  type = string
}

variable "tenant_id" {
  type = string
}

variable "subscription_id" {
  type = string
}

terraform {
  required_version = "0.12.17"
  
  required_providers {
    azurerm = "=1.36.0"
  }
}

provider "azurerm" {
  version = "=1.36.0"
  
  client_id = var.client_id
  client_secret = var.client_secret
  subscription_id = var.subscription_id
  tenant_id = var.tenant_id
}