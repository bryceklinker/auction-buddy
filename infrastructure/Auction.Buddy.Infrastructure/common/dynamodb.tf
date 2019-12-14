resource "aws_dynamodb_table" "events-table" {
  name = "${var.env_name}-${var.app_name}-events"
  hash_key = "AggregateId"
  billing_mode = "PROVISIONED"

  write_capacity = 5
  read_capacity = 5
  
  attribute {
    name = "AggregateId"
    type = "S"
  }
  
  tags = local.tags
}

resource "aws_dynamodb_table" "read-auctions" {
  name = "${var.env_name}-${var.app_name}-auctions-read"
  hash_key = "Id"
  billing_mode = "PROVISIONED"

  write_capacity = 5
  read_capacity = 5
  
  attribute {
    name = "Id"
    type = "S"
  }

  tags = local.tags
}

resource "aws_dynamodb_table" "read-auction-items" {
  name = "${var.env_name}-${var.app_name}-auction-items-read"
  hash_key = "Id"
  billing_mode = "PROVISIONED"
  
  write_capacity = 5
  read_capacity = 5
  
  attribute {
    name = "Id"
    type = "S"
  }
  
  tags = local.tags
}