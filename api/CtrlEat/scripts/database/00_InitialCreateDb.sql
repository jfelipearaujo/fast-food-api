CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231023004807_InitialCreateDb') THEN
    CREATE TABLE clients (
        "Id" uuid NOT NULL,
        "FullName_FirstName" character varying(250) NOT NULL,
        "FullName_LastName" character varying(250) NOT NULL,
        "Email" character varying(250) NOT NULL,
        "DocumentId" character varying(14) NOT NULL,
        "DocumentType" integer NOT NULL,
        "IsAnonymous" boolean NOT NULL,
        "CreatedAtUtc" timestamp(7) with time zone NOT NULL,
        "UpdatedAtUtc" timestamp(7) with time zone NOT NULL,
        CONSTRAINT "PK_clients" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231023004807_InitialCreateDb') THEN
    CREATE TABLE product_categories (
        "Id" uuid NOT NULL,
        "Description" character varying(250) NOT NULL,
        "CreatedAtUtc" timestamp(7) with time zone NOT NULL,
        "UpdatedAtUtc" timestamp(7) with time zone NOT NULL,
        CONSTRAINT "PK_product_categories" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231023004807_InitialCreateDb') THEN
    CREATE TABLE orders (
        "Id" uuid NOT NULL,
        "TrackId" character varying(6) NOT NULL,
        "Status" integer NOT NULL,
        "StatusUpdatedAt" timestamp(7) with time zone NOT NULL,
        "ClientId" uuid NOT NULL,
        "CreatedAtUtc" timestamp(7) with time zone NOT NULL,
        "UpdatedAtUtc" timestamp(7) with time zone NOT NULL,
        CONSTRAINT "PK_orders" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_orders_clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES clients ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231023004807_InitialCreateDb') THEN
    CREATE TABLE products (
        "Id" uuid NOT NULL,
        "Description" character varying(250) NOT NULL,
        "Price_Amount" numeric(7,2) NOT NULL,
        "Price_Currency" character varying(3) NOT NULL,
        "ImageUrl" character varying(250) NOT NULL,
        "ProductCategoryId" uuid NOT NULL,
        "CreatedAtUtc" timestamp(7) with time zone NOT NULL,
        "UpdatedAtUtc" timestamp(7) with time zone NOT NULL,
        CONSTRAINT "PK_products" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_products_product_categories_ProductCategoryId" FOREIGN KEY ("ProductCategoryId") REFERENCES product_categories ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231023004807_InitialCreateDb') THEN
    CREATE TABLE payments (
        "Id" uuid NOT NULL,
        "OrderId" uuid NOT NULL,
        "Status" integer NOT NULL,
        "Price_Amount" numeric(7,2) NOT NULL,
        "Price_Currency" character varying(3) NOT NULL,
        "CreatedAtUtc" timestamp(7) with time zone NOT NULL,
        "UpdatedAtUtc" timestamp(7) with time zone NOT NULL,
        CONSTRAINT "PK_payments" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_payments_orders_OrderId" FOREIGN KEY ("OrderId") REFERENCES orders ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231023004807_InitialCreateDb') THEN
    CREATE TABLE orders_items (
        "Id" uuid NOT NULL,
        "Quantity" integer NOT NULL,
        "Observation" character varying(250) NULL,
        "Description" character varying(250) NOT NULL,
        "Price_Amount" numeric(7,2) NOT NULL,
        "Price_Currency" character varying(3) NOT NULL,
        "ImageUrl" character varying(250) NOT NULL,
        "OrderId" uuid NOT NULL,
        "ProductId" uuid NOT NULL,
        "CreatedAtUtc" timestamp(7) with time zone NOT NULL,
        "UpdatedAtUtc" timestamp(7) with time zone NOT NULL,
        CONSTRAINT "PK_orders_items" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_orders_items_orders_OrderId" FOREIGN KEY ("OrderId") REFERENCES orders ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_orders_items_products_ProductId" FOREIGN KEY ("ProductId") REFERENCES products ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231023004807_InitialCreateDb') THEN
    CREATE TABLE stocks (
        "Id" uuid NOT NULL,
        "Quantity" integer NOT NULL,
        "ProductId" uuid NOT NULL,
        "CreatedAtUtc" timestamp(7) with time zone NOT NULL,
        "UpdatedAtUtc" timestamp(7) with time zone NOT NULL,
        CONSTRAINT "PK_stocks" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_stocks_products_ProductId" FOREIGN KEY ("ProductId") REFERENCES products ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231023004807_InitialCreateDb') THEN
    CREATE UNIQUE INDEX "IX_clients_DocumentId" ON clients ("DocumentId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231023004807_InitialCreateDb') THEN
    CREATE UNIQUE INDEX "IX_clients_Email" ON clients ("Email");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231023004807_InitialCreateDb') THEN
    CREATE INDEX "IX_orders_ClientId" ON orders ("ClientId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231023004807_InitialCreateDb') THEN
    CREATE INDEX "IX_orders_items_OrderId" ON orders_items ("OrderId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231023004807_InitialCreateDb') THEN
    CREATE INDEX "IX_orders_items_ProductId" ON orders_items ("ProductId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231023004807_InitialCreateDb') THEN
    CREATE INDEX "IX_payments_OrderId" ON payments ("OrderId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231023004807_InitialCreateDb') THEN
    CREATE INDEX "IX_products_ProductCategoryId" ON products ("ProductCategoryId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231023004807_InitialCreateDb') THEN
    CREATE UNIQUE INDEX "IX_stocks_ProductId" ON stocks ("ProductId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20231023004807_InitialCreateDb') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20231023004807_InitialCreateDb', '7.0.12');
    END IF;
END $EF$;
COMMIT;

