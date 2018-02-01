CREATE DATABASE IF NOT EXISTS DesastresNaturales;

USE DesastresNaturales;

CREATE TABLE IF NOT EXISTS TipoEvento(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    titulo VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS Causa(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    descripcion VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS Efectos(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    descripcion VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS Ubicacion(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    latitud DECIMAL(10,8) NOT NULL,
    longitud DECIMAL(11,8) NOT NULL
);

CREATE TABLE IF NOT EXISTS Desastre(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    tipoEvento INT NOT NULL,
    causa INT NOT NULL,
    efectos INT NOT NULL,
    ubicacion INT NOT NULL,
    fechaInicio DATETIME,
    fechaFinal DATETIME,
    nota VARCHAR(1024),
    FOREIGN KEY (tipoEvento) REFERENCES TipoEvento(id),
    FOREIGN KEY (causa) REFERENCES Causa(id),
    FOREIGN KEY (efectos) REFERENCES Efectos(id),
    FOREIGN KEY (ubicacion) REFERENCES Ubicacion(id)
);

INSERT INTO TipoEvento (titulo)
VALUES 
('Incendio Forestal'),
('Terremoto'),
('Ciclon'),
('Huracan');

INSERT INTO Causa (descripcion)
VALUES 
('Colilla de cigarro'),
('Fogata'),
('Epicentro Guerrero'),
('Huracan Diego');

INSERT INTO Efectos (descripcion)
VALUES 
('X Hectareas perdidas'),
('X Edificios derrumbados'),
('X Casas destruidas');

INSERT INTO Ubicacion (latitud, longitud)
VALUES 
(19.39068, -99.2836984),
(30.3401664, -117.7746269),
(19.39068, -99.2836984);

INSERT INTO Desastre (tipoEvento, causa, efectos, ubicacion, fechaInicio, fechaFinal, nota)
VALUES 
(2, 3, 2, 1, NOW(), NOW(), "Rescates en coordinación con la SEMAR"),
(4, 4, 1, 2, NOW(), NOW(), "Rescates en coordinación con la SEMAR"),
(1, 2, 3, 3, NOW(), NOW(), "Se investigan responsables");

SELECT Desastre.id, 
TipoEvento.titulo AS tipoEvento, Causa.descripcion AS causa, Efectos.descripcion AS efectos, 
Ubicacion.latitud AS latitud, Ubicacion.longitud AS logitud, 
Desastre.fechaInicio, Desastre.fechaFinal, Desastre.nota 
FROM Desastre 
INNER JOIN TipoEvento ON Desastre.tipoEvento = TipoEvento.id 
INNER JOIN Causa ON Desastre.causa = Causa.id 
INNER JOIN Efectos ON Desastre.efectos = Efectos.id 
INNER JOIN Ubicacion ON Desastre.ubicacion = Ubicacion.id;