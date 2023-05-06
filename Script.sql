CREATE DATABASE VotingSystem;
USE VotingSystem;

CREATE TABLE Candidate (
    id INT PRIMARY KEY AUTO_INCREMENT,
    dpi CHAR(13) UNIQUE NOT NULL,
    name VARCHAR(30) NOT NULL,
    party VARCHAR(60) NOT NULL,
    proposal TEXT NOT NULL
);

CREATE TABLE Person (
    id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100) NOT NULL,
    dpi CHAR(13) UNIQUE NOT NULL
);

CREATE TABLE Vote (
    id INT PRIMARY KEY AUTO_INCREMENT,
    person_id INT NOT NULL,
    candidate_id INT NOT NULL,
    vote INT NOT NULL,
    date DATE NOT NULL,
    ip_address VARCHAR(45) NOT NULL,
    FOREIGN KEY (person_id) REFERENCES Person(id),
    FOREIGN KEY (candidate_id) REFERENCES Candidate(id)
);

CREATE TABLE Status (
	id INT PRIMARY KEY AUTO_INCREMENT,
    table_name VARCHAR(60) NOT NULL,
    status bit(1) NOT NULL
);

INSERT INTO Candidate (dpi, name, party, proposal) VALUES ('6826508959056', 'Sandra Torres', 'BIEN', 'Cambio en Guatemala'), ('2273703329979', 'Otto', 'XD', 'Una');

INSERT INTO Person (name, dpi) VALUE ('Pablo', '1234567891234'), ('Paula', '9812345671823');

INSERT INTO Status (table_name, status) VALUES ('Candidate', 1), ('Vote', 1);

SELECT * FROM Candidate;
SELECT * FROM Person;
SELECT * FROM Vote;
SELECT * FROM Status;

