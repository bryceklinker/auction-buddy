resource "azurerm_app_service_plan" "api_plan" {
  name = local.app_service_plan_name
  location = var.location
  resource_group_name = local.resource_group_name
  
  sku {
    size = "F1"
    tier = "Free"
  }
  
  tags = local.tags
}

resource "azurerm_app_service" "api_service" {
  app_service_plan_id = azurerm_app_service_plan.api_plan.id
  location = var.location
  name = local.app_service_name
  resource_group_name = local.resource_group_name
  
  app_settings = {
    APPINSIGHTS_INSTRUMENTATIONKEY = azurerm_application_insights.insights.instrumentation_key
  }
  
  site_config {
    http2_enabled = true
    websockets_enabled = true
    
    cors {
      allowed_origins = ["*"]
      support_credentials = true
    }
  }

  tags = local.tags
}
