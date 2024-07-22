﻿-- Assicurati che i valori di TipoSoggiorno corrispondano esattamente ai valori definiti nel vincolo CHECK
INSERT INTO Prenotazioni (CodiceFiscale, NumeroCamera, DataPrenotazione, NumeroProgressivoAnno, Anno, DataInizioSoggiorno, DataFineSoggiorno, CaparraConfirmatoria, TariffaApplicata, TipoSoggiorno) VALUES
('RSSMRA85M01H501Z', 101, '2024-07-15', 1, 2024, '2024-07-20', '2024-07-25', 50.00, 300.00, 'Pernottamento Prima Colazione'),
('BNCLRA80A41F205X', 102, '2024-07-16', 2, 2024, '2024-07-22', '2024-07-28', 100.00, 600.00, 'Mezza Pensione');