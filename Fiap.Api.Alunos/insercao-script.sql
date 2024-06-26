-- Deletar todos os registros da tabela Alertas
DELETE FROM "RM552504"."Alertas";

-- Inserir dados de teste
INSERT INTO "RM552504"."Alertas" ("Id", "Nome", "Localizacao", "Descricao", "Data") VALUES (1, 'Inc�ndio', 'S�o Paulo', 'Inc�ndio em �rea florestal', TO_DATE('2024-06-01', 'YYYY-MM-DD'));
INSERT INTO "RM552504"."Alertas" ("Id", "Nome", "Localizacao", "Descricao", "Data") VALUES (2, 'Inunda��o', 'S�o Paulo', 'Inunda��o causada por chuvas intensas', TO_DATE('2024-06-10', 'YYYY-MM-DD'));
INSERT INTO "RM552504"."Alertas" ("Id", "Nome", "Localizacao", "Descricao", "Data") VALUES (3, 'Tempestade', 'S�o Paulo', 'Tempestade com ventos fortes', TO_DATE('2024-06-15', 'YYYY-MM-DD'));
INSERT INTO "RM552504"."Alertas" ("Id", "Nome", "Localizacao", "Descricao", "Data") VALUES (4, 'Terremoto', 'S�o Paulo', 'Terremoto de magnitude 6.5', TO_DATE('2024-06-20', 'YYYY-MM-DD'));
INSERT INTO "RM552504"."Alertas" ("Id", "Nome", "Localizacao", "Descricao", "Data") VALUES (5, 'Deslizamento', 'S�o Paulo', 'Deslizamento de terra em �rea montanhosa', TO_DATE('2024-06-25', 'YYYY-MM-DD'));

-- Confirmar as inser��es
COMMIT;


DECLARE
    v_base_year NUMBER := 1970;
BEGIN
    FOR i IN 150 .. 299 LOOP
        INSERT INTO "RM552504"."Alertas" (
            "Id", 
            "Nome",
            "Localizacao",
            "Descricao", 
            "Data"
        ) VALUES (
            i, 
            'Alerta ' || TO_CHAR(i), 
            'Alerta' || TO_CHAR(i), 
            TO_DATE(v_base_year + MOD(i, 30) || '-01-01', 'YYYY-MM-DD')  -- Random year from 1970 to 1999
        );
    END LOOP;
    COMMIT;
END;
