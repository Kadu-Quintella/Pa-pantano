CREATE TABLE Usuarios (
    Cpf VARCHAR(14) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL
);

CREATE TABLE Eventos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(100) NOT NULL,
    CapacidadeTotal INT NOT NULL,
    DataEvento DATETIME NOT NULL,
    PrecoPadrao DECIMAL(10,2) NOT NULL
);

CREATE TABLE Cupons (
    Codigo VARCHAR(50) PRIMARY KEY,
    PorcentagemDesconto DECIMAL(5,2) NOT NULL,
    ValorMinimoRegra DECIMAL(10,2) NOT NULL
);

CREATE TABLE Reservas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioCpf VARCHAR(14) NOT NULL,
    EventoId INT NOT NULL,
    CupomUtilizado VARCHAR(50) NULL,
    ValorFinalPago DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (UsuarioCpf) REFERENCES Usuarios(Cpf),
    FOREIGN KEY (EventoId) REFERENCES Eventos(Id),
    FOREIGN KEY (CupomUtilizado) REFERENCES Cupons(Codigo)
);
