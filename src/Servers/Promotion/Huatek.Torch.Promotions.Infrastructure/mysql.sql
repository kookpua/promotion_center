--  IF EXISTS(SELECT 1 FROM information_schema.tables 
--  WHERE table_name = '
--'__EFMigrationsHistory'' AND table_schema = DATABASE()) 
--BEGIN
--CREATE TABLE `__EFMigrationsHistory` (
--    `MigrationId` varchar(150) NOT NULL,
--    `ProductVersion` varchar(32) NOT NULL,
--    PRIMARY KEY (`MigrationId`)
--);

--END;

CREATE TABLE `Promotion` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Title` varchar(50) NOT NULL,
    `Description` varchar(1000) NOT NULL,
    `PromotionTypeId` int NOT NULL,
    `CreatedOnUtc` datetime NOT NULL,
    `CreatedCustomerId` int NOT NULL,
    `StartDate` datetime NOT NULL,
    `EndDate` datetime NOT NULL,
    `PromotionStateId` int NOT NULL,
    `Deleted` bit NOT NULL,
    `PromotionProductTypeId` int NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `PromotionProduct` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `PromotionId` int NOT NULL,
    `ProductId` int NOT NULL,
    `Price` decimal(18,4) NULL,
    `StockQuantity` int NULL,
    `Deleted` bit NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_PromotionProduct_Promotion_PromotionId` FOREIGN KEY (`PromotionId`) REFERENCES `Promotion` (`Id`) ON DELETE CASCADE
);

INSERT INTO `Promotion` (`Id`, `CreatedCustomerId`, `CreatedOnUtc`, `Deleted`, `Description`, `EndDate`, `PromotionProductTypeId`, `PromotionStateId`, `PromotionTypeId`, `StartDate`, `Title`)
VALUES (1, 1, '2020-07-09 18:36:52.880512', False, '活动1的说明', '2020-10-17 18:36:52.880631', 1, 1, 2, '2020-07-09 18:36:52.880592', '活动1');

INSERT INTO `Promotion` (`Id`, `CreatedCustomerId`, `CreatedOnUtc`, `Deleted`, `Description`, `EndDate`, `PromotionProductTypeId`, `PromotionStateId`, `PromotionTypeId`, `StartDate`, `Title`)
VALUES (2, 1, '2020-07-09 18:36:52.880787', False, '活动2的说明', '2020-10-17 18:36:52.880791', 2, 1, 1, '2020-07-09 18:36:52.880790', '活动2');

INSERT INTO `Promotion` (`Id`, `CreatedCustomerId`, `CreatedOnUtc`, `Deleted`, `Description`, `EndDate`, `PromotionProductTypeId`, `PromotionStateId`, `PromotionTypeId`, `StartDate`, `Title`)
VALUES (3, 1, '2020-07-09 18:36:52.880794', False, '活动3的说明', '2020-10-17 18:36:52.880794', 3, 1, 1, '2020-07-09 18:36:52.880794', '活动3');

INSERT INTO `PromotionProduct` (`Id`, `Deleted`, `Price`, `ProductId`, `PromotionId`, `StockQuantity`)
VALUES (1, False, 13, 6, 1, 111111);
INSERT INTO `PromotionProduct` (`Id`, `Deleted`, `Price`, `ProductId`, `PromotionId`, `StockQuantity`)
VALUES (2, False, 14, 5, 1, 11111);
INSERT INTO `PromotionProduct` (`Id`, `Deleted`, `Price`, `ProductId`, `PromotionId`, `StockQuantity`)
VALUES (3, False, 15, 4, 1, 1111);
INSERT INTO `PromotionProduct` (`Id`, `Deleted`, `Price`, `ProductId`, `PromotionId`, `StockQuantity`)
VALUES (4, False, 16, 3, 1, 111);
INSERT INTO `PromotionProduct` (`Id`, `Deleted`, `Price`, `ProductId`, `PromotionId`, `StockQuantity`)
VALUES (5, False, 17, 2, 1, 11);

CREATE INDEX `IX_PromotionProduct_PromotionId` ON `PromotionProduct` (`PromotionId`);

--INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
--VALUES ('20200709103653_initial', '3.1.5'); 
--privileges

