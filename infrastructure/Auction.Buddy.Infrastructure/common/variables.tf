variable "env_name" {
  type = string
}

variable "app_name" {
  type = string
}

variable "location" {
  type = string
  default = "North Central US"
}

locals {
  resource_group_name = "${var.app_name}-${var.env_name}-rg"
  app_service_plan_name = "${var.app_name}-${var.env_name}-plan"
  app_service_name = "${var.app_name}-${var.env_name}-service"
  insights_name = "${var.app_name}-${var.env_name}-insights"
  
  tags = {
    environment = var.env_name
    application = var.app_name
  }
}