CREATE DATABASE IF NOT EXISTS CyberOlympics;

USE CyberOlympics;

CREATE TABLE IF NOT EXISTS Competitor(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    firstName VARCHAR(50) NOT NULL,
    lastName VARCHAR(50) NOT NULL,
    country VARCHAR(16) NOT NULL,
    city VARCHAR(32) NOT NULL,
    bio VARCHAR(250)
);

CREATE TABLE IF NOT EXISTS Goal(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    measure VARCHAR(8) NOT NULL,
    max NUMERIC(5,2) NOT NULL
);

CREATE TABLE IF NOT EXISTS Sport(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL
);

CREATE TABLE IF NOT EXISTS Event(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    sport INT NOT NULL,
    beginDate DATETIME,
    endDate DATETIME,
    goal INT,
    FOREIGN KEY (sport) REFERENCES Sport(id),
    FOREIGN KEY (goal) REFERENCES Goal(id)
);

CREATE TABLE IF NOT EXISTS Benchmark(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    event INT NOT NULL,
    competitor INT NOT NULL,
    score NUMERIC(5,2) NOT NULL,
    FOREIGN KEY (event) REFERENCES Event(id),
    FOREIGN KEY (competitor) REFERENCES Competitor(id)
);

INSERT INTO Competitor (firstName, lastName, country, city, bio)
VALUES 
("John", "Doe", "USA", "San Diego", null),
("Juan", "Sanchez", "México", "Mexico City", null);

INSERT INTO Goal (measure, max)
VALUES 
("puntos", 80),
("puntaje", 100 );

INSERT INTO Sport (name)
VALUES 
('Curling'),
('Patinaje Artistico');

INSERT INTO Event (name, sport, beginDate, endDate, goal)
VALUES 
("Primer Curling", 1, NOW(), NOW(), 1),
("Primer Patinaje", 2, NOW(), NOW(), 2);

INSERT INTO Benchmark (event, competitor, score)
VALUES 
(1, 1, 80),
(2, 2, 100),
(1, 2, 0),
(2, 1, 0);

SELECT Benchmark.id AS ID, 
Event.name AS Evento, Sport.name AS Deporte, CONCAT(Competitor.firstName, " ", Competitor.lastName) AS Competidor, 
Competitor.country AS País, CONCAT(Goal.measure, " ", Benchmark.score) AS Puntaje
FROM Benchmark 
INNER JOIN Event ON Benchmark.event = Event.id 
INNER JOIN Competitor ON Benchmark.competitor = Competitor.id 
INNER JOIN Sport ON Event.sport = Sport.id 
INNER JOIN Goal ON Event.goal = Goal.id
ORDER BY Evento;