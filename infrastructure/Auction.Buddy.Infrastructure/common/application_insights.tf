resource "azurerm_application_insights" "insights" {
  name = local.insights_name
  location = var.location
  resource_group_name = local.resource_group_name
  application_type = "web"

  tags = local.tags
}