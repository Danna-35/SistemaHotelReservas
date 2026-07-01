-- ==========================================
-- USAR LA BASE DE DATOS
-- ==========================================
USE hotel_reservas;
GO

-- ==========================================
-- TABLA DE USUARIOS (LOGIN)
-- ==========================================
CREATE TABLE usuarios (
    id_usuario INT IDENTITY(1,1) PRIMARY KEY,
    usuario VARCHAR(50) NOT NULL,
    contraseña VARCHAR(50) NOT NULL
);
GO

-- Usuario para iniciar sesión
INSERT INTO usuarios (usuario, contraseña)
VALUES ('Danna', '108');
GO

-- ==========================================
-- TABLA CLIENTES
-- ==========================================
CREATE TABLE clientes (
    id_cliente INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    cedula VARCHAR(20) UNIQUE NOT NULL,
    telefono VARCHAR(20),
    correo VARCHAR(100),
    fecha_registro DATETIME DEFAULT GETDATE()
);
GO

-- ==========================================
-- TABLA HABITACIONES
-- ==========================================
CREATE TABLE habitaciones (
    id_habitacion INT IDENTITY(1,1) PRIMARY KEY,
    numero_habitacion VARCHAR(10) NOT NULL,
    tipo VARCHAR(50) NOT NULL,
    precio_noche DECIMAL(10,2) NOT NULL,
    estado VARCHAR(20) DEFAULT 'Disponible'
);
GO

-- ==========================================
-- TABLA RESERVAS
-- ==========================================
CREATE TABLE reservas (
    id_reserva INT IDENTITY(1,1) PRIMARY KEY,
    id_cliente INT NOT NULL,
    id_habitacion INT NOT NULL,
    fecha_entrada DATE NOT NULL,
    fecha_salida DATE NOT NULL,
    estado VARCHAR(20) DEFAULT 'Pendiente',

    FOREIGN KEY (id_cliente) REFERENCES clientes(id_cliente),
    FOREIGN KEY (id_habitacion) REFERENCES habitaciones(id_habitacion)
);
GO

-- ==========================================
-- TABLA PAGOS
-- ==========================================
CREATE TABLE pagos (
    id_pago INT IDENTITY(1,1) PRIMARY KEY,
    id_reserva INT NOT NULL,
    monto DECIMAL(10,2) NOT NULL,
    fecha_pago DATETIME DEFAULT GETDATE(),
    metodo_pago VARCHAR(20) NOT NULL,

    FOREIGN KEY (id_reserva) REFERENCES reservas(id_reserva)
);
GO

-- ==========================================
-- DATOS DE PRUEBA
-- ==========================================

-- Clientes
INSERT INTO clientes (nombre, apellido, cedula, telefono, correo)
VALUES
('Juan', 'Perez', '1312345678', '0999999999', 'juan@gmail.com'),
('Maria', 'Lopez', '1311111111', '0988888888', 'maria@gmail.com');
GO

-- Habitaciones
INSERT INTO habitaciones (numero_habitacion, tipo, precio_noche, estado)
VALUES
('101', 'Simple', 35.00, 'Disponible'),
('102', 'Doble', 55.00, 'Disponible'),
('103', 'Suite', 90.00, 'Ocupada');
GO

-- Reservas
INSERT INTO reservas (id_cliente, id_habitacion, fecha_entrada, fecha_salida)
VALUES
(1, 1, '2026-07-10', '2026-07-15'),
(2, 2, '2026-07-20', '2026-07-25');
GO

-- Pagos
INSERT INTO pagos (id_reserva, monto, metodo_pago)
VALUES
(1, 175.00, 'Efectivo'),
(2, 275.00, 'Tarjeta');
GO

-- ==========================================
-- CONSULTAS DE VERIFICACIÓN
-- ==========================================

-- Ver usuarios
SELECT * FROM usuarios;
GO

-- Ver clientes
SELECT * FROM clientes;
GO

-- Ver habitaciones
SELECT * FROM habitaciones;
GO

-- Ver reservas
SELECT * FROM reservas;
GO

-- Ver pagos
SELECT * FROM pagos;
GO

-- Verificar login
SELECT *
FROM usuarios
WHERE usuario = 'Danna'
AND contraseña = '108';
GO