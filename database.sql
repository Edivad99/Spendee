CREATE DATABASE IF NOT EXISTS Spendee;
USE Spendee;

DROP TABLE IF EXISTS Transactions;
DROP TABLE IF EXISTS Wallets;
DROP TABLE IF EXISTS Categories;


CREATE TABLE Wallets
(
	`ID` INT NOT NULL AUTO_INCREMENT,
	`Name` VARCHAR(100) NOT NULL,
	PRIMARY KEY(`ID`)
) ENGINE = InnoDB;

CREATE TABLE Categories
(
    `ID` INT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(100) NOT NULL,
    PRIMARY KEY (`ID`)
) ENGINE = InnoDB;

CREATE TABLE Transactions
(
    `ID` INT NOT NULL AUTO_INCREMENT,
    `Description` VARCHAR(100) NOT NULL,
    `Price` FLOAT NOT NULL,
	`Date` DATETIME NOT NULL,
    `CategoryID` INT NOT NULL,
	`WalletID` INT NOT NULL,
    PRIMARY KEY (`ID`),
    FOREIGN KEY (`CategoryID`) REFERENCES `Categories` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (`WalletID`) REFERENCES `Wallets` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE = InnoDB;

INSERT INTO `Wallets` (`ID`, `Name`) VALUES
(1, 'Test');

INSERT INTO `Categories` (`ID`, `Name`) VALUES
(1, 'Abbigliamento'),
(2, 'Ricarica telefonica'),
(3, 'Cibo'),
(4, 'Casa'),
(5, 'Abbonamenti'),
(6, 'Lavoro'),
(7, 'Shopping'),
(8, 'Trasporto'),
(9, 'Istruzione'),
(10, 'Spese bancarie');

INSERT INTO `Transactions` (`WalletID`, `ID`, `CategoryID`, `Description`, `Price`, `Date`) VALUES
(1, 1, 2, 'Iliad', '-20', '2021-11-28'),
(1, 2, 6, 'Stipendio Pattinaggio (Mese di Ottobre)', '77', '2021-11-08');
