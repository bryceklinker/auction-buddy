# Generating Migrations

## Event Persistence

```bash
dotnet ef migrations add {Migration Name} \
    --project ./src/Auction.Buddy.Persistence \
    --startup-project ./src/Auction.Buddy.Api \
    --context Auction.Buddy.Persistence.Common.Storage.EntityFrameworkEventPersistence \
    --output-dir Common/Storage/Migrations/EventPersistence
```

## Read Store

```bash
dotnet ef migrations add {Migration Name} \
    --project ./src/Auction.Buddy.Persistence \
    --startup-project ./src/Auction.Buddy.Api \
    --context Auction.Buddy.Persistence.Common.Storage.EntityFrameworkReadStore \
    --output-dir Common/Storage/Migrations/ReadStore
```