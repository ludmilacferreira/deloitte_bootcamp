-- ============================================================
-- Script de criacao da tabela EQUIPAMENTOS
-- Banco: desafiofinal_db (PostgreSQL 16)
-- ============================================================

CREATE SCHEMA IF NOT EXISTS public;

-- Tabela principal: equipamentos
DROP TABLE IF EXISTS public.equipamentos CASCADE;

CREATE TABLE public.equipamentos (
    id                    SERIAL PRIMARY KEY,
    codigo                VARCHAR(50)    NOT NULL,
    tipo                  INTEGER        NOT NULL,    -- 0=Caminhao, 1=Escavadeira, 2=Perfuratriz, 3=Carregadeira, 4=Trator
    modelo                VARCHAR(120)   NOT NULL,
    horimetro             NUMERIC(12,2)  NOT NULL DEFAULT 0,
    status_operacional    INTEGER        NOT NULL,    -- 0=Operacional, 1=EmManutencao, 2=Parado
    data_aquisicao        TIMESTAMPTZ    NOT NULL,
    localizacao_atual     VARCHAR(200)   NOT NULL
);

-- Indice unico em Codigo (constraint do desafio)
CREATE UNIQUE INDEX ux_equipamentos_codigo
    ON public.equipamentos (codigo);

-- Restricoes de dominio
ALTER TABLE public.equipamentos
    ADD CONSTRAINT chk_horimetro_positivo CHECK (horimetro >= 0),
    ADD CONSTRAINT chk_tipo_valido CHECK (tipo IN (0, 1, 2, 3, 4)),
    ADD CONSTRAINT chk_status_valido CHECK (status_operacional IN (0, 1, 2));

-- ============================================================
-- Dados de exemplo para teste
-- ============================================================
INSERT INTO public.equipamentos
(codigo, tipo, modelo, horimetro, status_operacional, data_aquisicao, localizacao_atual)
VALUES
('CAT-793F-000123', 0, 'Caterpillar 793F',    18234.50, 0, '2019-03-15', 'Mina Carajas N4E'),
('KOM-PC5500-0042', 1, 'Komatsu PC5500',      12500.00, 0, '2020-07-22', 'Mina Carajas N5S'),
('ATL-D11T-0007',   4, 'Caterpillar D11T',     9870.25, 1, '2018-11-10', 'Oficina Central'),
('SAN-DT4000-0015', 0, 'Sandvik DT4000',       5400.00, 2, '2022-01-05', 'Patio de Estacionamento'),
('LIE-R9800-0003',  1, 'Liebherr R 9800',     15600.75, 0, '2017-06-30', 'Mina Carajas N4E');

-- Consulta rapida para conferir
SELECT * FROM public.equipamentos ORDER BY id;
